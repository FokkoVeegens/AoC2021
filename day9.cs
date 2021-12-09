using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class day9
    {
        public static int Run1(string path)
        {
            int result = 0;
            string input = common.GetInput(path);


            return result;
        }

        public static int Run2(string path)
        {
            int result = 0;
            string input = common.GetInput(path);

            string[] rows = input.Split(Environment.NewLine);

            // Contains data; heights[column, row] or heights[x, y]
            //int[,] heights = new int[rows[0].Length, rows.Count()];
            List<pos> positions = new List<pos>();
            for (int row = 0; row < rows.Count(); row++)
            {
                int[] cols = rows[row].ToCharArray().Select(l => int.Parse(l.ToString())).ToArray();
                for (int col = 0; col < cols.Length; col++)
                {
                    //heights[col, row] = cols[col];
                    int left = 10;
                    if (col != 0)
                    {
                        left = cols[col - 1];
                    }
                    int above = 10;
                    if (row != 0)
                    {
                        int[] prevRowCols = rows[row - 1].ToCharArray().Select(l => int.Parse(l.ToString())).ToArray();
                        above = prevRowCols[col];
                    }
                    int right = 10;
                    if (col != cols.Length - 1)
                    {
                        right = cols[col + 1];
                    }
                    int below = 10;
                    if (row != rows.Length - 1)
                    {
                        int[] nextRowCols = rows[row + 1].ToCharArray().Select(l => int.Parse(l.ToString())).ToArray();
                        below = nextRowCols[col];
                    }
                    positions.Add(new pos { 
                        x = col, 
                        y = row, 
                        z = cols[col],
                        left = left,
                        above = above,
                        right = right,
                        below = below
                    });
                }
            }

            List<int> basins = new List<int>();

            List<pos> positionsToProcess = positions.Where(p => p.left > p.z & p.above > p.z & p.right > p.z & p.below > p.z).ToList();
            foreach (pos position in positionsToProcess)
            {
                int size = 0;
                ProcessPos(ref positions, position.x, position.y, ref size);
                basins.Add(size);
            }

            result = basins.OrderByDescending(b => b).Take(3).Aggregate(1, (accumulated, value) => accumulated * value);
            return result;
        }

        private static void ProcessPos(ref List<pos> positions, int x, int y, ref int size)
        {
            pos curPosition = positions.FirstOrDefault(p => p.x == x & p.y == y);
            if (curPosition.processed)
            {
                return;
            }
            size++;
            curPosition.processed = true;
            if (y != 0)
            {
                if (curPosition.above > curPosition.z & curPosition.above != 9)
                {
                    ProcessPos(ref positions, x, y - 1, ref size);
                }
            }
            if (y != positions.Max(p => p.y))
            {
                if (curPosition.below > curPosition.z & curPosition.below != 9)
                {
                    ProcessPos(ref positions, x, y + 1, ref size);
                }
            }
            if (x != 0)
            {
                if (curPosition.left > curPosition.z & curPosition.left != 9)
                {
                    ProcessPos(ref positions, x - 1, y, ref size);
                }
            }
            if (x != positions.Max(p => p.x))
            {
                if (curPosition.right > curPosition.z & curPosition.right != 9)
                {
                    ProcessPos(ref positions, x + 1, y, ref size);
                }
            }

        }
    }

    internal class pos
    {
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }
        public int above { get; set; }
        public int left { get; set; }
        public int right { get; set; }
        public int below { get; set; }
        public bool processed { get; set; } = false;
    }
}
