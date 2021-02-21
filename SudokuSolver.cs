using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver
{
    class SudokuSolver
    {
        private sudokuVars gameboard;

        public SudokuSolver(sudokuVars gameboard)
        {
            this.gameboard = gameboard;
        }
        public bool isComplete()
        {
            return false;
        }
    }
}
