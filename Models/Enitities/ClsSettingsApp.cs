
namespace ALAoun_Pos.Models
{
    public class ClsSettingsApp
    {
        public int settingId { get; set; }
        public string? settingKey { get; set; }
        public string? settingValue { get; set; }
        public string? settingType { get; set; }
        public bool IsEditable { get; set; }
        public string? Description { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int posId { get; set; }
    }
}