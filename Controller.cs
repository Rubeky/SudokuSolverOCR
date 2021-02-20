using System;

struct sudokuVariables
{
    public int[,] sudokuBoard;
    public bool[,,] sudokuGuesses;
}

namespace SudokuSolver
{
    class Controller
    {
        static void Main(string[] args)
        {
            var gameBoard = new sudokuVariables();

            // Not complete
            var startup = new Initialiser(gameBoard);

            // Not complete
            var solver = new SudokuSolver(gameBoard);
            if (solver.isComplete())
            {
                // Not complete, needs to interact with mouse and keyboard
                var actioner = new WriterToScreen(gameBoard);
            }
        }
    }
}
