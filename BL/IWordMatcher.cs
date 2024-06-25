namespace BL;

public interface IWordMatcher
{
    List<List<string>> MatchWords(int maxCombinationLength, int wordLength);
}