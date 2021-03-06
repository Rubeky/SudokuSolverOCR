using System;
using System.Drawing;

namespace SudokuSolver
{
    internal class OCRClass
    {
        public static int convertImage(bool[,] image)
        {
            //upperLeft, lowerRight
            Point[] edges = findEdges(image);

            if (edges[0].X == -1 || edges[0].Y == -1 || edges[1].X == -1 || edges[1].Y == -1)
                return 0;

            //Left, Right, Top, Bottom
            double[] percentSides = getSidePercentages(image, edges);

            //Left, top
            double[] skewedness = getSkewedness(image, edges);

            //List of if statements distinguishing which number each of them are
            
            if (percentSides[1] > 0.8)
                return 1;
            else if (percentSides[3] > 0.8)
                return 2;
            else if (percentSides[2] > 0.7)
            {
                if (percentSides[3] > 0.4)
                    return 5;
                else
                    return 7;
            }
            else if (percentSides[1] < 0.2)
                return 4;
            else if (skewedness[0] < 0.45)
                return 3;
            else if (percentSides[0] > 0.45)
                return 6;
            else if (skewedness[1] > 0.5)
                return 8;
            else
                return 9;
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
            int topNum = 0;
            int botNum = 0;
            int rightNum = 0;
            int leftNum = 0;
            int lengthX = edges[1].X - edges[0].X;
            int lengthY = edges[1].Y - edges[0].Y;

            for(int x = 0; x < lengthX; x++)
            {
                topNum += image[edges[0].X + x, edges[0].Y + 1] ? 1 : 0;
                botNum += image[edges[0].X + x, edges[1].Y - 1] ? 1 : 0;
            }

            for (int y = 0; y < lengthY; y++)
            {
                leftNum += image[edges[0].X + 1, edges[0].Y + y] ? 1 : 0;
                rightNum += image[edges[1].X - 1, edges[0].Y + y] ? 1 : 0;
            }

            output[0] = Convert.ToDouble(leftNum) / lengthY;
            output[1] = Convert.ToDouble(rightNum) / lengthY;
            output[2] = Convert.ToDouble(topNum) / lengthX;
            output[3] = Convert.ToDouble(botNum) / lengthX;

            return output;
        }

        public static double[] getSkewedness(bool[,] image, Point[] edges)
            //double[0] = left skewedness
            //double[1] = top skewedness
        {
            double[] output = new double[2];

            //Counting pixels
            int numPixelsLeft = 0;
            int numPixelsRight = 0;
            int numPixelsTop = 0;
            int numPixelsBottom = 0;

            //For iterating through correct indices
            int lengthX = edges[1].X - edges[0].X;
            int lengthY = edges[1].Y - edges[0].Y;

            //Getting left and right side weightings
            for(int x = 0; x < lengthX/2; x++)
            {
                for(int y = 0; y < lengthY; y++)
                {
                    numPixelsLeft += image[edges[0].X + x, edges[0].Y + y] ? 1 : 0;
                    numPixelsRight += image[edges[1].X - x, edges[0].Y + y] ? 1 : 0;
                }
            }

            for (int y = 0; y < lengthY / 2; y++)
            {
                for (int x = 0; x < lengthX; x++)
                {
                    numPixelsTop += image[edges[0].X + x, edges[0].Y + y] ? 1 : 0;
                    numPixelsBottom += image[edges[0].X + x, edges[1].Y - y] ? 1 : 0;
                }
            }
            output[0] = Convert.ToDouble(numPixelsLeft) / (numPixelsRight + numPixelsLeft);
            output[1] = Convert.ToDouble(numPixelsTop) / (numPixelsTop + numPixelsBottom);

            return output;
        }
    }
}