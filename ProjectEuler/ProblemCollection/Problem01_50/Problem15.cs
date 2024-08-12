using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EulerProject.ProblemCollection
{
    public class Problem15 : ProblemBase
    {
        const int upperLimit = 20;

        public override string Description
        {
            get
            {
                return @"Starting in the top left corner of a 2×2 grid, and only being able to move to the right and down, 
                        there are exactly 6 routes to the bottom right corner.

                        How many such routes are there through a 20×20 grid?";
            }
        }

        public override int ProblemNumber
        {
            get
            {
                return 15;
            }
        }

        public override string Solution1()
        {
            // In any route, there are 2 * upperLimit steps
            // In each step, it each moves right or moves down
            // In any valid route, there must be [upperlimit] steps that moves right
            // So the solution is Combination(upperlimit * 2, upperlimit)

            System.Numerics.BigInteger routesCount = Utils.Combination(upperLimit * 2, upperLimit);

            return routesCount.ToString();
        }

    }
}
