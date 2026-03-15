using Postgrest.Attributes;
using Postgrest.Models;
using System;

namespace ProjectEulerUtilities
{
    [Table("solutions")]
    public class SolutionModel : BaseModel
    {
        [PrimaryKey("id", false)]
        public Guid Id { get; set; }

        [Column("problem_id")]
        public Guid ProblemId { get; set; }

        [Column("solution_name")]
        public string SolutionName { get; set; }

        [Column("answer")]
        public string Answer { get; set; }

        [Column("execution_time_ms")]
        public long ExecutionTimeMs { get; set; }
    }
}