using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

using Tesseract;


namespace SudokuSolver
{

    class Initialiser
    //This is the class that generates the gameboard and sets up
    // the initial values
    {
        public Initialiser(sudokuVariables gameboard)
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
                }
            }

            var image = ImageRetrieval();
            OCR(image, gameboard);

        }

        private Pix ImageRetrieval()
        {
            //Future usage, taking screenshot and finding sudoku board
            /*
            int screenWidth = 1920;
            int screenHeight = 1080;
            Bitmap bmpScreenShot = new Bitmap(screenWidth, screenHeight);
            Graphics gfx = Graphics.FromImage((Image)bmpScreenShot);
            gfx.CopyFromScreen(0, 0, 0, 0, new Size(screenWidth, screenHeight));
            Pix image = PixConverter.ToPix(bmpScreenShot);
            */
            Pix image = Pix.LoadFromFile("Images/Sudoku1.png");
            return image;
        }
        private void OCR(Pix image, sudokuVariables gameboard)
        {
            // Todo: these are only accurate for testing images
            int boxSize = 55;
            int offsetX = 0;
            int offsetY = 0;

            Rect region;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default);

                    region = Rect.FromCoords(j * boxSize, i * boxSize, (j + 1) * boxSize, (i + 1) * boxSize);
                    var engineOutput = engine.Process(image);
                    gameboard.sudokuBoard[i, j] = int.Parse(engineOutput.GetText());
                    Console.Write(engineOutput.GetText());
                }
                Console.Write("\n");
            }
        }
    }
}
