using System;

struct sudokuVars
{
    public int[,] sudokuBoard;
    public bool[,,] viableNumbers;
    public int[] sudokuFirstBoxLocation;
    public void setup()
    {
        sudokuBoard = new int[9,9];
        viableNumbers = new bool[9, 9, 9];
        sudokuFirstBoxLocation = new int[2];

        sudokuFirstBoxLocation[0] = 0;
        sudokuFirstBoxLocation[1] = 0;


        for(int i = 0; i < 9; i++)
        {
            for(int j = 0; j < 9; j++)
            {
                sudokuBoard[i, j] = 0;

                for(int k = 0; k < 9; k++)
                {
                    viableNumbers[i, j, k] = true;
                }
            }
        }
    }
}

namespace SudokuSolver
{
    class Controller
        //This class orchestrates all other classes.
    {
        static void Main(string[] args)
        {
            var gameboard = new sudokuVars();
            gameboard.setup();

            // Not complete
            var startup = new VisualProcessing(gameboard);
            gameboard = startup.getArray();

            // Not complete
            var solver = new SudokuSolverLogic(gameboard);
            solver.solve();

<<<<<<< HEAD
            //print(gameboard);
            
            if (solver.isComplete(gameboard))
            {
                // Not complete, needs to interact with keyboard
                new WriterToScreen(gameboard);
=======
            new WriterToScreen(gameboard);

            print(gameboard);
            

            if (solver.isComplete())
            {
                // Not complete, needs to interact with keyboard
                
>>>>>>> 35ed00698d08a2c50ae070f7e66ac1aac9756278
            }
        }

        static void print(sudokuVars gameboard)
        {
            for(int y = 0; y < 9; y++)
            {
                for(int x = 0; x < 9; x++)
                {
                    Console.Write(gameboard.sudokuBoard[x, y].ToString() + "|");
                }
                Console.WriteLine();
                //Console.WriteLine("-----------------");
            }
        }
    }
}
