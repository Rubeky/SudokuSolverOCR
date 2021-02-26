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

        private void fillNumber(int x, int y, int num)
            //Fills number given into sudoku grid
        {
            //Filling value
            gameboard.sudokuBoard[x,y] = num;

            //Clearing guesses
            gameboard.viableNumbers[x, y, num] = false;
        }

        private bool[] sameBox(int x, int y)
            //Returns list of what numbers are already in the same box
        {
            var output = new bool[9];

            //Loops through values in the same box
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    output[gameboard.sudokuBoard[i + 3*x,j + 3*y]] = true;
                }
            }

            return output;
        }

        private bool[] sameLine(int x, int y)
            //Returns list of values in horizontal or vertical lines
        {
            var output = new bool[9];

            //Checking values in same vertical or horizontal box
            for(int i = 0; i < 9; i++)
            {
                output[gameboard.sudokuBoard[i, y]] = true;
                output[gameboard.sudokuBoard[x, i]] = true;
            }

            return output;
        }

        private bool autofill(int x, int y)
            //Returns true if number has been filled
        {
            //If location is outside of board
            if(x > 8 || y > 8)
            {
                return false;
            }

            //If board is already filled at location
            if(gameboard.sudokuBoard[x,y] != 0)
            {
                return false;
            }

            int numAvailable = 9;

            //Checking what numbers cannot be used, true = used
            var line = sameLine(x, y);
            var boxes = sameBox(x / 3, y / 3);

            //Combining the 3 vars above
            for(int i = 0; i < 9; i++)
            {
                if (line[i] || boxes[i])
                {
                    //Sets to false, used numbers are not viable numbers
                    gameboard.viableNumbers[x ,y ,i] = false;
                    numAvailable--;
                }
            }

            //If only 1 number is actually able to be filled
            if(numAvailable == 1)
            {
                for(int i = 0; i < 9; i++)
                {
                    if (!gameboard.viableNumbers[x ,y ,i])
                    {
                        fillNumber(x, y, i);
                    }
                }
                return true;
            }

            return false;
        }
    }
}
