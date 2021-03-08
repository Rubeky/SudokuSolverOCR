using System;
using System.Drawing;

namespace SudokuSolver
{
    class VisualProcessing
        //This class houses the logic needed to convert the image of the board into the sudokuVars object.
    {
        private sudokuVars gameboard;

        public VisualProcessing(sudokuVars gameboard)
        {
            this.gameboard = gameboard;

            var image = ImageRetrieval();
            image = cropImage(image);

            //Checking that image is actually correct size
            if(image.GetLength(0) > 100 && image.GetLength(1) > 100)
            {
                var blockImages = imageSplit(image, gameboard);

                bool[,] singleImage = new bool[blockImages.GetLength(2), blockImages.GetLength(3)];

                //Goes through block of images and gets all "images" turned into the corresponding number value
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        for (int x = 0; x < blockImages.GetLength(2); x++)
                        {
                            for (int y = 0; y < blockImages.GetLength(3); y++)
                            {
                                singleImage[x, y] = blockImages[i, j, x, y];
                            }
                        }
                        this.gameboard.sudokuBoard[i,j] = OCRClass.convertImage(singleImage);
                    }
                }
            }
            else
            {
                Console.WriteLine("Error: The sudoku board cannot be found.");
            }
            
        }

        private Color[,] ImageRetrieval()
        {

            //Future usage, taking screenshot and finding sudoku board
            // find out how to get screen dimensions
            int screenWidth = 1920;
            int screenHeight = 1080;
            Bitmap bmpScreenShot = new Bitmap(screenWidth, screenHeight);
            //Bitmap bmpScreenShot = new Bitmap("Images/EdgePiece.png");      //Testing purposes

            Graphics gfx = Graphics.FromImage(bmpScreenShot);
            gfx.CopyFromScreen(0, 0, 0, 0, new Size(screenWidth, screenHeight));

            // Saving the screenshot as a single pixel array
            Color[,] image = new Color[screenWidth, screenHeight];
            var colour = new Color();

            for (int i = 0; i < screenWidth; i++)
            {
                for (int j = 0; j < screenHeight; j++)
                {
                    colour = bmpScreenShot.GetPixel(i, j);
                    image[i, j] = colour;
                }
            }
            return image;
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
                            gameboard.sudokuFirstBoxLocation[0] = i;
                            gameboard.sudokuFirstBoxLocation[1] = j;

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

            //Setting location of first box so that we can click it later
            this.gameboard.sudokuFirstBoxLocation[0] = topLeft.X;
            this.gameboard.sudokuFirstBoxLocation[1] = topLeft.Y;


            return output;
        }

        //Todo
        private bool[,,,] imageSplit(Color[,] image, sudokuVars gameboard)
        {
            Color white = Color.FromArgb(255, 255, 255, 255);

            //To allow for difference in proportions if necessary
            int boxSizeX = image.GetLength(0) / 9;
            int boxSizeY = image.GetLength(1) / 9;

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
                            }
                        }
                    }
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
