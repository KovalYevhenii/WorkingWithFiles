using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace FileReader
{
    internal class MyFileReader
    {
        private int _spaceCount;
        public int SpaceCount
        {
            get => _spaceCount;
        }
        public Task ReadAllFileNamespaces(string path)
        {
            return Task.Run(() =>
            {
                using var reader = new StreamReader(path);
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                     _spaceCount += line.Count(char.IsWhiteSpace);
                }
            });
        }
    }
}
