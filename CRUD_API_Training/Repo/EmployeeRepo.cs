using CRUD_API_Training.Context;
using CRUD_API_Training.Dtos;
using CRUD_API_Training.Dtos.Employee;
using CRUD_API_Training.Interfaces;
using CRUD_API_Training.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_API_Training.Repo
{
    public class EmployeeRepo : IEmployee
    {
        private readonly EmployeeContext _context;

        public EmployeeRepo(EmployeeContext context)
        {
            _context = context;
        }

        public async Task<GetEmployeeRespond> GetEmployees(Pagination req)
        {
            try
            {
                int totalItems = await _context.Employees.Where(x => x.EmployeeID != 0 && x.IsActive == true).CountAsync();

                var employees = await _context.Employees.Where(x=> x.EmployeeID != 0 && x.IsActive == true).Select(e=>
                                new GetEmployeeItem
                                {
                                    EmployeeID= e.EmployeeID,
                                    Name=e.Name,
                                    DateOfBirth= e.DateOfBirth,
                                    NRC= e.NRC,
                                    Email=e.Email,
                                    Phone=e.Phone,
                                    Address=e.Address,
                                    Department=e.Department,
                                    Salary=e.Salary,
                                    Createdby=e.Createdby,
                                    Createddate=e.Createddate,
                                    Updatedby   =e.Updatedby,
                                    Updateddate=e.Updateddate,
                                    IsActive=e.IsActive,
                                }).Skip((req.PageNumber-1)*req.PageSize).Take(req.PageSize).ToListAsync();

                int pageItems = employees.Count();

                return new GetEmployeeRespond
                {
                    Items = employees,
                    TotalItems = totalItems,
                    PageItems = pageItems,
                    PageNumber = req.PageNumber
                };
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GetEmployeeRespond> GetEmployeeByID(int id)
        {
            try
            {
                var employee = await _context.Employees.Where(x => x.EmployeeID == id && x.IsActive==true).Select(x=>
                                new GetEmployeeItem
                                {
                                    EmployeeID= x.EmployeeID,
                                    Name=x.Name,
                                    DateOfBirth= x.DateOfBirth,
                                    NRC= x.NRC,
                                    Email= x.Email,
                                    Phone= x.Phone,
                                    Address= x.Address,
                                    Salary= x.Salary,
                                    Createdby= x.Createdby,
                                    Createddate= x.Createddate,
                                    Updatedby=x.Updatedby,
                                    Updateddate= x.Updateddate,
                                    IsActive=x.IsActive,
                                }).ToListAsync();

                return new GetEmployeeRespond 
                { 
                    Items = employee
                };
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseStatus> AddEmployee(AddEmployeeRequest req)
        {
            if(req != null)
            {
                var employee = new Employee
                {
                    Name= req.Name,
                    DateOfBirth= req.DateOfBirth,
                    NRC= req.NRC,
                    Email=req.Email,
                    Phone=req.Phone,
                    Address=req.Address,
                    Department =req.Department,
                    Salary=req.Salary,
                    Createddate = DateTime.Now,
                    Createdby = "Admin",
                    Updateddate = null,
                    Updatedby = null,
                    IsActive = true
                };

                var departments = await _context.Departments.Where(x =>
                                    x.DepartmentID != 0 && x.IsActive == true).Select(x=> x.Name).Distinct().ToListAsync();

                int id = await (from d in _context.Departments
                                where d.Name == req.Department && d.IsActive== true 
                                select d.DepartmentID).FirstAsync();

                if (departments.Contains(req.Department))
                {
                    var model = await _context.Departments.FindAsync(id);

                    if(model != null)
                    {
                        model.TotalEmployee++;
                        model.TotalSalary += req.Salary;

                        await _context.Employees.AddAsync(employee);
                        await _context.SaveChangesAsync();

                        return new ResponseStatus
                        {
                            StatusCode = StatusCodes.Status200OK,
                            Message = "New Employee Added"
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
                return new ResponseStatus
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "New Employee Added"
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

        public async Task<ResponseStatus> UpdateEmployee(UpdateEmployeeRequest req)
        {
            try
            {
                int id = req.EmployeeId;
                var employee = await _context.Employees.FindAsync(id);

                if(employee!=null)
                {
                    //To use to update department table
                    string temp_dep_name = employee.Department;
                    double temp_salary = employee.Salary;

                    //employee.EmployeeID = id;
                    employee.Name= req.Name;
                    employee.DateOfBirth = req.DateOfBirth;
                    employee.NRC = req.NRC;
                    employee.Email = req.Email; 
                    employee.Phone = req.Phone;
                    employee.Address = req.Address;
                    employee.Department= req.Department;
                    employee.Salary = req.Salary;
                    employee.Updateddate = DateTime.Now;
                    employee.Updatedby = "Admin";
                    employee.IsActive = true;


                    var departments = await _context.Departments.Where(x =>
                                    x.DepartmentID != 0 && x.IsActive == true).Select(x => x.Name).Distinct().ToListAsync();

                    if (departments.Contains(req.Department)==false)
                    {
                        return new ResponseStatus
                        {
                            StatusCode = StatusCodes.Status404NotFound,
                            Message = "Department Doesn't exits"

                        };
                    }

                    //For the increasing department

                    if(temp_dep_name != req.Department)
                    {
                        int Inc_Departmentid = await (from d in _context.Departments
                                                      where d.Name == req.Department && d.IsActive == true
                                                      select d.DepartmentID).FirstAsync();

                        /*if(Inc_Departmentid == null)
                        {
                            Inc_Departmentid = 0;
                        }*/

                        var Inc_department = await _context.Departments.FindAsync(Inc_Departmentid);

                        if (Inc_department != null && Inc_department.IsActive == true)
                        {
                            Inc_department.TotalEmployee += 1;
                            Inc_department.TotalSalary += req.Salary;
                        }

                        //For the decreasing department
                        int Des_Departmentid = await (from d in _context.Departments
                                                      where d.Name == temp_dep_name && d.IsActive == true
                                                      select d.DepartmentID).FirstAsync();

                        /*if(Des_Departmentid == null)
                        {
                            Des_Departmentid = 0;
                        }*/

                        var Dec_department = await _context.Departments.FindAsync(Des_Departmentid);

                        if (Dec_department != null && Dec_department.IsActive == true)
                        {
                            Dec_department.TotalEmployee -= 1;
                            Dec_department.TotalSalary -= temp_salary;
                        }
                    }
                    else
                    {
                        int Inc_Departmentid = await (from d in _context.Departments
                                                      where d.Name == req.Department && d.IsActive == true
                                                      select d.DepartmentID).FirstAsync();

                        var Inc_department = await _context.Departments.FindAsync(Inc_Departmentid);

                        if (Inc_department != null && Inc_department.IsActive == true)
                        {
                            Inc_department.TotalSalary = (Inc_department.TotalSalary-temp_salary)+req.Salary;
                        }
                    }

                    await _context.SaveChangesAsync();

                    return new ResponseStatus
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Employee has been updated"
                    };
                }
                else
                {
                    return new ResponseStatus
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Employee ID doesn't exits"
                    };
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<ResponseStatus> DeleteEmployee(int id)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(id);

                if (employee != null && employee.IsActive == true)
                {
                    employee.Updateddate = DateTime.Now;
                    employee.Updatedby = "Admin";
                    employee.IsActive = false;

                    var departments = await _context.Departments.Where(x =>
                                    x.DepartmentID != 0 && x.IsActive == true).Select(x => x.Name).Distinct().ToListAsync();

                    int Departmentid = await (from d in _context.Departments
                                    where d.Name == employee.Department && d.IsActive == true
                                    select d.DepartmentID).FirstAsync();


                    if (departments.Contains(employee.Department))
                    {
                        var department = await _context.Departments.FindAsync(Departmentid);

                        //if(department !=null)
                        //{
                            department.TotalEmployee--;
                            department.TotalSalary-=employee.Salary;

                            await _context.SaveChangesAsync();
                        //}
                       // else
                       // {
                           // await _context.SaveChangesAsync();
                       // }
                    }

                    return new ResponseStatus
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Employee has been deleted"
                    };
                }
                else
                {
                    return new ResponseStatus
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = "Employee ID doesn't exits"
                    };
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
    }
}
