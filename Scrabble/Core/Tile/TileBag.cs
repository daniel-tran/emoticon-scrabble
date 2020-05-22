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
                { 'O' , 10 } ,
                { '0' , 10 } ,
                { 'o' , 10 } ,
                { 'D' , 10 } ,
                { 'C' , 10 } ,
                { 'c' , 10 } ,
                { 'T' , 10 } ,
                { 'K' , 10 } ,
                { 'S' , 10 } ,
                { 's' , 10 } ,
                { 'I' , 10 } ,
                { 'v' , 10 } ,
                { 'V' , 10 } ,
                { 'L' , 10 } ,
                { '<' , 10 } ,
                { '(' , 10 } ,
                { '>' , 10 } ,
                { ')' , 10 } ,
                { '{' , 10 } ,
                { '}' , 10 } ,
                { ']' , 10 } ,
                { '[' , 10 } ,
                { '/' , 10 } ,
                { '\\' , 10 } ,
                { '|' , 10 } ,
                { 'P' , 10 } ,
                { 'p' , 10 } ,
                { 'F' , 10 } ,
                { 'J' , 10 } ,
                { 'B' , 10 } ,
                { 'b' , 10 } ,
                { '3' , 10 } ,
                { 'd' , 10 } ,
                { 'q' , 10 } ,
                { ':' , 10 } ,
                { '=' , 10 } ,
                { 'X' , 10 } ,
                { '8' , 10 } ,
                { ';' , 10 } ,
                { '-' , 8 } ,
                { '^' , 8 } ,
                { '*' , 8 } ,
                { ' ' , 8 } ,
                { '+' , 6 } ,
                { '\'' , 6 } ,
                { '.' , 6 } ,
                { '@' , 6 } ,
                { '~' , 6 } ,
                { 'u' , 6 } ,
                { '_' , 6 } ,
                { '!' , 6 } ,
                { 'n' , 6 } ,
                { 'w' , 6 } ,
            };

            return timesMapping[c];
        }
    }
}
