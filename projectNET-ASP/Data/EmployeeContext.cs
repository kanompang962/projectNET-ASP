using Microsoft.EntityFrameworkCore;
using projectNET_ASP.Models;

namespace projectNET_ASP.Data
{
    public class EmployeeContext:DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options) { }
        public DbSet<Employee> Employee { get; set; }
    }
}
