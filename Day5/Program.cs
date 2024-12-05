using AOC2024_CSharp;

var fileReader = new FileReader();
var filePath = "test_data.txt";
var fileContents = fileReader.ReadFileContents(filePath);

var orderingRules = new Dictionary<int, List<int>>();
var updates = new List<List<int>>();
var correctlyOrderedUpdates = new List<List<int>>();

foreach (var line in fileContents)
{
    if (line.Contains('|'))
    {
        var splitLine = line.Split('|');
        var key = int.Parse(splitLine[0]);
        var val = int.Parse(splitLine[1]);
        
        if (orderingRules.ContainsKey(key))
        {
            orderingRules[key].Add(val);
        } 
        else
        {
            orderingRules[key] = [val];
        }
    }
    
    if (line.Contains(','))
    {
        var lineData = line.Split(',')
            .Select(int.Parse).ToList();
        updates.Add(lineData);
    }
}

var results = ProcessUpdates();
Console.WriteLine($"Total Correct: {results.Item1} Total of Midpoints: {results.Item2}");

(int, int) ProcessUpdates()
{
    var totalCorrect = 0;
    var totalMidPoint = 0;

    foreach (var update in updates)
    {
        foreach (var page in update)
        {
            var currentPageIndex = update.IndexOf(page);
            var allIndexesInPage = update.Select(x => orderingRules[page]);
            Console.WriteLine("here");
        }

    }

    return (totalCorrect, totalMidPoint);
}