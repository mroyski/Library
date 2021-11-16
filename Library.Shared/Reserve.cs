using System;
using System.Collections.Generic;
using Library.Api.ValidationAttributes;

#nullable disable

namespace Library.Api.Shared
{
    public partial class Reserve
    {
        public int ReserveId { get; set; }
        public int? BookId { get; set; }
        public int? MemberId { get; set; }
        public DateTime? ReserveDate { get; set; }
        [ReservationStatus]
        public string ReserveStatus { get; set; }
    }
}
