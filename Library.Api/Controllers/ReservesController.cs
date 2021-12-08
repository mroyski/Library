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
        private readonly IReservesRepository _repo;

        public ReservesController(IReservesRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("{reserveId}", Name = "GetReservation")]
        public ActionResult<Reserve> GetReservation(int reserveId)
        {
            var reservation = _repo.GetReservation(reserveId);

            if (reservation == null)
                return NotFound();

            return Ok(reservation);
        }

        [HttpGet("books/{bookId}")]
        public ActionResult<IEnumerable<Reserve>> GetReservationsByBook(
            [FromRoute] int bookId, [FromQuery] string status)
        {
            var collection = _repo.GetReservationsByBook(bookId, status);

            if (collection == null)
                return NotFound();

            return Ok(collection.ToList());
        }

        [HttpPost("books/{bookId}/members/{memberId}")]
        public ActionResult AddReservation(int bookId, int memberId)
        {
            var reserve = _repo.AddReservation(bookId, memberId);

            return CreatedAtRoute(
                "GetReservation", 
                new { reserveId = reserve.ReserveId, bookId = reserve.BookId },
                reserve);
        }
    }
}
