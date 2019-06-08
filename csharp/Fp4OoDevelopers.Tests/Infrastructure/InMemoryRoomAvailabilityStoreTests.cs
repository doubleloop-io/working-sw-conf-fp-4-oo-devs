using Fp4OoDevelopers.Domain;
using Fp4OoDevelopers.Infrastructure;
using Xunit;

namespace Fp4OoDevelopers.Tests.Infrastructure
{
    public class InMemoryRoomAvailabilityStoreTests
    {
        [Fact]
        public void IncrementVersion()
        {
            var store = new InMemoryRoomAvailabilityStore();
            var newAvailability = new RoomAvailability(Ids.AvailableRoom, 10);

            store.Save(newAvailability);

            var availability = store.LoadForRoom(Ids.AvailableRoom);
            Assert.NotNull(availability);
            Assert.Equal(0, newAvailability.Version);
            Assert.Equal(1, availability.Version);
        }


        [Fact]
        public void OptimisticLock()
        {
            var store = new InMemoryRoomAvailabilityStore();
            store.Save(new RoomAvailability(Ids.AvailableRoom, 10));
            var availability = store.LoadForRoom(Ids.AvailableRoom);
            store.Save(availability);

            Assert.Throws<OptimisticLockException>(() => store.Save(availability));
        }
    }
}