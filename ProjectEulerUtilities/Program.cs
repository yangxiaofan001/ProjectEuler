using System;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Linq;

namespace ProjectEulerUtilities
{
    class Program
    {
        private const string SupabaseUrl = "https://yqpfmbpwytzhkdzaidof.supabase.co";
        private const string SupabaseKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InlxcGZtYnB3eXR6aGtkemFpZG9mIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NzM1MTIzMDcsImV4cCI6MjA4OTA4ODMwN30.ebsktKIjW9coooVlORcCrGf4DdRN4fXMlWDxk1oTyy8";

        static async Task Main(string[] args)
        {
            Console.WriteLine("=== Project Euler Utilities ===");
            Console.WriteLine("1. 爬取并更新题目标题 (Batch Update Titles)");
            Console.Write("\n请选择操作: ");
            
            var choice = Console.ReadLine();

            if (choice == "1")
            {
                await BatchUpdateTitles();
            }
        }

static async Task BatchUpdateTitles()
{
    // 记得 Trim() 你的凭据
    var client = new Supabase.Client(SupabaseUrl.Trim(), SupabaseKey.Trim());
    await client.InitializeAsync();

    using var httpClient = new HttpClient();
    httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36");

    int currentPage = 1;
    int maxPage = 1; // 默认先抓第一页

    do
    {
        Console.WriteLine($"\n[正在处理第 {currentPage} 页...]");
        string archiveUrl = $"https://projecteuler.net/archives;page={currentPage}";

        try
        {
            var html = await httpClient.GetStringAsync(archiveUrl);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            // --- 智能页数检测 (仅在第一页执行一次) ---
            if (currentPage == 1)
            {
                var paginationNodes = doc.DocumentNode.SelectNodes("//div[contains(@class, 'pagination')]//a");
                if (paginationNodes != null)
                {
                    Console.WriteLine($"paginationNodes.Count = {paginationNodes.Count}");
                    // 尝试从导航栏找到最大的数字
                    var pageNumbers = paginationNodes
                        .Select(n => n.FirstChild.InnerText)
                        .Where(t => int.TryParse(t, out _))
                        .Select(int.Parse);
                    Console.WriteLine($"pageNumbers.Count = {pageNumbers.Count()}");
                    
                    if (pageNumbers.Any())
                    {
                        maxPage = pageNumbers.Max();
                        Console.WriteLine($"🔍 检测到总页数为: {maxPage}");
                    }
                }
                else 
                Console.WriteLine("paginationNodes is null");
            }

            // --- 抓取题目逻辑 ---
            var rows = doc.DocumentNode.SelectNodes("//table[@id='problems_table']//tr");
            if (rows != null)
            {
                foreach (var row in rows)
                {
                    var cells = row.SelectNodes("td");
                    if (cells != null && cells.Count >= 2)
                    {
                        if (int.TryParse(cells[0].InnerText, out int id))
                        {
                            string title = cells[1].InnerText.Trim();
                            
                            // 更新数据库
                            var resp = await client.From<ProblemModel>().Where(x => x.Number == id).Get();
                            var problem = resp.Model;

                            if (problem != null && string.IsNullOrEmpty(problem.Title))
                            {
                                problem.Title = title;
                                await client.From<ProblemModel>().Update(problem);
                                Console.WriteLine($"✅ Updated #{id}: {title}");
                            }
                        }
                    }
                }
            }

            currentPage++;
            
            // 礼貌性等待，防止请求过快
            if (currentPage <= maxPage) await Task.Delay(1500);

        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ 处理第 {currentPage} 页时出错: {ex.Message}");
            break; // 出错则跳出循环，防止死循环
        }

    } while (currentPage <= maxPage);

    Console.WriteLine("\n🎉 任务圆满完成！");
}
    }
}