using Form.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Form.Data
{
    public class FormDBContext : DbContext
    {
        public FormDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees {  get; set; }
    }
}
