using static Fp4OoDevelopers.Functional.Syntax;

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
                .Map(roomAvailability =>
                {
                    roomAvailability.Book(command.CustomerId, command.Quantity)
                        .Map(_ =>
                        {
                            roomAvailabilityStore.Save(roomAvailability);
                            return Unit;
                        });
		    return Unit;
                });
        }
    }
}
