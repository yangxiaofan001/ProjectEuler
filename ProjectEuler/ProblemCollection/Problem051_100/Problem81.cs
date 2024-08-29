using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace EulerProject.ProblemCollection
{
    public class Problem81 : ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 81;
            }
        }

        public override string Description
        {
            get
            {
                return @"Problem 81 - Path Sum: Two Ways
In the 5 by 5 matrix below, the minimal path sum from the top left to the bottom right, by only moving to the right and down, is indicated in bold red and is equal to 2427.

[131]   673     234     103     18
[201]   [96]    [342]   965     150
630     803     [746]   [422]   111
537     699     497     [121]   956
805     732     524     [37]    [331]

Find the minimal path sum from the top left to the bottom right by only moving right and down in matrix.txt (right click and 'Save Link/Target As...'), a 31K text file containing an 80 by 80 matrix.
";
            }
        }

        public override string Solution1()
        {
            string idea = @"
starting from second anti-diagonl, {[5, 4], [4, 5]}, then {[5, 3], [4, 4], [3, 5]}, ..., {[5, 1], [4, 2], ..., [1, 5]}, {[1,1]}

in each set, loop through every number, add the minimum of the number to the right and number down

loop through each set until the left-top number
";
Console.WriteLine(idea);

            System.IO.StreamReader sr = new System.IO.StreamReader("Files/0081_matrix.txt");
            string line = "";
            List<List<int>> numbersList = new List<List<int>>();
            while((line = sr.ReadLine()) != null)
            {
                string [] numberStrings = line.Split(new char[]{',' });
                List<int> numbers = new List<int>();
                foreach(string s in numberStrings) numbers.Add(Convert.ToInt32(s));
                numbersList.Add(numbers);
            }
            sr.Close();

            int r = numbersList.Count - 1;
            int c = numbersList.Count - 2;
            while(true)
            {
                int rr = r;
                int cc = c;

                while(true)
                {

                    int numberDown = Int32.MaxValue;
                    int numberRight = Int32.MaxValue;

                    int rDown = rr + 1;
                    int cDown = cc;
                    int rRight = rr;
                    int cRight = cc + 1;

                    if (rDown >= 0 && rDown < numbersList.Count && cDown >= 0 && cDown < numbersList.Count) numberDown = numbersList[rDown][cDown];
                    if (rRight >= 0 && rRight < numbersList.Count && cRight >= 0 && cRight < numbersList.Count) numberRight = numbersList[rRight][cRight];

                    numbersList[rr][cc] = numbersList[rr][cc] + Math.Min(numberDown, numberRight);

                    // move Northeast
                    rr --;
                    cc ++;

                    // if out of range, break;
                    if (rr < 0 || rr >= numbersList.Count || cc < 0 || cc >= numbersList.Count ) break;
                }
                
                // move left - i--; if index out of range. move up - j--; if both out of range, finish
                if (c > 0)
                    c --;
                else if (r > 0)
                    r --;
                else
                    break;
            }

            return numbersList[0][0].ToString();
        }
    }
}
