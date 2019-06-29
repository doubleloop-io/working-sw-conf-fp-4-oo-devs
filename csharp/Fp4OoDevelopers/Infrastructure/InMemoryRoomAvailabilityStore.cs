using Fp4OoDevelopers.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using Fp4OoDevelopers.Functional;

namespace Fp4OoDevelopers.Infrastructure
{
    public class InMemoryRoomAvailabilityStore : IRoomAvailabilityStore
    {
        private readonly ConcurrentDictionary<Guid, string> roomAvailabilitiesByRoomId = new ConcurrentDictionary<Guid, string>();

        public Option<RoomAvailability> LoadForRoom(Guid roomId) =>
            roomAvailabilitiesByRoomId.TryGetValue(roomId, out var ret) ? Deserialize(ret) : null;

        public Either<string, Unit> Save(RoomAvailability roomAvailability)
        {
            var @new = Clone(roomAvailability);
            var current = roomAvailability;

            IncrementVersionOf(@new);

            roomAvailabilitiesByRoomId.AddOrUpdate(@new.RoomId,
                _ => Serialize(@new),
                (_, str) =>
                {
                    current = Deserialize(str);
                    return OptimisticLock(current, @new) ? str : Serialize(@new);
                });

            if (OptimisticLock(current, @new))
            {
                return $"Cannot save {nameof(RoomAvailability)}@{@new.Id} with version {@new.Version} cause current version is {current.Version}";
            }
            return Syntax.Unit;
        }

        private static bool OptimisticLock(RoomAvailability current, RoomAvailability @new) => 
            current.Version >= @new.Version;

        private static RoomAvailability Clone(RoomAvailability roomAvailability) => Deserialize(Serialize(roomAvailability));

        private static string Serialize(RoomAvailability roomAvailability) => JsonConvert.SerializeObject(roomAvailability);

        private static RoomAvailability Deserialize(string value) => JsonConvert.DeserializeObject<RoomAvailability>(value);

        private static void IncrementVersionOf(RoomAvailability availability) =>
            availability.GetType().GetProperty(nameof(RoomAvailability.Version))
                .SetValue(availability, availability.Version + 1);
    }
}