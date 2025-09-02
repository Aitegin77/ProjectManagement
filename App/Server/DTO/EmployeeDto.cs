using DTO.Abstract;

namespace DTO
{
    public class EmployeeDto
    {
        public abstract class Base
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Patronymic { get; set; }
        }

        public class List : GetFullName { }

        public class Create : Base
        {
            public string Mail { get; set; }
        }

        public class Get : Create, IBaseDto
        {
            public int Id { get; set; }
        }

        public class Edit : Create, IBaseDto
        {
            public int Id { get; set; }
        }

        public class GetFullName : Base, IBaseDto
        {
            public int Id { get; set; }
        }
    }
}
