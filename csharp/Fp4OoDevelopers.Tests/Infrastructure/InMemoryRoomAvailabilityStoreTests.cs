using Fp4OoDevelopers.Domain;
using Fp4OoDevelopers.Infrastructure;
using System;
using Fp4OoDevelopers.Functional;
using Xunit;
using static Fp4OoDevelopers.Functional.Syntax;

namespace Fp4OoDevelopers.Tests.Infrastructure
{
    public class InMemoryRoomAvailabilityStoreTests
    {
        [Fact]
        public void IncrementVersion()
        {
            var store = new InMemoryRoomAvailabilityStore();
            var newAvailability = new RoomAvailability(Ids.AvailableRoom, 10);

            var result = store.Save(newAvailability)
                .FlatMap(_ => store.LoadForRoom(Ids.AvailableRoom).ToEither("Not found"))
                .Map(availability =>
                {
                    Assert.Equal(0, newAvailability.Version);
                    Assert.Equal(1, availability.Version);
                    return Syntax.Unit;
                });
            Assert.Equal(Right<string, Unit>(Syntax.Unit), result);
        }


        [Fact]
        public void OptimisticLock()
        {
            var store = new InMemoryRoomAvailabilityStore();
            var newAvailability = new RoomAvailability(Ids.AvailableRoom, 10);

            var result = store.Save(newAvailability)
                .FlatMap(_ => store.LoadForRoom(Ids.AvailableRoom).ToEither("Not found"))
                .FlatMap(availability => store.Save(availability).Map(_ => availability))
                .FlatMap(availability => store.Save(availability))
                .Map(_ => Syntax.Unit);

            Assert.StartsWith("Cannot save", result.Match(x => x, _ => ""));
        }

        [Fact]
        public void MissingAvailability()
        {
            var store = new InMemoryRoomAvailabilityStore();

            var option = store.LoadForRoom(Guid.NewGuid());

            Assert.Equal(None, option);
        }
    }
}