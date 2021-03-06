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
                    if(gameboard.sudokuBoard[i + 3 * x, j + 3 * y] != 0)
                        output[gameboard.sudokuBoard[i + 3*x,j + 3*y] - 1] = true;
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
                if(gameboard.sudokuBoard[i, y] != 0)
                    output[gameboard.sudokuBoard[i, y] - 1] = true;

                if (gameboard.sudokuBoard[x, i] != 0)
                        output[gameboard.sudokuBoard[x, i] - 1] = true;
            }

            return output;
        }

        private bool[] linesFromBoxX(int y)
            //Returns list of numbers that aren't ruled out by possible values in adjacent boxes
        {
            bool[] output = new bool[9];
            bool numberInLine;
            int boxY = y / 3;

            for(int number = 0; number < 9; number++)
            {
                numberInLine = true;

                //All x indices
                for (int x = 0; x < 9; x++)
                {
                    //All lines that aren't the selected one
                    for (int i = 0; i < 3; i++)
                    {
                        if (i + boxY * 3 != y)
                        {
                            //If the number is found, it cannot only be in line
                            if(gameboard.viableNumbers[x, i + boxY * 3, number])
                            {
                                numberInLine = false;
                            }
                        }
                    }
                }

                if (numberInLine)
                {
                    output[number] = true;
                }
            }

            return output;
        }

        private bool[] linesFromBoxY(int x)
        {
            bool[] output = new bool[9];
            bool numberInLine;
            int boxX = x / 3;

            for (int number = 0; number < 9; number++)
            {
                numberInLine = true;

                //All y indices
                for (int y = 0; y < 9; y++)
                {
                    //All lines that aren't the selected one
                    for (int i = 0; i < 3; i++)
                    {
                        if (i + boxX * 3 != x)
                        {
                            //If the number is found, it cannot only be in line
                            if (gameboard.viableNumbers[i + boxX * 3, y, number])
                            {
                                numberInLine = false;
                            }
                        }
                    }
                }

                if (numberInLine)
                {
                    output[number] = true;
                }
            }

            return output;
        }


        public bool autofill(int x, int y)
            //Returns true if number has been filled
        {

            //If location is outside of board
            if(x > 8 || y > 8)
            {
                return false;
            }

            //If board is already filled at location
            if(this.gameboard.sudokuBoard[x,y] != 0)
            {
                return false;
            }

            int numAvailable = 9;

            //Checking what numbers cannot be used, true = used
            var line = sameLine(x, y);
            var boxes = sameBox(x / 3, y / 3);
            //Checks what numbers aren't possible based on pencilmarks
            var linesX = linesFromBoxX(y);
            var linesY = linesFromBoxY(x);


            //Combining the 2 vars above
            for (int i = 0; i < 9; i++)
            {
                if (line[i] || boxes[i] || linesX[i] || linesY[i])
                {
                    //Sets to false, used numbers are not viable numbers
                    gameboard.viableNumbers[x ,y ,i] = false;
                    numAvailable--;
                }
            }

            //If only 1 number is actually able to be filled, and number isn't already filled
            if (numAvailable == 1 && gameboard.sudokuBoard[x,y] == 0)
            {
                for(int i = 0; i < 9; i++)
                {
                    if (gameboard.viableNumbers[x ,y ,i])
                    {
                        fillNumber(x, y, i + 1);
                    }
                }
                return true;
            }

            return false;
        }
    }
}
