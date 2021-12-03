using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Library.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Library.Api.Helpers;
using Library.Shared;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksRepository _repo;

        public BooksController(IBooksRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks([FromQuery] BookResourceParameters bookResourceParameters)
        {
            return Ok(_repo.GetBooks(bookResourceParameters));
        }

        [HttpGet("{bookId}", Name = "GetBook")]
        public ActionResult<Book> GetBook(int bookId)
        {
            return Ok(_repo.GetBook(bookId));
        }

        [HttpPost]
        public ActionResult<string> AddBook([FromBody] Book book)
        {
            if (!TryValidateModel(book))
            {
                return ValidationProblem(ModelState);
            }

            _repo.AddBook(book);

            return CreatedAtRoute("GetBook", new { bookId = book.BookId }, book);
        }
    }
}
