using System;
using System.Collections.Generic;
using Library.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Data
{
    public interface IBooksRepository
    {
        IEnumerable<Book> GetBooks(BookResourceParameters bookResourceParameters);
        Book GetBook(int bookId);
        void AddBook(Book book);
    }
}
