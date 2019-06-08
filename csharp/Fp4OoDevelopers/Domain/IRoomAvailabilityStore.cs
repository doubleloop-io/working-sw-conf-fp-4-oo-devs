using System;

namespace Fp4OoDevelopers.Domain
{
    public interface IRoomAvailabilityStore
    {
        RoomAvailability LoadForRoom(Guid roomId);
        void Save(RoomAvailability roomAvailability);
    }

    public class OptimisticLockException : Exception
    {
        public OptimisticLockException(string entityName, Guid id, int currentVersion, int newVersion) : 
            base($"Cannot save {entityName}@{id} with version {newVersion} cause current version is {currentVersion}")
        {
        }
    }
}