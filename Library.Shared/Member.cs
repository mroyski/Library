using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Library.Shared
{
    public partial class Member
    {
        [Key]
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public DateTime? AccOpenDate { get; set; }
        public int? MaxBooksAllowed { get; set; }
        public int? PenaltyAmount { get; set; }
    }
}
