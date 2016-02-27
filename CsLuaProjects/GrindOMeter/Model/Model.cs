
namespace GrindOMeter.Model
{
    using System.Collections.Generic;
    using System.Linq;
    using Entity;
    using EntityAdaptor;
    using EntityStorage;
    using Lua;

    public class Model : IModel
    {
        private readonly IEntityAdaptorFactory adaptorFactory;
        private readonly Dictionary<EntityType, IEntityAdaptor> adaptors;
        private readonly IEntityStorage storage;

        public Model(IEntityAdaptorFactory adaptorFactory, IEntityStorage storage)
        {
            this.adaptorFactory = adaptorFactory;
            this.storage = storage;
            this.adaptors = new Dictionary<EntityType, IEntityAdaptor>();
            this.CreateAdaptor(EntityType.Currency);
            this.CreateAdaptor(EntityType.Item);
        }

        private void CreateAdaptor(EntityType type)
        {
            this.adaptors.Add(type, this.adaptorFactory.CreateAdoptor(type));
        }

        public List<IEntity> GetAvailableEntities(EntityType type)
        {
            return this.adaptors[type].GetAvailableEntities();
        }

        public IEntitySample GetCurrentSample(EntityType type, int entityId)
        {
            return new EntitySample(
                this.adaptors[type].GetCurrentAmount(entityId),
                Core.time()
                );
        }

        public List<IEntity> LoadTrackedEntities()
        {
            var knownEntities = new Dictionary<EntityType, List<IEntity>>();
            var trackedEntities = new List<IEntity>();

            foreach (var entity in this.storage.LoadTrackedEntities())
            {
                
                if (!knownEntities.ContainsKey(entity.Type))
                {
                    knownEntities[entity.Type] = this.GetAvailableEntities(entity.Type);
                }

                var entity1 = entity;
                var matchingEntity = knownEntities[entity.Type].FirstOrDefault(ke => ke.Id.Equals(entity1.Id) && ke.Type.Equals(entity1.Type));
                if (matchingEntity != null)
                {
                    trackedEntities.Add(matchingEntity);
                }
            }

            return trackedEntities;
        }

        public void SaveEntityTrackingFlag(EntityType type, int entityId, bool track)
        {
            var trackedEntity = new TrackedEntity(type, entityId);
            if (track)
            {
                this.storage.AddTrackedEntityIfMissing(trackedEntity);
            }
            else
            {
                this.storage.RemoveTrackedEntity(trackedEntity);
            }
        }
    }
}
