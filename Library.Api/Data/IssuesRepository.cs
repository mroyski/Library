using System;
using System.Collections.Generic;
using System.Linq;
using Library.Shared;

namespace Library.Api.Data
{
    public class IssuesRepository : IIssuesRepository
    {
        private readonly LibraryContext _context;

        public IssuesRepository(LibraryContext context)
        {
            _context = context;
        }
        public Issue AddIssue(int bookId, int memberId)
        {
            // check if already issued
            var unavailible = _context.Issues
                .Where(i => i.BookId == bookId)
                .Any(i => i.ReturnDate == null);

            if (unavailible)
                throw new Exception("Book is currently checked out");

            // check for reservations
            var reserved = _context.Reserves
                .Where(b => b.BookId == bookId)
                .Any(r => r.ReserveStatus == "pending");

            if (reserved)
            {
                var nextReserveMember = _context.Reserves
                        .Where(r => r.BookId == bookId)
                        .Where(r => r.ReserveStatus == "pending")
                        .OrderBy(r => r.ReserveDate)
                        .First();

                bool nextMemberMatch = nextReserveMember.MemberId == memberId;

                if (!nextMemberMatch)
                    throw new Exception("Book is currently on reserve.");
            }

            var issue = new Issue()
            {
                BookId = bookId,
                MemberId = memberId,
                IssueDate = DateTime.Now
            };

            _context.Issues.Add(issue);
            _context.SaveChanges();
            return issue;
        }

        public Issue GetIssue(int issueId)
        {
            var issue = _context.Issues.FirstOrDefault(i => i.LibIssueId == issueId);

            if (issue == null)
                throw new ArgumentException($"No match for issue id {issueId}");

            return issue;
        }

        public IEnumerable<Issue> GetIssues()
        {
            return _context.Issues;
        }

        public void ReturnBook(int issueId)
        {
            // Update Issue table
            var issue = _context.Issues.FirstOrDefault(i => i.LibIssueId == issueId);

            if (issue == null)
                throw new ArgumentException($"No match for issue id {issueId}");

            if (issue.ReturnDate != null)
                throw new Exception($"Issue was already been returned {issue.ReturnDate}");

            issue.ReturnDate = DateTime.Now;
            _context.Update(issue);
            _context.SaveChanges();

            // Update Reserve table
            // Close reserve for the returned issue
            var reserves = _context.Reserves as IQueryable<Reserve>;
            reserves = reserves.Where(r => r.BookId == issue.BookId);
            reserves = reserves.Where(r => r.MemberId == issue.MemberId);
            var reserve = reserves.FirstOrDefault(r => r.ReserveStatus == "pending");

            if (reserve != null)
            {
                reserve.ReserveStatus = "closed";
                _context.Reserves.Update(reserve);
                _context.SaveChanges();
            }
        }
    }
}
