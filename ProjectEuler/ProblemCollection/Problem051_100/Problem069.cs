using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace EulerProject.ProblemCollection
{
    public class Problem69 : ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 69;
            }
        }

        public override string Description
        {
            get
            {
                return @"
Problem 69 - Totient Maximum
                    ";
            }
        }

        int upperLimit = 1000000;

        public override string Solution1()
        {
            string idea = @"
phi function formula:

if n = p1^a1 * p2^a2 * ... * pk^ak, 

phi = n * (1 - 1/p1) * (1 - 1 / p2) * ... * (1 - pk)

notice that each p_i only appear once in the foluma, does not mattaer what the exponent a_i is
";

Console.WriteLine(idea);
            int sqrt = (int)Math.Sqrt(upperLimit);
            bool[] primeChecker = new bool[sqrt + 1];
            for (int i = 0; i <= sqrt; i++) primeChecker[i] = true;
            primeChecker[0] = false;
            primeChecker[1] = false;

            Dictionary<int, List<int>> primeFactorsMap = new Dictionary<int, List<int>>();
            for (int i = 0; i <= upperLimit; i++) primeFactorsMap.Add(i, new List<int>());

            for (int i = 2; i <= sqrt; i++)
            {
                if (primeChecker[i])
                {
                    for (int n = 2 * i; n <= upperLimit; n += i)
                    {
                        if (n <= sqrt) primeChecker[n] = false;
                        primeFactorsMap[n].Add(i);
                    }
                }
            }

            double maxPhi = 0;
            int iWithMaxPhi = 0;
            foreach (int i in primeFactorsMap.Keys)
            {
                double phi = (double)i;
                foreach (int p in primeFactorsMap[i])
                {
                    phi = phi / (double)p * (double)(p - 1);
                }
                double t = (double)i / phi;

                if (t > maxPhi)
                {
                    maxPhi = t;
                    iWithMaxPhi = i;
                }
            }

            return $"{iWithMaxPhi} has the maximum n/phi(n) of {maxPhi}";
        }
    }
}
