namespace CRUD_API_Training.Dtos.Employee
{
    public class GetEmployeesRequest : Pagination
    {
        public Filter filter { get; set; }
    }
    public class Filter
    {
        public string EmpId { get; set; }
        public string Department { get; set; }
    }
}
