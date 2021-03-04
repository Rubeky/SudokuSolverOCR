using System;

struct sudokuVars
{
    public int[,] sudokuBoard;
    public bool[,,] viableNumbers;
    public int[] sudokuFirstBoxLocation;
}

namespace SudokuSolver
{
    class Controller
        //This class orchestrates all other classes.
    {
        static void Main(string[] args)
        {
            var gameBoard = new sudokuVars();

            // Not complete
            var startup = new VisualProcessing(gameBoard);

            // Not complete
            var solver = new SudokuSolverLogic(gameBoard);
            solver.solve();
            if (solver.isComplete())
            {
                // Not complete, needs to interact with mouse and keyboard
                new WriterToScreen(gameBoard);
            }
        }
    }
}
