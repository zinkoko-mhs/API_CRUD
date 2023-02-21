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
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Promotion> Promotions { get; set; }
        public virtual DbSet<IsFavourited> IsFavourited { get; set; }
        public virtual DbSet<Images> Images { get; set; }
    }
}
