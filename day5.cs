using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal static class day5
    {
        public static int Run(string filePath)
        {
            string input = common.GetInput(filePath);

            int[,] matrix = new int[1000, 1000];
            foreach (string line in input.Split(Environment.NewLine))
            {
                string[] parts = line.Split(" -> ");
                int x1 = int.Parse(parts[0].Split(',')[0]);
                int y1 = int.Parse(parts[0].Split(',')[1]);
                int x2 = int.Parse(parts[1].Split(',')[0]);
                int y2 = int.Parse(parts[1].Split(',')[1]);

                // Vertical line
                if (x1 == x2)
                {
                    if (y1 > y2)
                    {
                        for (int i = y2; i <= y1; i++)
                        {
                            matrix[x1, i]++;
                        }
                    }
                    else
                    {
                        for (int i = y1; i <= y2; i++)
                        {
                            matrix[x1, i]++;
                        }
                    }
                }

                // Horizontal line
                if (y1 == y2)
                {
                    if (x1 > x2)
                    {
                        for (int i = x2; i <= x1; i++)
                        {
                            matrix[i, y1]++;
                        }
                    }
                    else
                    {
                        for (int i = x1; i <= x2; i++)
                        {
                            matrix[i, y1]++;
                        }
                    }
                }

                // Diagonal line
                if (Math.Abs(x1 - x2) == Math.Abs(y1 - y2))
                {
                    int xIncrement = 1;
                    if (x1 > x2)
                    {
                        xIncrement = -1;
                    }

                    int yIncrement = 1;
                    if (y1 > y2)
                    {
                        yIncrement = -1;
                    }

                    int x = x1;
                    int y = y1;
                    while (x != x2 || y != y2)
                    {
                        matrix[x, y]++;
                        x += xIncrement;
                        y += yIncrement;
                    }
                    matrix[x, y]++;
                }    
            }

            int count = 0;
            for (int x = 0; x < 1000; x++)
            {
                for (int y = 0; y < 1000; y++)
                {
                    if (matrix[x, y] > 1)
                    {
                        count++;
                    }
                }
            }
            return count;
        }
    }
}
