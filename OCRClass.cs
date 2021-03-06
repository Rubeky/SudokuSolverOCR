using System;
using System.Drawing;

namespace SudokuSolver
{
    internal class OCRClass
    {
        public static int convertImage(bool[,] image)
        {
            Point[] edges = findEdges(image);

            //Left, Right, Top, Bottom
            double[] percentSides = getSidePercentages(image, edges);

            //Right, Top
            double[] skewedness = getSkewedness(image, edges);
            

            

            return 0;
        }

        public static Point[] findEdges(bool[,] image)
            //Point[0] = upperLeft
            //Point[1] = lowerRight
        {
            Point[] output = new Point[2];
            Point upperLeft = new Point(0, 0);
            Point lowerRight = new Point(0, 0);
            bool edgeFound = false;
            int edgeIndex = 0;

            //Finding leftmost edge
            while (!edgeFound)
            {
                for (int y = 0; y < image.GetLength(1); y++)
                {
                    if (image[edgeIndex, y] == true)
                    {
                        edgeFound = true;
                        upperLeft.X = edgeIndex;
                    }

                    if (edgeIndex == image.GetLength(0) - 1)
                    {
                        edgeFound = true;
                        upperLeft.X = -1;
                    }
                }

                edgeIndex++;
            }
            edgeIndex = 0;
            edgeFound = false;

            //Finding topmost edge
            while (!edgeFound)
            {
                for (int x = 0; x < image.GetLength(0); x++)
                {
                    if (image[x, edgeIndex] == true)
                    {
                        edgeFound = true;
                        upperLeft.Y = edgeIndex;
                    }

                    if (edgeIndex == image.GetLength(1) - 1)
                    {
                        edgeFound = true;
                        upperLeft.Y = -1;
                    }
                }

                edgeIndex++;
            }
            edgeIndex = image.GetLength(0) - 1;
            edgeFound = false;

            //Finding rightmost edge
            while (!edgeFound)
            {
                for (int y = image.GetLength(1) - 1; y >= 0; y--)
                {
                    if (image[edgeIndex, y] == true)
                    {
                        edgeFound = true;
                        lowerRight.X = edgeIndex;
                    }

                    if (edgeIndex == 0)
                    {
                        edgeFound = true;
                        lowerRight.X = -1;
                    }
                }

                edgeIndex--;
            }
            edgeIndex = image.GetLength(1) - 1;
            edgeFound = false;

            //Finding rightmost edge
            while (!edgeFound)
            {
                for (int x = image.GetLength(0) - 1; x >= 0; x--)
                {
                    if (image[x, edgeIndex] == true)
                    {
                        edgeFound = true;
                        lowerRight.Y = edgeIndex;
                    }

                    if (edgeIndex == 0)
                    {
                        edgeFound = true;
                        lowerRight.Y = -1;
                    }
                }

                edgeIndex--;
            }

            //Formatting output
            output[0] = upperLeft;
            output[1] = lowerRight;
            return output;
        }

        public static double[] getSidePercentages(bool[,] image, Point[] edges)
            //double[0] = left
            //double[1] = right
            //double[2] = top
            //double[3] = bottom
        {
            double[] output = new double[4];
            return output;
        }

        public static double[] getSkewedness(bool[,] image, Point[] edges)
            //double[0] = right
            //double[1] = top
        {
            double[] output = new double[2];
            return output;
        }
    }
}