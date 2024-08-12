using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection
{
    public class Problem34 : ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 34;
            }
        }

        public override string Description
        {
            get
            {
                return @"
                    Digit factorials
                    Problem 34
                    145 is a curious number, as 1! + 4! + 5! = 1 + 24 + 120 = 145.

                    Find the sum of all numbers which are equal to the sum of the factorial of their digits.

                    Note: as 1! = 1 and 2! = 2 are not sums they are not included.
                    ";
            }
        }

        List<System.Numerics.BigInteger> permutationList = new List<System.Numerics.BigInteger>();
        void GeneratepermutationList()
        {
            for (int i = 0; i <= 9; i++)
                permutationList.Add(Utils.Permutation(i));
        }

        int maxDigits = 0;
        System.Numerics.BigInteger maxNumber = 0;
        void FindMaxNumber()
        {
            int digits = 1;
            GeneratepermutationList();

            while (permutationList[9] * digits > Utils.Pow(10, digits) - 1)
                digits++;

            digits--;
            maxDigits = digits;
            maxNumber = permutationList[9] * digits;
        }


        private bool NumbersMatch(System.Numerics.BigInteger n1, System.Numerics.BigInteger n2)
        {
            // returns true is n1 and n2 are composed by the same digits
            // for example  
            //      NumbersMatch(1450, 5401) returns true
            //      NumbersMatch(1450, 145) returns false
            //      NumbersMatch(1450, 1459) returns false
            string s1 = n1.ToString();
            string s2 = n2.ToString();

            if (s1.Length != s2.Length) return false;

            char[] ca1 = s1.ToCharArray();
            char[] ca2 = s2.ToCharArray();

            Array.Sort(ca1);
            Array.Sort(ca2);

            for (int i = 0; i < ca1.Length; i++)
            {
                if (ca1[i] != ca2[i])
                    return false;
            }

            return true;
        }

        void WriteLog(string line)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(@"c:\temp\log.txt", true);
            sw.WriteLine(line);
            sw.Close();
        }

        void ClearLog()
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(@"c:\temp\log.txt", false);
            sw.Close();
        }

        public Problem34()
        {
            ClearLog();
        }


        System.Numerics.BigInteger grandTotal = 0;
        System.Numerics.BigInteger numberCount = 0;

        public override string Solution1()
        {
            DateTime dtStart = DateTime.Now;
            grandTotal = 0;

            numberCount = 0;
            FindMaxNumber();

            System.Numerics.BigInteger number = 0;
            System.Numerics.BigInteger sum = 0;
            for (int d = maxDigits; d >= 2; d--)
            {
                // examine numbers with d digits
                for (int z = 0; z < d - 1; z++)
                {
                    // examine numbers with d digits and has z zeros in it. 
                    // Ignore the numbers with d digits and has (d - 1) zeros. 
                    //  They will not be the answer. For example, 10, 200, 7000, 50000....
                    //  Save a if (digits== 0) in the recursive method
                    sum = 0;
                    for (int f = 1; f <= 9; f++)
                    {
                        if (d == 5 && z == 1 && f == 4)
                        {
                            int x = 1;
                        }

                        // The first digit of number is f
                        sum = permutationList[f];
                        number = f;
                        for (int i = 0; i < z; i++)
                        {
                            // add z zeros after the first digit
                            // all zeros must be immediately following the first digit
                            sum += permutationList[0];
                            number *= 10;
                        }

                        // Process the remaining digits
                        //  Total of remaining digits is d - 1 -z
                        //  Each remaining digit must be bigger than the first digit
                        Solution1_Process(sum, number, d - 1 - z, f);
                    }
                }
            }

            DateTime dtEnd = DateTime.Now;

            return grandTotal.ToString();
        }

        private void Solution1_Process(System.Numerics.BigInteger sum, System.Numerics.BigInteger number, int digits, int lower)
        {
            if (digits == 1)
            {
                // Exit point

                for (int l = lower; l <= 9; l++)
                {
                    // l is the last digit 
                    numberCount++;
                    Solution1_ExamineNumberSum(number * 10 + l, sum + permutationList[l]);
                }
            }
            else
            {
                for (int n = lower; n <= 9; n++)
                {
                    // n is the next digit
                    // after n, there are (digits - 1) digits remaining to be processed
                    // each digits after n must be bigger than n
                    Solution1_Process(sum + permutationList[n], number * 10 + n, digits - 1, n);
                }
            }
        }

        private void Solution1_ExamineNumberSum(System.Numerics.BigInteger number, System.Numerics.BigInteger sum)
        {
            if (NumbersMatch(sum, number))
            {
                Console.WriteLine("sum = " + sum + "; number = " + number);
                WriteLog("sum = " + sum + "; number = " + number);
                grandTotal += sum;
            }
        }

    }
}
