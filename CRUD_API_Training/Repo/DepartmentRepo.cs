using CRUD_API_Training.Context;
using CRUD_API_Training.Dtos;
using CRUD_API_Training.Dtos.Department;
using CRUD_API_Training.Interfaces;
using CRUD_API_Training.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_API_Training.Repo
{
    public class DepartmentRepo : IDepartment
    {
        private readonly EmployeeContext _context;

        public DepartmentRepo(EmployeeContext context)
        {
            _context = context;
        }

        public async Task<GetDepartmentRespond> GetDepartments()
        {
            try
            {
                var departments = await _context.Departments.Where(x =>
                            x.DepartmentID != 0 && x.IsActive == true).Select(x=>
                            new GetDepartmentItem
                            {
                                DepartmentID = x.DepartmentID,
                                Name = x.Name,
                                TotalEmployee = x.TotalEmployee,
                                TotalSalary = x.TotalSalary,
                                Createddate = x.Createddate,
                                Createdby = x.Createdby,
                                Updateddate = x.Updateddate,
                                Updatedby = x.Updatedby,
                                IsActive = x.IsActive
                            })
                            .ToListAsync();

                return new GetDepartmentRespond
                {
                    Items = departments
                };

            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<ResponseStatus> CreateDepartment(AddDepartmentRequest req)
        {
            try
            {
                if(req!= null)
                {
                    var department = new Department
                    {
                        Name = req.Name,
                        TotalEmployee = req.TotalEmployee,
                        TotalSalary = req.TotalSalary,
                        Createddate = DateTime.Now,
                        Createdby = "Admin",
                        Updateddate = null,
                        Updatedby = null,
                        IsActive = true
                    };

                    await _context.Departments.AddAsync(department);
                    await _context.SaveChangesAsync();

                    return new ResponseStatus
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Department Created Successfully"
                    };
            
                }
                else
                {
                    return new ResponseStatus
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Bad Request"
                    };
                }

            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<ResponseStatus> UpdateDepartment(UpdateDepartmentRequest req)
        {
            try
            {
                int id = req.DepartmentID;
                var model = await _context.Departments.FindAsync(id);

                if(model != null && model.IsActive==true)
                {
                    model.DepartmentID = req.DepartmentID;
                    model.Name = req.Name;
                    model.TotalEmployee = req.TotalEmployee;
                    model.TotalSalary = req.TotalSalary;
                    model.Updateddate = DateTime.Now;
                    model.Updatedby = "Admin";
                    model.IsActive = true;

                    await _context.SaveChangesAsync();

                    return new ResponseStatus
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Department has been Updated"
                    };
                }
                else
                {
                    return new ResponseStatus
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Department Not found"
                    };
                }
            }
            catch(Exception E)
            {
                throw E;
            }
        }

        public async Task<ResponseStatus> DeleteDepartment(int id)
        {
            try
            {
                var department = await _context.Departments.FindAsync(id);

                if (department != null && department.IsActive == true)
                {
                    department.Updateddate = DateTime.Now;
                    department.Updatedby = "Admin";
                    department.IsActive = false;

                    await _context.SaveChangesAsync();

                    return new ResponseStatus
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Department has been deleted"
                    };
                }
                else
                {
                    return new ResponseStatus
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Department Not Found"
                    };
                }
            }
            catch
            {
                return new ResponseStatus
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Bad Request",
                };
            }
        }
    }
}
