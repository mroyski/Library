using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Shared
{
    public partial class Book
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public double? Cost { get; set; }
        public string Category { get; set; }
    }
}
