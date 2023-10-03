using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FourWins;

public class Program
{
    private static int activeRow, activeCol;
    public static int Main(string[] args)
    {
        //using commandline arguments to determine the fields measurements in following format: "9x9"
        //standart size will be 6x7 if the arguments are unparseable or if they are below 4 (minimum of 4x4 to even win)
        int numRows = 6;
        int numCols = 7;
        string? eingabe;
        int winnerPlayer;
        int addOnColumn;
        int playerNr = 1;

        if (args.Length != 0)
        {
            string[] rc = args[0].Split('x');
            if (!int.TryParse(rc[0], out numRows))
            {
                numRows = 6;
            }
            if (!int.TryParse(rc[1], out numCols))
            {
                numCols = 7;
            }
        }

        if (numCols < 4 || numRows < 4)
        {
            numRows = 6;
            numCols = 7;
        }
        int[,] game = new int[numRows, numCols];

        PrintGameField(game, numRows, numCols);

        //game loop
        while (!IsGameEnd(game, out winnerPlayer))
        {
            Console.WriteLine();
            Console.WriteLine($"Player {playerNr}s turn!");
            Console.Write("Choose a column: ");
            eingabe = Console.ReadLine();
            //validating input
            if (Int32.TryParse(eingabe, out addOnColumn))
            {
                //validating column position (must exist aka: be between 1 and the number of columns) and stops input when a column is full 
                if ((addOnColumn <= game.GetLength(1) && addOnColumn >= 1) && ((game[0, addOnColumn - 1]) == 0))
                { 
                    if (playerNr == 1)
                    {
                        AddPlayerDisc(game, playerNr, addOnColumn);
                        playerNr = 2;
                    }

                    else if (playerNr == 2)
                    {
                        AddPlayerDisc(game, playerNr, addOnColumn);
                        playerNr = 1;
                    }
                    PrintGameField(game, numRows, numCols);
                }
            }
        }
        //determine winner
        if (winnerPlayer == 1 || winnerPlayer == 2)
        {
            Console.WriteLine();
            Console.WriteLine($"Winner is: Player {winnerPlayer}");
        }
        return 0;    
    }

    /// <summary>
    /// Prints the game field on the console.
    /// </summary>
    /// <remarks>
    /// Walls are blue or other chars, player one is red and/or 'x' , player two is yellow and/or 'o'.
    /// </remarks>
    /// <param name="field">The field.</param>
    private static void PrintGameField(int[,] field, int numCols, int numRows)
    {
        Console.Clear();
        //going through all the rows
        for (int rowLV = 0; rowLV < numRows; rowLV++)
        {
            //first row which is fully blue, that comes before the array itself 
            if (rowLV == 0)
            {
                for (int ersteZeileLV = 0; ersteZeileLV < numCols; ersteZeileLV++)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Write("  ");
                    Console.Write("  ");
                    Console.ResetColor();
                }
                //last field of the first fully blue line
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.Write("  ");
                Console.ResetColor();
            }
            Console.WriteLine();
            //here begins the part where the gamefield is printed in a given format: 
            //always a blue (field = "__") and a colored / empty field depending on the arrays content
            for (int colLV = 0; colLV < numCols; colLV++)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.Write("  ");
                switch (field[rowLV, colLV])
                {
                    case 1:
                        {
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.Write("  ");
                            break;
                        }
                    case 2:
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.Write("  ");
                            break;
                        }
                    default:
                        {
                            Console.ResetColor();
                            Console.Write("  ");
                            break;
                        }
                }
                
            }
            //last field of a "mixed" line
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write("  ");
            Console.ResetColor();
            Console.WriteLine();
            //always followed by a fully blue line
            for (int colLV = 0; colLV < numCols; colLV++)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.Write("  ");
                Console.Write("  ");
                Console.ResetColor();
            }
            //last field of a fully blue line
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write("  ");
            Console.ResetColor();


        }
        Console.WriteLine();
        for (int j = 0; j < numCols; j++)
        {
            if (j < 10 && j > 0)
            {
                Console.Write($"   {j + 1}");
            }
            else
            {
                Console.Write($"  {j + 1}");
            }
        }

    }



    /// <summary>
    /// Adds the player disc to the game board on the given column.
    /// </summary>
    /// <param name="field">The playing field.</param>
    /// <param name="playerNr">The player nr.</param>
    /// <param name="addOnColumn">The column number to add the disc.</param>
    /// <remarks>
    /// Searches for the new place regarding the rules of four in a row.
    /// Note: the disc slides down. 
    /// </remarks>
    /// <returns>
    ///    <c>true</c> if the add of disc is possible; otherwise, <c>false</c>.
    /// </returns>
    private static bool AddPlayerDisc(int[,] field, int playerNr, int addOnColumn)
    {
        for (int j = field.GetLength(0) -1; j >= 0; j--)
        {
            if (field[j, addOnColumn-1] == 0)
            {
                field[j, addOnColumn-1] = playerNr;
                activeCol = addOnColumn - 1;
                activeRow = j;
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Determines if the game end is reached.
    /// Possible ends:
    /// Player 1 has four in a row
    /// Player 2 has four in a row
    /// Game field is full and no player has four in a row
    /// </summary>
    /// <param name="field">The field.</param>
    /// <param name="winnerPlayer">
    /// The winning player.
    /// 0: Nothing changed 
    /// 1: Player 1 wins
    /// 2: Player 2 wins
    /// -1: Draw - nobody won
    /// </param>
    /// <returns>
    ///   <c>true</c> if the game has ended; otherwise, <c>false</c>.
    /// </returns>
    private static bool IsGameEnd(int[,] field, out int winnerPlayer)
    {
        int rows = field.GetLength(0);
        int cols = field.GetLength(1);

        // horizontal wins
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols - 3; j++)
            {
                int player = field[i, j];
                if (player != 0 &&
                    player == field[i, j + 1] &&
                    player == field[i, j + 2] &&
                    player == field[i, j + 3])
                {
                    winnerPlayer = player;
                    return true;
                }
            }
        }

        // vertical wins
        for (int i = 0; i < rows - 3; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                int player = field[i, j];
                if (player != 0 &&
                    player == field[i + 1, j] &&
                    player == field[i + 2, j] &&
                    player == field[i + 3, j])
                {
                    winnerPlayer = player;
                    return true;
                }
            }
        }

        // right diagonal wins
        for (int i = 0; i < rows - 3; i++)
        {
            for (int j = 0; j < cols - 3; j++)
            {
                int player = field[i, j];
                if (player != 0 &&
                    player == field[i + 1, j + 1] &&
                    player == field[i + 2, j + 2] &&
                    player == field[i + 3, j + 3])
                {
                    winnerPlayer = player;
                    return true;
                }
            }
        }

        // left diagonal wins
        for (int i = 0; i < rows - 3; i++)
        {
            for (int j = 3; j < cols; j++)
            {
                int player = field[i, j];
                if (player != 0 &&
                    player == field[i + 1, j - 1] &&
                    player == field[i + 2, j - 2] &&
                    player == field[i + 3, j - 3])
                {
                    winnerPlayer = player;
                    return true;
                }
            }
        }

        // draw
        bool isFull = true;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (field[i, j] == 0)
                {
                    isFull = false;
                    break;
                }
            }
        }

        if (isFull)
        {
            winnerPlayer = 0;
            return true;
        }
        winnerPlayer = 0;
        return false;
    }
}

