using Microsoft.EntityFrameworkCore;

namespace E7_Audit
{
    public class AuditContext : DbContext
    {
        public DbSet<FileAudit> FileAudits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=e7.db");
        }
    }
}
