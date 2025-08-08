using DTO.Abstract;

namespace DTO
{
    public class EmployeeDto
    {
        public class GetFullName : IBaseDto
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Patronymic { get; set; }
        }
    }
}
