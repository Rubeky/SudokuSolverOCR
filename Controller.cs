﻿using System;

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

            print(gameboard);

            if (solver.isComplete())
            {
                // Not complete, needs to interact with mouse and keyboard
                //new WriterToScreen(gameBoard);
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
                Console.WriteLine("-----------------");
            }
        }
    }
}
