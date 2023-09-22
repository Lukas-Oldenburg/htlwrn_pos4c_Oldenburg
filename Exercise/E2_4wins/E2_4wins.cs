using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FourWins;

public class Program
{
    private static int numRows = 6;
    private static int numCols = 7;
    private static int activeRow, activeCol;
    private static int rounds;
    private static int playerNr = 1;
    public static int Main(string[] args)
    {
        //Erstellen des Arrays

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
        //Ende von Array erstellen
        string eingabe;
        int winnerPlayer;
        int addOnColumn;
        PrintGameField(game);
        while (!IsGameEnd(game, out winnerPlayer))
        {
            Console.WriteLine();
            Console.WriteLine($"Player {playerNr}s turn!");
            Console.Write("Choose a column: ");
            eingabe = Console.ReadLine();
            if (Int32.TryParse(eingabe, out addOnColumn))
            {
                if ((addOnColumn <= game.GetLength(1) && addOnColumn >= 1) && ((game[0, addOnColumn - 1]) == 0))
                { 
                    if (playerNr == 1)
                    {
                        AddPlayerDisc(game, playerNr, addOnColumn);
                        PrintGameField(game);
                        playerNr = 2;
                    }

                    else if (playerNr == 2)
                    {
                        AddPlayerDisc(game, playerNr, addOnColumn);
                        PrintGameField(game);
                        playerNr = 1;
                    }
                }
            }
        }
        if(winnerPlayer == 1 || winnerPlayer == 2)
            Console.Write($"Winner is: Player {winnerPlayer}");
        return 0;    
    }

    /// <summary>
    /// Prints the game field on the console.
    /// </summary>
    /// <remarks>
    /// Walls are blue or other chars, player one is red and/or 'x' , player two is yellow and/or 'o'.
    /// </remarks>
    /// <param name="field">The field.</param>
    private static void PrintGameField(int[,] field)
    {
        Console.Clear();
        for (int rowLV = 0; rowLV < numRows; rowLV++)
        {
            //alle rows durchgehen, beginnend mit der ersten blauen zeile
            if(rowLV == 0)
            {
                //erste blaue zeile
                for (int ersteZeileLV = 0; ersteZeileLV < numCols; ersteZeileLV++)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Write("  ");
                    Console.Write("  ");
                    Console.ResetColor();
                }
                //letztes feld der ersten zeile
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.Write("  ");
                Console.ResetColor();
            }
            //absatz: beginn der nächsten zeile
            Console.WriteLine();
            //zeile zeichnen immer "1 feld blau" und "1 feld spielbar"
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
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write("  ");
            Console.ResetColor();
            Console.WriteLine();
            //zeile zeichnen die ganz blau ist
            for (int colLV = 0; colLV < numCols; colLV++)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.Write("  ");
                Console.Write("  ");
                Console.ResetColor();
            }
            // unterstes rechtes feld
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
        rounds++;
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
        //Winner prüfen senkrecht
        if (field[activeRow, activeCol] == playerNr &&
            field[activeRow+1, activeCol] == playerNr &&
            field[activeRow+2, activeCol] == playerNr &&
            field[activeRow+3, activeCol] == playerNr)
        {
            winnerPlayer = playerNr;
            return true;
        }

        //Volles Feld
        if (rounds == numCols * numRows)
        {
            winnerPlayer = 0;
            Console.WriteLine();
            Console.WriteLine("Nobody won. Try again");
            return true;
        }





        winnerPlayer = 1;
            
        return false; 
    }
}
