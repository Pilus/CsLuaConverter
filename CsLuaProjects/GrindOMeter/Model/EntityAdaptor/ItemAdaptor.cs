namespace GrindOMeter.Model.EntityAdaptor
{
    using System.Collections.Generic;
    using System.Linq;
    using BlizzardApi.Global;
    using Entity;

    public class ItemAdaptor : IEntityAdaptor
    {
        private const int FirstBagId = -1;
        private const int LastBagId = 11;

        public List<IEntity> GetAvailableEntities()
        {
            var list = new List<IEntity>();

            for (var bagId = FirstBagId; bagId <= LastBagId; bagId++)
            {
                for (var slot = 1; slot < Global.Api.GetContainerNumSlots(bagId); slot++)
                {
                    var itemId = Global.Api.GetContainerItemID(bagId, slot);
                    if (itemId != null && !list.Any(item => item.Id.Equals(itemId)))
                    {
                        var item = new Item((int)itemId, Global.Api.GetItemInfo((int)itemId));
                        if (item.StackSize > 1)
                        {
                            list.Add(item);
                        }
                    }
                }
            }

            return list;
        }

        public int GetCurrentAmount(int entityId)
        {
            return Global.Api.GetItemCount(entityId);
        }
    }
}
