using Microsoft.EntityFrameworkCore;
using EventLogsAPI.Models;

namespace EventLogsAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<EventLog> EventLogs { get; set; }
    }
}
