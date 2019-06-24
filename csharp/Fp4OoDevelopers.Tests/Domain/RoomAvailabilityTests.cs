using Fp4OoDevelopers.Domain;
using Fp4OoDevelopers.Functional;
using Xunit;

namespace Fp4OoDevelopers.Tests.Domain
{
    public class RoomAvailabilityTests
    {
        [Fact]
        public void SuccessfulBooking()
        {
            var availability = new RoomAvailability(Ids.AvailableRoom, 10);

            var result = availability.BookEither(Ids.JonSnow, 1);

            Assert.Equal(new Right<string, Unit>(Syntax.Unit), result);
            Assert.Equal(9, availability.Quantity);
            Assert.Equal(new RoomAvailabilityBooking(Ids.JonSnow, 1), availability.BookingFor(Ids.JonSnow));
        }

        [Fact]
        public void NoAvailability()
        {
            var availability = new RoomAvailability(Ids.NotAvailableRoom, 0);

            var result = availability.BookEither(Ids.JonSnow, 1);

            Assert.Equal(new Left<string, Unit>("ERROR"), result);
            Assert.Equal(0, availability.Quantity);
            Assert.Equal(None<RoomAvailabilityBooking>.Instance, availability.BookingFor(Ids.JonSnow));
        }

        [Fact]
        public void CustomerAlreadyBook()
        {
            var availability = new RoomAvailability(Ids.AvailableRoom, 10);
            availability.BookEither(Ids.JonSnow, 1);

            var result = availability.BookEither(Ids.JonSnow, 2);

            Assert.Equal(new Left<string, Unit>("ERROR"), result);
            Assert.Equal(9, availability.Quantity);
            Assert.Equal(new RoomAvailabilityBooking(Ids.JonSnow, 1), availability.BookingFor(Ids.JonSnow));
        }
    }
}