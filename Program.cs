string day = "5";
string path = @"C:\Users\fokkov\OneDrive - Delta-N\Desktop\input.txt";

if (!File.Exists(path))
{
    Console.WriteLine("File not found");
}

if (day == "5")
{
    int result = AdventOfCode.day5.Run(path);
    Console.WriteLine(result.ToString());
    Console.ReadKey();
}