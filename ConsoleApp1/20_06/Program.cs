using static Programm0;
class Programm1 
{ 
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
            i += !char.IsDigit(arr[i]) ? 1 : 0;
            j -= !char.IsDigit(arr[j]) ? 1 : 0;
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

        var sr = new StreamReader(path);
        var text = sr.ReadToEnd();
        sr.Close();
        return text;
    }

    static string GetLongestWord(string text, char[] wordSeparators)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentNullException(nameof(text));
        }

        if (wordSeparators == null || wordSeparators.Length == 0)
        {
            throw new ArgumentNullException(nameof(wordSeparators), "Words separators array is null or empty");
        }

        return text.ToLower()
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

        int index = 0, newIndex = 0, count = 0;
        while(index < text.Length)
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

    static string ReplaceWithDictionary(string text, Dictionary<string, string> replaceDictionary)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentNullException(nameof(text));
        }

        if (replaceDictionary == null || replaceDictionary.Count() == 0)
        {
            throw new ArgumentNullException(nameof(replaceDictionary), "Dictionary is null or empty");
        }

        foreach(var q in replaceDictionary)
        {
            text = text.Replace(q.Key, q.Value);
        }

        return text;
    }

    static Dictionary<string, IEnumerable<string>> GetWordsWithSimilarLastAndFirstLetters(string text, char[] wordSeparators)
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

    static string[] GetSentencesWithoutChars(string text, char[] sentencesSeparator, char[] bannedChars)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentNullException(nameof(text));
        }

        if (sentencesSeparator == null || sentencesSeparator.Length == 0)
        {
            throw new ArgumentNullException(nameof(sentencesSeparator), "Sentence separators array is null or empty");
        }

        if (bannedChars == null || bannedChars.Length == 0)
        {
            throw new ArgumentNullException(nameof(bannedChars), "Banned chars array is null or empty");
        }

        return text.Split(sentencesSeparator)
            .Where(sentence => sentence.IndexOfAny(bannedChars) == -1)
            .ToArray();
    }

    static string[] GetSentencesWithSentenceSeparator(string text, char[] sentSeparators)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentNullException(nameof(text));
        }

        if (sentSeparators == null || sentSeparators.Length == 0)
        {
            throw new ArgumentNullException(nameof(sentSeparators), "Sentence separators array is null or empty");
        }

        List<string> sentences = new();
        for (int i = 0, j = i; j < text.Length; j++)
        {
            if (sentSeparators.Contains(text[j]))
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

    private static Dictionary<string, string> WORDS_FOR_DIGITS = new()
    {
        { "0", "zero" },
        { "1", "one" },
        { "2", "two" },
        { "3", "three" },
        { "4", "four" },
        { "5", "five" },
        { "6", "six" },
        { "7", "seven" },
        { "8", "eight" },
        { "9", "nine" }
    };

    private static char[] WORD_SEPARATORS = { '.', ',', '!', '?', ' ', ';', '>', '<'};
    private static char[] SENTENCE_SEPARATOR = { '.', '!', '?' };
    private static char[] BANNED_CHARS = { ',' };

    public static void Main()
    {
        string menu = "0.Input text\n1.Print text\n2.Get the longest word and count it into the text\n3.Replace digits with words\n4.Print first interrogative than exclamatory sentences\n5.Print sentences without points\n6.Print words with similar first and last letters\n7.Additional task: reverse digits in a word\n8.Quit";
        string textInputMenu = "1.Read from console\n2.Read from file\n3.Cancel";
        string text = string.Empty;
        int option;
        int textInputOption;
        while (true)
        {
            try
            {
                option = PrintMessageAndChooseValue(menu, 0, 8);
                switch (option)
                {
                    case 0:
                        textInputOption = PrintMessageAndChooseValue(textInputMenu, 1, 3);
                        string inpText;
                        
                        switch (textInputOption)
                        {
                            case 1:       
                                Console.WriteLine("Input text:");
                                #pragma warning disable CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.
                                inpText = Console.ReadLine();
                                #pragma warning restore CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.
                                break;
                            case 2:
                                Console.WriteLine("Input file's path:");
                                #pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                                inpText = ReadTextFromFile(Console.ReadLine());
                                #pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                                break;
                            default:
                                continue;
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
                    
                    case 1:
                        Console.WriteLine(text);
                        break;
                    
                    case 2:
                        int count;
                        string word;
                        word = GetLongestWord(text, WORD_SEPARATORS);
                        count = Count(text, word);
                        Console.WriteLine($"The longest word - {word}; count - {count}");
                        break;

                    case 3:
                        Console.WriteLine("Dictionary:");
                        foreach (var pair in WORDS_FOR_DIGITS)
                        {
                            Console.WriteLine($"{pair.Key}: {pair.Value}");
                        }

                        Console.WriteLine();
                        Console.WriteLine(ReplaceWithDictionary(text, WORDS_FOR_DIGITS));
                        break;
                    
                    case 4:
                        var sentences = GetSentencesWithSentenceSeparator(text, SENTENCE_SEPARATOR);
                        var interrogative = sentences.Where(sent => sent.EndsWith('?'));
                        var exclamatory = sentences.Where(sent => sent.EndsWith('!'));
                        
                        if (interrogative.Any())
                        {
                            Console.WriteLine("Interrogatives:\n" + string.Join("\n", interrogative));
                        }
                        else
                        {
                            Console.WriteLine("Interrogatives not found");
                        }

                        if (exclamatory.Any())
                        {
                            Console.WriteLine("Exclamatories:\n" + string.Join("\n", exclamatory));
                        }
                        else
                        {
                            Console.WriteLine("Exclamatories not found");
                        }

                        break;

                    case 5:
                        Console.WriteLine(string.Join("\n", GetSentencesWithoutChars(text, SENTENCE_SEPARATOR, BANNED_CHARS)));
                        break;
                    
                    case 6:
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
                    
                    case 7:
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
        //../../../TextFile1.txt
    }
}