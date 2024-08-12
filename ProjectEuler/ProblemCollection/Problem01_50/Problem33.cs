using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection
{
    public class Problem33 : ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 33;
            }
        }

        public override string Description
        {
            get
            {
                return @"
                    Digit cancelling fractions
                    Problem 33
                    The fraction 49/98 is a curious fraction, as an inexperienced mathematician in attempting to simplify it 
                    may incorrectly believe that 49/98 = 4/8, which is correct, is obtained by cancelling the 9s.

                    We shall consider fractions like, 30/50 = 3/5, to be trivial examples.

                    There are exactly four non-trivial examples of this type of fraction, less than one in value, 
                    and containing two digits in the numerator and denominator.

                    If the product of these four fractions is given in its lowest common terms, find the value of the denominator.
                    ";
            }
        }

        private List<int> primeNumberList = new List<int>();
        void ProducePrimeNumberList(int upperLimit)
        {
            for (int i = 2; i <= upperLimit / 2; i++)
            {
                if (Utils.IsPrime(i))
                    primeNumberList.Add(i);
            }
        }

        public override string Solution1()
        {
            int productNumerator = 1;
            int productDenominator = 1;
            ProducePrimeNumberList(99 / 2);

            for (int numerator = 10; numerator <= 98; numerator++)
            {
                for (int denominator = numerator + 1; denominator <= 99; denominator++)
                {
                    if (numerator % 10 == 0 && denominator % 10 == 0) continue;

                    int dN = numerator / 10;
                    int rN = numerator % 10;

                    int dD = denominator / 10;
                    int rD = denominator % 10;

                    int smallNumerator = rN;
                    int smallDenominator = rN;

                    if (dN == dD)
                    {
                        if (rN == rD) continue;

                        smallNumerator = rN;
                        smallDenominator = rD;
                    }
                    else if (dN == rD)
                    {
                        if (rN == dD) continue;

                        smallNumerator = rN;
                        smallDenominator = dD;
                    }
                    else if (rN == dD)
                    {
                        if (dN == rD) continue;

                        smallNumerator = dN;
                        smallDenominator = rD;
                    }
                    else if (rN == rD)
                    {
                        if (dN == dD) continue;

                        smallNumerator = dN;
                        smallDenominator = dD;
                    }
                    else
                        continue;

                    if (numerator * smallDenominator == denominator * smallNumerator)
                    {
                        productNumerator *= numerator;
                        productDenominator *= denominator;
                        Console.WriteLine(numerator + " / " + denominator + " = " + smallNumerator + " / " + smallDenominator);
                    }                    
                }
            }

            int simpleProductNumerator = 1;
            int simpleProductDenominator = 1;
            SimplizeFraction(productNumerator, productDenominator, ref simpleProductNumerator, ref simpleProductDenominator);


            return simpleProductDenominator.ToString();
        }

        void SimplizeFraction(int numerator, int denominator, ref int simpleNumerator, ref int simpleDenominator)
        {
            simpleNumerator = numerator;
            simpleDenominator = denominator;

            if (numerator == 0)
            {
                simpleNumerator = 0;
                simpleDenominator = 1;
            }
            else if (denominator % numerator == 0)
            {
                simpleNumerator = 1;
                simpleDenominator = denominator / numerator;
            }
            else
            {
                foreach(int p in primeNumberList.Where(x => x <= numerator / 2))
                {
                    if (Utils.IsPrime(p))
                    {
                        while (simpleNumerator % p == 0 && simpleDenominator % p == 0)
                        {
                            simpleNumerator /= p;
                            simpleDenominator /= p;
                        }
                    }
                }
            }
        }

        public override string Solution2()
        {
            int productNumerator = 1;
            int productDenominator = 1;
            ProducePrimeNumberList(99 / 2);

            for (int numerator = 1; numerator <= 8; numerator++)
            {
                for (int denominator = numerator + 1; denominator <= 9; denominator++)
                {
                    for (int commonNumber = 1; commonNumber <= 9; commonNumber++)
                    {
                        int[] bigNumerators = new int[]{
                            numerator * 10 + commonNumber,
                            numerator * 10 + commonNumber,
                            numerator + commonNumber * 10,
                            numerator + commonNumber * 10};
                        int[] bigDenominators = new int[]{
                            denominator * 10 + commonNumber,
                            denominator + commonNumber * 10,
                            denominator * 10 + commonNumber,
                            denominator + commonNumber * 10};

                        for (int i = 0; i < 4; i++)
                        {
                            if (numerator * bigDenominators[i]  == denominator * bigNumerators[i])
                            {
                                productNumerator *= numerator;
                                productDenominator *= denominator;
                                Console.WriteLine(bigNumerators[i] + " / " + bigDenominators[i] + " = " + numerator + " / " + denominator);
                            }
                        }
                    }
                }
            }

            int simpleProductNumerator = 1;
            int simpleProductDenominator = 1;
            SimplizeFraction(productNumerator, productDenominator, ref simpleProductNumerator, ref simpleProductDenominator);


            return simpleProductDenominator.ToString();
        }
    }
}
