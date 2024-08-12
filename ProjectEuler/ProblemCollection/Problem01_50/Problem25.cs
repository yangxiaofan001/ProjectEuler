using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection
{
    public class Problem25 : ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 25;
            }
        }

        public override string Description
        {
            get
            {
                return @"The Fibonacci sequence is defined by the recurrence relation:
                            Fn = Fn−1 + Fn−2, where F1 = 1 and F2 = 1.
                            Hence the first 12 terms will be:

                            F1 = 1
                            F2 = 1
                            F3 = 2
                            F4 = 3
                            F5 = 5
                            F6 = 8
                            F7 = 13
                            F8 = 21
                            F9 = 34
                            F10 = 55
                            F11 = 89
                            F12 = 144
                            The 12th term, F12, is the first term to contain three digits.

                            What is the index of the first term in the Fibonacci sequence to contain 1000 digits?
                            ";
            }
        }

        public override string Solution1()
        {
            int index = 3;
            string x1 = "1";
            string x2 = "2";
            string x = "";

            while (x.Length < 1000)
            {
                x = Utils.StringAddition(x1, x2);
                x1 = x2;
                x2 = x;
                index++;
            }

            //Console.Write("x = " + x);
            return index.ToString();
        }

        public override string Solution2()
        {
            // does not work
            // there is a limit, when index = 1477, x becomes infinity
            double x = 1;
            int index = 2;
            double phi = (1 + Math.Sqrt(5)) / 2;

            while (x < Math.Pow(10, 1000))
            {
                x = Math.Round(x * phi);
                index++;
            }


            return @"// does not work
            // there is a limit, when index = 1477, x becomes infinity" + index.ToString();
        }

        public override string Solution3()
        {
            string result
                = Math.Ceiling((1000 - 1 + Math.Log10(Math.Sqrt(5))) / Math.Log10((1 + Math.Sqrt(5)) / 2)).ToString();

            return result;
        }
    }
}
