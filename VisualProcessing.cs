using System;
using System.Drawing;

namespace SudokuSolver
{
    class VisualProcessing
    {
        private sudokuVars gameboard;
        public VisualProcessing(Color[,] image, sudokuVars gameboard)
        {
            this.gameboard = gameboard;
            image = cropImage(image);
            if(image.GetLength(0) > 100 && image.GetLength(1) > 100)
            {
                imageSplit(image, gameboard);
            }
            else
            {
                Console.WriteLine("Error: The sudoku board cannot be found.");
            }
            
        }

        private Color[,] cropImage(Color[,] image)
        {
            //Colour of the sudoku grid on Sudoku.com
            Color edgePixels = Color.FromArgb(255, 52, 72, 97);

            // Points to be filled with locations of sudoku
            Point topLeft = new Point();
            Point bottomRight = new Point();
            bool topLeftFound = false;

            //Work on Sudoku board detection?
            for(int i = 100; i < image.GetLength(0) - 100; i++)
            {
                for(int j = 100; j < image.GetLength(1) - 100; j++)
                {
                    if(image[i,j] == edgePixels && image[i + 100, j] == edgePixels && image[i, j + 100] == edgePixels 
                        && image[i - 1, j - 1] != edgePixels && image[i + 1, j + 1] == edgePixels)
                    {
                        if (!topLeftFound)
                        {
                            topLeft.X = i;
                            topLeft.Y = j;
                            topLeftFound = true;
                        }
                    }

                    if (image[i, j] == edgePixels && image[i - 100, j] == edgePixels && image[i, j - 100] == edgePixels 
                        && image[i + 1, j + 1] != edgePixels && image[i - 1, j - 1] == edgePixels)
                    {
                        bottomRight.X = i;
                        bottomRight.Y = j;
                    }

                }
            }

            //Making output correct size of sudoku
            Point sudokuSize = new Point();
            sudokuSize.X = bottomRight.X - topLeft.X;
            sudokuSize.Y = bottomRight.Y - topLeft.Y;

            Color[,] output = new Color[sudokuSize.X, sudokuSize.Y];

            for(int i = 0; i < sudokuSize.X; i++)
            {
                for(int j = 0; j < sudokuSize.Y; j++)
                {
                    output[i, j] = image[i + topLeft.X, j + topLeft.Y];
                }
            }

            if(output.GetLength(0) != output.GetLength(1))
            {
                Console.WriteLine("Error: The sudoku box does not seem to be square, is this right?");
            }

            return output;
        }

        //Todo
        private bool[,,,] imageSplit(Color[,] image, sudokuVars gameboard)
        {
            Color white = Color.FromArgb(255, 255, 255, 255);

            //To allow for difference in proportions if necessary
            int boxSizeX = image.GetLength(0) / 9;
            int boxSizeY = image.GetLength(1) / 9;

            int numBlack = 0;

            bool[,,,] output = new bool[9, 9, boxSizeX - 10, boxSizeY - 10];

            //Goes through the boxes in vertical lines, starting at top left
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    for (int x = 0; x < boxSizeX - 10; x++)
                    {
                        for (int y = 0; y < boxSizeY - 10; y++)
                        {
                            if (image[x + boxSizeX * i + 5, y + boxSizeY * j + 5] != white)
                            {
                                output[i, j, x, y] = true;
                                numBlack++;
                            }
                            else
                            {
                                output[i, j, x, y] = false;
                            }
                        }
                    }
                    Console.WriteLine("Number of black pixels for square " + i.ToString() + " " + j.ToString() + " = " + numBlack.ToString());
                    numBlack = 0;
                }
            }

            return output;
        }

        public sudokuVars getArray()
        {
            return gameboard;
        }
    }
}
