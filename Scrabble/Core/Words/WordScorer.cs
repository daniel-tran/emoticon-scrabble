using Scrabble.Core.Tile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scrabble.Core.Words
{
    public static class WordScorer
    {
        private static TileType[,] _tilePositions;

        /// <summary>
        /// Get the total score for a word. Use's the words location on the board to appropriately apply
        /// word modifiers such as Double Word, Triple Letter etc...
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static int ScoreWord(Word word)
        {
            if (_tilePositions == null)
                LoadTilePositions();

            var score = 0;
            var tripleWordModifer = false;
            var doubleWordModifier = false;

            foreach (var tile in word.Tiles)
            {
                var tileScore = LetterValue(tile.Text[0]);

                // Only "in play" tiles can be affected by the tile modifier.
                if (tile.TileInPlay)
                {
                    switch (tile.TileType)
                    {
                        case TileType.TripleLetter:
                            //MessageBox.Show($"{tile.Text[0]} is on a TL!");
                            tileScore *= 3;
                            break;
                        case TileType.TripleWord:
                            //MessageBox.Show($"{tile.Text[0]} has triggerd a TW!");
                            tripleWordModifer = true;
                            break;
                        case TileType.DoubleLetter:
                            //MessageBox.Show($"{tile.Text[0]} is on a DL!");
                            tileScore *= 2;
                            break;
                        case TileType.DoubleWord:
                            //MessageBox.Show($"{tile.Text[0]} has triggerd a DW!");
                            doubleWordModifier = true;
                            break;
                        default:
                            break;
                    }
                }

                score += tileScore;
            }

            // Apply the triple or double word modifiers.
            if (doubleWordModifier)
                score *= 2;

            if (tripleWordModifer)
                score *= 3;

            return score;
        }

        /// <summary>
        /// Get the "raw" score of a word, i.e that is just the score of the letters in the word,
        /// this will not use the position of the board to apply triple/double modifiers etc.
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static int RawWordScore(string word)
        {
            return word.Sum(c => LetterValue(c));
        }

        /// <summary>
        /// Get the locations of the different types of tiles on the board.
        /// </summary>
        /// <returns></returns>
        public static TileType[,] GetTileTypes()
        {
            if (_tilePositions == null)
                LoadTilePositions();

            return _tilePositions;
        }

        /// <summary>
        /// Parse the input file for the locations of the special tiles on the board such as triple word, double letter etc.
        /// </summary>
        private static void LoadTilePositions()
        {
            _tilePositions = new TileType[15, 15];
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Resources\initial_board.txt");

            int row = 0;
            int col = 0;
            foreach (var w in File.ReadAllLines(path))
            {
                foreach (var tp in w.Trim().Split(','))
                {
                    if (string.IsNullOrEmpty(tp))
                        continue;

                    switch (tp.Trim())
                    {
                        case "RE":
                            _tilePositions[row, col] = TileType.Regular;
                            break;
                        case "CE":
                            _tilePositions[row, col] = TileType.Center;
                            break;
                        case "TW":
                            _tilePositions[row, col] = TileType.TripleWord;
                            break;
                        case "TL":
                            _tilePositions[row, col] = TileType.TripleLetter;
                            break;
                        case "DW":
                            _tilePositions[row, col] = TileType.DoubleWord;
                            break;
                        case "DL":
                            _tilePositions[row, col] = TileType.DoubleLetter;
                            break;
                        default:
                            throw new Exception($"Unknown tile type in inital_board file: {tp}");
                    }
                    col += 1;
                }

                col = 0;
                row += 1;
            }
        }

        /// <summary>
        /// How many points is a provided character worth?
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static int LetterValue(char c)
        {
            var scoreMapping = new Dictionary<char, int>()
            {
                { 'O' , 1 } ,
                { '0' , 1 } ,
                { 'o' , 1 } ,
                { 'D' , 1 } ,
                { 'C' , 1 } ,
                { 'c' , 1 } ,
                { 'T' , 1 } ,
                { 'K' , 1 } ,
                { 'S' , 1 } ,
                { 's' , 1 } ,
                { 'I' , 1 } ,
                { 'v' , 1 } ,
                { 'V' , 1 } ,
                { 'L' , 1 } ,
                { '<' , 1 } ,
                { '(' , 1 } ,
                { '>' , 1 } ,
                { ')' , 1 } ,
                { '{' , 1 } ,
                { '}' , 1 } ,
                { ']' , 1 } ,
                { '[' , 1 } ,
                { '/' , 1 } ,
                { '\\' , 1 } ,
                { '|' , 1 } ,
                { 'P' , 1 } ,
                { 'p' , 1 } ,
                { 'F' , 1 } ,
                { 'J' , 1 } ,
                { 'B' , 1 } ,
                { 'b' , 1 } ,
                { '3' , 1 } ,
                { 'd' , 1 } ,
                { 'q' , 1 } ,
                { ':' , 1 } ,
                { '=' , 1 } ,
                { 'X' , 1 } ,
                { '8' , 1 } ,
                { ';' , 1 } ,
                { '-' , 3 } ,
                { '^' , 3 } ,
                { '*' , 3 } ,
                { ' ' , 3 } ,
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

            return scoreMapping[c];
        }
    }
}
