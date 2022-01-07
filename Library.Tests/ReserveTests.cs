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
    public class ReserveTests
    {
        private readonly ReservesRepository mockRepo;
        public ReserveTests()
        {
            var libraryContext = new LibraryContextBuilder()
                                    .WithSimpleReserveData()
                                    .WithSimpleIssueData()
                                    .Build();

            mockRepo = new ReservesRepository(libraryContext);
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
