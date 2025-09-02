using DAL.Entities.Abstract;

namespace DAL.Entities
{
    public class Document : BaseEntity
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
