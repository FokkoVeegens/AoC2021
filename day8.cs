using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class day8
    {
        //                                    0        1      2        3       4        5         6       7        8         9
        private static string[] digits = { "abcefg", "cf", "acdeg", "acdfg", "bcdf", "abdfg", "abdefg", "acf", "abcdefg", "abcdfg" };
        // chars   a b c d e f g
        // indexes 0 1 2 3 4 5 6
        private static char[] allChars = "abcdefg".ToCharArray();

        public static int Run1(string path)
        {
            int result = 0;
            string input = common.GetInput(path);
            string[] lines = input.Split(Environment.NewLine);
            foreach (string line in lines)
            {
                string[] lineParts = line.Split(new string[] { " | " }, StringSplitOptions.RemoveEmptyEntries);
                string[] signalEntries = lineParts[0].Split(' ');
                string[] outputValues = lineParts[1].Split(' ');

                foreach (string outputValue in outputValues)
                {
                    if (outputValue.Length == digits[1].Length | outputValue.Length == digits[4].Length | outputValue.Length == digits[7].Length | outputValue.Length == digits[8].Length )
                    {
                        result++;
                    }
                }
            }
            return result;
        }

        public static int Run2(string path)
        {
            int result = 0;
            string input = common.GetInput(path);
            string[] lines = input.Split(Environment.NewLine);
            foreach (string line in lines)
            {
                string[] lineParts = line.Split(new string[] { " | " }, StringSplitOptions.RemoveEmptyEntries);
                result += getOutputValue(lineParts[0], lineParts[1]);
            }
            return result;
        }

        private static int getOutputValue(string signal, string output)
        {
            int result = 0;
            string[] signalEntries = signal.Split(' ');
            string[] outputValues = output.Split(' ');

            // All characters of the signal in one array
            char[] signalCharacters = signal.ToCharArray();

            // Determine occurrence per character/segment (A=8, B=6,C=8, D=7, E=4, F=9, G=7)
            int[] charCount = new int[allChars.Length];
            for (int i = 0; i < allChars.Length; i++)
            {
                charCount[i] = signalCharacters.Count(s => s == allChars[i]);
            }

            // mappings do translation from signal entries (wires) to segments in the display
            // index = 0 based, so mappings[0] means the mapping for number 1 in the display
            string[] mappings = new string[charCount.Length];
            for (int mapping = 0; mapping < mappings.Length; mapping++)
            {
                mappings[mapping] = "abcdefg";
            }

            // Deduce 1 (segments c and f)
            string signalEntry = signalEntries.FirstOrDefault(s => s.Length == digits[1].Length);
            mappings[GetIndexOfSegment('c')] = CombineStrings(signalEntry, mappings[GetIndexOfSegment('c')]);
            mappings[GetIndexOfSegment('f')] = CombineStrings(signalEntry, mappings[GetIndexOfSegment('f')]);

            // Deduce 4 (segments b, c, d and f)
            signalEntry = signalEntries.FirstOrDefault(s => s.Length == digits[4].Length);
            mappings[GetIndexOfSegment('b')] = CombineStrings(signalEntry, mappings[GetIndexOfSegment('b')]);
            mappings[GetIndexOfSegment('c')] = CombineStrings(signalEntry, mappings[GetIndexOfSegment('c')]);
            mappings[GetIndexOfSegment('d')] = CombineStrings(signalEntry, mappings[GetIndexOfSegment('d')]);
            mappings[GetIndexOfSegment('f')] = CombineStrings(signalEntry, mappings[GetIndexOfSegment('f')]);

            // Deduce 7 (segments a, c and f)
            signalEntry = signalEntries.FirstOrDefault(s => s.Length == digits[7].Length);
            mappings[GetIndexOfSegment('a')] = CombineStrings(signalEntry, mappings[GetIndexOfSegment('a')]);
            mappings[GetIndexOfSegment('c')] = CombineStrings(signalEntry, mappings[GetIndexOfSegment('c')]);
            mappings[GetIndexOfSegment('f')] = CombineStrings(signalEntry, mappings[GetIndexOfSegment('f')]);
            

            // Deducing 8 doesn't make sense

            // Segment B is the only one to occur 6 times
            mappings[GetIndexOfSegment('b')] = allChars[Array.FindIndex(charCount, c => c == 6)].ToString();

            // Segment E is the only one to occur 4 times
            mappings[GetIndexOfSegment('e')] = allChars[Array.FindIndex(charCount, c => c == 4)].ToString();

            // Segment F is the only one to occur 9 times
            mappings[GetIndexOfSegment('f')] = allChars[Array.FindIndex(charCount, c => c == 9)].ToString();

            // Find mappings that only have 1 segment letter left and remove that letter in other mappings. Repeat this 4 times, because when new 1 segment letter items occur, they help deducing
            for (int i = 0; i < 4; i++)
            {
                foreach (string mapping in mappings.Where(m => m.Length == 1))
                {
                    for (int m = 0; m < mappings.Length; m++)
                    {
                        if (mappings[m].Length > 1)
                        {
                            mappings[m] = mappings[m].Replace(mapping, "");
                        }
                    }
                }
            }

            string resultstring = "";
            foreach (string outputValue in outputValues)
            {
                resultstring += GetNumberFromSegmentSequence(mappings, outputValue).ToString();
            }
            result = int.Parse(resultstring);

            return result;
        }

        private static int GetIndexOfSegment(char segment)
        {
            return Array.FindIndex(allChars, s => s == segment);
        }

        private static string CombineStrings(string str1, string str2)
        {
            char[] str1arr = str1.ToCharArray();
            char[] str2arr = str2.ToCharArray();
            string result = "";
            foreach (char c in str1arr)
            {
                if (str2arr.Count(s => s == c) > 0)
                {
                    result += c.ToString();
                }
            }
            return result;
        }

        private static int GetNumberFromSegmentSequence(string[] mappings, string input)
        {
            int result = 0;
            char[] inputArr = input.ToCharArray();
            for (int i = 0; i < inputArr.Length; i++)
            {
                inputArr[i] = GetRightChar(mappings, inputArr[i]);
            }
            result = Array.FindIndex(digits, d => d == new string(inputArr.OrderBy(i => i).ToArray()));

            return result;
        }

        private static char GetRightChar(string[] mappings, char wrongChar)
        {
            return allChars[Array.FindIndex(mappings, m => m == wrongChar.ToString())];
        }
    }
}
