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
            var roomAvailability = roomAvailabilityStore.LoadForRoom(command.RoomId);

            if (roomAvailability != null)
            {
                if (roomAvailability.Book(command.CustomerId, command.Quantity))
                {
                    roomAvailabilityStore.Save(roomAvailability);
                }
            }
        }
    }
}
