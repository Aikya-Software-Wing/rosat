using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace SpellingCorrector
{
    public sealed class SpellingSuggestions
    {
        private static readonly Lazy<SpellingSuggestions> lazy = new Lazy<SpellingSuggestions>(() => new SpellingSuggestions());

        public static SpellingSuggestions Instance { get { return lazy.Value; } }

        private static string[] Dictionary;
        public int NumberOfSuggestions { get; set; }

        private SpellingSuggestions()
        {
            Dictionary = System.IO.File.ReadAllLines(HostingEnvironment.MapPath("~/Content/txt/words.txt"));
            NumberOfSuggestions = 10;
        }

        private int Minimum(int x, int y, int z)
        {
            return Math.Min(Math.Min(x, y), z);
        }

        private int EditDistane(String mistake, String actual, int mistakeLength, int actualLength)
        {
            int[,] table = new int[mistakeLength + 1, actualLength + 1];

            for (int i = 0; i <= mistakeLength; i++)
            {
                for (int j = 0; j <= actualLength; j++)
                {
                    if (i == 0)
                    {
                        table[i, j] = j;
                    }
                    else if (j == 0)
                    {
                        table[i, j] = i;
                    }
                    else if (mistake[i - 1] == actual[j - 1])
                    {
                        table[i, j] = table[i - 1, j - 1];
                    }
                    else
                    {
                        table[i, j] = 1 + Minimum(table[i, j - 1], table[i - 1, j],
                            table[i - 1, j - 1]);
                    }
                }
            }

            return table[mistakeLength, actualLength];
        }

        private bool IsWordInDictionary(string word)
        {
            return Dictionary.Where(x => x == word).Count() > 0;
        }

        public List<string> GetSuggestionsForWord(string inputWord)
        {
            List<KeyValuePair<int, string>> suggestions = new List<KeyValuePair<int, string>>();

            if (Dictionary.Where(x => x == inputWord).Count() > 0)
            {
                suggestions.Add(new KeyValuePair<int, string>(-1, inputWord));
            }
            else
            {
                foreach (var dictionaryWord in Dictionary)
                {
                    suggestions.Add(new KeyValuePair<int, string>(EditDistane(inputWord, dictionaryWord, inputWord.Length, dictionaryWord.Length), dictionaryWord));
                }
            }

            return suggestions.OrderBy(x => x.Key).Take(NumberOfSuggestions).Select(x => x.Value).ToList();
        }
    }
}
