using System;
using System.Collections.Generic;
using System.Linq;
using Library.Api.Data;
using Library.Shared;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Library.Tests
{
    public class DbSetBuilder
    {
        private readonly Mock<DbSet<Reserve>> mockSet;
        private readonly Mock<LibraryContext> mockContext;
        private readonly ReservesRepository mockRepo;
        public DbSetBuilder()
        {
            var data = new List<Reserve>
            {
                new Reserve { ReserveId = 1, BookId = 1, MemberId = 1, ReserveDate = DateTime.Now, ReserveStatus = "pending" }
            }.AsQueryable();

            mockSet = DbSetBuilder<Reserve>.Build(data);
            mockContext = ContextBuilder.BuildReserveContext(mockSet);
            mockRepo = new ReservesRepository(mockContext.Object);
        }

        [Fact]
        public void GetReservation_ReturnsReservationById()
        {
            var reserve = mockRepo.GetReservation(1);

            Assert.Equal(1, reserve.ReserveId);
        }

        [Fact]
        public void MemberHasExistingReservation_ThrowsException()
        {
            Assert.Throws<Exception>(() => mockRepo.AddReservation(1, 1));
        }
    }
}
