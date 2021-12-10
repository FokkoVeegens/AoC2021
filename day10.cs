using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class day10
    {
        private static string[] openChars = { "(", "[", "{", "<" };
        private static string[] closeChars = { ")", "]", "}", ">" };
        private static int[] scores = { 3, 57, 1197, 25137 };
        private static int[] scores2 = { 1, 2, 3, 4 };
        private static string[] pairs = { "()", "[]", "{}", "<>" };
        public static int Run1(string path)
        {
            int result = 0;
            string[] lines = common.GetInput(path).Split(Environment.NewLine);
            foreach (string line in lines)
            {
                string processedLine = StripObviousPairs(line);
                if (closeChars.Any(processedLine.Contains))
                {
                    // Corrupt
                    string illegalChar = FindFirstIllegalChar(processedLine);
                    int illegalScore = scores[Array.FindIndex(closeChars, c => c == illegalChar)];
                    result += illegalScore;
                }
            }

            return result;
        }

        public static long Run2(string path)
        {
            long result = 0;
            List<long> outcomes = new List<long>();
            string[] lines = common.GetInput(path).Split(Environment.NewLine);
            foreach (string line in lines)
            {
                string processedLine = StripObviousPairs(line);
                if (!closeChars.Any(processedLine.Contains))
                {
                    outcomes.Add(GetLineScore(processedLine));
                }
            }
            result = outcomes.OrderBy(o => o).ToArray()[Math.Abs(outcomes.Count/2)];
            return result;
        }


        private static string StripObviousPairs(string input)
        {
            string output = input;
            while (pairs.Any(output.Contains))
            {
                for (int i = 0; i < pairs.Length; i++)
                {
                    output = output.Replace(pairs[i], "");
                }
            }
            return output;
        }

        private static string FindFirstIllegalChar(string input)
        {
            int[] finds = new int[closeChars.Length];
            for (int i = 0; i < finds.Length; i++)
            {
                finds[i] = input.IndexOf(closeChars[i]);
            }
            return input.Substring(finds.Where(f => f > -1).Min(), 1);
        }

        private static long GetLineScore(string line)
        {
            long result = 0;
            for (int i = line.Length - 1; i >= 0; i--)
            {
                int indexOfChar = Array.FindIndex(openChars, o => o == line.Substring(i, 1));
                result = (result * 5) + scores2[indexOfChar];
            }
            return result;
        }

    }

}
