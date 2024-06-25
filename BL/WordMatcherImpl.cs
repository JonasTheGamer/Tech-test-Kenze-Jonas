using DAL;

namespace BL;

public class WordMatcherImpl : IWordMatcher
{
    private ICombinationReader _combinationReader;

    public WordMatcherImpl(ICombinationReader combinationReader)
    {
        _combinationReader = combinationReader;
    }

    /*public List<List<string>> MatchWords(int maxCombinationLength, int wordLength)
    {
        List<string> combinations = _combinationReader.ReadCombination();
        foreach (var combination in combinations)
        {
            Console.WriteLine(combination);
        }

        // TODO: Implement logic

        return new List<List<string>>();
    }*/

    public List<List<string>> MatchWords(int maxCombinationLength, int wordLength)
    {
        List<List<string>> matches = new List<List<string>>();

        HashSet<string> wordsSet = new HashSet<string>(_combinationReader.ReadCombination());
        List<string> words = wordsSet.Where(word => word.Length == wordLength).ToList();
        List<string> wordPartsList = wordsSet.Where(word => word.Length < wordLength).ToList();
        HashSet<string> wordParts = new HashSet<string>(wordPartsList);

        foreach (string wordPart in wordParts)
        {
            FindMatchesRecursive(wordPart, words, wordParts, new List<string>(), matches, maxCombinationLength,
                wordLength);
        }

        return matches;
    }

    private static void FindMatchesRecursive(string currentPart, List<string> words, HashSet<string> wordParts,
        List<string> currentCombination, List<List<string>> matches, int maxCombinationLength, int targetLength)
    {
        currentCombination.Add(currentPart);

        // Make sure that the loop no longer runs when the combination would be too long
        if (currentCombination.Count == maxCombinationLength)
        {
            return;
        }

        // Loop through the words to see if any of them match the currentPart
        foreach (string word in words)
        {
            string newBeginning = string.Join("", currentCombination);
            if (word.StartsWith(newBeginning))
            {
                string remainingPart = word.Substring(newBeginning.Length);

                // If the currentPart is the word itself, match!
                if (remainingPart.Length == 0 && currentPart.Length == targetLength)
                {
                    matches.Add(new List<string>(currentCombination) { "=" + word });
                }
                
                // If the remaining part itself is in the wordParts, match!
                // For example: currentPart is foo & remainingPart is bar, and foobar is in the wordParts
                else if (wordParts.Contains(remainingPart))
                {
                    List<string> newCombination = new List<string>(currentCombination) { remainingPart };
                    matches.Add(newCombination);
                }
                // Otherwise, continue the search
                else
                {
                    foreach (string nextPart in wordParts)
                    {
                        FindMatchesRecursive(nextPart, words, wordParts, new List<string>(currentCombination), matches,
                            maxCombinationLength, targetLength);
                    }
                }
            }
        }

        currentCombination.RemoveAt(currentCombination.Count - 1);
    }
}