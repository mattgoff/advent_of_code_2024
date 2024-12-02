namespace AOC2024_CSharp;

public class FileReader
{
    public IEnumerable<string> ReadFileContents(string filePath)
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        string[] files = Directory.GetFiles(currentDirectory);
        
        if (string.IsNullOrEmpty(filePath))
            throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));

        if (!File.Exists(filePath))
            throw new FileNotFoundException($"The file '{filePath}' does not exist.", filePath);

        return File.ReadLines(filePath);
    }
}