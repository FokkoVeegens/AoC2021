string day = "6";
string path = @"C:\Users\fokkov\OneDrive - Delta-N\Desktop\testinput.txt";

if (!File.Exists(path))
{
    Console.WriteLine("File not found");
}

int result = -1;
switch (day)
{
    case "5":
        result = AdventOfCode.day5.Run(path);
        break;
    case "6":
        result = AdventOfCode.day6.Run(path);
        break;
    default:
        break;
}

Console.WriteLine(result.ToString());