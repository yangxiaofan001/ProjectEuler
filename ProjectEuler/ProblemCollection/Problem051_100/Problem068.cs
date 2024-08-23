using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace EulerProject.ProblemCollection
{
    public class Problem68 : ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 68;
            }
        }

        public override string Description
        {
            get
            {
                return @"
Problem 68 - Magic 5-gon ring
                    ";
            }
        }

        public override string Solution1()
        {
            string idea = @"
Try number at each position: outer ring a1 a2 a3 a4 a5, inner ring a6 a7 a8 a9 a10

rules: 
    no repeat
    10 not in inner ring, otherwise the result is not a 16-digit number
    a1 + a6 + a7 = a2 + a7 + a8 = a3 + a8 + a9 = a4 + a9 + a10 = a5 + a10 + a6
    a1 is the smallest number in the outer ring

Brutal force takes several milliseconds    
";

Console.WriteLine(idea);
            List<int> availableNumbers0 = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            BigInteger maxX = 0;

            for (int a1 = 1; a1 <= 10; a1++)
            {
                List<int> availableNumbers1 = new List<int>(availableNumbers0);
                availableNumbers1.Remove(a1);

                for (int a6 = 1; a6 <= 9; a6++)
                {
                    if (!availableNumbers1.Contains(a6)) continue;
                    List<int> availableNumbers2 = new List<int>(availableNumbers1);
                    availableNumbers2.Remove(a6);

                    for (int a7 = 1; a7 <= 9; a7++)
                    {
                        if (!availableNumbers2.Contains(a7)) continue;
                        List<int> availableNumbers3 = new List<int>(availableNumbers2);
                        availableNumbers3.Remove(a7);

                        int sum = a1 + a6 + a7;

                        for (int a2 = 1; a2 <= 10; a2++)
                        {
                            if (!availableNumbers3.Contains(a2)) continue;
                            List<int> availableNumbers4 = new List<int>(availableNumbers3);
                            availableNumbers4.Remove(a2);

                            int a8 = sum - a2 - a7;
                            if (!availableNumbers4.Contains(a8))
                            {

                                continue;
                            }
                            if (a8 == 10) continue;
                            List<int> availableNumbers5 = new List<int>(availableNumbers4);
                            availableNumbers5.Remove(a8);

                            for (int a3 = 1; a3 <= 10; a3++)
                            {
                                if (!availableNumbers5.Contains(a3)) continue;

                                List<int> availableNumbers6 = new List<int>(availableNumbers5);
                                availableNumbers6.Remove(a3);

                                int a9 = sum - a3 - a8;
                                if (!availableNumbers6.Contains(a9)) continue;
                                if (a9 == 10) continue;
                                List<int> availableNumbers7 = new List<int>(availableNumbers6);
                                availableNumbers7.Remove(a9);
                                for (int a4 = 1; a4 <= 10; a4++)
                                {
                                    if (!availableNumbers7.Contains(a4)) continue;
                                    List<int> availableNumbers8 = new List<int>(availableNumbers7);
                                    availableNumbers8.Remove(a4);

                                    int a10 = a3 + a8 + a9 - a4 - a9;
                                    if (!availableNumbers8.Contains(a10)) continue;
                                    if (a10 == 10) continue;
                                    availableNumbers8.Remove(a10);
                                    int a5 = a4 + a9 + a10 - a6 - a10;
                                    if (!availableNumbers8.Contains(a5)) continue;

                                    if (a1 < a2 && a1 < a3 && a1 < a4 && a1 < a5)
                                    {
                                        List<int> solutionNumbers = new List<int>{
                                            a1, a6, a7,
                                            a2, a7, a8,
                                            a3, a8, a9,
                                            a4, a9, a10,
                                            a5, a10, a6
                                        };

                                        BigInteger x = 0;
                                        foreach (int n in solutionNumbers)
                                            x = x * (n == 10 ? 100 : 10) + n;
                                        if (x > maxX) maxX = x;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return maxX.ToString();


        }
    }
}
