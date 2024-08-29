using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace EulerProject.ProblemCollection
{
    public class Problem82 : ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 82;
            }
        }

        public override string Description
        {
            get
            {
                return @"Problem 82 - Path Sum: Three Ways
NOTE: This problem is a more challenging version of Problem 81.

The minimal path sum in the 5 by 5 matrix below, by starting in any cell in the left column and finishing in any cell in the right column, and only moving up, down, and right, is indicated in red and bold; 
the sum is equal to 994.

131     1018
201     783
630     1586
537     1850
805     1624

Find the minimal path sum from the left column to the right column in matrix.txt (right click and 'Save Link/Target As...'), a 31K text file containing an 80 by 80 matrix.
";
            }
        }

        public override string Solution1()
        {
            string idea = @"
starting from second right column

in each column, loop through each number, find the minimum sum to each of the numbers in the column to the right, add it to current number

loop through each column until the first column
";
            Console.WriteLine(idea);

            System.IO.StreamReader sr = new System.IO.StreamReader("Files/0082_matrix.txt");
            string line = "";
            List<List<int>> numbersList = new List<List<int>>();
            while ((line = sr.ReadLine()) != null)
            {
                string[] numberStrings = line.Split(new char[] { ',' });
                List<int> numbers = new List<int>();
                foreach (string s in numberStrings) numbers.Add(Convert.ToInt32(s));
                numbersList.Add(numbers);
            }
            sr.Close();

            int c = numbersList.Count - 2;
            while (c >= 0)
            {
                int[] numbers = new int[numbersList.Count];
                for (int fromRow = 0; fromRow < numbersList.Count; fromRow++)
                {
                    int minPathSum = Int32.MaxValue;
                    for (int toRow = 0; toRow < numbersList.Count; toRow++)
                    {
                        int pathSum = numbersList[fromRow][c];
                        if (fromRow == toRow)
                        {
                            pathSum += numbersList[fromRow][c + 1];
                        }
                        else if (fromRow < toRow)
                        {
                            int rightDownSum = numbersList[fromRow][c + 1];

                            for (int r = fromRow + 1; r <= toRow; r++)
                                rightDownSum += numbersList[r][c + 1];

                            int downRightSum = 0;
                            for (int r = fromRow + 1; r <= toRow; r++)
                                downRightSum += numbersList[r][c];
                            downRightSum += numbersList[toRow][c + 1];

                            pathSum += Math.Min(rightDownSum, downRightSum);
                        }
                        else
                        {
                            int rightUpSum = numbersList[fromRow][c + 1];

                            for (int r = fromRow - 1; r >= toRow; r--)
                                rightUpSum += numbersList[r][c + 1];

                            int upRightSum = 0;
                            for (int r = fromRow - 1; r >= toRow; r--)
                                upRightSum += numbersList[r][c];

                            upRightSum += numbersList[toRow][c + 1];

                            pathSum += Math.Min(rightUpSum, upRightSum);
                        }

                        minPathSum = Math.Min(minPathSum, pathSum);
                    }

                    numbers[fromRow] = minPathSum;
                }

                for (int row = 0; row < numbersList.Count; row++)
                    numbersList[row][c] = numbers[row];
                c--;
            }

            int answer = Int32.MaxValue;
            for (int i = 0; i < numbersList.Count; i++)
                answer = Math.Min(answer, numbersList[i][0]);

            return answer.ToString();
        }
    }
}
