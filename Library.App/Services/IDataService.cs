using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Shared;

namespace Library.App.Services
{
    public interface IDataService
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<IEnumerable<Issue>> GetIssues();
    }
}
