using Tests.Data.Abstract;

namespace Tests.Data
{
    internal class ProjectTestData : BaseTestData
    {
        public static string Name = "Project-1";
        public static string Customer = "Customer-2";
        public static string Performer = "Performer-3";
        public static DateOnly Date = DateOnly.FromDateTime(DateTime.Today);
        public static int Priority = 4;
    }
}
