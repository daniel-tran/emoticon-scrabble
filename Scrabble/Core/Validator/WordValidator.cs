using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble.Core.Validator
{
    public static class WordValidator
    {
        public static List<string> ValidWords { get; set; }

        /// <summary>
        /// Loads the list of valid words from the input file.
        /// These words are from the Collin's dictionary of valid scrabble words.
        /// </summary>
        private static void LoadWords()
        {
            ValidWords = new List<string>();

            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Resources\valid_words.txt");
            foreach (var w in File.ReadAllLines(path))
            {
                ValidWords.Add(w);
            }
        }

        /// <summary>
        /// Check if a provided word is present in the list of known valid words.
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static bool CheckWord(string word)
        {
            if (ValidWords == null || !ValidWords.Any())
            {
                LoadWords();
            }

            return ValidWords.FirstOrDefault(w => w == word) != null;
        }
    }
}
