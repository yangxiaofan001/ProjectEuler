using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace EulerProject.ProblemCollection
{
    public class Problem79 : ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 79;
            }
        }

        public override string Description
        {
            get
            {
                return @"Problem 79 - Passcode Derivation

A common security method used for online banking is to ask the user for three random characters from a passcode. 
For example, if the passcode was 531278, they may ask for the 2nd, 3rd, and 5th characters; the expected reply would be: 317.

The text file, keylog.txt, contains fifty successful login attempts.

Given that the three characters are always asked for in order, analyse the file so as to determine the shortest possible secret passcode of unknown length.                    
";
            }
        }

        public override string Solution1()
        {
            string idea = @"
each iteration:
    loop through each line [c1c2c3], use LINQ to find lines that has c1 not in the 1st place
    If found - c1 is not the first char in the passcode
    If not found - c1 is the next first char in the passcode, remove c1 in all lines

repeat, until all lines are empty
";

Console.WriteLine(idea);

            string line = "";
            string passcode = "";
            List<string> lines = new List<string>();

            System.IO.StreamReader sr = new System.IO.StreamReader("Files/0079_keylog.txt");
            while((line = sr.ReadLine())!= null) lines.Add(line);
            sr.Close();

            List<char> invalidNumbers = new List<char>();
            List<char> validNumbers = new List<char>();

            while (lines.Any(l => l.Length > 0))
            {
                invalidNumbers = new List<char>();
                validNumbers = new List<char>();
                for(int i = 0; i < lines.Count; i ++)
                {
                    if (string.IsNullOrEmpty(lines[i])) continue;
                    if (invalidNumbers.Contains(lines[i][0])) continue;
                    if (validNumbers.Contains(lines[i][0])) continue;
                    if (lines.Any(l => !string.IsNullOrEmpty(l) && l.IndexOf(lines[i][0]) >  0))
                    {
                        invalidNumbers.Add(lines[i][0]);
                        continue;
                    }

                    validNumbers.Add(lines[i][0]);
                }

                if (validNumbers.Count > 1) 
                {
                    string msg = "There are more than one possible first char:";
                    foreach(char c in validNumbers)
                        msg = msg + $"{c} ";
                    
                    throw new Exception(msg);
                }
                else
                {
                    Console.WriteLine($"There are exactly one possible first char: {validNumbers[0]}");
                    passcode = passcode + validNumbers[0];
                }

                foreach(char c in validNumbers)
                {
                    for(int i = 0; i < lines.Count; i ++)
                        if (!string.IsNullOrEmpty(lines[i]) && lines[i][0] == c)
                        lines[i] = lines[i].Substring(1);
                }
            }

            return passcode;
        }
    }
}
