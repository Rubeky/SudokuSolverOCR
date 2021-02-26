using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver
{
    class SudokuSolverLogic
        //This class is meant to house all the checking logic for
        // solving the game.
    {
        private object board;

        public SudokuSolverLogic(sudokuVars gameboard)
        {
            this.board = new SudokuInteraction(gameboard);
        }
        public bool isComplete()
        {
            return false;
        }
    }
}
