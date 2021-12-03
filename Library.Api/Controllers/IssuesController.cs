using System;
using System.Collections.Generic;
using System.Linq;
using Library.Api.Data;
using Library.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/issues")]
    public class IssuesController : ControllerBase
    {
        private readonly IIssuesRepository _repo;
        public IssuesController(IIssuesRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Issue>> GetIssues()
        {
            return Ok(_repo.GetIssues());
        }

        [HttpGet("{issueId}", Name = "GetIssue")]
        public ActionResult<Issue> GetIssue(int issueId)
        {
            return Ok(_repo.GetIssue(issueId));
        }

        [HttpPost("{issueId}/return")]
        public ActionResult<Issue> ReturnBook(int issueId)
        {
            _repo.ReturnBook(issueId);
            return NoContent();
        }

        [HttpPost("books/{bookId}/members/{memberId}")]
        public ActionResult<Issue> AddIssue(int bookId, int memberId)
        {
            var issue = _repo.AddIssue(bookId, memberId);

            return CreatedAtRoute("GetIssue", new { issueId = issue.LibIssueId }, issue);
        }
    }
}
