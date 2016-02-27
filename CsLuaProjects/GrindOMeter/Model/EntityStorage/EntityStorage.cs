namespace GrindOMeter.Model.EntityStorage
{
    using System.Collections.Generic;
    using System.Linq;
    using BlizzardApi.Global;
    using CsLuaFramework;
    using Lua;

    public class EntityStorage : IEntityStorage
    {
        public const string TrackedEntitiesSavedVariableName = "GrindOMeter_TrackedEntities";

        private List<TrackedEntity> trackedEntities;
        private readonly ISerializer serializer;

        public EntityStorage()
        {
            this.serializer = new Serializer();
        }

        public List<TrackedEntity> LoadTrackedEntities()
        {
            if (this.trackedEntities != null) return this.trackedEntities;

            var savedData = (NativeLuaTable)Global.Api.GetGlobal(TrackedEntitiesSavedVariableName);
            this.trackedEntities = savedData == null
                ? new List<TrackedEntity>()
                : this.serializer.Deserialize<List<TrackedEntity>>(savedData);

            return this.trackedEntities;
        }

        public void RemoveTrackedEntity(TrackedEntity trackedEntity)
        {
            this.ThrowIfEntitiesNotLoaded();
            var entity = this.trackedEntities.FirstOrDefault(e => e.Equals(trackedEntity));
            if (entity == null)
            {
                throw new EntityStorageException("Could not find the tracked entity to remove.");
            };

            this.trackedEntities.Remove(entity);
            this.SaveTrackedEntities();
        }

        public void AddTrackedEntityIfMissing(TrackedEntity trackedEntity)
        {
            this.ThrowIfEntitiesNotLoaded();
            if (this.trackedEntities.Any(e => e.Equals(trackedEntity))) return;

            this.trackedEntities.Add(trackedEntity);
            this.SaveTrackedEntities();
        }

        private void SaveTrackedEntities()
        {
            Global.Api.SetGlobal(TrackedEntitiesSavedVariableName, this.serializer.Serialize(this.trackedEntities));
        }

        private void ThrowIfEntitiesNotLoaded()
        {
            if (this.trackedEntities == null)
            {
                throw new EntityStorageException("Entities not yet loaded.");
            }
        }

    }
}
