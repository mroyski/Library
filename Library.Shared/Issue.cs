using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Library.Shared
{
    public partial class Issue
    {
        [Key]
        public int LibIssueId { get; set; }
        public int? BookId { get; set; }
        public int? MemberId { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
