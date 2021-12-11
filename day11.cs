using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class day11
    {
        public static int Run1(string path)
        {
            int result = 0;
            string input = common.GetInput(path);
            string[] lines = input.Split(Environment.NewLine);
            int[,] values = new int[lines[0].Length, lines.Count()];

            int y = 0;
            foreach (string line in lines)
            {
                char[] chars = line.ToCharArray();
                for (int x = 0; x < chars.Length; x++)
                {
                    values[x, y] = int.Parse(chars[x].ToString());
                }
                y++;
            }

            int flashCount = 0;
            for (int step = 0; step < 100; step++)
            {
                // Process step (increase values by 1)
                for (int x = 0; x <= values.GetUpperBound(0); x++)
                {
                    for (y = 0; y <= values.GetUpperBound(1); y++)
                    {
                        values[x, y]++;
                        if (values[x, y] > 10)
                        {
                            Console.WriteLine("[Process Step] {0} found at x={1} and y={2}", values[x, y], x, y);
                        }
                    }
                }

                int[,] dumbosFlashed = new int[lines[0].Length, lines.Count()];

                // Process flashes
                while (HasFlashes(values))
                {
                    int[,] oldvalues = values;
                    for (int x = 0; x <= values.GetUpperBound(0); x++)
                    {
                        for (y = 0; y <= values.GetUpperBound(1); y++)
                        {
                            if (oldvalues[x, y] >= 10)
                            {
                                flashCount += ProcessFlashes(ref values, ref dumbosFlashed, x, y);
                            }
                        }
                    }
                }
                Console.WriteLine("Step {0}", step + 1);
                Console.Write(ConvertToTable(values));
            }
            result = flashCount;

            return result;
        }

        public static int Run2(string path)
        {
            int result = 0;
            string input = common.GetInput(path);
            string[] lines = input.Split(Environment.NewLine);
            int[,] values = new int[lines[0].Length, lines.Count()];

            int y = 0;
            foreach (string line in lines)
            {
                char[] chars = line.ToCharArray();
                for (int x = 0; x < chars.Length; x++)
                {
                    values[x, y] = int.Parse(chars[x].ToString());
                }
                y++;
            }

            int flashCount = 0;
            int step = 1;
            int sum = 1;
            while (sum != 0)
            {
                // Process step (increase values by 1)
                for (int x = 0; x <= values.GetUpperBound(0); x++)
                {
                    for (y = 0; y <= values.GetUpperBound(1); y++)
                    {
                        values[x, y]++;
                        if (values[x, y] > 10)
                        {
                            Console.WriteLine("[Process Step] {0} found at x={1} and y={2}", values[x, y], x, y);
                        }
                    }
                }

                int[,] dumbosFlashed = new int[lines[0].Length, lines.Count()];

                // Process flashes
                while (HasFlashes(values))
                {
                    int[,] oldvalues = values;
                    for (int x = 0; x <= values.GetUpperBound(0); x++)
                    {
                        for (y = 0; y <= values.GetUpperBound(1); y++)
                        {
                            if (oldvalues[x, y] >= 10)
                            {
                                flashCount += ProcessFlashes(ref values, ref dumbosFlashed, x, y);
                            }
                        }
                    }
                }
                
                Console.WriteLine("Step {0}", step);
                Console.WriteLine(ConvertToTable(values));
                sum = values.Cast<int>().Sum();
                result = step;
                step++;
            }

            return result;
        }

        internal static string ConvertToTable(int[,] values)
        {
            string result = "";
            for (int y = 0; y <= values.GetUpperBound(1); y++)
            {
                for (int x = 0; x <= values.GetUpperBound(0); x++)
                {
                    result += values[x, y];
                }
                result += Environment.NewLine;
            }
            return result;
        }

        internal static bool HasFlashes(int[,] values)
        {
            for (int x = 0; x <= values.GetUpperBound(0); x++)
            {
                for (int y = 0; y <= values.GetUpperBound(1); y++)
                {
                    if (values[x, y] >= 10)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        internal static int ProcessFlashes(ref int[,] values, ref int[,] dumbosFlashed, int x, int y)
        {
            int result = 0;
            values[x, y] = 0;
            dumbosFlashed[x, y] = 1;
            result++;
            for (int offsetX = -1; offsetX <= 1; offsetX++)
            {
                for (int offsetY = -1; offsetY <= 1; offsetY++)
                {
                    if (offsetX == 0 & offsetY == 0)
                    {
                        continue;
                    }
                    if (IsValidPosToUpdate(x + offsetX, y + offsetY, values.GetUpperBound(0), values.GetUpperBound(1)))
                    {
                        if (dumbosFlashed[x + offsetX, y + offsetY] == 0)
                        {
                            values[x + offsetX, y + offsetY]++;
                        }
                    }
                }
            }
            return result;
        }

        internal static bool IsValidPosToUpdate(int x, int y, int xUpperBound, int yUpperBound)
        {
            if (x == -1 | y == -1 | x > xUpperBound | y > yUpperBound)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
