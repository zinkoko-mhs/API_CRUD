namespace CRUD_API_Training.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string NRC { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
        public double Salary { get; set; }
        public DateTime Createddate { get; set; }
        public string Createdby { get; set; }
        public DateTime? Updateddate { get; set; }
        public string? Updatedby { get; set; }
        public bool IsActive { get; set; }

    }
}
