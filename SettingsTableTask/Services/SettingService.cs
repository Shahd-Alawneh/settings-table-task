using Microsoft.EntityFrameworkCore;
using SettingsTableTask.Data;
using SettingsTableTask.Models;

namespace SettingsTableTask.Services
{
    public class SettingService
    {
        private readonly AppDbContext _context;

        public SettingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Setting>> GetAll()
        {
            return await _context.Settings.ToListAsync();
        }

        public async Task Update(Setting setting)
        {
            var existing = await _context.Settings.FindAsync(setting.Id);

            if (existing != null)
            {
                existing.SettingValue = setting.SettingValue;
                await _context.SaveChangesAsync();
            }
        }
    }
}