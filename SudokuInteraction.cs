using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver
{
    class SudokuInteraction
        //This class is designed to be an easy access point to the 
        // sudoku board in ways that follow the rules of the game.
    {
        private sudokuVars gameboard;

        public SudokuInteraction(sudokuVars gameboard)
        {
            this.gameboard = gameboard;
        }
    }
}
