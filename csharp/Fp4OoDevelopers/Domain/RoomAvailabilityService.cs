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

        public void Book(BookCommand command)
        {
            roomAvailabilityStore.LoadForRoom(command.RoomId)
                .ToEither("Cannot find availability for required room")
                .FlatMap(roomAvailability => Book(roomAvailability, command))
                .Map(roomAvailability =>
                {
                    roomAvailabilityStore.Save(roomAvailability);
                    return Syntax.Unit;
                });
        }

        private Either<string, RoomAvailability> Book(RoomAvailability roomAvailability, BookCommand command) =>
            roomAvailability.Book(command.CustomerId, command.Quantity).Map(_ => roomAvailability);
    }
}
