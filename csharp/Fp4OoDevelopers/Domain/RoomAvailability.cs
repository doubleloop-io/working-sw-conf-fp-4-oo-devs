using System;
using System.Collections.Generic;
using System.Linq;
using Fp4OoDevelopers.Functional;
using Newtonsoft.Json;

namespace Fp4OoDevelopers.Domain
{
    public class RoomAvailability
    {
        [JsonProperty]
        private readonly IReadOnlyDictionary<Guid, RoomAvailabilityBooking> bookingsByCustomerId = new Dictionary<Guid, RoomAvailabilityBooking>();

        public RoomAvailability(Guid roomId, int quantity)
        {
            Id = Guid.NewGuid();
            RoomId = roomId;
            Quantity = quantity;
            Version = 0;
        }

        private RoomAvailability(Guid id, Guid roomId, int quantity, IEnumerable<RoomAvailabilityBooking> bookings, int version)
        {
            Id = id;
            RoomId = roomId;
            Quantity = quantity;
            Version = version;
            bookingsByCustomerId = bookings.ToDictionary(x => x.CustomerId, x => x);
        }

        [JsonProperty]
        public Guid Id { get; private set; }
        [JsonProperty]
        public Guid RoomId { get; private set; }
        [JsonProperty]
        public int Quantity { get; private set; }
        [JsonProperty]
        public int Version { get; private set; }

        public Either<string, RoomAvailability> Book(Guid customerId, int quantity)
        {
            if (quantity > Quantity)
            {
                return "Not enough availability";
            }
            if (bookingsByCustomerId.ContainsKey(customerId))
            {
                return "Customer already booked this property";
            }
            return new RoomAvailability(
                Id,
                RoomId,
                Quantity - quantity,
                bookingsByCustomerId.Values.Union(new[] {new RoomAvailabilityBooking(customerId, quantity)}),
                Version);
        }

        public Option<RoomAvailabilityBooking> BookingFor(Guid customerId) => 
            Option<RoomAvailabilityBooking>.Pure(bookingsByCustomerId.TryGetValue(customerId, out var ret) ? ret : null);
    }
}
