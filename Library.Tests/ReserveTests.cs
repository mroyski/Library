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
        private readonly LibraryContext mockContext;
        private readonly ReservesRepository mockRepo;
        public ReserveTests()
        {
            mockContext = new LibraryContextBuilder()
                                    .WithSimpleReserveData()
                                    .WithSimpleIssueData()
                                    .Build();

            mockRepo = new ReservesRepository(mockContext);
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

        [Fact]
        public void BookWithReservationReturned_RemovesCurrentReservation()
        {
            var reserve = mockRepo.GetReservation(1);
            Assert.Equal("pending", reserve.ReserveStatus);

            var issueRepo = new IssuesRepository(mockContext);
            issueRepo.ReturnBook(1);

            Assert.Equal("closed", reserve.ReserveStatus);
        }
    }
}
