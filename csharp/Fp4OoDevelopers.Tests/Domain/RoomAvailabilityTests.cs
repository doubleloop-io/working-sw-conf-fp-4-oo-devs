using Fp4OoDevelopers.Domain;
using Fp4OoDevelopers.Functional;
using Xunit;
using static Fp4OoDevelopers.Functional.Syntax;

namespace Fp4OoDevelopers.Tests.Domain
{
    public class RoomAvailabilityTests
    {
        [Fact]
        public void SuccessfulBooking()
        {
            var availability = new RoomAvailability(Ids.AvailableRoom, 10);

            var result = availability.BookImmutable(Ids.JonSnow, 1)
                .Map(x =>
                {
                    Assert.Equal(9, x.Quantity);
                    Assert.Equal(new RoomAvailabilityBooking(Ids.JonSnow, 1), x.BookingFor(Ids.JonSnow));
                    return Syntax.Unit;
                });
            Assert.Equal(Right<string, Unit>(Syntax.Unit), result);
            Assert.Equal(10, availability.Quantity);
        }

        [Fact]
        public void NoAvailability()
        {
            var availability = new RoomAvailability(Ids.NotAvailableRoom, 0);

            var result = availability.BookImmutable(Ids.JonSnow, 1);

            Assert.Equal(Left<string, RoomAvailability>("Not enough availability"), result);
        }

        [Fact]
        public void CustomerAlreadyBook()
        {
            var availability = new RoomAvailability(Ids.AvailableRoom, 10);

            var result = availability.BookImmutable(Ids.JonSnow, 1)
                .FlatMap(x => x.BookImmutable(Ids.JonSnow, 2));

            Assert.Equal(Left<string, RoomAvailability>("Customer already booked this property"), result);
        }
    }
}