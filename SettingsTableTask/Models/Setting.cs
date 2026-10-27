using System.ComponentModel.DataAnnotations;

namespace SettingsTableTask.Models
{
    public class Setting
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string SettingKey { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string SettingValue { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Description { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}