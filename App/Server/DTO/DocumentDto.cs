using DTO.Abstract;

namespace DTO
{
    public class DocumentDto
    {
        public abstract class Base
        {
            public string Name { get; set; }
        }

        public class Get : Base, IBaseDto
        {
            public int Id { get; set; }
        }
    }
}
