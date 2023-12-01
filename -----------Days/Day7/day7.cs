using System;
using System.Collections.Generic;
using System.IO;

namespace advent_of_code
{
    public class day7
    {
        private string currentDir = "/";
        
        public void Run()
        {
#region Part 1
            Console.WriteLine("Day 7 - Part 1:");
            string[] terminal = File.ReadAllLines("-----------Days/Day7/input.txt");
            List<Command> cmds = GetAllCommands(terminal);
            List<LocalFile> allFiles = GetAllFiles(cmds);

            int total = 0;
            foreach (LocalFile file in allFiles)
            {
                if (file.size <= 100000 && file.isDirectory)
                {
                    total += file.size;
                }
            }
            Console.WriteLine(total);
#endregion

#region Part 2
            Console.WriteLine("Day 7 - Part 2:");
            int spaceNeeded = 30000000 - (70000000 - SearchForFile("/", allFiles).size);

            allFiles.Sort((a, b) => {
                return a.size - b.size;
            }); // ascending
            
            allFiles = allFiles.FindAll(f => {
                return f.isDirectory && f.size >= spaceNeeded;
            }); // filter list to find only directories + bigger/equal to than space needed

            Console.WriteLine(allFiles[0].size);
#endregion
        }

        private List<LocalFile> GetAllFiles(List<Command> cmds)
        {
            List<LocalFile> files = new List<LocalFile>();
            files.Add(new LocalFile("/", 0, null, true));

            foreach (Command cmd in cmds)
            {
                if (cmd.cmd == "cd")
                {
                    switch (cmd.parameter)
                    {
                        case "..":
                            currentDir = GetParentDirectory(currentDir);
                            break;
                        case "/":
                            currentDir = "/";
                            break;
                        default:
                            currentDir += cmd.parameter + "/";
                            break;
                    }
                }
                else if (cmd.cmd == "ls")
                {
                    foreach (string result in cmd.results)
                    {
                        string[] fileInfo = result.Split(" ");

                        if (fileInfo[0] == "dir")
                        {
                            // type dir.
                            LocalFile dir = new LocalFile(currentDir + fileInfo[1], 0, SearchForFile(GetParentDirectory(currentDir + fileInfo[1]), files), true);
                            files.Add(dir);
                        }
                        else
                        {
                            // type file
                            LocalFile dir = SearchForFile(currentDir, files);
                            int fileSize = int.Parse(fileInfo[0]);
                            CalculateDirectorySize(dir, fileSize);

                            LocalFile file = new LocalFile(currentDir + fileInfo[1], fileSize, dir, false);
                            files.Add(file);
                        }
                    }
                }
            }

            return files;
        }

        private void CalculateDirectorySize(LocalFile file, int addSize)
        {
            if (file.isDirectory)
            {
                file.size += addSize;
                if (file.parent != null && file.parent.isDirectory)
                {
                    CalculateDirectorySize(file.parent, addSize);
                }
            }
        }

        private LocalFile SearchForFile(string path, List<LocalFile> files)
        {
            foreach (LocalFile file in files)
            {
                if (file.path == path || file.path + "/" == path)
                {
                    return file;
                }
            }

            return null;
        }

        private string GetParentDirectory(string path)
        {
            if (path.EndsWith('/')) { path = path.Substring(0, path.Length - 1); }
            if (path.LastIndexOf("/") <= 0) return "/";
            return path.Substring(0, path.LastIndexOf("/") + 1);
        }

        private List<Command> GetAllCommands(string[] terminal)
        {
            List<Command> cmds = new List<Command>();
            for (int i = 0; i < terminal.Length; i++)
            {
                string output = terminal[i];
                if (output.StartsWith('$'))
                {
                    string[] command = output.Replace("$ ", "").Split(" "); // [0] is the command [1] is the parameter;

                    Command cmd = new Command(command[0], command.Length == 2 ? command[1] : "");
                    if (cmd.cmd == "ls") // only ls has results
                    {
                        while (i+1 < terminal.Length && !terminal[i+1].StartsWith('$'))
                        {
                            i++;
                            cmd.results.Add(terminal[i]);
                        }
                    }

                    cmds.Add(cmd);
                }
            }

            return cmds;
        }

        private class LocalFile
        {
            public string path = "";
            public int size = 0;
            public LocalFile parent = null;

            public bool isDirectory = false;

            public LocalFile(string path, int size, LocalFile parent, bool isDirectory)
            {
                this.path = path;
                this.size = size;
                this.parent = parent;
                this.isDirectory = isDirectory;
            }
        }

        private class Command
        {
            public string cmd = "";
            public string parameter = "";
            public List<string> results = new List<string>();

            public Command(string cmd, string parameter)
            {
                this.cmd = cmd;
                this.parameter = parameter;
            }
        }
    }
}