using Library.Api.Data;
using Library.Shared;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Tests
{
    public class LibraryContextBuilder
    {
        private readonly Mock<LibraryContext> _context = new Mock<LibraryContext>();

        public LibraryContext Build()
        {
            return _context.Object;
        }

        public LibraryContextBuilder WithSimpleReserveData()
        {
            var reserveData = new List<Reserve>
            {
                new Reserve { ReserveId = 1, BookId = 1, MemberId = 1, ReserveDate = DateTime.Now, ReserveStatus = "pending" }
            }.AsQueryable();

            var reserveMockSet = DbSetBuilder<Reserve>.Build(reserveData);
            AddReserveDbSetToContext(reserveMockSet);
            return this;
        }

        public LibraryContextBuilder WithSimpleIssueData()
        {
            var issueData = new List<Issue>
            {
                new Issue { LibIssueId = 1, BookId = 1, MemberId = 1, IssueDate = DateTime.Now, ReturnDate = null }
            }.AsQueryable();

            var issueMockSet = DbSetBuilder<Issue>.Build(issueData);
            AddIssueDbSetToContext(issueMockSet);
            return this;
        }

        public LibraryContextBuilder AddReserveDbSetToContext(Mock<DbSet<Reserve>> mockSet)
        {
            _context.Setup(c => c.Reserves).Returns(mockSet.Object);
            return this;
        }

        public LibraryContextBuilder AddIssueDbSetToContext(Mock<DbSet<Issue>> mockSet)
        {
            _context.Setup(c => c.Issues).Returns(mockSet.Object);
            return this;
        }
    }
}
