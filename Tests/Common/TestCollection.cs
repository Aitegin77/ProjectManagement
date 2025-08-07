using Tests.Common.Fixtures;

namespace Tests.Common
{
    [CollectionDefinition("Test collection")]
    public class TestCollection : ICollectionFixture<MapsterFixture> { }
}
