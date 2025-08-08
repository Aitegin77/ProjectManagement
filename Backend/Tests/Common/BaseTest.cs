using DAL.Context;

namespace Tests.Common
{
    public class BaseTest : IDisposable
    {
        protected AppDbContext Context { get; set; }

        public BaseTest() =>
            Context = ContextFactory.Create();

        public void Dispose() =>
            ContextFactory.Destroy(Context);
    }
}
