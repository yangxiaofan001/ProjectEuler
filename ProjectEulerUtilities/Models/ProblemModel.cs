using Postgrest.Attributes;
using Postgrest.Models;
using System;

namespace ProjectEulerUtilities
{
    [Table("problems")]
    public class ProblemModel : BaseModel
    {
        [PrimaryKey("id", false)] // false 表示 ID 由数据库生成
        public Guid Id { get; set; }

        [Column("number")]
        public int Number { get; set; }

        [Column("title")] // 新加的列
        public string Title { get; set; }

        [Column("final_answer")]
        public string FinalAnswer { get; set; }

        [Column("solved_on")]
        public DateTime? SolvedOn { get; set; }
    }
}