using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection
{
    public class Problem30 : ProblemBase
    {
        // https://projecteuler.net/thread=30;page=5
        // from snahgle 

        int expo = 0;
        int maxDigits = 0;
        System.Numerics.BigInteger maxNumber = 0;
        System.Numerics.BigInteger[] _np = new System.Numerics.BigInteger[10];
        System.Numerics.BigInteger numberCount;
        System.Numerics.BigInteger grandTotal;

        public override int ProblemNumber
        {
            get
            {
                return 30;
            }
        }

        public override string Description
        {
            get
            {
                return @"
                    Surprisingly there are only three numbers that can be written as the sum of fourth powers of their digits:

                    1634 = 14 + 64 + 34 + 44
                    8208 = 84 + 24 + 04 + 84
                    9474 = 94 + 44 + 74 + 44
                    As 1 = 14 is not a sum it is not included.

                    The sum of these numbers is 1634 + 8208 + 9474 = 19316.

                    Find the sum of all the numbers that can be written as the sum of fifth powers of their digits.
                    ";
            }
        }

        System.Numerics.BigInteger Exponent(int root, int pow)
        {
            if (root < 0 || pow < 0)
                throw new ApplicationException("Try Math.Pow instead.");

            if (pow == 0) return 1;
            if (root == 0) return 0;
            if (root == 1) return 1;

            if (pow == 1) return root;
            return root * Exponent(root, pow - 1);
        }

        private void FindMaxNumber()
        {
            int digits = 1;
            System.Numerics.BigInteger _9Expo = Exponent(9, expo);
            maxNumber = 9;

            while (maxNumber <= digits * _9Expo)
            {
                digits++;
                maxNumber = (maxNumber + 1) * 10 - 1;
            }
            maxDigits = digits;
            maxNumber = maxDigits * _9Expo;

            for (int i = 0; i < 10; i++)
            {
                _np[i] = Exponent(i, expo);
            }
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

        public Problem30()
        {
            ClearLog();
        }

        public override string Solution1()
        {
            // brutal force
            grandTotal = 0;
            expo = 5;
            FindMaxNumber();

            for (System.Numerics.BigInteger i = 2; i <= maxNumber; i++)
            {
                System.Numerics.BigInteger sum = 0;
                string s = i.ToString();

                for (int j = 0; j < i.ToString().Length; j++)
                {
                    int l = i.ToString()[j] - '0';
                    sum += _np[l];
                }

                if (i == sum)
                {
                    grandTotal += sum;
                    Console.WriteLine(sum);
                }
            }

            return grandTotal.ToString();
        }

        public override string Solution2()
        {
            // small improvement over solution 1
            // when examine a number, 
            // if the sum of all digits is an odd number, and the number being exmained is an even number, the number can be ignored.
            // if the sum of all digits is an even number, and the number being exmained is an odd number, the number can be ignored.

            grandTotal = 0;
            expo = 5;
            FindMaxNumber();
            DateTime dtStart = DateTime.Now;

            for (System.Numerics.BigInteger i = 2; i <= maxNumber; i++)
            {
                System.Numerics.BigInteger sum = 0;
                string s = i.ToString();

                for (int j = 0; j < s.Length; j++)
                {
                    sum += s[j] - '0';
                }
                if ((i - sum) % 2 != 0) continue;

                sum = 0;

                for (int j = 0; j < s.Length; j++)
                {
                    sum += _np[s[j] - '0'];
                }

                if (i == sum)
                {
                    grandTotal += sum;
                    Console.WriteLine(sum);
                }
            }

            DateTime dtEnd = DateTime.Now;
            Console.WriteLine("Calculated grand total. Done in " + ((dtEnd - dtStart).Seconds * 1000 + (dtEnd - dtStart).Milliseconds).ToString() + " ms");

            return grandTotal.ToString();
        }

        public override string Solution3()
        {
            // hard code for loop level of 5, pretty fast. Impossible to write when level is bigger, for example, 20
            List<int> list = new List<int>();
            grandTotal = 0;
            expo = 5;
            FindMaxNumber();

            for (int i1 = 0; i1 <= 9; i1++)
            {
                for (int i2 = 0; i2 <= 9; i2++)
                {
                    for (int i3 = 0; i3 <= 9; i3++)
                    {
                        for (int i4 = 0; i4 <= 9; i4++)
                        {
                            for (int i5 = 0; i5 <= 9; i5++)
                            {
                                for (int i6 = 0; i6 <= 9; i6++)
                                {
                                    int digits = 0;

                                    if (i1 != 0)
                                    {
                                        digits = 6;
                                        if (i3 > i4 || i4 > i5 || i5 > i6)
                                            continue;

                                        if (((i2 == 0) && (i1 > i3)) || ((i2 != 0) && (i1 > i2 || i2 > i3)))
                                            continue;
                                    }
                                    else if (i2 != 0)
                                    {
                                        digits = 5;

                                        if (i4 > i5 || i5 > i6)
                                            continue;

                                        if (((i3 == 0) && (i2 > i4)) || ((i3 != 0) && (i2 > i3 || i3 > i4)))
                                            continue;
                                    }
                                    else if (i3 != 0)
                                    {
                                        digits = 4;
                                        if (i5 > i6) continue;

                                        if ((i4 == 0 && i3 > i5) || (i4 != 0 && (i3 > i4 || i4 > i5)))
                                        {
                                            continue;
                                        }
                                    }
                                    else if (i4 != 0)
                                    {
                                        digits = 3;
                                        if ((i5 == 0 && i4 > i6) || (i5 != 0 && (i4 > i5 || i5 > i6)))
                                        {
                                            continue;
                                        }
                                    }
                                    else if (i5 != 0)
                                    {
                                        digits = 2;
                                        if (i6 != 0 && i5 > i6)
                                            continue;
                                    }
                                    else
                                        continue; // i1, i2, i3, i4 and i5 are all 0: 1 digit number

                                    int i = i1 * 100000 + i2 * 10000 + i3 * 1000 + i4 * 100 + i5 * 10 + i6;
                                    if (i > 234459) continue;

                                    System.Numerics.BigInteger sum = _np[i1] + _np[i2] + _np[i3] + _np[i4] + _np[i5] + _np[i6];

                                    char[] s = i.ToString().ToArray();
                                    char[] ssum = sum.ToString().ToArray();
                                    if (digits != ssum.Length)
                                        continue;

                                    Array.Sort(s);
                                    Array.Sort(ssum);

                                    bool bValid = true;
                                    for (int x = 0; x < s.Length; x++)
                                    {
                                        if (s[x] != ssum[x])
                                        {
                                            bValid = false;
                                            break;
                                        }
                                    }

                                    if (bValid)
                                        grandTotal += sum;
                                }
                            }
                        }
                    }
                }
            }
            return grandTotal.ToString();
        }

        int s4s5MaxExpo = 20;

        public override string Solution4()
        {
            #region explanation
            // Best solution
            // https://projecteuler.net/thread=30;page=5
            // from snahgle 

            // only process 'minimal' numbers - the number that each digit is equal to or smaller than the next digit
            // Find the max digits (total digits in the max number)
            // Say the max number has 4 digits
            //      Process the minimal numbers that has 4 digits, with no zeros in the number
            //          then the minimal numbers that has 4 digits, with 1 zeros in the number
            //          then the minimal numbers that has 4 digits, with 2 zeros in the number
            //          then the minimal numbers that has 4 digits, with 3 zeros in the number
            //      then the minimal numbers that has 3 digits, with no zeros in the number
            //          then the minimal numbers that has 3 digits, with 1 zeros in the number
            //          then the minimal numbers that has 3 digits, with 2 zeros in the number
            //      then the minimal numbers that has 2 digits, with no zeros in the number
            //          then the minimal numbers that has 2 digits, with 1 zeros in the number

            // To process all the minimal numbers that has x digits, with y zeros
            //      in one for loop 1 to 9, fill the first number
            //      fill y zeros
            //      call recursive method 'fill' to fill the rest digits (no zeros)

            /*
             * execution output
             * expo = 02  maxDigits = 03  maxNumber = 243  grandTotal = 0	numberCount = 122	0 ms


                sum = 153; number = 135
                sum = 371; number = 137
                sum = 370; number = 307
                sum = 407; number = 407
                expo = 03  maxDigits = 04  maxNumber = 2916  grandTotal = 1301	numberCount = 637	2 ms


                sum = 1634; number = 1346
                sum = 9474; number = 4479
                sum = 8208; number = 2088
                expo = 04  maxDigits = 05  maxNumber = 32805  grandTotal = 19316	numberCount = 2287	4 ms


                sum = 194979; number = 147999
                sum = 92727; number = 22779
                sum = 54748; number = 44578
                sum = 93084; number = 30489
                sum = 4151; number = 1145
                sum = 4150; number = 1045
                expo = 05  maxDigits = 06  maxNumber = 354294  grandTotal = 443839	numberCount = 6904	13 ms


                sum = 548834; number = 344588
                expo = 06  maxDigits = 07  maxNumber = 3720087  grandTotal = 548834	numberCount = 17640	30 ms


                sum = 14459929; number = 12445999
                sum = 1741725; number = 1124577
                sum = 9926315; number = 1235699
                sum = 4210818; number = 1012488
                sum = 9800817; number = 1007889
                expo = 07  maxDigits = 08  maxNumber = 38263752  grandTotal = 40139604	numberCount = 40673	77 ms


                sum = 88593477; number = 34577889
                sum = 24678051; number = 10245678
                sum = 24678050; number = 20045678
                expo = 08  maxDigits = 09  maxNumber = 387420489  grandTotal = 137949578	numberCount = 87280	185 ms


                sum = 912985153; number = 112355899
                sum = 472335975; number = 233455779
                sum = 534494836; number = 334445689
                sum = 146511208; number = 101124568
                expo = 09  maxDigits = 10  maxNumber = 3874204890  grandTotal = 2066327172	numberCount = 176644	384 ms


                sum = 4679307774; number = 3044677779
                expo = 10  maxDigits = 11  maxNumber = 38354628411  grandTotal = 4679307774	numberCount = 340225	873 ms


                sum = 82693916578; number = 12356678899
                sum = 32164049651; number = 10123445669
                sum = 94204591914; number = 10124445999
                sum = 44708635679; number = 30445667789
                sum = 32164049650; number = 10023445669
                sum = 40028394225; number = 20022344589
                sum = 42678290603; number = 20023466789
                sum = 49388550606; number = 30045566889
                expo = 11  maxDigits = 12  maxNumber = 376572715308  grandTotal = 418030478906	numberCount = 627890	1875 ms


                expo = 12  maxDigits = 13  maxNumber = 3671583974253  grandTotal = 0	numberCount = 1116641	3803 ms


                sum = 564240140138; number = 100123444568
                expo = 13  maxDigits = 14  maxNumber = 35586121596606  grandTotal = 564240140138	numberCount = 1921335	7244 ms


                sum = 28116440335967; number = 10123344566789
                expo = 14  maxDigits = 15  maxNumber = 343151886824415  grandTotal = 28116440335967	numberCount = 3202724	13346 ms


                expo = 15  maxDigits = 16  maxNumber = 3294258113514384  grandTotal = 0	numberCount = 5182704	23540 ms


                sum = 4338281769391371; number = 1112333346778899
                sum = 4338281769391370; number = 1012333346778899
                expo = 16  maxDigits = 17  maxNumber = 31501343210481297  grandTotal = 8676563538782741	numberCount = 8260561	41008 ms


                sum = 21897142587612075; number = 10112224556777889
                sum = 35641594208964132; number = 10122334445566899
                sum = 35875699062250035; number = 20002335555667899
                sum = 233411150132317; number = 101111223333457
                expo = 17  maxDigits = 18  maxNumber = 300189270593998242  grandTotal = 93647847008958559	numberCount = 12758516	69842 ms


                expo = 18  maxDigits = 19  maxNumber = 2851798070642983299  grandTotal = 0	numberCount = 19372008	112456 ms


                sum = 4498128791164624869; number = 1112244446667888999
                sum = 3289582984443187032; number = 1022233344457888899
                sum = 4929273885928088826; number = 2022234567888888999
                sum = 1517841543307505039; number = 1000113334455557789
                expo = 19  maxDigits = 20  maxNumber = 27017034353459841780  grandTotal = 14234827204843405766	numberCount = 29156582	181111 ms


                sum = 63105425988599693916; number = 10123345556668899999
                expo = 20  maxDigits = 21  maxNumber = 255310974640195504821  grandTotal = 63105425988599693916	numberCount = 43157296	285761 ms
             */
            #endregion

            for (expo = 2; expo <= s4s5MaxExpo; expo++)
            {
                DateTime dtStart = DateTime.Now;
                grandTotal = 0;

                numberCount = 0;
                FindMaxNumber();

                for (int d = maxDigits; d >= 2; d--)
                {
                    for (int z = 0; z <= d - 1; z++)
                    {
                        // Fill the first digits with 1 to 9
                        // Then fill the following digit(s) with numberOfZeros
                        // Then call Solution4_Fill to fill the remaining digits with 1 to 9, no zeros
                        int[] allDigits = new int[d];

                        for (allDigits[0] = 1; allDigits[0] <= 9; allDigits[0]++)
                        {
                            int level = d;

                            for (int index = 1; index <= z; index++)
                            {
                                allDigits[index] = 0;
                                level--;
                            }

                            if (level > 1)
                                Solution4_Process(allDigits, level - 1, lower: allDigits[0], upper: 9);
                        }
                    }
                }

                DateTime dtEnd = DateTime.Now;

                Console.Write("expo = " + expo.ToString("00"));
                Console.Write("  maxDigits = " + maxDigits.ToString("00"));
                Console.Write("  maxNumber = " + maxNumber);
                Console.Write("  grandTotal = " + grandTotal);
                Console.Write("\tnumberCount = " + numberCount);
                Console.WriteLine("\t" + ((int)((dtEnd - dtStart).TotalMilliseconds)).ToString() + " ms");
                Console.WriteLine("");
                Console.WriteLine("");

                WriteLog("expo = " + expo.ToString("00") +
                    "  maxDigits = " + maxDigits.ToString("00") +
                    "  maxNumber = " + maxNumber +
                    "  grandTotal = " + grandTotal +
                    "\tnumberCount = " + numberCount +
                    "\t" + ((int)((dtEnd - dtStart).TotalMilliseconds)).ToString() + " ms");
                WriteLog("");
                WriteLog("");
            }

            return grandTotal.ToString();
        }

        private void Solution4_Process(int[] dArray, int level, int lower, int upper)
        {
            // Fill the number array with number 1 - 9, no zeros
            // Each digit is no lessor than the preceeding digit
            if (level == 1)
            {
                for (int i = lower; i <= upper; i++)
                {
                    dArray[dArray.Length - 1] = i;

                    #region process sum, grandtotal
                    System.Numerics.BigInteger number = 0;
                    System.Numerics.BigInteger q = 1;
                    for (int p = dArray.Length - 1; p >= 0; p--)
                    {
                        number += dArray[p] * q;
                        q *= 10;
                    }

                    if (number <= maxNumber)
                    {
                        numberCount++;

                        System.Numerics.BigInteger sum = 0;
                        for (int x = 0; x < dArray.Length; x++)
                        {
                            sum += _np[dArray[x]];
                        }

                        if (NumbersMatch(sum, number))
                        {
                            grandTotal += sum;
                            Console.WriteLine("sum = " + sum + "; number = " + number);
                            WriteLog("sum = " + sum + "; number = " + number);
                        }
                    }
                    #endregion
                }
            }
            else
            {
                for (int i = lower; i <= upper; i++)
                {
                    dArray[dArray.Length - level] = i;
                    Solution4_Process(dArray, level - 1, i, upper);
                }
            }

        }

        public override string Solution5()
        {
            #region explanation
            // Improved from solution 4. Use less than 1/3 of the time.

            // only process 'minimal' numbers - the number that each digit is equal to or smaller than the next digit
            // 
            // Find the max number and max digits (total digits in the max number)

            // two level loops
            //      for (int d = maxDigits; d >= 2; d--)  // examine numbers that has [d] digits
            //      {
            //          for (int z = 0; z < d - 1; z++)   // examine numbers that has [d] digits, and has [z] zeros in it.
            //          {
            //              ...                    
            //          }
            //      }
            //
            // To process all the minimal numbers that has x digits, with y zeros
            //      in for loop 1 to 9, fill the first number
            //      fill z zeros
            //      call recursive method 'Solution5_Process' to process the rest digits (no zeros)

            //  In the recursive method, when digit is 1, we know all the digits of a number
            //      now we can compare the digits in sum and the number. 
            //      if they match, one answer is found.
            /*
             * execution output
             * 
                exponent = 02  maxDigits = 03  maxNumber = 243  grandTotal = 0	numberCount = 255	0 ms


                sum = 153; number = 135
                sum = 371; number = 137
                sum = 370; number = 307
                sum = 407; number = 407
                exponent = 03  maxDigits = 04  maxNumber = 2916  grandTotal = 1301	numberCount = 960	0 ms


                sum = 1634; number = 1346
                sum = 9474; number = 4479
                sum = 8208; number = 2088
                exponent = 04  maxDigits = 05  maxNumber = 32805  grandTotal = 19316	numberCount = 2952	0 ms


                sum = 194979; number = 147999
                sum = 92727; number = 22779
                sum = 54748; number = 44578
                sum = 93084; number = 30489
                sum = 4151; number = 1145
                sum = 4150; number = 1045
                exponent = 05  maxDigits = 06  maxNumber = 354294  grandTotal = 443839	numberCount = 7947	15 ms


                sum = 548834; number = 344588
                exponent = 06  maxDigits = 07  maxNumber = 3720087  grandTotal = 548834	numberCount = 19377	1 ms


                sum = 14459929; number = 12445999
                sum = 1741725; number = 1124577
                sum = 9926315; number = 1235699
                sum = 4210818; number = 1012488
                sum = 9800817; number = 1007889
                exponent = 07  maxDigits = 08  maxNumber = 38263752  grandTotal = 40139604	numberCount = 43677	46 ms


                sum = 88593477; number = 34577889
                sum = 24678051; number = 10245678
                sum = 24678050; number = 20045678
                exponent = 08  maxDigits = 09  maxNumber = 387420489  grandTotal = 137949578	numberCount = 92287	37 ms


                sum = 912985153; number = 112355899
                sum = 472335975; number = 233455779
                sum = 534494836; number = 334445689
                sum = 146511208; number = 101124568
                exponent = 09  maxDigits = 10  maxNumber = 3874204890  grandTotal = 2066327172	numberCount = 184655	155 ms


                sum = 4679307774; number = 3044677779
                exponent = 10  maxDigits = 11  maxNumber = 38354628411  grandTotal = 4679307774	numberCount = 352605	333 ms


                sum = 82693916578; number = 12356678899
                sum = 32164049651; number = 10123445669
                sum = 94204591914; number = 10124445999
                sum = 44708635679; number = 30445667789
                sum = 32164049650; number = 10023445669
                sum = 40028394225; number = 20022344589
                sum = 42678290603; number = 20023466789
                sum = 49388550606; number = 30045566889
                exponent = 11  maxDigits = 12  maxNumber = 376572715308  grandTotal = 418030478906	numberCount = 646525	721 ms


                exponent = 12  maxDigits = 13  maxNumber = 3671583974253  grandTotal = 0	numberCount = 1143935	1392 ms


                sum = 564240140138; number = 100123444568
                exponent = 13  maxDigits = 14  maxNumber = 35586121596606  grandTotal = 564240140138	numberCount = 1961115	2578 ms


                sum = 28116440335967; number = 10123344566789
                exponent = 14  maxDigits = 15  maxNumber = 343151886824415  grandTotal = 28116440335967	numberCount = 3268609	4680 ms


                exponent = 15  maxDigits = 16  maxNumber = 3294258113514384  grandTotal = 0	numberCount = 5311574	7950 ms


                sum = 4338281769391371; number = 1112333346778899
                sum = 4338281769391370; number = 1012333346778899
                exponent = 16  maxDigits = 17  maxNumber = 31501343210481297  grandTotal = 8676563538782741	numberCount = 8436114	12589 ms


                sum = 21897142587612075; number = 10112224556777889
                sum = 35641594208964132; number = 10122334445566899
                sum = 35875699062250035; number = 20002335555667899
                sum = 233411150132317; number = 101111223333457
                exponent = 17  maxDigits = 18  maxNumber = 300189270593998242  grandTotal = 93647847008958559	numberCount = 13122929	20616 ms


                exponent = 18  maxDigits = 19  maxNumber = 2851798070642983299  grandTotal = 0	numberCount = 20029819	32651 ms


                sum = 4498128791164624869; number = 1112244446667888999
                sum = 3289582984443187032; number = 1022233344457888899
                sum = 4929273885928088826; number = 2022234567888888999
                sum = 1517841543307505039; number = 1000113334455557789
                exponent = 19  maxDigits = 20  maxNumber = 27017034353459841780  grandTotal = 14234827204843405766	numberCount = 30044814	50965 ms


                sum = 63105425988599693916; number = 10123345556668899999
                exponent = 20  maxDigits = 21  maxNumber = 255310974640195504821  grandTotal = 63105425988599693916	numberCount = 44351954	80653 ms
             */
            #endregion

            for (expo = 2; expo <= s4s5MaxExpo; expo++)
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
                            // The first digit of number is f
                            sum = _np[f];
                            number = f;
                            for (int i = 0; i < z; i++)
                            {
                                // add z zeros after the first digit
                                // all zeros must be immediately following the first digit
                                number *= 10;
                            }

                            // Process the remaining digits
                            //  Total of remaining digits is d - 1 -z
                            //  Each remaining digit must be bigger than the first digit
                            Solution5_Process(sum, number, d - 1 - z, f);
                        }
                    }
                }

                DateTime dtEnd = DateTime.Now;

                Console.Write("exponent = " + expo.ToString("00"));
                Console.Write("  maxDigits = " + maxDigits.ToString("00"));
                Console.Write("  maxNumber = " + maxNumber);
                Console.Write("  grandTotal = " + grandTotal);
                Console.Write("\tnumberCount = " + numberCount);
                Console.WriteLine("\t" + ((int)((dtEnd - dtStart).TotalMilliseconds)).ToString() + " ms");
                Console.WriteLine("");
                Console.WriteLine("");

                WriteLog("exponent = " + expo.ToString("00") +
                    "  maxDigits = " + maxDigits.ToString("00") +
                    "  maxNumber = " + maxNumber +
                    "  grandTotal = " + grandTotal +
                    "\tnumberCount = " + numberCount +
                    "\t" + ((int)((dtEnd - dtStart).TotalMilliseconds)).ToString() + " ms");
                WriteLog("");
                WriteLog("");
            }

            return grandTotal.ToString();
        }

        private void Solution5_Process(System.Numerics.BigInteger sum, System.Numerics.BigInteger number, int digits, int lower)
        {
            if (digits == 1)
            {
                // Exit point

                for (int l = lower; l <= 9; l++)
                {
                    // l is the last digit 
                    numberCount++;
                    Solution5_ExamineNumberSum(number * 10 + l, sum + _np[l]);
                }
            }
            else
            {
                for (int n = lower; n <= 9; n++)
                {
                    // n is the next digit
                    // after n, there are (digits - 1) digits remaining to be processed
                    // each digits after n must be bigger than n
                    Solution5_Process(sum + _np[n], number * 10 + n, digits - 1, n);
                }
            }
        }

        private void Solution5_ExamineNumberSum(System.Numerics.BigInteger number, System.Numerics.BigInteger sum)
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
