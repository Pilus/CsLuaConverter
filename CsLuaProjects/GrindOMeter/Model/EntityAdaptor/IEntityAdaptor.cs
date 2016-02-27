
namespace GrindOMeter.Model.EntityAdaptor
{
    using System.Collections.Generic;
    using Entity;

    public interface IEntityAdaptor
    {
        List<IEntity> GetAvailableEntities();
        int GetCurrentAmount(int entityId);
    }
}
