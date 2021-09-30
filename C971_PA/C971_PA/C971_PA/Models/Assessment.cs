using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace C971_PA.Models
{
    [Table("wgu_assessments")]
    public class Assessment
    {
        [PrimaryKey, AutoIncrement, Unique, NotNull, Column("assessment_key")]
        public int AssessmentKey { get; set; }
        [MaxLength(250), Unique, NotNull]
        public string Name { get; set; }
        [MaxLength(2),NotNull]
        public string Type { get; set; }
        [MaxLength(250),NotNull]
        public int CourseID { get; set; }
        [MaxLength(250), NotNull]
        public DateTime DueDate { get; set; }
        [MaxLength(1)]
        public int AssessmentDueNotification { get; set; }

        public static readonly List<string> PossibleTypes = new List<string> { "OA", "PA" };
    }
}
