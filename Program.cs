string day = "10";
string path = @"C:\Users\fokkov\OneDrive - Delta-N\Desktop\input.txt";

if (!File.Exists(path))
{
    Console.WriteLine("File not found");
}

int result1 = -1;
long result2 = -1;
switch (day)
{
    case "5":
        result1 = AdventOfCode.day5.Run(path);
        break;
    case "6":
        result1 = AdventOfCode.day6.Run(path);
        break;
    case "7":
        result1 = AdventOfCode.day7.Run1(path);
        result2 = AdventOfCode.day7.Run2(path);
        break;
    case "8":
        result1 = AdventOfCode.day8.Run1(path);
        result2 = AdventOfCode.day8.Run2(path);
        break;
    case "9":
        // result1 solved in Excel
        result2 = AdventOfCode.day9.Run2(path);
        break;
    case "10":
        result1 = AdventOfCode.day10.Run1(path);
        result2 = AdventOfCode.day10.Run2(path);
        break;
    default:
        break;
}

Console.WriteLine(result1.ToString());
Console.WriteLine(result2.ToString());