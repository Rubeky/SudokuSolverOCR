using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Drawing;
using System.Threading;

namespace SudokuSolver
{
    class WriterToScreen
    //This class inputs the values of the sudoku into the board itself.
    {
        public WriterToScreen(sudokuVars gameboard)
        {
            //Move mouse to correct location
            moveMouseAndClick(gameboard);

            //Loop to fill in all inputs into Sudoku.com
            Keyboard keyboard = new Keyboard();

            for(int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    //Filling value then going to next box
                    fillValue(keyboard, gameboard.sudokuBoard[j, i]);
                    Thread.Sleep(10);
                    keyboard.Send(Keyboard.ScanCodeShort.RIGHT);
                    Thread.Sleep(10);
                }

                //Setting to next row
                keyboard.Send(Keyboard.ScanCodeShort.DOWN);
                Thread.Sleep(10);
                for (int j = 0; j < 9; j++)
                {
                    keyboard.Send(Keyboard.ScanCodeShort.LEFT);
                    Thread.Sleep(10);
                }
            }

            //Print congratulations!
        }

        private void moveMouseAndClick(sudokuVars gameboard)
        {
            int X = gameboard.sudokuFirstBoxLocation[0] + 5;
            int Y = gameboard.sudokuFirstBoxLocation[1] + 5;
            SetCursorPos(X, Y);

            mouse_event(MOUSEEVENTF_LEFTDOWN, 50, 50, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 50, 50, 0, 0);
        }

        private void fillValue(Keyboard keyboard, int value)
        {
            switch (value)
            {
                case 1:
                    keyboard.Send(Keyboard.ScanCodeShort.KEY_1);
                    break;
                case 2:
                    keyboard.Send(Keyboard.ScanCodeShort.KEY_2);
                    break;
                case 3:
                    keyboard.Send(Keyboard.ScanCodeShort.KEY_3);
                    break;
                case 4:
                    keyboard.Send(Keyboard.ScanCodeShort.KEY_4);
                    break;
                case 5:
                    keyboard.Send(Keyboard.ScanCodeShort.KEY_5);
                    break;
                case 6:
                    keyboard.Send(Keyboard.ScanCodeShort.KEY_6);
                    break;
                case 7:
                    keyboard.Send(Keyboard.ScanCodeShort.KEY_7);
                    break;
                case 8:
                    keyboard.Send(Keyboard.ScanCodeShort.KEY_8);
                    break;
                case 9:
                    keyboard.Send(Keyboard.ScanCodeShort.KEY_9);
                    break;
            }   
            
        }


        //Nitty gritty of User32 API
        //Setting cursor position
        [DllImport("User32.Dll")]
        public static extern long SetCursorPos(int x, int y);

        //Clicking left button setup
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, int dwExtraInfo);
        private const uint MOUSEEVENTF_LEFTDOWN = 0x02;
        private const uint MOUSEEVENTF_LEFTUP = 0x04;
    }
}
