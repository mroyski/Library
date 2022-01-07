using Library.Api.Data;
using Library.Shared;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Library.Tests
{
    public static class ContextBuilder
    {
        public static readonly Mock<LibraryContext> _context = new Mock<LibraryContext>();
        public static Mock<LibraryContext> BuildReserveContext(Mock<DbSet<Reserve>> mockSet)
        {
            _context.Setup(c => c.Reserves).Returns(mockSet.Object);
            return _context;
        }

        public static Mock<LibraryContext> BuildIssueContext(Mock<DbSet<Issue>> mockSet)
        {
            _context.Setup(c => c.Issues).Returns(mockSet.Object);
            return _context;
        }
    }
}
