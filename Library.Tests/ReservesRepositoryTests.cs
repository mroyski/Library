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
    public class ReservesRepositoryTests
    {
        private readonly Mock<LibraryContext> mockContext;
        private readonly Mock<DbSet<Reserve>> mockSet;
        private readonly ReservesRepository mockRepo;
        private List<Reserve> data;
        public ReservesRepositoryTests()
        {
            var data = new List<Reserve>
            {
                new Reserve { ReserveId = 1, BookId = 1, MemberId = 1, ReserveDate = DateTime.Now, ReserveStatus = "pending" }
            }.AsQueryable();

            mockSet = new Mock<DbSet<Reserve>>();
            mockSet.As<IQueryable<Reserve>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Reserve>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Reserve>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Reserve>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext = new Mock<LibraryContext>();
            mockContext.Setup(c => c.Reserves).Returns(mockSet.Object);

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
