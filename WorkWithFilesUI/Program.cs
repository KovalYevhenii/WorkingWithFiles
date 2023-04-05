using System.Linq.Expressions;
using System;
using System.IO;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace WorkWithFilesUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path1 = @"c:\Otus\TestDir1";
            string path2 = @"c:\Otus\TestDir2";

            Console.WriteLine("mkdir - Create 2 Directories c:\\Otus\\TestDir1 and c:\\Otus\\TestDir2");
            Console.WriteLine("mkfile - Create files in directory");
            Console.WriteLine("cd - change directory");
            Console.WriteLine("ls - list all files in directory");
            Console.WriteLine("mkfile - create a file");
            Console.WriteLine("rmfile - remove file");
            Console.WriteLine("edit - edit file");
            Console.WriteLine("read - read file");
            Console.WriteLine("q - exit\n");

            while (true)
            {
                Console.Write("> ");
                var command = Console.ReadLine()?.Split(" ");
                command = command ?? new string[] { "" };

                switch (command[0])
                {
                    case "cd":
                        if (command.Length < 2)
                        {
                            Console.WriteLine("Please provide a directory name");
                            break;
                        }

                        if (command[1].Equals("TestDir1", StringComparison.OrdinalIgnoreCase))
                        {
                            if (Directory.Exists(path1))
                            {
                                Directory.SetCurrentDirectory(path1);
                                Console.WriteLine($"Current directory: {path1}");
                            }
                            else
                            {
                                Console.WriteLine($"Directory {path1} does not exist");
                            }
                        }
                        else if (command[1].Equals("TestDir2", StringComparison.OrdinalIgnoreCase))
                        {
                            if (Directory.Exists(path2))
                            {
                                Directory.SetCurrentDirectory(path2);
                                Console.WriteLine($"Current directory: {path2}");
                            }
                            else
                            {
                                Console.WriteLine($"Directory {path2} does not exist");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid directory name");
                        }
                        break;

                    case "ls":
                        var currentDirectory = Environment.CurrentDirectory;

                        foreach (var file in Directory.GetFiles(currentDirectory))
                        {
                            var fileInfo = new FileInfo(file);
                            var fileParts = file.Split(Path.DirectorySeparatorChar);
                            Console.WriteLine($"F |{fileInfo.CreationTime} | {fileParts[fileParts.Length - 1]}");
                        }
                        break;

                    case "mkdir":
                        try
                        {
                            if (Directory.Exists(path1) && Directory.Exists(path2))
                            {
                                throw new Exception("Directories are already existing");

                            }
                            Directory.CreateDirectory(path1);
                            Directory.CreateDirectory(path2);
                            Console.WriteLine("Directories created");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error creating directories: {ex.Message}");
                            break;
                        }
                        break;

                    case "mkfile":
                        if (command.Length < 2)
                        {
                            Console.WriteLine("Please provide a file name ");
                            break;
                        }
                        using (var file = File.Create(command[1]))
                        {

                        }
                        break;
                    case "edit":
                        if (command.Length < 2)
                        {
                            Console.WriteLine("Plese provide a file name to edit");
                            break;
                        }
                        if (!File.Exists(command[1]))
                        {
                            Console.WriteLine($"File{command[1]} does not exist");
                            break;
                        }
                        Console.WriteLine("Enter text to add to the file or press q to exit:");
                        try
                        {
                            using (var file = File.Create(command[1]))
                            {
                                while (true)
                                {
                                    var input = Console.ReadLine();
                                    if (input == "q")
                                    {
                                        break;
                                    }
                                    file.Write(Encoding.UTF8.GetBytes(input + " " + DateTime.Now + Environment.NewLine));
                                }
                            }
                        }
                        catch (UnauthorizedAccessException)
                        {
                            Console.WriteLine($"Error: The file '{command[1]}' is read-only and cannot be edited.");
                        }
                        break;
                    case "read":
                        try
                        {
                            var task = Task.Run(() =>
                            {
                                var text = File.ReadAllLinesAsync(command[1]);
                                Console.WriteLine(text.Result.First());
                            });
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("IO Error");
                        }
                        break;

                    case "rmfile":
                        File.Delete(command[1]);
                        break;

                    case "q":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            }
        }
    }
}




