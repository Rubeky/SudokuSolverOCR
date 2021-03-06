using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver
{
    class SudokuSolverLogic
        //This class is meant to house all the checking logic for
        // solving the game.
    {
        private SudokuInteraction board;

        public SudokuSolverLogic(sudokuVars gameboard)
        {
            this.board = new SudokuInteraction(gameboard);
        }

        public void solve()
        {
            bool unsolvable = false;
            //Goes until
            while (!unsolvable)
            {
                unsolvable = true;

                //Loops through all boxes to check if they're filled
                for(int i = 0; i < 9; i++)
                {
                    for(int j = 0; j < 9; j++)
                    {
                        if(this.board.autofill(i, j))
                        {
                            unsolvable = false;
                        }

                    }
                }
            }
        }

        public bool isComplete()
        {
            return false;
        }
    }
}
