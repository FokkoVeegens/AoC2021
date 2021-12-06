using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal static class common
    {
        public static string GetInput(string path)
        {
            string input = string.Empty;
            using (StreamReader sr = new StreamReader(path))
            {
                input = sr.ReadToEnd();
                sr.Close();
            }
            return input;
        }
    }
}
