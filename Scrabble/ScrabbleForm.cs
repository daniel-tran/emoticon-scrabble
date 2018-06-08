using Scrabble.Core;
using Scrabble.Core.Validator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scrabble
{
    public partial class ScrabbleForm : Form
    {
        public ScrabbleTile[,] _tiles;
        public List<RackTile> _rackTiles;
        public TileBag _tileBag;

        public const int BOARD_WIDTH = 15;
        public const int BOARD_HEIGHT = 15;
        public const int TILE_SIZE = 48;
        public const int RACK_TILES = 7;

        public WordValidator WordValidator { get; set; }

        public ScrabbleForm()
        {
            InitializeComponent();
            SetupTiles();
            SetupRack();

            this.WordValidator = new WordValidator { ScrabbleForm = this };
        }

        private void SetupTiles()
        {
            _tiles = new ScrabbleTile[BOARD_WIDTH, BOARD_HEIGHT];
            _tileBag = new TileBag();

            for (int x = 1; x <= BOARD_WIDTH; x++)
            {
                for (int y = 1; y <= BOARD_HEIGHT; y++)
                {
                    var tile = new ScrabbleTile
                    {
                        XLoc = x - 1,
                        YLoc = y - 1
                    };
                    tile.BackColor = SystemColors.ButtonFace;
                    tile.Location = new Point(x * (TILE_SIZE + 2), y * (TILE_SIZE + 2));
                    tile.Size = new Size(TILE_SIZE, TILE_SIZE);
                    tile.UseVisualStyleBackColor = false;
                    tile.Font = new Font("Verdana", 15.75F, FontStyle.Regular);
                    tile.Click += Tile_Click;
                    Controls.Add(tile);

                    _tiles[x - 1, y - 1] = tile;
                }
            }
        }

        /// <summary>
        /// Setup the rack which will contain the player's tiles.
        /// </summary>
        private void SetupRack()
        {
            _rackTiles = new List<RackTile>();

            for (int x = 1; x <= RACK_TILES; x++)
            {
                var tile = new RackTile();
                tile.BackColor = Color.Goldenrod;
                tile.Location = new Point(175 + (x * (TILE_SIZE + 2)), 825);
                tile.Size = new Size(TILE_SIZE, TILE_SIZE);
                tile.UseVisualStyleBackColor = false;
                tile.ForeColor = Color.Black;
                tile.Font = new Font("Verdana", 15.75F, FontStyle.Regular);
                tile.Click += RackTile_Click;
                Controls.Add(tile);

                _rackTiles.Add(tile);
            }

            FillRack();
        }

        /// <summary>
        /// Fills the player's rack with tiles. Will attempt to fill the rack completely,
        /// or just take as many as it can if there's not enough tiles left to completely re-fill 
        /// the rack.
        /// </summary>
        private void FillRack()
        {
            // How many letters in the rack are missing?
            int missingLetters = _rackTiles.Where(r => string.IsNullOrEmpty(r.Text)).Count();

            // Take random letters from the tile back, and fill up the rack again.
            var letters = _tileBag.TakeLetters(missingLetters);
            for (int x = 0; x < letters.Length; x++)
            {
                var tile = _rackTiles.FirstOrDefault(r => string.IsNullOrEmpty(r.Text));

                tile.Letter = letters[x];
                tile.LetterValue = _tileBag.LetterValue(letters[x]);

                tile.Text = letters[x].ToString();
            }
        }

        /// <summary>
        /// Reset the tiles which are flagged as 'in play' after a players turn.
        /// </summary>
        private void ResetTilesInPlay()
        {
            for (int x = 0; x < BOARD_WIDTH; x++)
            {
                for (int y = 0; y < BOARD_HEIGHT; y++)
                {
                    _tiles[x, y].TileInPlay = false;
                }
            }
        }

        /// <summary>
        /// Event handler for when a tile in the player's rack is clicked.
        /// Highlights the tile so you can clearly see it's been selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RackTile_Click(object sender, EventArgs e)
        {
            var tile = (RackTile) sender;
            var alreadySelected = tile.LetterSelected;

            // Reset the selected display of all tiles.
            foreach (var t in _rackTiles)
            {
                tile.OnLetterDeselected();
            }

            // If it wasn't already selected (e.g you're now de-selecting it)
            // then apply the "selected" styling to the button.
            if (!alreadySelected)
            {
                tile.OnLetterSelected();
            }
        }

        /// <summary>
        /// Handles clicking on a tile on the game board. If the player has previously selected
        /// a letter, this will place that selected letter down.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tile_Click(object sender, EventArgs e)
        {
            var tile = (ScrabbleTile) sender;
            //var mea = (MouseEventArgs) e;

            // Clicked on a tile that they have just put down so move it back to the rack.
            if (tile.TileInPlay)
            {
                foreach (var t in _rackTiles)
                {
                    if (t.Letter.ToString() == tile.Text && string.IsNullOrEmpty(t.Text))
                    {
                        // Put the tile back in the rack
                        t.Text = tile.Text;

                        // Reset the scrabble tile
                        tile.OnLetterRemoved();
                        return;
                    }
                }

                throw new Exception("Unable to replace tile in the rack!!!");
            }

            // Tile already in use, can't move there
            if (!string.IsNullOrEmpty(tile.Text))
                return;

            // All is good - handle placing the tile on th board from the rack.
            foreach (var t in _rackTiles)
            {
                if (t.LetterSelected)
                {
                    tile.OnLetterPlaced(t.Letter.ToString());

                    t.ClearDisplay();
                    break;
                }
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            var tilesPlayed = _rackTiles.Where(r => string.IsNullOrEmpty(r.Text)).Count();
            if (tilesPlayed == 0)
            {
                MessageBox.Show("You must place some tiles on the board and make a word to play. You can pass if cannot make a word.");
                return;
            }

            var moveValid = ValidateTilePositions();
            var validWord = WordValidator.ValidateAllWordsInPlay();
            var moveDirection = GetMovementDirection();

            //MessageBox.Show(moveValid ? "Tile placements are valid" : "Tile placements are not valid!!!");
            //MessageBox.Show(string.Format("Is {0} valid: {1}", GetWordInPlay(), validWord));
            //MessageBox.Show(string.Format("Move Direction: {0}", moveDirection));

            if (moveValid && validWord)
            {
                ResetTilesInPlay();
                FillRack();
            }

            // Also need to:
            // 1) Ensure a tile played is adjacent to an existing letter
            // 4) Total up the points from the move
            // 5) Move to the other person's turn
        }

        /// <summary>
        /// Return the direction the player is moving on the board with their tiles. 
        /// Either across, down or none.
        /// </summary>
        /// <returns></returns>
        private MovementDirection GetMovementDirection()
        {
            var tilesInPlay = new List<ScrabbleTile>();

            for (int x = 0; x < BOARD_WIDTH; x++)
            {
                for (int y = 0; y < BOARD_HEIGHT; y++)
                {
                    if (_tiles[x, y].TileInPlay)
                        tilesInPlay.Add(_tiles[x, y]);
                }
            }

            // No direction because less than 2 tiles have been played
            // Todo: What if you only play one tile but it does join up with something already on the board?
            //       There is a movement direction involved there...
            if (tilesInPlay.Count <= 1)
            {
                return MovementDirection.None;
            }

            int xChange = tilesInPlay[1].XLoc - tilesInPlay[0].XLoc;
            int yChange = tilesInPlay[1].YLoc - tilesInPlay[0].YLoc;

            return xChange > 0 ? MovementDirection.Across : yChange > 0 ? MovementDirection.Down : MovementDirection.None;
        }

        /// <summary>
        /// Ensure that where the tiles have been placed on the board are valid locations.
        /// </summary>
        /// <returns></returns>
        private bool ValidateTilePositions()
        {
            var tilesInPlay = new List<ScrabbleTile>();

            // Todo: need to ensure this still words when you have a gap in letters you have placed due to using existing letters on the board.
            for (int x = 0; x < BOARD_WIDTH; x++)
            {
                for (int y = 0; y < BOARD_HEIGHT; y++)
                {
                    if (_tiles[x, y].TileInPlay)
                        tilesInPlay.Add(_tiles[x, y]);
                }
            }

            // Only one tile in play so it's valid
            if (tilesInPlay.Count() <= 1)
            {
                return true;
            }

            for (int x = 1; x < tilesInPlay.Count; x++)
            {
                int xChange = tilesInPlay[x - 1].XLoc - tilesInPlay[x].XLoc;
                int yChange = tilesInPlay[x - 1].YLoc - tilesInPlay[x].YLoc;

                // Moved too much in the X direction
                if (xChange < -1 || xChange > 1)
                    return false;

                // Moved too much in the Y direction
                if (yChange < -1 || yChange > 1)
                    return false;

                // Moved in both an X and Y direction
                if (xChange != 0 && yChange != 0)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Resets any tiles that the user had attempted to put down on the board.
        /// This should be called to clean up the board before allowing the user to do actions such
        /// as swap their letters or pass their turn.
        /// </summary>
        private void ResetTilesOnBoardFromTurn()
        {
            // Reset any tiles the player had put on the board
            for (int x = 0; x < BOARD_WIDTH; x++)
            {
                for (int y = 0; y < BOARD_HEIGHT; y++)
                {
                    if (_tiles[x, y].TileInPlay)
                        _tiles[x, y].PerformClick();
                }
            }
        }

        /// <summary>
        /// Handles the event when the user wants to pass their turn.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPass_Click(object sender, EventArgs e)
        {
            var verification = MessageBox.Show("Do you really want to pass your turn?", "Pass your turn", MessageBoxButtons.YesNo);
            if (verification == DialogResult.Yes)
            {
                ResetTilesOnBoardFromTurn();
                // Todo: switch to the other player's turn.
            }
        }

        /// <summary>
        /// Handles allowing the user to swap their tiles.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSwap_Click(object sender, EventArgs e)
        {
            ResetTilesOnBoardFromTurn();

            var verification = MessageBox.Show("Do you really want to swap the tiles you have selected?", "Swap your letters", MessageBoxButtons.YesNo);
            if (verification == DialogResult.Yes)
            {

                var tiles = _rackTiles.Where(c => c.LetterSelected).ToList();

                // Trying to swap no letters at all.
                if (tiles.Count == 0)
                {
                    MessageBox.Show("You must select at least one letter from your rack to swap.");
                    return;
                }

                // Trying to swap more letters than are left in the bag
                if (tiles.Count > _tileBag.LetterCountRemaining())
                {
                    MessageBox.Show($"You can only swap {_tileBag.LetterCountRemaining()} letter(s) or less.");
                    return;
                }

                tiles.ForEach(t => {
                    _tileBag.GiveLetter(t.Text[0]);
                    t.ClearDisplay();
                });

                // Todo: find out if the rack should be re-filled before the user is assigned some more tiles?
                // Currently, with this implementation, the user could receive tiles straight back that they have just swapped.
                FillRack();

                // Todo: switch to the other player's turn.
            }
        }
    }
}
