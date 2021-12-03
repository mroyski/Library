using System;
using System.Collections.Generic;
using Library.Shared;

namespace Library.Api.Data
{
    public interface IIssuesRepository
    {
        IEnumerable<Issue> GetIssues();
        Issue GetIssue(int issueId);
        void ReturnBook(int issueId);
        Issue AddIssue(int bookId, int memberId);

    }
}
