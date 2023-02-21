using CRUD_API_Training.Dtos;
using CRUD_API_Training.Dtos.Department;
using CRUD_API_Training.Dtos.Employee;

namespace CRUD_API_Training.Interfaces
{
    public interface IDepartment
    {
        Task<GetDepartmentRespond> GetDepartments(Pagination req);
        Task<GetEmployeeRespond> GetEmployeesByDepartment(GetEmployeesRequest req);
        Task<ResponseStatus> CreateDepartment(AddDepartmentRequest req);
        Task<ResponseStatus> UpdateDepartment(UpdateDepartmentRequest req);
        Task<ResponseStatus> DeleteDepartment(int id);
    }
}
