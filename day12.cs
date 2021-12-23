using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class day12
    {
        public static int Run1(string path)
        {
            int result = 0;
            string input = common.GetInput(path);
            string[] lines = input.Split(Environment.NewLine);

            // Build inputdata
            List<Connection> connections = new();
            foreach (string line in lines)
            {
                string point1 = line.Split('-')[0];
                string point2 = line.Split('-')[1];
                Connection con = new Connection
                {
                    Point1 = point1,
                    Point1IsBig = Regex.Match(point1, "[A-Z][A-Z]").Success,
                    Point2 = point2,
                    Point2IsBig = Regex.Match(point2, "[A-Z][A-Z]").Success,
                    Point1IsStart = (point1 == "start"),
                    Point2IsStart = (point2 == "start"),
                    Point1IsEnd = (point1 == "end"),
                    Point2IsEnd = (point2 =="end")
                };
            }

            // Process paths


            return result;
        }

        public static int Run2(string path)
        {
            int result = 0;
            string input = common.GetInput(path);
            string[] lines = input.Split(Environment.NewLine);

            return result;
        }


    }

    internal class Connection
    {
        public string Point1 { get; set; }
        public bool Point1IsBig { get; set; }
        public string Point2 { get; set; }
        public bool Point2IsBig { get; set; }
        public bool Point1IsStart { get; set; }
        public bool Point1IsEnd { get; set; }
        public bool Point2IsStart { get; set; }
        public bool Point2IsEnd { get; set; }
        public bool HasStart 
        {
            get { return Point1IsStart | Point2IsStart; } 
        }
        public bool HasEnd 
        { 
            get { return Point1IsEnd | Point2IsEnd; } 
        }

    }
}
