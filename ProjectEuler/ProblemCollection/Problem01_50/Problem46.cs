using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection
{
    public class Problem46 : ProblemBase
    {
        public override string Description
        {
            get
            {
                return @"
                    Goldbach's other conjecture
                    Problem 46
                    It was proposed by Christian Goldbach that every odd composite number can be written as the sum of a prime and twice a square.

                    9 = 7 + 2×12
                    15 = 7 + 2×22
                    21 = 3 + 2×32
                    25 = 7 + 2×32
                    27 = 19 + 2×22
                    33 = 31 + 2×12

                    It turns out that the conjecture was false.

                    What is the smallest odd composite that cannot be written as the sum of a prime and twice a square?
                    ";
            }
        }

        public override int ProblemNumber
        {
            get
            {
                return 46;
            }
        }

        public override string Solution1()
        {
            int compositeNumber = 9;
            int factor = 2;
            bool bFound = false;

            int nextFactor = factor + 1;
            int nextBaseNumber = nextFactor * nextFactor * 2;

            while (!bFound)
            {

                while (compositeNumber < nextBaseNumber)
                {
                    bool bFoundPrime = false;
                    for (int i = factor; i >= 1; i--)
                    {
                        int x = compositeNumber - 2*i*i;
                        if (Utils.IsPrime(x))
                        {
                            bFoundPrime = true;
                            break;
                        }
                    }

                    if (!bFoundPrime)
                    {
                        bFound = true;
                        break;
                    }

                    compositeNumber = FindNextCompositeNumber(compositeNumber);
                }

                nextFactor++;
                nextBaseNumber = nextFactor * nextFactor * 2;
                factor ++;
            }

            return compositeNumber.ToString();
        }

        private int FindNextCompositeNumber(int compositeNumber)
        {
            while (true)
            {
                compositeNumber += 2;
                if (!Utils.IsPrime(compositeNumber))
                    return compositeNumber;
            }
        }
    }
}
