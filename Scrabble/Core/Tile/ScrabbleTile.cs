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
                case TileType.TripleWord:
                    this.BackColor = Color.Orange;
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

        public void SetTileType()
        {
            // Todo: don't hardcode these in, need a smarter way to be able to define them

            if (XLoc == 7 && YLoc == 7)
                this.TileType = TileType.Center;

            else if ((XLoc == 0 || XLoc == 14) && (YLoc == 3 || YLoc == 11))
                this.TileType = TileType.TripleWord;

            else if((YLoc == 0 || YLoc == 14) && (XLoc == 3 || XLoc == 11))
                this.TileType = TileType.TripleWord;

            else
                this.TileType = TileType.Regular;
        }
    }
}
