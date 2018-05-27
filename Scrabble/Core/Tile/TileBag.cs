using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble.Core
{
    public class TileBag
    {
        public Dictionary<char, int> Letters { get; set; }

        public TileBag()
        {
            this.SetupBag();
        }

        /// <summary>
        /// Sets up a tile bag ready for a game.
        /// </summary>
        public void SetupBag()
        {
            Letters = new Dictionary<char, int>();
            for (char c = 'A'; c <= 'Z'; c++)
            {
                Letters[c] = LetterCount(c);
            }
        }

        public string TakeLetters(int numLetters)
        {
            var letters = "";
            var random = new Random();

            while (letters.Length < numLetters)
            {
                // Ran out of letters
                if (!Letters.Values.Where(v => v > 0).Any())
                    break;

                // Take a random character from the tilebag.
                var randChar = (char)random.Next('A', 'Z' + 1);
                if (Letters[randChar] > 0)
                {
                    letters += randChar;
                    Letters[randChar] -= 1;
                }
            }

            return letters;
        }

        /// <summary>
        /// How many times times a provided character appear in the tile bag
        /// at the start of a game?
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public int LetterCount(char c)
        {
            var timesMapping = new Dictionary<char, int>()
            {
                { 'E', 12 }, { 'A', 9 }, { 'I', 9 }, { 'O', 8 },
                { 'N', 6 }, { 'R', 6 }, { 'T', 6 }, { 'L', 4  },
                { 'S', 4 }, { 'U', 4 }, { 'D', 4 }, { 'G', 3 },
                { 'B', 2 }, { 'C', 2 }, { 'M', 2 }, { 'P', 2 },
                { 'F', 2 }, { 'H', 2 }, { 'V', 2 }, { 'W', 2 },
                { 'Y', 2 }, { 'K', 1 }, { 'J', 1 }, { 'X', 1 },
                { 'Q', 1 }, { 'Z', 1 },
            };

            return timesMapping[c];
        }

        /// <summary>
        /// How many points is a provided character worth?
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public int LetterValue(char c)
        {
            var scoreMapping = new Dictionary<char, int>()
            {
                // One point
                { 'E', 1 }, { 'A', 1 }, { 'I', 1 }, { 'O', 1 },
                { 'N', 1 }, { 'R', 1 }, { 'T', 1 }, { 'L', 1 },
                { 'S', 1 }, { 'U', 1 },

                // Two points
                { 'D', 2 }, { 'G', 2 },

                // Three points
                { 'B', 3 }, { 'C', 3 }, { 'M', 3 }, { 'P', 3 },

                // Four points
                { 'F', 4 }, { 'H', 4 }, { 'V', 4 }, { 'W', 4 }, { 'Y', 4 },

                // Five points
                { 'K', 5 },

                // Eight points
                { 'J', 8 }, { 'X', 8 },

                // Ten pints
                { 'Q', 10 }, { 'Z', 10 },
            };

            return scoreMapping[c];
        }

    }
}
