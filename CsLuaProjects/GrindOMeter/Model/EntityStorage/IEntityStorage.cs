namespace GrindOMeter.Model.EntityStorage
{
    using System.Collections.Generic;

    public interface IEntityStorage
    {
        List<TrackedEntity> LoadTrackedEntities();
        void AddTrackedEntityIfMissing(TrackedEntity trackedEntity);
        void RemoveTrackedEntity(TrackedEntity trackedEntity);
    }
}
