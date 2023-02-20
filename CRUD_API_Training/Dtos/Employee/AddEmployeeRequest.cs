namespace CRUD_API_Training.Dtos.Employee
{
    public class AddEmployeeRequest
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string NRC { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
        public double Salary { get; set; }
    }
}
