using Fp4OoDevelopers.Domain;
using Xunit;

namespace Fp4OoDevelopers.Tests.Domain
{
    public class RoomAvailabilityTests
    {
        [Fact]
        public void SuccessfulBooking()
        {
            var availability = new RoomAvailability(Ids.AvailableRoom, 10);

            var result = availability.Book(Ids.JonSnow, 1);

            Assert.True(result);
            Assert.Equal(9, availability.Quantity);
            Assert.Equal(new RoomAvailabilityBooking(Ids.JonSnow, 1), availability.BookingFor(Ids.JonSnow));
        }

        [Fact]
        public void NoAvailability()
        {
            var availability = new RoomAvailability(Ids.NotAvailableRoom, 0);

            var result = availability.Book(Ids.JonSnow, 1);

            Assert.False(result);
            Assert.Equal(0, availability.Quantity);
            Assert.Null(availability.BookingFor(Ids.JonSnow));
        }

        [Fact]
        public void CustomerAlreadyBook()
        {
            var availability = new RoomAvailability(Ids.AvailableRoom, 10);
            availability.Book(Ids.JonSnow, 1);

            var result = availability.Book(Ids.JonSnow, 2);

            Assert.False(result);
            Assert.Equal(9, availability.Quantity);
            Assert.Equal(new RoomAvailabilityBooking(Ids.JonSnow, 1), availability.BookingFor(Ids.JonSnow));
        }
    }
}