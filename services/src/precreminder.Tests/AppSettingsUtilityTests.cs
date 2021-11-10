using NUnit.Framework;
using prescreminder.API.Domain;

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
