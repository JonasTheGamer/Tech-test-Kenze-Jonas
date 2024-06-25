namespace UI_CA;

public class CombinationConsolePinter : ICombinationPresenter
{
    private string _separator;
    private string _equal;
    
    public CombinationConsolePinter(string separator, string equal)
    {
        _separator = separator;
        _equal = equal;
    }
    
    public void PresentCombination(List<List<string>> combinations)
    {
        foreach (List<string> combination in combinations)
        {
            string joinedElements = string.Join(_separator, combination);
            
            // Print
            Console.WriteLine($"{joinedElements}{_equal}{string.Join("", combination)}");
        }
    }
}