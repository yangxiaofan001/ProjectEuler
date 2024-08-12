using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection
{
    public class Problem18 :ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 18;
            }
        }

        public override string Description
        {
            get
            {
                return @"
                    By starting at the top of the triangle below and moving to adjacent numbers on the row below, the maximum total from top to bottom is 23.

                    3
                    74
                    246
                    8593

                    That is, 3 + 7 + 4 + 9 = 23.

                    Find the maximum total from top to bottom of the triangle below:

                    75
                    95 64
                    17 47 82
                    18 35 87 10
                    20 04 82 47 65
                    19 01 23 75 03 34
                    88 02 77 73 07 63 67
                    99 65 04 28 06 16 70 92
                    41 41 26 56 83 40 80 70 33
                    41 48 72 33 47 32 37 16 94 29
                    53 71 44 65 25 43 91 52 97 51 14
                    70 11 33 28 77 73 17 78 39 68 17 57
                    91 71 52 38 17 14 91 43 58 50 27 29 48
                    63 66 04 68 89 53 67 30 73 16 69 87 40 31
                    04 62 98 27 23 09 70 98 73 93 38 53 60 04 23

                    NOTE: As there are only 16384 routes, it is possible to solve this problem by trying every route. However, Problem 67, is the same challenge with a triangle containing one-hundred rows; it cannot be solved by brute force, and requires a clever method! ;o)
                    ";
            }
        }

        List<string> pyramid = new List<string>();
        static long calledTimes;
        DateTime dtStart;
        List<string> stringPyramid = new List<string>{
                "75", 
                "95 64", 
                "17 47 82", 
                "18 35 87 10", 
                "20 04 82 47 65", 
                "19 01 23 75 03 34", 
                "88 02 77 73 07 63 67", 
                "99 65 04 28 06 16 70 92", 
                "41 41 26 56 83 40 80 70 33", 
                "41 48 72 33 47 32 37 16 94 29", 
                "53 71 44 65 25 43 91 52 97 51 14", 
                "70 11 33 28 77 73 17 78 39 68 17 57", 
                "91 71 52 38 17 14 91 43 58 50 27 29 48", 
                "63 66 04 68 89 53 67 30 73 16 69 87 40 31", 
                "04 62 98 27 23 09 70 98 73 93 38 53 60 04 23", };

        public override string Solution1()
        {
            dtStart = DateTime.Now;

            calledTimes = 0;


            List<List<int>> intPyramid = new List<List<int>>();

            foreach (string level in stringPyramid)
            {
                List<string> stringLevel = level.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                List<int> intLevel = new List<int>();
                foreach (string s in stringLevel) intLevel.Add(Convert.ToInt32(s));
                intPyramid.Add(intLevel);
            }

            long result = Solution1MaxTotal(intPyramid);

            Console.WriteLine("Solution1MaxTotal was called " + calledTimes.ToString() + " times.");
            return result.ToString();
        }

        private long Solution1MaxTotal(List<List<int>> pyramid)
        {
            calledTimes++;

            if (dtStart.AddMilliseconds(2000) < DateTime.Now)
                throw new ApplicationException("Solution1MaxTotal takes too much time.");

            long result = 0;
            if (pyramid.Count == 1)
                result = (pyramid[0][0]);
            else
            {
                List<List<int>> lPyramid = LeftPyramid(pyramid);
                List<List<int>> rPyramid = RightPyramid(pyramid);

                long lTotal = Solution1MaxTotal(lPyramid);
                long rTotal = Solution1MaxTotal(rPyramid);

                result = (pyramid[0][0]) + Math.Max(lTotal, rTotal);
            }

            return result;
        }

        private List<List<int>> LeftPyramid(List<List<int>> pyramid)
        {
            if (pyramid.Count < 2) throw new ApplicationException("Cannot get left pyramid from a pyramid with only one level.");

            List<List<int>> lPyramid = new List<List<int>>();
            for (int i = 1; i < pyramid.Count; i++)
            {
                List<int> intLevel = new List<int>();
                for (int j = 0; j < pyramid[i].Count - 1; j++)
                    intLevel.Add(pyramid[i][j]);

                lPyramid.Add(intLevel);
            }

            return lPyramid;
        }

        private List<List<int>> RightPyramid(List<List<int>> pyramid)
        {
            if (pyramid.Count < 2) throw new ApplicationException("Cannot get left pyramid from a pyramid with only one level.");

            List<List<int>> rPyramid = new List<List<int>>();
            for (int i = 1; i < pyramid.Count; i++)
            {
                List<int> intLevel = new List<int>();
                for (int j = 1; j < pyramid[i].Count; j++)
                    intLevel.Add(pyramid[i][j]);

                rPyramid.Add(intLevel);
            }

            return rPyramid;
        }

        public override string Solution2()
        {
            dtStart = DateTime.Now;

            calledTimes = 0;


            List<List<int>> intPyramid = new List<List<int>>();

            foreach (string level in stringPyramid)
            {
                List<string> stringLevel = level.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                List<int> intLevel = new List<int>();
                foreach (string s in stringLevel) intLevel.Add(Convert.ToInt32(s));
                intPyramid.Add(intLevel);
            }

            long result = Solution2MaxTotal(intPyramid);

            Console.WriteLine("Solution2MaxTotal was called " + calledTimes.ToString() + " times.");
            return result.ToString();
        }

        private long Solution2MaxTotal(List<List<int>> pyramid)
        {
            calledTimes++;

            //if (dtStart.AddMilliseconds(2000) < DateTime.Now)
            //    throw new ApplicationException("Solution2MaxTotal takes too much time.");

            long result = 0;
            if (pyramid.Count == 1)
                result = (pyramid[0][0]);
            else if (pyramid.Count == 2)
                result = pyramid[0][0] + Math.Max(pyramid[1][0], pyramid[1][1]);
            else
            {
                if (pyramid.Count == 3)
                {
                    int x = 1;
                }

                List<List<int>> lPyramid = LeftPyramid(pyramid);
                List<List<int>> rPyramid = RightPyramid(pyramid);

                List<List<int>> llPyramid = LeftPyramid(lPyramid);
                List<List<int>> rrPyramid = RightPyramid(rPyramid);

                List<List<int>> sharedPyramid = RightPyramid(lPyramid);
                long sharedTotal = Solution2MaxTotal(sharedPyramid);

                long lTotal = lPyramid[0][0] + Math.Max(Solution2MaxTotal(llPyramid), sharedTotal);
                long rTotal = rPyramid[0][0] + Math.Max(Solution2MaxTotal(rrPyramid), sharedTotal);

                result = (pyramid[0][0]) + Math.Max(lTotal, rTotal);
            }

            return result;
        }


        public override string Solution3()
        {
            List<List<int>> intPyramid = new List<List<int>>();

            foreach (string level in stringPyramid)
            {
                List<string> stringLevel = level.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                List<int> intLevel = new List<int>();
                foreach (string s in stringLevel)
                {
                    intLevel.Add(Convert.ToInt32(s));
                }
                intPyramid.Add(intLevel);
            }

            while (true)
            {
                List<int> sumLine = new List<int>();
                for (int i = 0; i < intPyramid[intPyramid.Count - 2].Count; i++)
                {
                    intPyramid[intPyramid.Count - 2][i] =
                        intPyramid[intPyramid.Count - 2][i] +
                        Math.Max(intPyramid[intPyramid.Count - 1][i], intPyramid[intPyramid.Count - 1][i + 1]);
                }

                intPyramid.RemoveAt(intPyramid.Count - 1);

                if (intPyramid.Count == 1)
                    break;
            }

            return intPyramid[0][0].ToString();
        }

    }
}
