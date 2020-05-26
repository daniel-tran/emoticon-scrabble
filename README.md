# Emoticon Scrabble
The game of Scrabble built as a Windows Forms app in C#, but played using (generalised) Emoticons instead of regular words.
This implementation is largely taken from [https://github.com/Package/Scrabble](https://github.com/Package/Scrabble) and tweaked to support characters outside of the regular Scrabble letters.

This project was largely inspired from [one of Doug Savage's sticky note cartoons from 2013](https://www.savagechickens.com/2013/10/scrabble-2.html) and exists more as a proof-of-concept for such an idea.

## How to play
### Placing and removing a tile
Place a tile on the board by first clicking on the tile in your rack, and clicking on a position on the board. You can click on multiple tiles in your rack, allowing them to be placed onto multiple board locations in the same order they were selected in.
Tiles will automatically highlight as green to indicate that a valid emoticon has been formed, and red to show that an invalid emoticon has been formed and the play cannot be made until it is resolved.

To remove a tile from board, click on it and it will be returned back to the rack. This can only be done for tiles that were placed by the player on the current turn.

### Earning points and Win Conditions
After placing a valid emoticon on the board, click Play to confirm the emoticon. The player will earn points as indicated on the Play button, and the next player's turn starts.

The game ends when either all tiles have been exhausted from the bag, or after four consecutive passes.

### Swapping and Passing
To swap out tiles, select the tiles in your rack and click Swap. This ends your turn and refreshes the selected tiles in your rack with new tiles from the bag.

Clicking on Pass will end your turn after confirming the pop-up prompt.

### In-game Help
Click on Hint to show what Emoticons you can form using tiles in the current player's rack.

You can also view the remaining letters and their remaining quantities in the bag by clicking on Letters Remaining.

### Closing the game
Click on the Exit button to close the game.

## Running the game from source
Open `Scrabble.sln` in Visual Studio (or any other editor that supports C# solution files) and build in either Debug or Release.
This should create a bunch of resources under `Scrabble\bin\Debug` or `Scrabble\bin\Release` where you can manually run Scrabble.exe.

## Known Limitations
These have largely carried over from the original source code at https://github.com/Package/Scrabble and have no expected timeline for implementation, as this particular project was not primarily intended to be a cohesive Scrabble engine:

- **No functionality to play against a computer opponent.**
- **No functionality for network or LAN play.** Local multiplayer using the same machine only.
- **No functionality to change player names during runtime.**
- **No wildcard tiles.** The blank tile is a literal whitespace character, and is treated as a normal tile.
- **No timer during a player's turn.**
- **No Unicode characters.** This is probably doable by following the steps under "Advanced: Using custom tiles" although it is outside the current scope of this project.
- **Cannot minimize the game window.** Current workaround is to open Task Manager > Scrabble > expand the processes > right click > Minimize.
Note that maximizing is possible by double-clicking on the game form.

## Advanced: Using custom words
*Looking to play Scrabble using your own set of words?*

If you intend on using the standard Scrabble tiles, the easiest approach is to clone the source repository at [https://github.com/Package/Scrabble](https://github.com/Package/Scrabble) and modify the `Scrabble\Resources\valid_words.txt` file and rebuild.

Otherwise, follow the steps in the next section.

## Advanced: Using custom tiles
To add your own tiles (and words using your custom tiles) to this Scrabble implementation, you need to modify the following files:

 1. `Scrabble\Resources\valid_words.txt`: This file contains all valid words in Scrabble.
 2. `Scrabble\Core\Words\WordScorer.cs`: There is a method called `LetterValue()` which contains a mapping of each letter to the number of points scored when said letter is used in a valid word.
 3. `Scrabble\Core\Tile\TileBag.cs`: This file contains multiple areas that require modification -
The `LetterCount()` method sets the distribution of each tile in the game.
The `GetLetterArray()` method contains a character array which is used as the central source of truth whenever other classes need to get the list of all tile values.
