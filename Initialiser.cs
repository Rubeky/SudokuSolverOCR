using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;


namespace SudokuSolver
{

    class Initialiser
    //This is the class that generates the gameboard and sets up
    // the initial values

    // TODO:
    // make screen width and height not hard-coded
    {
        public Initialiser(sudokuVars gameboard)
        {
            gameboard.sudokuBoard = new int[9, 9];
            gameboard.sudokuGuesses = new bool[9, 9, 9];


            // Initialising sudokuGuesses with all values
            for (uint i = 0; i < 9; i++)
            {
                for (uint j = 0; j < 9; j++)
                {
                    for (uint k = 0; k < 9; k++)
                    {
                        gameboard.sudokuGuesses[i, j, k] = true;
                    }
                    gameboard.sudokuBoard[i, j] = 0;
                }
            }

            var image = ImageRetrieval();
            var visPros = new VisualProcessing(image);
            gameboard = visPros.getArray();

        }

        private int[,,] ImageRetrieval()
        {
            //Future usage, taking screenshot and finding sudoku board
            // find out how to get screen dimensions
            int screenWidth = 1920;
            int screenHeight = 1080;
            Bitmap bmpScreenShot = new Bitmap(screenWidth, screenHeight);
            Graphics gfx = Graphics.FromImage(bmpScreenShot);
            gfx.CopyFromScreen(0, 0, 0, 0, new Size(screenWidth, screenHeight));
            
            // No need to worry about alpha because screenshots don't utilise this
            int[,,] image = new int[screenWidth,screenHeight,3];
            var colour = new Color();

            for(int i = 1; i < screenWidth; i++)
            {
                for (int j = 1; j < screenHeight; j++)
                {
                    colour = bmpScreenShot.GetPixel(i, j);
                    image[i, j, 0] = colour.R;
                    image[i, j, 1] = colour.G;
                    image[i, j, 2] = colour.B;
                }
            }
            return image;
        }
    }
}
