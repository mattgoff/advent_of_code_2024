using AOC2024_CSharp;

var searchWord = "XMAS";

var fileReader = new FileReader();
var filePath = "data.txt";
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
            var right = newCordList(row, row, col, col + searchWord.Length - 1);
            var left = newCordList(row, row, col, col - searchWord.Length + 1);
            var down = newCordList(row, row + searchWord.Length - 1, col, col);
            var up = newCordList(row, row - searchWord.Length + 1, col, col);
            var upLeft = newCordList(row, row - searchWord.Length + 1, col, col - searchWord.Length + 1);
            var upRight = newCordList(row, row - searchWord.Length + 1 , col, col + searchWord.Length - 1);
            var downLeft = newCordList(row, row + searchWord.Length - 1, col, col - searchWord.Length + 1);
            var downRight = newCordList(row, row + searchWord.Length - 1, col, col + searchWord.Length - 1);
            
            var rightArray = right
                .Select(cord => dataStore[cord[0]][cord[1]])
                .ToList();
            foundCount += DoCordsEqualSearchWord(rightArray);
            
            var leftArray = left
                .Select(cord => dataStore[cord[0]][cord[1]])
                .ToList();
            foundCount += DoCordsEqualSearchWord(leftArray);
            
            var downArray = down
                .Select(cord => dataStore[cord[0]][cord[1]])
                .ToList();
            foundCount += DoCordsEqualSearchWord(downArray);
            
            var upArray = up
                .Select(cord => dataStore[cord[0]][cord[1]])
                .ToList();
            foundCount += DoCordsEqualSearchWord(upArray);
            
            var upLeftArray = upLeft
                .Select(cord => dataStore[cord[0]][cord[1]])
                .ToList();
            foundCount += DoCordsEqualSearchWord(upLeftArray);
            
            var upRightArray = upRight
                .Select(cord => dataStore[cord[0]][cord[1]])
                .ToList();
            foundCount += DoCordsEqualSearchWord(upRightArray);
            
            var downLeftArray = downLeft
                .Select(cord => dataStore[cord[0]][cord[1]])
                .ToList();
            foundCount += DoCordsEqualSearchWord(downLeftArray);
            
            var downRigntArray = downRight
                .Select(cord => dataStore[cord[0]][cord[1]])
                .ToList();
            foundCount += DoCordsEqualSearchWord(downRigntArray);
        }
    }
}

Console.WriteLine($"Found count = {foundCount}");

void debugArray(List<char> arrayToCheck, int row, int col)
{
    var temp = string.Join("", arrayToCheck);
    if (temp.Length == searchWord.Length)
    {
        Console.WriteLine($"Row: {row} Col: {col} String: {temp}");
    }
}

int DoCordsEqualSearchWord(List<char> cordsToCheck)
{
    var stringList = string.Join("", cordsToCheck);
    if (stringList == searchWord)
    {
        return 1;
    }
    return 0;
};

List<List<int>> newCordList(int startRow, int stopRow, int startCol, int stopCol)
{
    int rowStep = startRow < stopRow ? 1 : (startRow > stopRow ? -1 : 0); // no row change
    int colStep = startCol < stopCol ? 1 : (startCol > stopCol ? -1 : 0); // no col change

    var returnChords = new List<List<int>>();

    if (rowStep == 0) // Only columns are changing
    {
        for (var col = startCol; col != stopCol + colStep; col += colStep)
        {
            var newCords = new List<int> {startRow, col};
            if (AreValidCords(newCords))
            {
                returnChords.Add(newCords);
            }
        }
    }
    else if (colStep == 0) // Only rows are changing
    {
        for (var row = startRow; row != stopRow + rowStep; row += rowStep)
        {
            var newCords = new List<int> {row, startCol};
            if (AreValidCords(newCords))
            {
                returnChords.Add([row, startCol]);
            }
        }
    }
    else // Both rows and columns are changing
    {
        var trow = startRow;
        var tcol = startCol;
        for (var i = 0; i < searchWord.Length; i++)
        {
            if (AreValidCords([trow, tcol]))
            {
                returnChords.Add([trow,tcol]);
                trow += rowStep;
                tcol += colStep;
            }
            else
            {
                return [];
            }
        }
    }

    if (returnChords.Count == searchWord.Length)
    {
        return returnChords;
    }
    return [];
}

bool AreValidCords(List<int> newCord)
{
    if (
        newCord[0] <= dataStore.Count() - 1 &&
        newCord[0] >= 0 &&
        newCord[1] >= 0 &&
        newCord[1] <= dataStore[0].Count() - 1
    )
    {
        return true;
    }

    return false;
}