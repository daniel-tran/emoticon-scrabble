using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scrabble.Core.Validator
{
    public class WordValidator
    {
        public List<string> ValidWords { get; set; }
        public ScrabbleForm ScrabbleForm { get; set; }

        public WordValidator()
        {
            this.LoadWords();
        }

        /// <summary>
        /// Loads the list of valid words from the input file.
        /// These words are from the Collin's dictionary of valid scrabble words.
        /// </summary>
        private void LoadWords()
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
        public bool CheckWord(string word)
        {
            if (ValidWords == null || !ValidWords.Any())
            {
                LoadWords();
            }

            // Todo: maybe not all 1 length words should be valid???
            if (word.Length == 1)
                return true;

            return ValidWords.FirstOrDefault(w => w == word) != null;
        }

        /// <summary>
        /// Validate all the words on the board.
        /// </summary>
        /// <returns></returns>
        public bool ValidateAllWordsInPlay()
        {
            var words = new List<string>();

            for (int x = 0; x < ScrabbleForm.BOARD_WIDTH; x++)
            {
                for (int y = 0; y < ScrabbleForm.BOARD_HEIGHT; y++)
                {
                    if (!string.IsNullOrEmpty(ScrabbleForm._tiles[x, y].Text))
                    {
                        foreach (var w in GetSurroundingWords(x, y))
                        {
                            if (!words.Contains(w))
                                words.Add(w);
                        }
                    }
                }
            }

            foreach (var w in words)
            {
                MessageBox.Show($"{w} valid: {CheckWord(w)}");
            }

            return words.All(w => CheckWord(w));
        }

        /// <summary>
        /// Traverse the board horizontally and vertically from a given point (x, y)
        /// to find the full word in play in both the horizontal and vertical direction.
        /// These words are then validated to ensure that the move is valid.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public List<string> GetSurroundingWords(int x, int y)
        {
            var words = new List<string>();

            string horizontal = string.Empty;
            string vertical = string.Empty;

            // Start/End location for the horizonal word
            var tx = x;
            while (tx >= 0 && !string.IsNullOrEmpty(ScrabbleForm._tiles[tx, y].Text))
                tx -= 1;

            var tx2 = x;
            while (tx2 < ScrabbleForm.BOARD_WIDTH && !string.IsNullOrEmpty(ScrabbleForm._tiles[tx2, y].Text))
                tx2 += 1;

            for (var i = Math.Max(tx, 0); i <= Math.Min(tx2, ScrabbleForm.BOARD_WIDTH - 1); i++)
                horizontal += ScrabbleForm._tiles[i, y].Text;

            // Start/End location for the vertical word
            var ty = y;
            while (ty >= 0 && !string.IsNullOrEmpty(ScrabbleForm._tiles[x, ty].Text))
                ty -= 1;

            var ty2 = y;
            while (ty2 < ScrabbleForm.BOARD_WIDTH && !string.IsNullOrEmpty(ScrabbleForm._tiles[x, ty2].Text))
                ty2 += 1;

            for (var i = Math.Max(ty, 0); i <= Math.Min(ty2, ScrabbleForm.BOARD_HEIGHT - 1); i++)
                vertical += ScrabbleForm._tiles[x, i].Text;

            if (!string.IsNullOrEmpty(horizontal) && horizontal.Length > 1)
                words.Add(horizontal);

            if (!string.IsNullOrEmpty(vertical) && vertical.Length > 1)
                words.Add(vertical);

            return words;
        }
    }
}
