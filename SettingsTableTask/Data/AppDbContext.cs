using Microsoft.EntityFrameworkCore;
using SettingsTableTask.Models;

namespace SettingsTableTask.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Setting> Settings { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}