namespace ProfileBook_Native.Core.Services.Settings
{
    public interface ISettingsService
    {
        bool IsAuthCompleted { get; set; }
        bool IsDarkModeOn { get; set; }
        bool IsRememberMe { get; set; }
        string Language { get; set; }
        int UserId { get; set; }
    }
}
