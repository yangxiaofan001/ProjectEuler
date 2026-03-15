using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EulerProject.ProblemCollection;
using System.Threading.Tasks;

namespace EulerProject
{
    class Program
    {
        static async Task Main(string[] args)
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
                await SolveProblem(worker, problemNumber);
            }
        }

        private static async Task SolveProblem(ProblemBase worker, int problemNumber)
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
                        Console.WriteLine("\nProblem " + problemNumber.ToString() + " " + solution.Name + "  answer = " + answer + ". Done in " + stopWatch.ElapsedMilliseconds.ToString() + " milliseconds.\n");

                        if (!String.IsNullOrEmpty(answer))
                        await SyncToSupabase(problemNumber, solution.Name, answer, stopWatch.ElapsedMilliseconds);
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

        private static async Task SyncToSupabase(int num, string solName, string ans, long ms)
        {
            var url = "https://yqpfmbpwytzhkdzaidof.supabase.co";
            var key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InlxcGZtYnB3eXR6aGtkemFpZG9mIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NzM1MTIzMDcsImV4cCI6MjA4OTA4ODMwN30.ebsktKIjW9coooVlORcCrGf4DdRN4fXMlWDxk1oTyy8";
            var client = new Supabase.Client(url, key);
            await client.InitializeAsync();

            // 1. 获取对应的 Problem 记录
            var problemResp = await client.From<ProblemModel>()
                                        .Where(x => x.Number == num)
                                        .Get();
            Console.WriteLine("Retrieved Problem");
            var problem = problemResp.Model;

            if (problem == null) return;

            // 2. 核心逻辑：检查该解法是否已经记录过
            var existingSol = await client.From<SolutionModel>()
                                        .Where(x => x.ProblemId == problem.Id)
                                        .Where(x => x.SolutionName == solName)
                                        .Get();

            // 如果该解法已存在，直接静默退出，不打扰用户
            if (existingSol.Models.Count > 0) 
            {
                return; 
            }

            // 3. 只有新解法才询问
            Console.WriteLine($"\n[New Solution Detected: {solName}]");
            Console.WriteLine("是否同步此结果到 Supabase? (y/n)");
            
            if (Console.ReadLine().ToLower() == "y")
            {
                // 更新 Problem 状态
                problem.SolvedOn = DateTime.Now;
                problem.FinalAnswer = ans;
                await client.From<ProblemModel>().Update(problem);

                // 插入新的 Solution 记录
                var sol = new SolutionModel {
                    ProblemId = problem.Id,
                    SolutionName = solName,
                    Answer = ans,
                    ExecutionTimeMs = ms
                };
                await client.From<SolutionModel>().Insert(sol);

                Console.WriteLine("✅ 同步成功！");
            }
        }
            }
}
