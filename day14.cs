using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class day14
    {
        public static int Run1(string path)
        {
            int result = 0;
            string input = common.GetInput(path);
            string[] lines = input.Split(Environment.NewLine);
            string template = lines[0];
            List<InsertionRule> rules = new();
            foreach (string line in lines)
            {
                if (line.Contains("->"))
                {
                    rules.Add(new InsertionRule
                    {
                        Pair = line.Split(" -> ")[0],
                        Element = line.Split(" -> ")[1]
                    }); ;
                }
            }

            string newTemplate = string.Empty;
            for (int step = 1; step <= 10; step++)
            {
                newTemplate = template.Substring(0, 1);
                for (int i = 0; i < template.Length - 1; i++)
                {
                    string curPair = template.Substring(i, 2);
                    InsertionRule curRule = rules.FirstOrDefault(r => r.Pair == curPair);
                    if (curRule != null)
                    {
                        newTemplate += curRule.Element + curPair.Substring(1, 1);
                    }
                }
                template = newTemplate;
            }
            List<string> elements = rules.Select(r => r.Element).Distinct().ToList();
            List<Result> results = new();
            foreach (string element in elements)
            {
                results.Add(new Result { Element = element, NumberOf = template.Count(t => t == element.ToCharArray()[0]) });
            }
            result = results.OrderByDescending(r => r.NumberOf).First().NumberOf - results.OrderBy(r => r.NumberOf).First().NumberOf;
            return result;
        }

        public static long Run2(string path)
        {
            long result = 0;
            string input = common.GetInput(path);
            string[] lines = input.Split(Environment.NewLine);
            string template = lines[0];
            List<InsertionRule> rules = new();
            foreach (string line in lines)
            {
                if (line.Contains("->"))
                {
                    rules.Add(new InsertionRule
                    {
                        Pair = line.Split(" -> ")[0],
                        Element = line.Split(" -> ")[1],
                        ToInsert = line.Split(" -> ")[1] + line.Split(" -> ")[0].Substring(1)
                    }); ;
                }
            }

            string[] pairs = rules.Select(r => r.Pair).ToArray();
            string[] replacements = rules.Select(r => r.ToInsert).ToArray();
            StringBuilder newTemplate = new();
            InsertionRule curRule;
            for (int step = 1; step <= 40; step++)
            {
                Console.WriteLine("Step {0}", step);
                newTemplate.Append(template.Substring(0, 1));
                for (int i = 0; i < template.Length - 1; i++)
                {
                    string toInsert = replacements[Array.FindIndex(pairs, p => p == template.Substring(i, 2))];
                    //curRule = rules.First(r => r.Pair == template.Substring(i, 2));
                    newTemplate.Append(toInsert); //curRule.ToInsert);
                }
                template = newTemplate.ToString();
            }
            List<string> elements = rules.Select(r => r.Element).Distinct().ToList();
            List<Result2> results = new();
            foreach (string element in elements)
            {
                results.Add(new Result2 { Element = element, NumberOf = template.Count(t => t == element.ToCharArray()[0]) });
            }
            result = results.OrderByDescending(r => r.NumberOf).First().NumberOf - results.OrderBy(r => r.NumberOf).First().NumberOf;
            return result;
        }
    }

    internal class InsertionRule
    {
        public string Pair { get; set; }
        public string ToInsert { get; set; }
        public string Element { get; set; }
    }

    internal class Result
    {
        public string Element { get; set; }
        public int NumberOf { get; set; }
    }

    internal class Result2
    {
        public string Element { get; set; }
        public long NumberOf { get; set; }
    }
}
