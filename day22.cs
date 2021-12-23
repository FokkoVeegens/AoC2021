using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class day22
    {
        public static int Run1(string path)
        {
            int result = 0;
            string input = common.GetInput(path);
            string[] lines = input.Split(Environment.NewLine);
            List<Cube> cubesOn = new();
            bool[,,] cubes = new bool[101, 101, 101];
            foreach (string line in lines)
            {
                string[] mainSegments = line.Split(" ");
                bool on = (mainSegments[0] == "on");
                string[] coordinatelist = mainSegments[1].Split(",");
                List<Coordinate> coordinates = new();
                foreach (string coordinatelistitem in coordinatelist)
                {
                    coordinates.Add(GetCoordinate(coordinatelistitem));
                }

                if (coordinates.Any(c => c.Start < -50 | c.End > 50))
                {
                    continue;
                }
                
                for (int x = coordinates.First(c => c.Vector == "x").Start; x <= coordinates.First(c => c.Vector == "x").End; x++)
                {
                    for (int y = coordinates.First(c => c.Vector == "y").Start; y <= coordinates.First(c => c.Vector == "y").End; y++)
                    {
                        for (int z = coordinates.First(c => c.Vector == "z").Start; z <= coordinates.First(c => c.Vector == "z").End; z++)
                        {
                            cubes[x+50, y+50, z+50] = on;
                        }
                    }
                }
            }
            int count = 0;
            for (int x = 0; x < 101; x++)
            {
                for (int y = 0; y < 101; y++)
                {
                    for (int z = 0; z < 101; z++)
                    {
                        if (cubes[x, y, z] == true)
                        {
                            count++;
                        }
                    }
                }
            }
            result = count;
            return result;
        }

        public static int Run2(string path)
        {
            int result = 0;
            string input = common.GetInput(path);
            string[] lines = input.Split(Environment.NewLine);

            return result;
        }

        internal static Coordinate GetCoordinate(string input)
        {
            Coordinate result = new Coordinate
            {
                Vector = input.Substring(0, 1),
                Start = int.Parse(input.Substring(2).Split("..")[0]),
                End = int.Parse(input.Substring(2).Split("..")[1])
            };
            result.BlockCount = result.End - result.Start + 1;
            return result;
        }
    }

    internal class Cube
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }

    internal class Coordinate
    {
        public string Vector { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public int BlockCount { get; set; }
    }
}
