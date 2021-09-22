using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace C971_PA.Models
{
    [Table("wgu_courses")]
    public class Course
    {

        [PrimaryKey, AutoIncrement, Unique, NotNull, Column("course_key")]
        public int CourseKey { get; set; }
        [MaxLength(250), Unique, NotNull]
        public string Name { get; set; }
        [MaxLength(1000), NotNull]
        public string Description { get; set; }
        [MaxLength(250)]
        public int? TermID { get; set; }
        [MaxLength(250), NotNull]
        public int InstructorID { get; set; }
        [MaxLength(250), NotNull]
        public DateTime Start { get; set; }
        [MaxLength(250), NotNull]
        public DateTime End { get; set; }
        [MaxLength(250), NotNull]
        public DateTime DueDate { get; set; }
        [MaxLength(250), NotNull]
        public string Status { get; set; }
        [MaxLength(500)]
        public string Notes { get; set; }

        public static readonly List<string> PossibleStatuses = new List<string>{ "In Progress", "Plan to Take", "Completed", "Dropped" };

        public override string ToString()
        {
            return Name;
        }

    }
}
