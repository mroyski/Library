using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Library.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Library.Api.Helpers;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BooksController()
        {
            _context = new LibraryContext();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks([FromQuery] BookResourceParameters bookResourceParameters)
        {
            var books = _context.Books as IQueryable<Book>;

            if (!string.IsNullOrWhiteSpace(bookResourceParameters.OrderBy))
            {
                var formattedParameter = ParameterHelper.FormatParameter<Book>(bookResourceParameters.OrderBy);
                var property = typeof(Book).GetProperty(formattedParameter);
                
                if (property == null)
                    return BadRequest();

                books = books.OrderBy(formattedParameter);
            }
            return Ok(books.ToList());
        }

        [HttpGet("{bookId}", Name = "GetBook")]
        public ActionResult<Book> GetBook(int bookId)
        {
            var book = _context.Books.FirstOrDefault(b => b.BookId == bookId);

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        public ActionResult<string> AddBook([FromBody] Book book)
        {
            if (!TryValidateModel(book))
            {
                return ValidationProblem(ModelState);
            }

            _context.Add(book);
            _context.SaveChanges();

            return CreatedAtRoute("GetBook", new { bookId = book.BookId }, book);
        }
    }
}
