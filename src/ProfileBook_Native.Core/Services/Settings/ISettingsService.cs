namespace ProfileBook_Native.Core.Services.Settings
{
    public interface ISettingsService
    {
        bool IsAuthCompleted { get; set; }
        int Theme { get; set; }
        bool IsRememberMe { get; set; }
        string Language { get; set; }
        int CurrentUserId { get; set; }
        int SortOption { get; set; }
    }
}
