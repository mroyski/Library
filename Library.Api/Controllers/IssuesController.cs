using System;
using System.Collections.Generic;
using System.Linq;
using Library.Api.Models;
using Library.Api.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/issues")]
    public class IssuesController : ControllerBase
    {
        private readonly LibraryContext _context;
        public IssuesController()
        {
            _context = new LibraryContext();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Issue>> GetIssues()
        {
            return _context.Issues.ToList();
        }

        [HttpGet("{issueId}", Name = "GetIssue")]
        public ActionResult<Issue> GetIssue(int issueId)
        {
            var issue = _context.Issues.FirstOrDefault(i => i.LibIssueId == issueId);

            if (issue == null)
                return NotFound();

            return Ok(issue);
        }

        [HttpPost("{issueId}/return")]
        public ActionResult<Issue> ReturnBook(int issueId)
        {
            // // Update Issue table
            var issue = _context.Issues.FirstOrDefault(i => i.LibIssueId == issueId);

            if (issue == null)
                return NotFound();

            if (issue.ReturnDate != null)
                return new BadRequestObjectResult(new Exception("Issue has already been returned."));

            issue.ReturnDate = DateTime.Now;
            _context.Update(issue);
            _context.SaveChanges();

            // Update Reserve table
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

            return NoContent();
        }

        [HttpPost("books/{bookId}/members/{memberId}")]
        public ActionResult<Issue> AddIssue(int bookId, int memberId)
        {
            // check if already issued
            var unavailible = _context.Issues
                .Where(i => i.BookId == bookId)
                .Any(i => i.ReturnDate == null);

            if (unavailible)
                return new BadRequestObjectResult(new Exception("Book is currently checked out."));

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
                    return new BadRequestObjectResult(new Exception("Book is currently on reserve."));
            }

            var issue = new Issue()
            {
                BookId = bookId,
                MemberId = memberId,
                IssueDate = DateTime.Now
            };

            _context.Issues.Add(issue);
            _context.SaveChanges();

            // update "pending" reserve to "open"
            // var reserve = _context.Reserves
            //     .Where(r => r.BookId == bookId)
            //     .Where(r => r.MemberId == memberId)
            //     .FirstOrDefault(r => r.ReserveStatus == "pending");

            // if (reserve != null)
            // {
            //     reserve.ReserveStatus = "open";
            //     _context.Reserves.Update(reserve);
            //     _context.SaveChanges();
            // }

            return CreatedAtRoute("GetIssue", new { issueId = issue.LibIssueId }, issue);
        }
    }
}
