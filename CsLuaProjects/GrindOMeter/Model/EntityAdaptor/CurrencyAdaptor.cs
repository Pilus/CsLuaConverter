namespace GrindOMeter.Model.EntityAdaptor
{
    using System.Collections.Generic;
    using BlizzardApi.Global;
    using Entity;

    public class CurrencyAdaptor : IEntityAdaptor
    {
        private const int ThresholdForContinuousMissingCurrency = 100;
        public List<IEntity> GetAvailableEntities()
        {
            var list = new List<IEntity>();

            var id = 1;
            var continuousNullCount = 0;
            while (continuousNullCount < ThresholdForContinuousMissingCurrency)
            {
                var currencyInfo = Global.Api.GetCurrencyInfo(id);

                if (!string.IsNullOrEmpty(currencyInfo.Value1))
                {
                    var currency = new Currency(id, currencyInfo);
                    if (currency.IsDiscovered)
                    {
                        list.Add(currency);
                    }

                    continuousNullCount = 0;
                }
                else
                {
                    continuousNullCount++;
                }
                id++;
            }

            return list;
        }

        public int GetCurrentAmount(int entityId)
        {
            return Global.Api.GetCurrencyInfo(entityId).Value2;
        }
    }
}
