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
            roomAvailabilityStore.LoadForRoomOption(command.RoomId)
                .Map(roomAvailability =>
                {
                    if (roomAvailability.Book(command.CustomerId, command.Quantity))
                    {
                        roomAvailabilityStore.Save(roomAvailability);
                    }

                    return Unit.Instance;
                });
        }
    }
}
