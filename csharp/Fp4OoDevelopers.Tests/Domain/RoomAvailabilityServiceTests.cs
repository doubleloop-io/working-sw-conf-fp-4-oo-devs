using System;
using Fp4OoDevelopers.Domain;
using Fp4OoDevelopers.Functional;
using Fp4OoDevelopers.Infrastructure;
using Xunit;

namespace Fp4OoDevelopers.Tests.Domain
{
    public class RoomAvailabilityServiceTests
    {
        private readonly TestRoomAvailabilityStore store;
        private readonly RoomAvailabilityService service;

        public RoomAvailabilityServiceTests()
        {
            store = new TestRoomAvailabilityStore(
                new RoomAvailability(Ids.AvailableRoom, 10),
                new RoomAvailability(Ids.OptimisticLockRoom, 1));
            service = new RoomAvailabilityService(store);
        }

        [Fact]
        public void SuccessfulBooking()
        {
            service.Book(new BookCommand(Ids.AvailableRoom, Ids.JonSnow, 1));

            Assert.NotNull(store.Saved);
            Assert.Equal(Ids.AvailableRoom, store.Saved.RoomId);
            Assert.Equal(9, store.Saved.Quantity);
        }

        [Fact]
        public void OptimisticLock()
        {
            Assert.Throws<OptimisticLockException>(() =>
                service.Book(new BookCommand(Ids.OptimisticLockRoom, Ids.JonSnow, 1)));
        }

        private class TestRoomAvailabilityStore : IRoomAvailabilityStore
        {
            public RoomAvailability Saved { get; private set; }

            private readonly IRoomAvailabilityStore store;

            public TestRoomAvailabilityStore(params RoomAvailability[] availabilities)
            {
                store = new InMemoryRoomAvailabilityStore();
                foreach (var availability in availabilities)
                    store.SaveEither(availability);
            }

            public Option<RoomAvailability> LoadForRoom(Guid roomId)
            {
                return store.LoadForRoom(roomId);
            }

            public void Save(RoomAvailability roomAvailability)
            {
                if (roomAvailability.RoomId == Ids.OptimisticLockRoom)
                    throw new OptimisticLockException(nameof(RoomAvailability), roomAvailability.Id, 2, 1);
                store.Save(roomAvailability);
                Saved = roomAvailability;
            }

            public Either<string, Unit> SaveEither(RoomAvailability roomAvailability)
            {
                if (roomAvailability.RoomId == Ids.OptimisticLockRoom)
                    return "Optimistic lock";
                return store.SaveEither(roomAvailability)
                    .Map(_ =>
                    {
                        Saved = roomAvailability;
                        return _;
                    });
            }
        }
    }
}
