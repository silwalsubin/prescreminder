using contracts.Persistence;
using NUnit.Framework;
using prescreminder.Utilities;

namespace precreminder.Tests
{
    public class AppSettingsUtilityTests
    {
        [Test]
        public void AppSettingsTest()
        {
            var persistenceSettings = AppSettingsUtility.GetSettings<PersistenceSettings>();
            Assert.IsNotNull(persistenceSettings);
        }
    }
}
