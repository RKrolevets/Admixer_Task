using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeInaRow
{
    /// <summary>
    /// 9x9 matrix with random numbers from 0 to 3 inclusive
    /// </summary>
    public class Field
    {
        private Random randomNumbers = new Random();
        public int?[,] Matrix { get; private set; } = new int?[9, 9];
        private List<List<int>> comparedItems = new ();
        public Field ()
        {
            FillEmptyCell();
        }

        private void FillEmptyCell()
        {
            for (int height = 0; height < Matrix.GetLength(0); height++)
            {
                for (var width = 0; width < Matrix.GetLength(1); width++)
                {
                    if (Matrix[height, width] == null)
                        Matrix[height, width] = randomNumbers.Next(0, 4);
                }
            }         
        }

        /// <summary>
        /// If 3 or more numbers coincide horizontally/vertically, remove them by moving all matrix elements from top 
        /// to bottom to fill the empty space. Then fill the remaining empty elements with random numbers from 0 to 3 inclusive
        /// </summary>
        /// <returns></returns>
        public bool FindReplaceDuplicates()
        {
            bool isAnyMatch = IsCompared();
            if (isAnyMatch)
            {
                DeleteRepeats();
                MoveNumbersDown();
                FillEmptyCell();
            }
            return isAnyMatch;
        }

        private bool IsCompared()
        {
            bool isAnyMatch = false;
            for (int width = 0; width < Matrix.GetLength(1); width++)
            {
                for (var height = 1; height < Matrix.GetLength(0) - 1; height++)
                {
                    VerticalComparer(width, height, ref isAnyMatch);
                    HorizontalComparer(height, width, ref isAnyMatch);
                }
            }
            return isAnyMatch;
        }

        public void VerticalComparer(int first, int second, ref bool isAnyMatch)
        {
            if (Matrix[first, second] == Matrix[first, second - 1]
                        && Matrix[first, second] == Matrix[first, second + 1])
            {
                isAnyMatch = true;
                if (!comparedItems.Contains(new List<int> { first, second - 1 }))
                    comparedItems.Add(new List<int> { first, second - 1 });
                if (!comparedItems.Contains(new List<int> { first, second }))
                    comparedItems.Add(new List<int> { first, second });
                if (!comparedItems.Contains(new List<int> { first, second + 1 }))
                    comparedItems.Add(new List<int> { first, second + 1 });
            }
        }

        public void HorizontalComparer(int first, int second, ref bool isAnyMatch)
        {
            if (Matrix[first, second] == Matrix[first - 1, second]
                        && Matrix[first, second] == Matrix[first + 1, second])
            {
                isAnyMatch = true;
                if (!comparedItems.Contains(new List<int> { first - 1, second }))
                    comparedItems.Add(new List<int> { first - 1, second });
                if (!comparedItems.Contains(new List<int> { first, second }))
                    comparedItems.Add(new List<int> { first, second });
                if (!comparedItems.Contains(new List<int> { first + 1, second }))
                    comparedItems.Add(new List<int> { first + 1, second });
            }
        }

        private void DeleteRepeats()
        {
            foreach (var item in comparedItems)
            {
                Matrix[item[0], item[1]] = null;
            }
            comparedItems.Clear();
        }

        private void MoveNumbersDown ()
        {
            for (var width = 0; width < Matrix.GetLength(0); width++)
            {
                for (int top = 0, bottom = Matrix.GetLength(1)-1; top < bottom; )
                {
                    if (Matrix[top, width] == null)
                    {
                        top++;
                        continue;
                    }

                    if (Matrix[bottom, width] != null)
                    {
                        bottom--;
                        continue;
                    }
                    else
                    {
                        SwapNullToTop(bottom, top, width);
                        top++;
                    }
                }
            }
        }

        private void SwapNullToTop (int bottom, int top, int width)
        {
            while (bottom > top)
            {
                (Matrix[bottom, width], Matrix[bottom-1, width]) = (Matrix[bottom - 1, width], Matrix[bottom, width]);
                bottom--;
            }
        }

        /// <summary>
        /// Print table of current numbers from matrix in console
        /// </summary>
        public void Print()
        {
            for (int height = 0; height < Matrix.GetLength(0); height++)
            {
                for (var width = 0; width < Matrix.GetLength(1); width++)
                {
                    Console.Write($"{Matrix[height, width]}\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}