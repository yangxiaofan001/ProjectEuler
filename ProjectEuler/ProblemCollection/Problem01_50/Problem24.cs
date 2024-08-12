using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection
{
    public class Problem24 : ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 24;
            }
        }

        public override string Description
        {
            get
            {
                return 
                    @"
                        A permutation is an ordered arrangement of objects. 
                        For example, 3124 is one possible permutation of the digits 1, 2, 3 and 4. 
                        If all of the permutations are listed numerically or alphabetically, we call it lexicographic order. 
                        The lexicographic permutations of 0, 1 and 2 are:

                        012   021   102   120   201   210

                        What is the millionth lexicographic permutation of the digits 0, 1, 2, 3, 4, 5, 6, 7, 8 and 9?
                    ";
            }
        }

        public override string Solution1()
        {
            // 0 xxx xxx xxx: 362880
            // 1 xxx xxx xxx: 362880    (725760)
            // === over 2 xxx xxx xxx: 362880   (1088640) == over
            // total 725760
            // 2 0xx xxx xxx: 40320     (766080)
            // 2 1xx xxx xxx: 40320     (806400)
            // 2 3xx xxx xxx: 40320     (846720)
            // 2 4xx xxx xxx: 40320     (887040)
            // 2 5xx xxx xxx: 40320     (927360)
            // 2 6xx xxx xxx: 40320     (967680)
            // === over 2 7xx xxx xxx: 40320     (1038000) == over
            // 2 70x xxx xxx: 5040     (972720)
            // 2 71x xxx xxx: 5040     (977760)
            // 2 73x xxx xxx: 5040     (982800)
            // 2 74x xxx xxx: 5040     (987840)
            // 2 75x xxx xxx: 5040     (992880)
            // 2 76x xxx xxx: 5040     (997920)
            // === over 2 78x xxx xxx: 5040     (1002960) == over
            // 2 780 xxx xxx: 720      (998640)
            // 2 781 xxx xxx: 720      (999360)
            // === over 2 783 xxx xxx: 720      (1000080) == over
            // 2 783 0xx xxx: 120      (999480)
            // 2 783 1xx xxx: 120      (999600)
            // 2 783 4xx xxx: 120      (999720)
            // 2 783 5xx xxx: 120      (999840)
            // 2 783 6xx xxx: 120      (999960)
            // === over 2 783 9xx xxx: 120      (1000080) == over
            // 2 783 90x xxx: 24       (999984)
            // === over 2 784 91x xxx: 24       (1000008) === over
            // 2 783 910 xxx: 6         (999990)
            // 2 783 914 xxx: 6         (999996)
            // === over 2 783 915 xxx: 24       (1000002) === over
            // 2 783 915 0xx: 2         (999998)
            // 2 783 915 4xx: 2         (1000000)

            // max: 2 783 915 460


            string result = "";
            int currentLevel = 9;
            System.Numerics.BigInteger position = 0;

            while (result.Length < 10)
            { 
                System.Numerics.BigInteger currentPermutation = (long)(Utils.Permutation(currentLevel));
                bool bFinished = false;

                for (int i = 0; i < 10; i++)
                {
                    if (result.Contains((char)('0' + i)))
                        continue;

                    if (position + currentPermutation > 1000000)
                    {
                        result = result + (char)('0' + i);
                        currentLevel--;
                        break;
                    }
                    else if (position + currentPermutation == 1000000)
                    {
                        result = result + (char)('0' + i);
                        bFinished = true;
                        break;
                    }
                    else
                        position += currentPermutation;
                }

                if (bFinished) break;
            }

            for (int i = result.Length; i < 10; i++)
            {
                for (int x = 9; x >= 0; x--)
                {
                    if (!result.Contains((char)('0' + x)))
                    {
                        result = result + (char)('0' + x);
                        break;
                    }
                }
            }

            return result;
        }
    }
}
