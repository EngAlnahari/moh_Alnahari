using Microsoft.EntityFrameworkCore;
using Mntry_Awqaf.Models;

namespace Mntry_Awqaf.Data
{
    public class DynamicFormsDbContext : DbContext
    {
        public DynamicFormsDbContext(DbContextOptions<DynamicFormsDbContext> options)
            : base(options)
        {
        }

        public DbSet<DynamicField> DynamicFields { get; set; }
        public DbSet<FieldAssignment> FieldAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Optional configuration
        }
    }
}
