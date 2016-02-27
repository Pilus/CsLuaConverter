namespace GrindOMeter.IntegrationTests
{
    using System.Collections.Generic;
    using BlizzardApi.Global;
    using Moq;
    using TestUtils;
    using WoWSimulator.ApiMocks;

    public class CurrencySystem : IApiMock
    {
        public Dictionary<int, int> Amounts = new Dictionary<int, int>();

        private int GetAmount(int id)
        {
            return this.Amounts.ContainsKey(id) ? this.Amounts[id] : 0;
        }

        public void Mock(Mock<IApi> apiMock)
        {
            apiMock.Setup(api => api.GetCurrencyInfo(It.IsAny<int>()))
                .Returns((int id) =>
                {
                    if (id >= 77 && id <= 160)
                    {
                        return TestUtil.StructureMultipleValues("CurrencyName" + id, this.GetAmount(id), "iconPath" + id,
                            7, 100, 1000, id < 100);
                    }
                    return TestUtil.StructureMultipleValues("", 0, "", 0, 0, 0, false);
                });
        }
    }
}