using AOC2024_CSharp;

var searchWord = "XMAS";

var fileReader = new FileReader();
var filePath = "test_data.txt";
var fileContents = fileReader.ReadFileContents(filePath);

var dataStore = new List<List<char>>();

foreach (var line in fileContents)
{
    var chars = line.ToCharArray();
    dataStore.Add(chars.ToList());
}

var foundCount = 0;

for (var row = 0; row <= dataStore.Count - 1; row++)
{
    for (var col = 0; col <= dataStore[row].Count - 1; col++)
    {
        if (dataStore[row][col] == 'X')
        {
            var right = cordList(row, row, col, col + searchWord.Length -1);
            // var left = cordList(row, row, col - searchWord.Length + 1, col + 1);
            // var left = cordList(row, row, col, col - searchWord.Length);
            // var down = cordList(row, row + searchWord.Length, col, col);
            // var up = cordList(row, row - searchWord.Length, col, col);
            
            var tempArray = new List<char>();
            
            foreach (var cord in right)
            {
                Console.WriteLine($"In right looking for {dataStore[cord[0]][cord[1]]}");
                tempArray.Add(dataStore[cord[0]][cord[1]]);
            }
            foundCount += DoCordsEqualSearchWord(tempArray);
            
            // foreach (var cord in left)
            // {
            //     tempArray.Add(dataStore[cord[0]][cord[1]]);
            // }
            // tempArray.Reverse();
            // foundCount += DoCordsEqualSearchWord(tempArray);
            
            var _temmp = string.Join("", tempArray);
        }
    }
}

Console.WriteLine($"Found count = {foundCount}");

int DoCordsEqualSearchWord(List<char> cordsToCheck)
{
    var stringList = string.Join("", cordsToCheck);
    
    if (cordsToCheck.Count != 4)
    {
        return 0;
    }

    if (stringList == searchWord)
    {
        return 1;
    }
    return 0;
};




List<List<int>> cordList(int startRow, int stopRow, int startCol, int stopCol)
{
    if (startCol < 0 || startRow < 0)
    {
        return new List<List<int>>();
    }
    var rowIncreasing = startRow < stopRow;
    var rowDecreasing = startRow > stopRow;
    var colIncreasing = startCol < stopCol;
    var colDecreasing = startCol > stopCol;
    
    var returnChords = new List<List<int>>();

    if (!rowIncreasing && !rowDecreasing)
    {
        for (var col = startCol; col != stopCol; col+= colIncreasing ? 1 : -1)
        {
            returnChords.Add(new List<int>(){startRow, col});
        }
    }

    if (returnChords.Count == searchWord.Length)
    {
        return returnChords;
    }
    
    return new List<List<int>>();
}
