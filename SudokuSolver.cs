using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver
{
    class SudokuSolver
    {
        private sudokuVariables gameboard;

        public SudokuSolver(sudokuVariables gameboard)
        {
            this.gameboard = gameboard;
        }
        public bool isComplete()
        {
            return false;
        }
    }
}
