using Scrabble.Core.Tile;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scrabble.Core
{
    public class ScrabbleTile : Button
    {
        public int XLoc { get; set; }
        public int YLoc { get; set; }

        public bool TileInPlay { get; set; }

        public TileType TileType { get; set; }

        public void OnLetterPlaced(string letter)
        {
            this.Text = letter;
            this.TileInPlay = true;
            SetRegularBackgroundColour();
        }

        public void OnLetterRemoved()
        {
            this.Text = string.Empty;
            this.TileInPlay = false;
            SetRegularBackgroundColour();
        }

        public string GetLocation()
        {
            return string.Format("{0}, {1}", XLoc, YLoc);
        }

        public void SetRegularBackgroundColour()
        {
            switch (this.TileType)
            {
                case TileType.Regular:
                    this.BackColor = SystemColors.ButtonFace;
                    break;
                case TileType.Center:
                    this.BackColor = Color.Purple;
                    break;
                case TileType.TripleLetter:
                    this.BackColor = Color.ForestGreen;
                    break;
                case TileType.TripleWord:
                    this.BackColor = Color.Orange;
                    break;
                case TileType.DoubleLetter:
                    this.BackColor = Color.RoyalBlue;
                    break;
                case TileType.DoubleWord:
                    this.BackColor = Color.OrangeRed;
                    break;
                default:
                    this.BackColor = SystemColors.ButtonFace;
                    break;
            }

            if (!string.IsNullOrEmpty(this.Text))
                this.BackColor = Color.Goldenrod;

            if (this.TileInPlay)
                this.BackColor = Color.Yellow;
        }
    }
}
