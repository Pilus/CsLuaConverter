

namespace GrindOMeter.Model
{
    using System.Collections.Generic;
    using Entity;

    public interface IModel
    {
        List<IEntity> GetAvailableEntities(EntityType type);
        IEntitySample GetCurrentSample(EntityType type, int entityId);
        void SaveEntityTrackingFlag(EntityType type, int entityId, bool track);
        List<IEntity> LoadTrackedEntities();
    }
}
