using System;
using Fp4OoDevelopers.Functional;

namespace Fp4OoDevelopers.Domain
{
    public interface IRoomAvailabilityStore
    {
        Option<RoomAvailability> LoadForRoom(Guid roomId);
        Either<string, Unit> Save(RoomAvailability roomAvailability);
    }

    public class OptimisticLockException : Exception
    {
        public OptimisticLockException(string message) : base(message)
        {
        }

        public OptimisticLockException(string entityName, Guid id, int currentVersion, int newVersion) : 
            base($"Cannot save {entityName}@{id} with version {newVersion} cause current version is {currentVersion}")
        {
        }
    }
}