using AOC2024_CSharp;

var fileReader = new FileReader();
var filePath = "data.txt";
var fileContents = fileReader.ReadFileContents(filePath);
var dataStore = new List<List<int>>();

foreach (var line in fileContents)
{
    var ints = line.Split(" ").Select(int.Parse).ToList();
    dataStore.Add(ints);
}

var part1SafeCount = 0;

foreach (var report in dataStore)
{
    var safeResult = safeTest(report);
    
    if (safeResult)
    {
        part1SafeCount += 1;
    }
    else
    {
        // This is for part2 remark out this else blcok for just part1
        Console.WriteLine($"Unsafe at {string.Join(",", report)}");
        for (var i = 0; i < report.Count; i++)
        {
            var newList = report.Where((item, index) => index != i).ToList();
            Console.WriteLine($"\t Retrying with {string.Join(",", newList)}");
            var pop1Test = safeTest(newList);
            if (pop1Test)
            {
                part1SafeCount += 1;
                break;
            }
        }
    }
}

bool safeTest(List<int> report)
{
    bool isSafe = true;
    bool isIncreasing = report[0] < report[1];
    
    for (int i = 0; i < report.Count - 1; i++)
    {
        var diff = Math.Abs(report[i] - report[i + 1]);
        var isIncOrDec = report[i] < report[i + 1];
        
        if (diff == 0 || diff > 3 || isIncreasing != isIncOrDec)
        {
            isSafe = false;
        }
    }

    return isSafe;
}

Console.WriteLine($"Part 1 Safe report count {part1SafeCount}");
