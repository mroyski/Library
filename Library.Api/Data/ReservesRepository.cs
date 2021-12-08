using System;
using System.Collections.Generic;
using System.Linq;
using Library.Shared;

namespace Library.Api.Data
{
    public class ReservesRepository : IReservesRepository
    {
        private readonly LibraryContext _context;

        public ReservesRepository(LibraryContext context)
        {
            _context = context;
        }

        public Reserve AddReservation(int bookId, int memberId)
        {
            var existingReserve = _context.Reserves.FirstOrDefault(r => r.MemberId == memberId &&
                                                          r.BookId == bookId &&
                                                          r.ReserveStatus != "closed");
            if (existingReserve != null)
                throw new Exception("Member already has open or pending reservation on this book.");

            var alreadyReserved = _context.Reserves
                .Where(r => r.BookId == bookId)
                .Any(r => r.ReserveStatus == "open" || r.ReserveStatus == "pending");

            var reserve = new Reserve
            {
                BookId = bookId,
                MemberId = memberId,
                ReserveDate = DateTime.Now,
                ReserveStatus = "pending"
            };

            // if (!TryValidateModel(reserve))
            //     return ValidationProblem(ModelState);

            _context.Reserves.Add(reserve);
            _context.SaveChanges();

            return reserve;
        }

        public Reserve GetReservation(int reserveId)
        {
            return _context.Reserves.FirstOrDefault(r => r.ReserveId == reserveId);
        }

        public IEnumerable<Reserve> GetReservationsByBook(int bookId, string status)
        {
            var collection = _context.Reserves as IQueryable<Reserve>;

            collection = collection.Where(r => r.BookId == bookId);

            if (status != null)
            {
                if (!Constants.ReservationStatuses.Statuses.Contains(status.ToLower()))
                {
                    throw new Exception("Invalid Reservation Status");
                }

                collection = collection.Where(r => r.ReserveStatus == status);
            }

            return collection;
        }
    }
}
