using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble.Core
{
    public class TileBag
    {
        public List<char> Letters { get; set; }

        public TileBag()
        {
            SetupBag();
        }

        /// <summary>
        /// Sets up a tile bag ready for a game.
        /// </summary>
        public void SetupBag()
        {
            Letters = new List<char>();
            string allChars = " !()*+-./038:;<=>@BCDFIJKLOPSTVX['\\]^_bcdnopqsuvw{|}~";
            char[] chars = allChars.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                char c = chars[i];
                for (int x = 0; x < LetterCount(c); x++)
                {
                    this.Letters.Add(c);
                }
            }

            Letters = Letters.OrderBy(l => Guid.NewGuid()).ToList();
        }

        /// <summary>
        /// Returns how many letters are left in the rack.
        /// </summary>
        public int LetterCountRemaining()
        {
            return Letters.Count;
        }

        /// <summary>
        /// Give a letter back to the bag. This would be triggered when a user swaps a tile.
        /// </summary>
        /// <param name="letter"></param>
        public void GiveLetter(char letter)
        {
            Letters.Add(letter);
        }

        public string TakeLetters(int numLetters)
        {
            var letters = "";
            var random = new Random();

            while (letters.Length < numLetters)
            {
                // Ran out of letters
                if (Letters.Count == 0)
                    break;

                var randomLetter = Letters[random.Next(0, Letters.Count)];
                letters += randomLetter;
                Letters.Remove(randomLetter);
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
                { ':' , 5 } ,
                { '=' , 5 } ,
                { 'X' , 5 } ,
                { '8' , 5 } ,
                { ';' , 5 } ,
                { 'B' , 5 } ,
                { 'O' , 2 } ,
                { '0' , 2 } ,
                { 'o' , 2 } ,
                { 'D' , 2 } ,
                { 'C' , 2 } ,
                { 'c' , 2 } ,
                { 'T' , 2 } ,
                { 'K' , 2 } ,
                { 'S' , 2 } ,
                { 's' , 2 } ,
                { 'I' , 2 } ,
                { 'v' , 2 } ,
                { 'V' , 2 } ,
                { 'L' , 2 } ,
                { '<' , 2 } ,
                { '(' , 2 } ,
                { '>' , 2 } ,
                { ')' , 2 } ,
                { '{' , 2 } ,
                { '}' , 2 } ,
                { ']' , 2 } ,
                { '[' , 2 } ,
                { '/' , 2 } ,
                { '\\' , 2 } ,
                { '|' , 2 } ,
                { 'P' , 2 } ,
                { 'p' , 2 } ,
                { 'F' , 2 } ,
                { 'J' , 2 } ,
                { 'b' , 2 } ,
                { '3' , 2 } ,
                { 'd' , 2 } ,
                { 'q' , 2 } ,
                { '-' , 3 } ,
                { '^' , 3 } ,
                { '*' , 3 } ,
                { ' ' , 3 } ,
                { '+' , 2 } ,
                { '\'' , 2 } ,
                { '.' , 2 } ,
                { '@' , 2 } ,
                { '~' , 2 } ,
                { 'u' , 2 } ,
                { '_' , 2 } ,
                { '!' , 2 } ,
                { 'n' , 2 } ,
                { 'w' , 2 } ,
            };

            return timesMapping[c];
        }
    }
}
