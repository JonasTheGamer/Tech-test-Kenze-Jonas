using BL;
using DAL;
using UI_CA;

// Settings
string input = @"input.txt";

// Prepare objects
ICombinationReader reader = new FileCombinationReader(input);
IWordMatcher matcher = new WordMatcherImpl(reader);
ICombinationPresenter presenter = new CombinationConsolePinter("+","=");

// Execute
presenter.PresentCombination(matcher.MatchWords(3, 6));

// Enter to close
Console.WriteLine("\nPress Enter to close");
Console.ReadLine();
