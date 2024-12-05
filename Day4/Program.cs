using AOC2024_CSharp;

var searchWord = "XMAS";
var p2SearchWord = "MAS";

var fileReader = new FileReader();
var filePath = "data.txt";
var fileContents = fileReader.ReadFileContents(filePath);

var dataStore = new List<List<char>>();

foreach (var line in fileContents)
{
    var chars = line.ToCharArray();
    dataStore.Add(chars.ToList());
}

var p1FoundCount = 0;
var p2FoundCount = 0;

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
            p1FoundCount += DoCordsEqualSearchWord(rightArray);
            
            var leftArray = left
                .Select(cord => dataStore[cord[0]][cord[1]])
                .ToList();
            p1FoundCount += DoCordsEqualSearchWord(leftArray);
            
            var downArray = down
                .Select(cord => dataStore[cord[0]][cord[1]])
                .ToList();
            p1FoundCount += DoCordsEqualSearchWord(downArray);
            
            var upArray = up
                .Select(cord => dataStore[cord[0]][cord[1]])
                .ToList();
            p1FoundCount += DoCordsEqualSearchWord(upArray);
            
            var upLeftArray = upLeft
                .Select(cord => dataStore[cord[0]][cord[1]])
                .ToList();
            p1FoundCount += DoCordsEqualSearchWord(upLeftArray);
            
            var upRightArray = upRight
                .Select(cord => dataStore[cord[0]][cord[1]])
                .ToList();
            p1FoundCount += DoCordsEqualSearchWord(upRightArray);
            
            var downLeftArray = downLeft
                .Select(cord => dataStore[cord[0]][cord[1]])
                .ToList();
            p1FoundCount += DoCordsEqualSearchWord(downLeftArray);
            
            var downRigntArray = downRight
                .Select(cord => dataStore[cord[0]][cord[1]])
                .ToList();
            p1FoundCount += DoCordsEqualSearchWord(downRigntArray);
        }

        if (dataStore[row][col] == 'A')
        {
            var newCords = xCordList(row, col);
            var leftDiag = newCords.Item1.Select(cord => dataStore[cord[0]][cord[1]]).ToList();
            var rightDiag = newCords.Item2.Select(cord => dataStore[cord[0]][cord[1]]).ToList();
            p2FoundCount += DoCordsXSearchWord(leftDiag, rightDiag);
        }
    }
}

Console.WriteLine($"P1 Found count = {p1FoundCount}");
Console.WriteLine($"P2 Found count = {p2FoundCount}");

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

int DoCordsXSearchWord(List<Char> left, List<Char> right)
{
    var leftWord = string.Join("", left);
    var leftWordReversed = new string(leftWord.Reverse().ToArray());
    var rightWord = string.Join("", right);
    var rightWordReversed = new string(rightWord.Reverse().ToArray());

    if ((leftWord == p2SearchWord || leftWordReversed == p2SearchWord) && (rightWord == p2SearchWord || rightWordReversed == p2SearchWord))
    {
        return 1;
    }
    return 0;
}

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

(List<List<int>>, List<List<int>>) xCordList(int row, int col)
{
    var leftDiagCords = new List<List<int>>();
    leftDiagCords.Add([row - 1, col - 1]);
    leftDiagCords.Add([row, col]);
    leftDiagCords.Add([row + 1, col + 1]);
    
    var rightDiagCords = new List<List<int>>();
    rightDiagCords.Add([row - 1, col + 1]);
    rightDiagCords.Add([row, col]);
    rightDiagCords.Add([row + 1, col - 1]);

    var validLeft = true;
    var validRight = true;

    foreach (var cord in leftDiagCords)
    {
        if (!AreValidCords(cord))
        {
            return ([], []);
        }
    }
    
    foreach (var cord in rightDiagCords)
    {
        if (!AreValidCords(cord))
        {
            return ([], []);
        }
    }

    return (leftDiagCords, rightDiagCords);
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