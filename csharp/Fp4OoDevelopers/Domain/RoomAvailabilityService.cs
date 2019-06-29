using Fp4OoDevelopers.Functional;

namespace Fp4OoDevelopers.Domain
{
    public class RoomAvailabilityService
    {
        private readonly IRoomAvailabilityStore roomAvailabilityStore;

        public RoomAvailabilityService(IRoomAvailabilityStore roomAvailabilityStore)
        {
            this.roomAvailabilityStore = roomAvailabilityStore;
        }

        public Either<string, Unit> Book(BookCommand command) =>
            roomAvailabilityStore.LoadForRoom(command.RoomId)
                .ToEither("Cannot find availability for required room")
                .FlatMap(roomAvailability => roomAvailability.Book(command.CustomerId, command.Quantity))
                .FlatMap(roomAvailability => roomAvailabilityStore.Save(roomAvailability));
    }
}
