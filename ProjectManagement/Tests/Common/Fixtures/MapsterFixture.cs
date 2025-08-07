using AppProjectManagement.Extensions;
using Microsoft.AspNetCore.Builder;

namespace Tests.Common.Fixtures
{
    internal class MapsterFixture
    {
        public MapsterFixture()
        {
            WebApplication.CreateBuilder().Build().RegisterMapping();
        }
    }
}
