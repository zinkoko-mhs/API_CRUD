using CRUD_API_Training.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_API_Training.Context
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext()
        {
        }

        public EmployeeContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
    }
}
