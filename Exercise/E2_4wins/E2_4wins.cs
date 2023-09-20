using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FourWins;

public class Program
{
    public static int Main(string[] args)
    {
        int numRows = 6; int numCols = 7;
        int[,] game = new int[numRows, numCols];
        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numCols; col++)
            {
                game[row, col] = 1;
            }
        }
        PrintGameField(game, numRows, numCols);
        return 0;    
    }

    /// <summary>
    /// Prints the game field on the console.
    /// </summary>
    /// <remarks>
    /// Walls are blue or other chars, player one is red and/or 'x' , player two is yellow and/or 'o'.
    /// </remarks>
    /// <param name="field">The field.</param>
    private static void PrintGameField(int[,] field, int numRows, int numCols)
    {
       


        for (int rowLV = 0; rowLV < numRows; rowLV++)
        {
            if(rowLV == 0)
            {
                for (int ersteZeileLV = 0; ersteZeileLV < numCols; ersteZeileLV++)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Write("  ");
                    Console.Write("  ");
                    Console.ResetColor();
                }
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.Write("  ");
                Console.ResetColor();
            }
            Console.WriteLine();
            for (int colLV = 0; colLV < numCols; colLV++)
            {
                 Console.BackgroundColor = ConsoleColor.Blue;
                 Console.Write("  ");
                 Console.ResetColor();
                 Console.Write("  ");
            }
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write("  ");
            Console.ResetColor();
            Console.WriteLine();
            for (int colLV = 0; colLV < numCols; colLV++)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.Write("  ");
                Console.Write("  ");
                Console.ResetColor();
            }
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write("  ");
            Console.ResetColor();
            if ()

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
        return true;
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
        winnerPlayer = 0;
        return true;  
    }
}
