using System;
using System.Drawing;

namespace SudokuSolver
{
    class VisualProcessing
    {
        private sudokuVars gameboard;
        public VisualProcessing(Color[,] image, sudokuVars gameboard)
        {
            image = cropImage(image);
            this.gameboard = gameboard;
        }

        private Color[,] cropImage(Color[,] image)
        {
            Color edgePixels = Color.FromArgb(255, 52, 72, 97);
            // Temporary
            Color[,] output = new Color[100,100];
            int numberOfCorners = 0;

            //Work on Sudoku board detection?
            for(int i = 100; i < image.GetLength(0) - 100; i++)
            {
                for(int j = 100; j < image.GetLength(1) - 100; j++)
                {
                    if(image[i,j] == edgePixels && image[i + 100, j] == edgePixels && image[i, j + 100] == edgePixels && image[i + 1, j + 1] == edgePixels)
                    {
                        numberOfCorners++;
                        Console.WriteLine(i.ToString() + " " + j.ToString());
                    }

                    if (image[i, j] == edgePixels && image[i - 100, j] == edgePixels && image[i, j - 100] == edgePixels && image[i - 1, j - 1] == edgePixels)
                    {
                        numberOfCorners++;
                        Console.WriteLine(i.ToString() + " " + j.ToString());
                    }

                }
            }

            //Temporary
            return output;
        }
        public sudokuVars getArray()
        {
            return gameboard;
        }
    }
}
