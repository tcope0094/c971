using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace C971_PA.Models
{
    [Table("wgu_terms")]
    public class Term
    {
        [PrimaryKey, AutoIncrement, Unique, NotNull, Column("term_key")]
        public int TermKey { get; set; }
        [MaxLength(250), Unique, NotNull]
        public string Name { get; set; }
        [MaxLength(250), NotNull]
        public DateTime Start { get; set; }
        [MaxLength(250), NotNull]
        public DateTime End { get; set; }
    }
}
