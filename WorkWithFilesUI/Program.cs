using System.Linq.Expressions;
using System;
using System.IO;
using System.Text;

namespace WorkWithFilesUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path1 = @"c:\Otus\TestDir1";
            string path2 = @"c:\Otus\TestDir2";
            try
            {
                Directory.Delete(path1, true);
                Directory.Delete(path2, true);
                if (!Directory.Exists(path1))
                {
                    DirectoryInfo directoryInfo1 = Directory.CreateDirectory(path1);
                    Console.WriteLine("The directory was created successfully at {0}", Directory.GetCreationTime(path1));
                }
                if (!Directory.Exists(path2))
                {
                    DirectoryInfo directoryInfo2 = Directory.CreateDirectory(path2);
                    Console.WriteLine("The directory was created successfully at {0}", Directory.GetCreationTime(path2));
                }

                for (int i = 0; i < 10; i++)
                {
                    string fileName1 = Path.Combine(path1, $"TestFile{i}.txt");
                    using (StreamWriter writer = new StreamWriter(fileName1, false, Encoding.UTF8))
                    {
                        writer.Write(fileName1 + " " + DateTime.Now.ToString());
                       
                    }
                    string fileName2 = Path.Combine(path2, $"TestFile{i}.txt");
                    using (StreamWriter writer = new StreamWriter(fileName2, false, Encoding.UTF8))
                    {
                        writer.Write(fileName2 + " " + DateTime.Now.ToString());
                    }
                }

                string[] files1 = Directory.GetFiles(path1);
                Console.WriteLine("Files in directory {0}",path1);

                foreach(string file in files1)
                {
                    Console.WriteLine("File_name: {0} {1}",Path.GetFileName(file),File.ReadAllText(file));
                }

                string[] files2 = Directory.GetFiles(path2);
                Console.WriteLine("Files in directory {0}", path2);

                foreach (string file in files2)
                {
                    Console.WriteLine("File_name: {0} {1}", Path.GetFileName(file), File.ReadAllText(file));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The process failed: {0}", ex.ToString());
            }
        }
    }
}




