using System;
using System.Collections.Generic;
using System.Linq;
using Library.Api.Data;
using Library.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/reserves")]
    public class ReservesController : ControllerBase
    {
        private readonly LibraryContext _context;

        public ReservesController()
        {
            _context = new LibraryContext();
        }

        [HttpGet("{reserveId}", Name = "GetReservation")]
        public ActionResult<IEnumerable<Reserve>> GetReservation(int reserveId)
        {
            var reservation = _context.Reserves.FirstOrDefault(r => r.ReserveId == reserveId);

            if (reservation == null)
                return NotFound();

            return Ok(reservation);
        }

        [HttpGet("books/{bookId}")]
        public ActionResult<IEnumerable<Reserve>> GetReservationsByBook(
            [FromRoute] int bookId, [FromQuery] string status)
        {
            var collection = _context.Reserves as IQueryable<Reserve>;

            collection = collection.Where(r => r.BookId == bookId);

            if (status != null)
            {
                if (!Constants.ReservationStatuses.Statuses.Contains(status.ToLower()))
                {
                    return new BadRequestObjectResult(new Exception("Invalid Reservation Status"));
                }

                collection = collection.Where(r => r.ReserveStatus == status);
            }

            return Ok(collection.ToList());
        }

        [HttpPost("books/{bookId}/members/{memberId}")]
        public ActionResult AddReservation(int bookId, int memberId)
        {
            var existingReserve = _context.Reserves.FirstOrDefault(r => r.MemberId == memberId &&
                                                                     r.BookId == bookId &&
                                                                     r.ReserveStatus != "closed");
            if (existingReserve != null)
                return new BadRequestObjectResult(
                    new Exception("Member already has open or pending reservation on this book."));

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

            if (!TryValidateModel(reserve))
                return ValidationProblem(ModelState);

            _context.Reserves.Add(reserve);
            _context.SaveChanges();

            return CreatedAtRoute(
                "GetReservation", 
                new { reserveId = reserve.ReserveId, bookId = reserve.BookId },
                reserve);
        }
    }
}
