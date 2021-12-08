using System;
using System.Collections.Generic;
using Library.Shared;

namespace Library.Api.Data
{
    public interface IReservesRepository
    {
        Reserve GetReservation(int reserveId);
        IEnumerable<Reserve> GetReservationsByBook(int bookId, string status);
        Reserve AddReservation(int bookId, int memberId);
    }
}
