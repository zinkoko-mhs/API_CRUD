namespace CRUD_API_Training.Dtos.Department
{
    public class UpdateDepartmentRequest
    {
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public int TotalEmployee { get; set; }
        public double TotalSalary { get; set; }
    }
}
