using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace C971_PA.Models
{
    [Table("wgu_instructors")]
    public class Instructor
    {
        [PrimaryKey,AutoIncrement,Unique,NotNull,Column("instructor_key")]
        public int InstructorKey { get; set; }
        [MaxLength(250),Unique, NotNull]
        public string Name { get; set; }
        [MaxLength(250), NotNull]
        public string Email { get; set; }
        [MaxLength(250), NotNull]
        public string Phone { get; set; }

    }
}
