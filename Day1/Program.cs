using AOC2024_CSharp;

var fileReader = new FileReader();
var filePath = "data.txt";
var fileContents = fileReader.ReadFileContents(filePath);
var leftData = new List<int>();
var rightData = new List<int>();

ProcessFileContents(fileContents, leftData, rightData);

leftData.Sort();
rightData.Sort();

var rightDataCounts = rightData.GroupBy(item => item)
    .ToDictionary(group => group.Key, group => group.Count());

// part1
int totalDelta = CalculateTotalDelta(leftData, rightData);
Console.WriteLine($"Total absolute difference: {totalDelta}");

// part2
var totalDelta2 = CalculateTotalDelta2(leftData, rightDataCounts);
Console.WriteLine($"Total delta: {totalDelta2}");


void ProcessFileContents(IEnumerable<string> fileContents, List<int> leftData, List<int> rightData)
{
    foreach (var line in fileContents)
    {
        var parts = line.Split("  ").Select(int.Parse).ToList();
        leftData.Add(parts[0]);
        rightData.Add(parts[1]);;
    }
}

int CalculateTotalDelta(List<int> leftData, List<int> rightData)
{
    int totalAbsoluteDifference = 0;
    for (int i = 0; i < leftData.Count; i++)
    {
        totalAbsoluteDifference += Math.Abs(leftData[i] - rightData[i]);
    }
    return totalAbsoluteDifference;
}


int CalculateTotalDelta2(IEnumerable<int> leftData, Dictionary<int, int> rightDataCounts)
{
    int totalDelta = 0;
    foreach (var item in leftData)
    {
        // Use ternary operation for a more concise count retrieval
        int count = rightDataCounts.ContainsKey(item) ? rightDataCounts[item] : 0;
        totalDelta += item * count;
    }
    return totalDelta;
}

