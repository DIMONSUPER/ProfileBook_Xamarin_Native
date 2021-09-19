using Plugin.Settings.Abstractions;

namespace ProfileBook_Native.Core.Services.Settings
{
    public class SettingsService : ISettingsService
    {
        private readonly ISettings _settings;

        public SettingsService(ISettings settings)
        {
            _settings = settings;
        }

        #region -- ISettingsService implementation --

        public bool IsAuthCompleted
        {
            get => _settings.GetValueOrDefault(nameof(IsAuthCompleted), false);
            set => _settings.AddOrUpdateValue(nameof(IsAuthCompleted), value);
        }

        public bool IsDarkModeOn
        {
            get => _settings.GetValueOrDefault(nameof(IsDarkModeOn), false);
            set => _settings.AddOrUpdateValue(nameof(IsDarkModeOn), value);
        }

        public bool IsRememberMe
        {
            get => _settings.GetValueOrDefault(nameof(IsRememberMe), false);
            set => _settings.AddOrUpdateValue(nameof(IsRememberMe), value);
        }

        public string Language
        {
            get => _settings.GetValueOrDefault(nameof(Language), string.Empty);
            set => _settings.AddOrUpdateValue(nameof(Language), value);
        }

        public int UserId
        {
            get => _settings.GetValueOrDefault(nameof(UserId), -1);
            set => _settings.AddOrUpdateValue(nameof(UserId), value);
        }

        #endregion
    }
}
