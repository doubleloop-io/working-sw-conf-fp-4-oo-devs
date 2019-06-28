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

        public void Save(RoomAvailability roomAvailability)
        {
            var @new = Clone(roomAvailability);

            IncrementVersionOf(@new);

            roomAvailabilitiesByRoomId.AddOrUpdate(@new.RoomId,
                _ => Serialize(@new),
                (_, str) =>
                {
                    var current = Deserialize(str);
                    if (current.Version >= @new.Version)
                        ThrowOptimisticLock(@new, current);
                    return Serialize(@new);
                });
        }

        public Either<string, Unit> SaveEither(RoomAvailability roomAvailability)
        {
            var @new = Clone(roomAvailability);
            RoomAvailability current = null;

            IncrementVersionOf(@new);

            roomAvailabilitiesByRoomId.AddOrUpdate(@new.RoomId,
                _ =>
                {
                    current = roomAvailability;
                    return Serialize(@new);
                },
                (_, str) =>
                {
                    current = Deserialize(str);
                    return current.Version >= @new.Version ? str : Serialize(@new);
                });

            if (current.Version >= @new.Version)
            {
                return $"Cannot save {nameof(RoomAvailability)}@{@new.Id} with version {@new.Version} cause current version is {current.Version}";
            }
            return Syntax.Unit;
        }

        private static void ThrowOptimisticLock(RoomAvailability @new, RoomAvailability current) => 
            throw new OptimisticLockException(nameof(RoomAvailability), @new.Id, current.Version, @new.Version);

        private static RoomAvailability Clone(RoomAvailability roomAvailability) => Deserialize(Serialize(roomAvailability));

        private static string Serialize(RoomAvailability roomAvailability) => JsonConvert.SerializeObject(roomAvailability);

        private static RoomAvailability Deserialize(string value) => JsonConvert.DeserializeObject<RoomAvailability>(value);

        private static void IncrementVersionOf(RoomAvailability availability) =>
            availability.GetType().GetProperty(nameof(RoomAvailability.Version))
                .SetValue(availability, availability.Version + 1);
    }
}