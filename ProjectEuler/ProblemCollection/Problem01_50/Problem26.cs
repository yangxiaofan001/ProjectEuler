using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection
{
    public class Problem26 : ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 26;
            }
        }

        public override string Description
        {
            get
            {
                return 
                @"
                A unit fraction contains 1 in the numerator. 
                The decimal representation of the unit fractions with denominators 2 to 10 are given:

                    1/2	= 	0.5
                    1/3	= 	0.(3)
                    1/4	= 	0.25
                    1/5	= 	0.2
                    1/6	= 	0.1(6)
                    1/7	= 	0.(142857)
                    1/8	= 	0.125
                    1/9	= 	0.(1)
                    1/10	= 	0.1

                    Where 0.1(6) means 0.166666..., and has a 1-digit recurring cycle. 
                    It can be seen that 1/7 has a 6-digit recurring cycle.

                    Find the value of d < 1000 for which 1/d contains the longest recurring cycle in its decimal fraction part.
                ";
            }
        }

        public override string Solution1()
        {
            int maxRepeatDigits = 0;
            int maxRepeatDenominator = 0;
            string result = "";

            //System.IO.StreamWriter sw = new System.IO.StreamWriter(@"c:\temp\a.txt", false);
            //sw.Close();

            for (int l = 1; l < 1000; l++)
            {
                string div = Utils.StringDivision(1, l);

                if (!div.Contains("(")) continue;

                int digits = Convert.ToInt32(div.Split(new char[] { '(' })[1].Replace("...)", ""));
                if (digits > maxRepeatDigits)
                {
                    maxRepeatDigits = digits;
                    maxRepeatDenominator = l;
                    result = div;
                }

                //sw = new System.IO.StreamWriter(@"c:\temp\a.txt", true);
                //sw.WriteLine(l + "\t\t" + digits);
                //sw.Close();
            }

            return "1 / " + maxRepeatDenominator + " has a cycle length of " + maxRepeatDigits + ": " + result;
        }
    }
}
