using System;
using System.Collections.Generic;
using System.Linq;
using Library.Api.Helpers;
using Library.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core;

namespace Library.Api.Data
{
    public class BooksRepository : IBooksRepository
    {
        private readonly LibraryContext _context;

        public BooksRepository(LibraryContext context)
        {
            _context = context;
        }

        public void AddBook(Book book)
        {
            _context.Add(book);
            _context.SaveChanges();
        }

        public Book GetBook(int bookId)
        {
            var book = _context.Books.FirstOrDefault(b => b.BookId == bookId);

            if (book == null)
                throw new ArgumentException($"No match for book id {bookId}");

            return book;
        }

        public IEnumerable<Book> GetBooks(BookResourceParameters bookResourceParameters)
        {
            var books = _context.Books as IQueryable<Book>;

            if (!string.IsNullOrWhiteSpace(bookResourceParameters.OrderBy))
            {
                var formattedParameter = ParameterHelper.FormatParameter<Book>(bookResourceParameters.OrderBy);
                var property = typeof(Book).GetProperty(formattedParameter);

                if (property == null)
                    throw new ArgumentException("Property does not exist");

                books = books.OrderBy(formattedParameter);
            }
            return books;
        }
    }
}
