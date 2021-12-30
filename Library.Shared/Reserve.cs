using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Library.Api.ValidationAttributes;

#nullable disable

namespace Library.Shared
{
    public partial class Reserve
    {
        [Key]
        public int ReserveId { get; set; }
        public int? BookId { get; set; }
        public int? MemberId { get; set; }
        public DateTime? ReserveDate { get; set; }
        [ReservationStatus]
        public string ReserveStatus { get; set; }
    }
}
