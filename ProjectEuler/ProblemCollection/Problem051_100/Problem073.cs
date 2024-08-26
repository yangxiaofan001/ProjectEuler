using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace EulerProject.ProblemCollection
{
    public class Problem73 : ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 73;
            }
        }

        public override string Description
        {
            get
            {
                return @"Problem 73 - Counting Fractions in a Range
";
            }
        }

        int upperLimit = 12000;

        public override string Solution1()
        {
            string idea = @"
Example 2100:
    prime factor list {2, 3, 5, 7}

numbers grater than 1/3 * 2100 and less than 1/2 * 2100: 701 to 1049, total 349 numbers
minus
    numbers can be devided by 2: 702 - 1048, total 174 numbers
    numbers can be devided by 3: 702 - 1047, total 116 numbers
    numbers can be devided by 5: 705 - 1045, total 69 numbers
    numbers can be devided by 7: 707 - 1043, total 49 numbers
plus
    numbers can be devided by 2*3=6: 702 - 1044, total 58 numbers
    numbers can be devided by 2*5=10: 710 - 1040, total 34 numbers
    numbers can be devided by 2*7=14: 714 - 1036, total 24 numbers
    numbers can be devided by 3*5=15: 705 - 1035, total 23 numbers
    numbers can be devided by 3*7=21: 714 - 1029, total 16 numbers
    numbers can be devided by 5*7=35: 735 - 1015, total 9 numbers
minus
    numbers can be divided by 2*3*5 = 30: 720 - 1020, total 11 numbers
    numbers can be divided by 2*3*7 = 42: 714 - 1008, total 8 numbers
    numbers can be divided by 2*5*7 = 70: 770 - 980, total 4 numbers
    numbers can be divided by 3*5*7 = 105: 735 - 945, total 3 numbers
plus
    numbers can be divided by 2*3*5*7 = 210: 840 - 840, total 1 numbers



349 - (174+116+69+49) + (58+34+24+23+16+9) - (11+8+4+3) + 1 = 80

there is 80 numbers that
    . greater than 1/3 of 2100: >= 701
    . less than 1/2 of 2100: <=1049
    . coprime with 2100, that is, cannot be devided by 2, 3, 5, 7
";
Console.WriteLine(idea);

            int sqrt = (int)Math.Sqrt(upperLimit);
            
            int [] remains = new int[upperLimit + 1];
            for(int i = 0; i <= upperLimit; i ++) remains[i] = i;

            bool [] primeChecher = new bool[upperLimit + 1];
            for(int i = 0; i <= upperLimit; i ++) primeChecher[i] = true;

            List<List<int>> primeFactors = new List<List<int>>();
            for(int i = 0; i <= upperLimit; i ++) primeFactors.Add(new List<int>());

            for(int p = 2; p <= sqrt; p ++)
            {
                if (primeChecher[p])
                {
                    primeFactors[p].Add(p);
                    for(int j = 2 * p; j <= upperLimit; j += p)
                    {
                        primeChecher[j] = false;
                        primeFactors[j].Add(p);
                        int x = remains[j];
                        while(x % p== 0) x /= p;
                        remains[j] = x;
                    }
                }
            }

            for(int i = 2; i <= upperLimit; i ++)
            {
                if (remains[i] >= sqrt) primeFactors[i].Add(remains[i]);
            }

            //  List<int> phiList = Utils.GetAllPhiUnderP(upperLimit);

            BigInteger sum = 0;

            for(int i = 2; i <= upperLimit; i ++)
            {
                BigInteger lowerbound = i / 3 + 1;
                BigInteger  upperbound = i / 2 - (i % 2 == 0 ? 1 : 0);
                BigInteger count = upperbound - lowerbound + 1;
                
                BigInteger coprimeCount = count;
                for(int c = 1; c <= primeFactors[i].Count; c ++)
                {
                    List<List<int>> pListList = Utils.CombinationList<int>(primeFactors[i], c);
                    foreach(List<int> pList in pListList)
                    {
                        int sign = (c % 2 == 0) ? 1 : -1;
                        BigInteger prod = 1;
                        foreach(int p in pList) prod *= p;

                        BigInteger start = lowerbound % prod == 0 ? lowerbound : (lowerbound + prod - lowerbound % prod);
                        BigInteger end = upperbound % prod == 0 ? upperbound : (upperbound - upperbound % prod);

                        if (end >= start)
                            coprimeCount += sign * ((end - start) / prod + 1);
                    }
                }

                sum += coprimeCount;
            }

            return sum.ToString();
        }
    }
}
