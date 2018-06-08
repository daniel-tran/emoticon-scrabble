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

        public void OnLetterPlaced(string letter)
        {
            this.Text = letter;
            this.BackColor = Color.Goldenrod;
            this.BackColor = Color.Yellow;
            this.TileInPlay = true;
        }

        public void OnLetterRemoved()
        {
            this.Text = string.Empty;
            this.BackColor = SystemColors.ButtonFace;
            this.TileInPlay = false;
        }

        public string GetLocation()
        {
            return string.Format("{0}, {1}", XLoc, YLoc);
        }
    }
}
