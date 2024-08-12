using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection
{
    public class Problem19 : ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 19;
            }
        }

        public override string Description
        {
            get
            {
                return @"
                    You are given the following information, but you may prefer to do some research for yourself.

                    1 Jan 1900 was a Monday.
                    Thirty days has September,
                    April, June and November.
                    All the rest have thirty-one,
                    Saving February alone,
                    Which has twenty-eight, rain or shine.
                    And on leap years, twenty-nine.
                    A leap year occurs on any year evenly divisible by 4, but not on a century unless it is divisible by 400.
                    How many Sundays fell on the first of the month during the twentieth century (1 Jan 1901 to 31 Dec 2000)?";
            }
        }

        public override string Solution1()
        {
            DateTime dt = new DateTime(1901, 1, 1);
            while (dt.DayOfWeek != DayOfWeek.Sunday) 
                dt = dt.AddDays(1);
            List<DateTime> sundaysOnFirstDayOfMonth = new List<DateTime>();

            while (dt < new DateTime(2001, 1, 1))
            {
                if (dt.Day == 1)
                    sundaysOnFirstDayOfMonth.Add(dt);

                dt = dt.AddDays(7);
            }

            return sundaysOnFirstDayOfMonth.Count.ToString();
        }

        public override string Solution2()
        {
            // put all first days in a list
            List<int> firstDayOfMonths = new List<int> { 0 };
            for (int year = 1901; year < 2001; year++)
            {
                firstDayOfMonths.Add(firstDayOfMonths[firstDayOfMonths.Count - 1] + 31);
                if (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0))
                    firstDayOfMonths.Add(firstDayOfMonths[firstDayOfMonths.Count - 1] + 29);
                else
                    firstDayOfMonths.Add(firstDayOfMonths[firstDayOfMonths.Count - 1] + 28);
                firstDayOfMonths.Add(firstDayOfMonths[firstDayOfMonths.Count - 1] + 31);
                firstDayOfMonths.Add(firstDayOfMonths[firstDayOfMonths.Count - 1] + 30);
                firstDayOfMonths.Add(firstDayOfMonths[firstDayOfMonths.Count - 1] + 31);
                firstDayOfMonths.Add(firstDayOfMonths[firstDayOfMonths.Count - 1] + 30);
                firstDayOfMonths.Add(firstDayOfMonths[firstDayOfMonths.Count - 1] + 31);
                firstDayOfMonths.Add(firstDayOfMonths[firstDayOfMonths.Count - 1] + 31);
                firstDayOfMonths.Add(firstDayOfMonths[firstDayOfMonths.Count - 1] + 30);
                firstDayOfMonths.Add(firstDayOfMonths[firstDayOfMonths.Count - 1] + 31);
                firstDayOfMonths.Add(firstDayOfMonths[firstDayOfMonths.Count - 1] + 30);
                firstDayOfMonths.Add(firstDayOfMonths[firstDayOfMonths.Count - 1] + 31);
            }

            // check if each day in the list is a sunday
            int sundaysOnFirstDayOfMonths = 0;
            foreach (int dayFrom19010101 in firstDayOfMonths)
            {
                // -5
                // First sunday in range is 1901.01.06, which is 5 days later than 1901.01.01
                if ((dayFrom19010101 - 5) % 7 == 0)
                    sundaysOnFirstDayOfMonths++;
            }

            return sundaysOnFirstDayOfMonths.ToString();
        }
    }
}
