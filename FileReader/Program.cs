using System.Diagnostics;
using System.Text;

namespace FileReader
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            MyFileReader read = new();
            Stopwatch stopwatch = new();
            var filePath = Directory.GetFiles(@"Your_Path");

            stopwatch.Start();
            var tasks = filePath.Select(async filePath =>
            {
                await read.ReadAllFileNamespaces(filePath);
                return read.SpaceCount;
            });
            await Task.WhenAll(tasks);
            stopwatch.Stop();

            Console.WriteLine($"Total number of whitespaces:{read.SpaceCount}, execution Time: {stopwatch.ElapsedMilliseconds} ms.");
        }
    }
}
