using Scrabble.Core;
using Scrabble.Core.Log;
using Scrabble.Core.Players;
using Scrabble.Core.Stats;
using Scrabble.Core.Tile;
using Scrabble.Core.Words;
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
        public const int BOARD_WIDTH = 15;
        public const int BOARD_HEIGHT = 15;
        public const int TILE_SIZE = 48;
        public const int RACK_TILES = 7;

        public WordValidator WordValidator { get; set; }
        public StatManager StatManager { get; set; }
        public RackManager RackManager { get; set; }
        public TileManager TileManager { get; set; }
        public PlayerManager PlayerManager { get; set; }
        public GameLog Logger { get; set; }


        public ScrabbleForm()
        {
            InitializeComponent();

            this.TileManager = new TileManager { ScrabbleForm = this };
            TileManager.SetupTiles();

            this.WordValidator = new WordValidator { ScrabbleForm = this };
            this.StatManager = new StatManager();
            this.RackManager = new RackManager { ScrabbleForm = this };
            this.Logger = new GameLog(this);

            this.PlayerManager = new PlayerManager { ScrabbleForm = this };
            PlayerManager.SetupPlayers();
        }

        /// <summary>
        /// Handles playing a turn.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlay_Click(object sender, EventArgs e)
        {
            var tilesPlayed = PlayerManager.CurrentPlayer.Tiles.Where(r => string.IsNullOrEmpty(r.Text)).Count();
            if (tilesPlayed == 0)
            {
                MessageBox.Show("You must place some tiles on the board and make a word to play. You can pass if cannot make a word.");
                return;
            }

            var checkTilePositions = TileManager.ValidateTilePositions();
            var moveResult = WordValidator.ValidateAllWordsInPlay();

            if (checkTilePositions && moveResult.Valid)
            {
                TileManager.ResetTilesInPlay();
                RackManager.FillRack(PlayerManager.CurrentPlayer.Tiles);

                StatManager.Moves += 1;
                moveResult.Words.ForEach(w => Logger.LogMessage($"{PlayerManager.CurrentPlayer.Name} played {w.Text} for {w.Score} points"));
                Logger.LogMessage($"Turn ended - total score: {moveResult.TotalScore}");
                PlayerManager.CurrentPlayer.Score += moveResult.TotalScore;
                PlayerManager.SwapCurrentPlayer();
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
                TileManager.ResetTilesOnBoardFromTurn();
                StatManager.Passes += 1;
                Logger.LogMessage($"Turned ended - {PlayerManager.CurrentPlayer.Name} passed their turn.");
                PlayerManager.SwapCurrentPlayer();
            }
        }

        /// <summary>
        /// Handles allowing the user to swap their tiles.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSwap_Click(object sender, EventArgs e)
        {
            TileManager.ResetTilesOnBoardFromTurn();

            var verification = MessageBox.Show("Do you really want to swap the tiles you have selected?", "Swap your letters", MessageBoxButtons.YesNo);
            if (verification == DialogResult.Yes)
            {

                var tiles = PlayerManager.CurrentPlayer.Tiles.Where(c => c.LetterSelected).ToList();

                // Trying to swap no letters at all.
                if (tiles.Count == 0)
                {
                    MessageBox.Show("You must select at least one letter from your rack to swap.");
                    return;
                }

                // Trying to swap more letters than are left in the bag
                if (tiles.Count > TileManager.TileBag.LetterCountRemaining())
                {
                    MessageBox.Show($"You can only swap {TileManager.TileBag.LetterCountRemaining()} letter(s) or less.");
                    return;
                }
                

                tiles.ForEach(t => {
                    TileManager.TileBag.GiveLetter(t.Text[0]);
                    t.ClearDisplay();
                });

                RackManager.FillRack(PlayerManager.CurrentPlayer.Tiles);

                StatManager.Swaps += 1;
                Logger.LogMessage($"Turn ended - {PlayerManager.CurrentPlayer.Name} swapped {tiles.Count} tile(s).");

                PlayerManager.SwapCurrentPlayer();
            }
        }
    }
}

