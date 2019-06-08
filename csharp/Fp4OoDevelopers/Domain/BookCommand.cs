using System;

namespace Fp4OoDevelopers.Domain
{
    public class BookCommand
    {
        public BookCommand(Guid roomId, Guid customerId, int quantity)
        {
            RoomId = roomId;
            CustomerId = customerId;
            Quantity = quantity;
        }

        public Guid RoomId { get; }
        public Guid CustomerId { get; }
        public int Quantity { get; }
    }
}