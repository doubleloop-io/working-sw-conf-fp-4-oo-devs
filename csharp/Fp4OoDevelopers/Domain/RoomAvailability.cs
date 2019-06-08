using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Fp4OoDevelopers.Domain
{
    public class RoomAvailability
    {
        [JsonProperty]
        private readonly Dictionary<Guid, RoomAvailabilityBooking> bookingsByCustomerId = new Dictionary<Guid, RoomAvailabilityBooking>();

        public RoomAvailability(Guid roomId, int quantity)
        {
            Id = Guid.NewGuid();
            RoomId = roomId;
            Quantity = quantity;
            Version = 0;
        }

        [JsonProperty]
        public Guid Id { get; private set; }
        [JsonProperty]
        public Guid RoomId { get; private set; }
        [JsonProperty]
        public int Quantity { get; private set; }
        [JsonProperty]
        public int Version { get; private set; }

        public bool Book(Guid customerId, int quantity)
        {
            if (quantity > Quantity || bookingsByCustomerId.ContainsKey(customerId))
            {
                return false;
            }
            Quantity -= quantity;
            bookingsByCustomerId[customerId] = new RoomAvailabilityBooking(customerId, quantity);
            return true;
        }

        public RoomAvailabilityBooking BookingFor(Guid customerId)
        {
            return bookingsByCustomerId.TryGetValue(customerId, out var ret) ? ret : null;
        }
    }
}
