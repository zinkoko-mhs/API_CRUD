namespace CRUD_API_Training.Dtos.Department
{
    public class GetDepartmentRespond
    {
        public List<GetDepartmentItem> Items { get; set; }
    }

    public class GetDepartmentItem
    {
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public int TotalEmployee { get; set; }
        public double TotalSalary { get; set; }
        public DateTime Createddate { get; set; }
        public string Createdby { get; set; }
        public DateTime? Updateddate { get; set; }
        public string? Updatedby { get; set; }
        public bool IsActive { get; set; }
    }
}
