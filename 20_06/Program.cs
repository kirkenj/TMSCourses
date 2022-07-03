using static testRepo.Programm;
class Programm1
{
    enum MenuItemSelection
    {
        InputText = 0,
        PrintText = 1,
        GetWordWithHighestDigitsAmmount = 2,
        GetLongestWordAndCountIntoTheText = 3, 
        ReplaceDigitsWithWords = 4,
        PrintFirstInterrogativeThanExclamatorySentences = 5, 
        PrintSentencesWithoutPoints = 6,
        PrintWordsWithSimilarFirstAndLastLetters = 7, 
        ReverseDigitsInWord = 8, 
        Quit = 9
    }

    private static readonly string MAIN_MENU = 
        "0.Input text\n" +
        "1.Print text\n" +
        "2.Get word with highest digits ammount\n" +
        "3.Get the longest word and count it into the text\n" +
        "4.Replace digits with words\n" +
        "5.Print first interrogative than exclamatory sentences\n" +
        "6.Print sentences without points\n" +
        "7.Print words with similar first and last letters\n" +
        "8.Additional task: reverse digits in a word\n" +
        "9.Quit";
    
    private static readonly string TEXT_INPUT_MENU = 
        "1.Read from console\n" +
        "2.Read from file\n" +
        "3.Cancel";

    private static readonly (string Key, string Value)[] WORDS_FOR_DIGITS = new (string, string)[]
    {
        ( "0", "zero" ),
        ( "1", "one" ),
        ( "2", "two" ),
        ( "3", "three" ),
        ( "4", "four" ),
        ( "5", "five" ),
        ( "6", "six" ),
        ( "7", "seven" ),
        ( "8", "eight" ),
        ( "9", "nine" )
    };

    private static readonly char[] WORD_SEPARATORS = { '.', ',', '!', '?', ' ', ';', '>', '<', '-' };
    private static readonly char[] SENTENCE_SEPARATORS = { '.', '!', '?' };
    private static readonly char[] BANNED_CHARS = { ',' };

    static string ReverseDigitsInText(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentNullException(nameof(text));
        }

        char[] arr = text.ToCharArray();
        int i = 0, j = arr.Length - 1;
        while (i < j)
        {
            i += char.IsDigit(arr[i]) ? 0 : 1;
            j -= char.IsDigit(arr[j]) ? 0 : 1;
            if (char.IsDigit(arr[i]) && char.IsDigit(arr[j]))
            {
                (arr[i], arr[j]) = (arr[j--], arr[i++]);
            }
        }

