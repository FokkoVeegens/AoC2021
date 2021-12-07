using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class day7
    {
        public static int Run1(string path)
        {
            int result = int.MaxValue;
            string input = common.GetInput(path);
            int[] locations = input.Split(',').Select(int.Parse).ToArray();

            int maxLocation = locations.Max();
            for (int location = 0; location <= maxLocation; location++)
            {
                int fuelcost = locations.Select(inp => Math.Abs(inp - location)).Sum();
                if (fuelcost < result)
                {
                    result = fuelcost;
                }
            }
            return result;
        }

        public static int Run2(string path)
        {
            int result = int.MaxValue;
            string input = common.GetInput(path);
            int[] locations = input.Split(',').Select(int.Parse).ToArray();

            int maxLocation = locations.Max();
            int[] fuelCostList = GetFuelCostList(maxLocation);
            for (int location = 0; location <= maxLocation; location++)
            {
                int fuelcost = locations.Select(inp => fuelCostList[Math.Abs(inp - location)]).Sum();
                if (fuelcost < result)
                {
                    result = fuelcost;
                }
            }
            return result;
        }

        private static int[] GetFuelCostList(int maxLocation)
        {
            int[] result = new int[maxLocation + 1];
            result[0] = 0;
            for (int i = 1; i <= maxLocation; i++)
            {
                result[i] = result[i - 1] + i;
            }
            return result;
        }
    }
}
