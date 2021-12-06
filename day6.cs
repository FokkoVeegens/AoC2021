using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal static class day6
    {
        public static int Run(string path)
        {
            string input = common.GetInput(path);

            List<int> fishes = input.Split(',').Select(int.Parse).ToList();

            for (int day = 1; day <= 80; day++)
            {
                Console.WriteLine("{0} Day {1} (fish count: {2})", DateTime.Now.ToString("HH:mm:ss"), day, fishes.Count);
                int fishesToAdd = 0;
                for (int fish = 0; fish < fishes.Count; fish++)
                {
                    // Produces new fish
                    if (fishes[fish] == 0)
                    {
                        fishesToAdd++;
                        fishes[fish] = 6;
                    }
                    else
                    {
                        fishes[fish]--;
                    }

                }

                // Add fishes outside loop, otherwise days are already deducted on the same day as the fish was produced
                for (int i = 0; i < fishesToAdd; i++)
                {
                    fishes.Add(8);
                }
            }
            return fishes.Count;
        }
    }
}
