using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EulerProject.ProblemCollection;

namespace EulerProject
{
    class Program
    {
        static void Main(string[] args)
        {
            int problemNumber = -1;

            Dictionary<int, Type> problemClasses = new Dictionary<int, Type>();

            List<Type> classList = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().ToList();

            foreach (Type t in classList)
            {
                if (t.IsSubclassOf(typeof(EulerProject.ProblemCollection.ProblemBase))
                    && !t.FullName.Contains("EulerProject.ProblemCollection.ProblemBase"))
                {
                    ProblemBase worker = ((ProblemBase)(System.Activator.CreateInstance(t)));
                    problemClasses.Add(worker.ProblemNumber, t);
                }
            }

            while (true)
            {
                Console.WriteLine("Enter a Euler project problem number. Enter 0 to exit.");
                if (!Int32.TryParse(Console.ReadLine(), out problemNumber)) continue;
                if (problemNumber == 0) break;

                if (!problemClasses.Keys.Contains(problemNumber)) continue;

                ProblemBase worker = ((ProblemBase)(System.Activator.CreateInstance(problemClasses[problemNumber])));
                SolveProblem(worker, problemNumber);
            }
        }

        private static void SolveProblem(ProblemBase worker, int problemNumber)
        {
            Console.WriteLine(worker.Description);
            System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();

            string answer;
            foreach (System.Reflection.MethodInfo solution in worker.GetType().GetMethods())
            {
                stopWatch.Reset();
                if (solution.Name.ToLower().StartsWith("solution"))
                {
                    try
                    {
                        stopWatch.Start();
                        answer = solution.Invoke(worker, null).ToString();
                        stopWatch.Stop();
                        Console.WriteLine("\nProblem " + problemNumber.ToString() + " " + solution.Name + "  answer = " + answer + ". Done in " + stopWatch.ElapsedMilliseconds.ToString() + " milliseconds");
                    }
                    catch (Exception ex)
                    {
                        if (ex != null && (ex is SolutionNotImplementedException || (ex.InnerException != null && ex.InnerException is SolutionNotImplementedException)))
                            continue;
                        else
                        {
                            Console.WriteLine(ex.Message);
                            if (ex.InnerException != null)
                                Console.WriteLine(ex.InnerException.Message); 
                        }
                    }
                }
            }

        }

    }
}
