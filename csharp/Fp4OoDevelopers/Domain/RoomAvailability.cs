using System;
using System.Collections.Generic;
using Fp4OoDevelopers.Functional;
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

        public Either<string, Unit> BookEither(Guid customerId, int quantity)
        {
            if (quantity > Quantity || bookingsByCustomerId.ContainsKey(customerId))
            {
                return new Left<string, Unit>("ERROR");
            }
            Quantity -= quantity;
            bookingsByCustomerId[customerId] = new RoomAvailabilityBooking(customerId, quantity);
            return new Right<string, Unit>(Syntax.Unit);
        }

        public Option<RoomAvailabilityBooking> BookingFor(Guid customerId) => 
            Option<RoomAvailabilityBooking>.Pure(bookingsByCustomerId.TryGetValue(customerId, out var ret) ? ret : null);
    }
}
