using CRUD_API_Training.Dtos;
using CRUD_API_Training.Dtos.Employee;

namespace CRUD_API_Training.Interfaces
{
    public interface IEmployee
    {
        Task<GetEmployeeRespond> GetEmployees(Pagination req);
        Task<GetEmployeeRespond> GetEmployeeByID(int id);
        Task<ResponseStatus> AddEmployee(AddEmployeeRequest req);
        Task<ResponseStatus> UpdateEmployee(UpdateEmployeeRequest req);
        Task<ResponseStatus> DeleteEmployee(int id);
    }
}
