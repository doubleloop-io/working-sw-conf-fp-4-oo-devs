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
                .FlatMap(roomAvailability => roomAvailability.BookImmutable(command.CustomerId, command.Quantity))
                .FlatMap(roomAvailability => roomAvailabilityStore.Save(roomAvailability))
                .Match(error => throw new OptimisticLockException(error), x => x);
        }
    }
}
