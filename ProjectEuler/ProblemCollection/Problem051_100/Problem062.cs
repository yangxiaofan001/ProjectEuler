using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection.Problem051_100
{
    public class Problem062 : ProblemBase
    {
        public override string Description
        {
            get
            {
                return @"
Problem 62 - Cubic Permutations

The cube, 41063625 (345^3), can be permuted to produce two other cubes: 56623104 (384^3) and 66430123 (405^3). In fact, 41063623 is the smallest cube which has exactly three permutations of its digits which are also cube.

Find the smallest cube for which exactly five permutations of its digits are cube.
";
            }
        }

        public override int ProblemNumber
        {
            get
            {
                return 62;
            }
        }

        int[] GetSortedDigitArray(long a)
        {
            List<int> aDigits = new List<int>();
            while(a > 0)
            {
                aDigits.Add((int)( a % 10));
                a /= 10;
            }

            int[] aDigitArray = aDigits.ToArray();
            Array.Sort(aDigitArray);

            return aDigitArray;
        }

        bool CompareSortedDigitArrays(int[] aDigitArray, int[] bDigitArray)
        {
            if (aDigitArray.Length != bDigitArray.Length) return false;

            for(int i = 0; i < aDigitArray.Length; i ++)
                if(aDigitArray[i] != bDigitArray[i]) 
                    return false;

            return true;
        }

        public override string Solution1()
        {
            string answer = @"
assume the upperlimit is 99999^3 - not proved, if a solution is not found, need to extend the upperlimit (not coded)
init n as 345, where 345^3 is the first cube has exactly three permutations of its digits which are also cube.

increase n by 1 in each loop, 
add the kv pair [n, int[] sorted digit array of n] to a dictionary
look back in the dictionary, find cubes that have the same sorted digit array
if 4 such cubes are found, return the smallest number among the 4 + 1 numbers
            ";

            Console.WriteLine(answer);



            Dictionary<long, int[]> cubeList = new Dictionary<long, int[]>();
            long n = 345;

            while(n < 99999)
            {
                long cube = n * n * n;
                int [] sortedDigitArray = GetSortedDigitArray(cube);

                List<long> answerList = new List<long>{cube};

                int perm = 0;
                foreach(long k in cubeList.Keys)
                {
                    if (CompareSortedDigitArrays(cubeList[k], sortedDigitArray))
                    {
                        answerList.Add(k);
                        perm ++;
                    }
                }
                if (perm == 4)
                {
                    foreach(long l in answerList) Console.Write($"{l} ");
                    Console.WriteLine();
                    answer = answerList.Min(x => x).ToString();
                    break;
                }

                cubeList.Add(cube, sortedDigitArray);
                n ++;
            }

            return answer;
        }
    }
}
