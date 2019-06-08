using System;

namespace Fp4OoDevelopers.Domain
{
    public class RoomAvailabilityBooking
    {
        public RoomAvailabilityBooking(Guid customerId, int quantity)
        {
            CustomerId = customerId;
            Quantity = quantity;
        }

        public Guid CustomerId { get; }

        public int Quantity { get; }

        protected bool Equals(RoomAvailabilityBooking other)
        {
            return CustomerId.Equals(other.CustomerId) && Quantity == other.Quantity;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((RoomAvailabilityBooking) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (CustomerId.GetHashCode() * 397) ^ Quantity;
            }
        }
    }
}