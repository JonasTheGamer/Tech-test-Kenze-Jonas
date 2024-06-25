namespace DAL;

public class FileCombinationReader : ICombinationReader
{
    private string _input;

    public FileCombinationReader(string input)
    {
        _input = input;
    }

    public List<string> ReadCombination()
    {
        try
        {
            string[] readText = File.ReadAllLines(_input);
            return readText.ToList();
        }
        catch (IOException e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
            throw;
        }
    }
}