        return new string(arr);
    }

    static string ReadTextFromFile(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentNullException(nameof(path));
        }

        if (!File.Exists(path))
        {
            throw new FileNotFoundException();
        }

        if (!path.EndsWith(".txt"))
        {
            throw new FormatException("File format is not 'txt'");
        }

        string text = string.Empty;
        using (var sr = new StreamReader(path))
        {
            text = sr.ReadToEnd();
        }

        return text;
    }

    static string GetWordWithMaximalCount(string text, in char[] wordSeparators, Func<string, int> countFunc)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentNullException(nameof(text));
        }

        if (wordSeparators == null || wordSeparators.Length == 0)
        {
            throw new ArgumentNullException(nameof(wordSeparators), "Words separators array is null or empty");
        }

        return text.Split(wordSeparators, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Distinct()
            .OrderByDescending(g => countFunc(g))
            .First();
    }

    static string GetLongestWord(string text, in char[] wordSeparators)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentNullException(nameof(text));
        }

        if (wordSeparators == null || wordSeparators.Length == 0)
        {
            throw new ArgumentNullException(nameof(wordSeparators), "Words separators array is null or empty");
        }

        return text
            .Split(wordSeparators, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Distinct()
            .OrderByDescending(word => word.Length).First();
    }

    static int Count(string text, string substring, bool doConvertToLower = true)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentNullException(nameof(text));
        }

        if (string.IsNullOrWhiteSpace(substring))
        {
            throw new ArgumentNullException(nameof(substring));
        }

        if (doConvertToLower)
        {
            substring = substring.ToLower();
            text = text.ToLower();
        }

        int index = 0, newIndex, count = 0;
        while (index < text.Length)
        {
            newIndex = text.IndexOf(substring, index);
            if (newIndex == -1)
            {
                break;
            }
            count++;
            index = newIndex + substring.Length;
        }

        return count;
    }

    static string ReplaceWithDictionary(string text, in (string Key, string Value)[] replaceDictionary)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentNullException(nameof(text));
        }

        if (replaceDictionary == null || replaceDictionary.Length == 0)
        {
            throw new ArgumentNullException(nameof(replaceDictionary), "Dictionary is null or empty");
        }

        foreach (var (Key, Value) in replaceDictionary)
        {
            text = text.Replace(Key, Value);
        }

        return text;
    }

    static Dictionary<string, IEnumerable<string>> GetWordsWithSimilarLastAndFirstLetters(string text, in char[] wordSeparators)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentNullException(nameof(text));
        }

        if (wordSeparators == null || wordSeparators.Length == 0)
        {
            throw new ArgumentNullException(nameof(wordSeparators), "Words separators array is null or empty");
        }

        var res = text.ToLower()
            .Split(wordSeparators, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Distinct()
            .ToList();
        Dictionary<string, IEnumerable<string>> dict = new();
        while (res.Any())
        {
            var word = res.First();
            var words = res.Where(r => r[0] == word[0] && r[^1] == word[^1]).ToList();
            if (words.Count > 1)
            {
                dict.Add(word[0].ToString() + word[^1].ToString(), words);
            }

            foreach (var wordIter in words)
            {
                res.Remove(wordIter);
            }
        }
        return dict;
    }

    static string[] GetSentencesWithoutChars(string text, (char[] sentencesSeparator, char[] bannedChars) charArraysDataTuple)
    {//i can't use parameters with in, out, ref in anonimous methods, lyambdas, queries or local function - CS1628
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentNullException(nameof(text));
        }

        if (charArraysDataTuple.sentencesSeparator == null || charArraysDataTuple.sentencesSeparator.Length == 0)
        {
            throw new ArgumentNullException(nameof(charArraysDataTuple.sentencesSeparator), "Sentence separators array is null or empty");
        }

        if (charArraysDataTuple.bannedChars == null || charArraysDataTuple.bannedChars.Length == 0)
        {
            throw new ArgumentNullException(nameof(charArraysDataTuple.bannedChars), "Banned chars array is null or empty");
        }

        return text.Split(charArraysDataTuple.sentencesSeparator)
            .Where(sentence => sentence.IndexOfAny(charArraysDataTuple.bannedChars) == -1)
            .ToArray();
    }

    static string[] GetTextTrimmedByFunction(in string text, Func<char, bool> ifTrueTextDevisionFunction)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentNullException(nameof(text));
        }

        if (ifTrueTextDevisionFunction == null)
        {
            throw new ArgumentNullException(nameof(ifTrueTextDevisionFunction));
        }

        List<string> sentences = new();
        for (int i = 0, j = i; j < text.Length; j++)
        {
            if (ifTrueTextDevisionFunction(text[j]))
            {
                var sent = text[i..(j + 1)].Trim();
                if (sent.Length > 1)
                {
                    sentences.Add(sent);
                }
                i = j + 1;
            }
        }

        return sentences.ToArray();
    }

    public static (IEnumerable<string> sentsForItem1, IEnumerable<string> sentsForItem2) GetSentencesEndingWithChar(string text, (char firstItem, char secondItem) charTuple)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentNullException(nameof(text));
        }

        var sentences = GetTextTrimmedByFunction(text, SENTENCE_SEPARATORS.Contains);
        return (sentences.Where(sent => sent.EndsWith(charTuple.firstItem)), sentences.Where(sent => sent.EndsWith(charTuple.secondItem)));
    }

    public static void Main()
    {
        //../../../res/TextFile1.txt
        string text = string.Empty;
        MenuItemSelection option;
        int textInputOption;
        int bufferInt;
        while (true)
        {
            try
            {
                bufferInt = PrintMessageAndChooseValue(MAIN_MENU, 0, 9);
                if (Enum.IsDefined(typeof(MenuItemSelection), bufferInt))
                {
                    option = (MenuItemSelection)bufferInt;
                }
                else
                {
                    throw new ArgumentException($"Value is not defined for {typeof(MenuItemSelection)}", nameof(option));
                }

                switch (option)
                {
                    case MenuItemSelection.InputText:
                        textInputOption = PrintMessageAndChooseValue(TEXT_INPUT_MENU, 1, 3);
                        string inpText;

                        switch (textInputOption)
                        {
#pragma warning disable CS8600, CS8604  // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.
                            case 1:
                                Console.WriteLine("Input text:");
                                inpText = Console.ReadLine();
                                break;
                            case 2:
                                Console.WriteLine("Input file's path:");
                                inpText = ReadTextFromFile(Console.ReadLine());
                                break;
                            default:
                                continue;
#pragma warning restore CS8604, CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                        };

                        if (string.IsNullOrWhiteSpace(inpText))
                        {
                            Console.WriteLine("Invalid input text. Change is not applied");
                        }
                        else
                        {
                            text = inpText;
                            Console.WriteLine("Success");
                        }
                        break;

                    case MenuItemSelection.PrintText:
                        Console.WriteLine(text);
                        break;

                    case MenuItemSelection.GetWordWithHighestDigitsAmmount:
                        var digitsWord = GetWordWithMaximalCount(text, WORD_SEPARATORS, (someText) => someText.Count(ch => char.IsDigit(ch)));
                        Console.WriteLine($"The word with the highest figits ammount is '{digitsWord}'");
                        break;

                    case MenuItemSelection.GetLongestWordAndCountIntoTheText:
                        string word = GetLongestWord(text, WORD_SEPARATORS);
                        Console.WriteLine($"The longest word - {word}; count - {Count(text, word)}");
                        break;

                    case MenuItemSelection.ReplaceDigitsWithWords:
                        Console.WriteLine("Dictionary:");
                        foreach (var pair in WORDS_FOR_DIGITS)
                        {
                            Console.WriteLine($"{pair.Key}: {pair.Value}");
                        }

                        Console.WriteLine();
                        Console.WriteLine(ReplaceWithDictionary(text, WORDS_FOR_DIGITS));
                        break;

                    case MenuItemSelection.PrintFirstInterrogativeThanExclamatorySentences:
                        var sentTuple= GetSentencesEndingWithChar(text, ('!', '?'));
                        Console.WriteLine(sentTuple.sentsForItem1.Any() ? "Interrogatives:\n" + string.Join("\n", sentTuple.sentsForItem1) : "Interrogatives not found");
                        Console.WriteLine(sentTuple.sentsForItem2.Any() ? "Exclamatories:\n" + string.Join("\n", sentTuple.sentsForItem2) : "Exclamatories not found");
                        break;

                    case MenuItemSelection.PrintSentencesWithoutPoints:
                        Console.WriteLine(string.Join("\n", GetSentencesWithoutChars(text, (SENTENCE_SEPARATORS, BANNED_CHARS))));
                        break;

                    case MenuItemSelection.PrintWordsWithSimilarFirstAndLastLetters:
                        var dict = GetWordsWithSimilarLastAndFirstLetters(text, WORD_SEPARATORS);
                        if (!dict.Any())
                        {
                            Console.WriteLine("No such words found");
                            continue;
                        }

                        foreach (var pair in dict)
                        {
                            Console.WriteLine($"{pair.Key}: {string.Join(", ", pair.Value)}");
                        }
                        break;

                    case MenuItemSelection.ReverseDigitsInWord:
                        Console.WriteLine(ReverseDigitsInText(text));
                        break;

                    default:
                        Console.WriteLine("Programm stoped...");
                        return;
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}