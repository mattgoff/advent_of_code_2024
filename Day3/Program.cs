using System.Text.RegularExpressions;
using AOC2024_CSharp;

var fileReader = new FileReader();
var filePath = "data.txt";
var fileContents = fileReader.ReadFileContents(filePath);

var multPattern = "mul(";
var enablePattern = "do()";
var disablePattern = "don't()";

string mulPattern = @"mul\(\d{1,3},\d{1,3}\)";
string numbersPattern = @"\d+,\d+";

var currentlyEnabled = true;

Regex mulSearch = new Regex(mulPattern);
Regex numbers = new Regex(numbersPattern);

int part1Total = 0;
int part2Total = 0;

foreach (var line in fileContents)
{
    var leftPointer = 0;

    while (leftPointer + 4 < line.Length)
    {
        if (leftPointer + disablePattern.Length < line.Length)
        {
            if (line.Substring(leftPointer, disablePattern.Length) == disablePattern)
            {
                currentlyEnabled = false;
            }
        }
        
        if (line.Substring(leftPointer, 4) == enablePattern)
        {
            currentlyEnabled = true;
        }
        
        if (line.Substring(leftPointer, 4) == multPattern)
        {
            var tempString = "";
            
            if (leftPointer + 12 <= line.Length)
            {
                tempString = line.Substring(leftPointer, 12);
            }
            else
            {
                tempString = line.Substring(leftPointer, line.Length - leftPointer);
            }
            
            MatchCollection matches = mulSearch.Matches(tempString);
            if (matches.Count == 1)
            {
                var ints = numbers
                    .Matches(matches[0].Value)[0]
                    .Value
                    .Split(",")
                    .Select(int.Parse);
                
                int product = ints.Aggregate(1, (acc, x) => acc * x);
                part1Total += product;
                
                if (currentlyEnabled)
                {
                    part2Total += product;
                }
            }
        }
        leftPointer += 1;
    }
}

Console.WriteLine($"Part 1 Total is: {part1Total}");
Console.WriteLine($"Part 2 Total is: {part2Total}");
