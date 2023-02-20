using CRUD_API_Training.Dtos;
using CRUD_API_Training.Dtos.Department;

namespace CRUD_API_Training.Interfaces
{
    public interface IDepartment
    {
        Task<GetDepartmentRespond> GetDepartments();
        Task<ResponseStatus> CreateDepartment(AddDepartmentRequest req);
        Task<ResponseStatus> UpdateDepartment(UpdateDepartmentRequest req);
        Task<ResponseStatus> DeleteDepartment(int id);
    }
}
