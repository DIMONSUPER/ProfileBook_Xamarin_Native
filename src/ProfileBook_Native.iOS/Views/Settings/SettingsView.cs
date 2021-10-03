using MvvmCross.Binding.BindingContext;
using ProfileBook_Native.Core.Resources.Strings;
using ProfileBook_Native.Core.ViewModels.Settings;
using ProfileBook_Native.iOS.Contols;
using UIKit;

namespace ProfileBook_Native.iOS.Views.Settings
{
    public partial class SettingsView : BaseViewController<SettingsViewModel>
    {
        private LanguagePickerDataSource _languagePickerSource;

        private readonly CustomSelectedItemView _dateRadioButton;
        private readonly CustomSelectedItemView _nickNameRadioButton;
        private readonly CustomSelectedItemView _nameRadioButton;
        private readonly RadioButtonViewGroup _sortOptionsRadioButtonGroup;

        private readonly CustomSelectedItemView _lightRadioButton;
        private readonly CustomSelectedItemView _darkRadioButton;
        private readonly RadioButtonViewGroup _themeRadioButtonGroup;

        public SettingsView()
        {
            _dateRadioButton = new();
            _nickNameRadioButton = new();
            _nameRadioButton = new();
            _sortOptionsRadioButtonGroup = new RadioButtonViewGroup(0, _dateRadioButton, _nickNameRadioButton, _nameRadioButton);

            _lightRadioButton = new();
            _darkRadioButton = new();
            _themeRadioButtonGroup = new(0, _lightRadioButton, _darkRadioButton);
        }

        #region -- Overrides --

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            foreach(var view in _sortOptionsRadioButtonGroup.Items)
            {
                SortOptionsStackView.AddArrangedSubview(view);
            }

            foreach (var view in _themeRadioButtonGroup.Items)
            {
                ThemeOptionsStackView.AddArrangedSubview(view);
            }
        }

        protected override void BindView()
        {
            base.BindView();

            BindLangugesPickerView();

            this.CreateBinding(_sortOptionsRadioButtonGroup)
                .For(x => x.SelectedIndex)
                .To<SettingsViewModel>(x => x.SelectedSortOption)
                .Apply();

            this.CreateBinding(_themeRadioButtonGroup)
                .For(x => x.SelectedIndex)
                .To<SettingsViewModel>(x => x.CurrentTheme)
                .Apply();
        }

        public override void SetLocalizableStrings()
        {
            Title = Strings.Settings;
            SortByLabel.Text = Strings.SortBy;
            ThemeLabel.Text = Strings.Theme;
            _dateRadioButton.TitleLabel.Text = Strings.Date;
            _nickNameRadioButton.TitleLabel.Text = Strings.NickName;
            _nameRadioButton.TitleLabel.Text = Strings.Name;
            _lightRadioButton.TitleLabel.Text = Strings.Light;
            _darkRadioButton.TitleLabel.Text = Strings.Dark;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _languagePickerSource.SelectedItemChanged -= OnSelectedItemChanged;
            }

            base.Dispose(disposing);
        }

        #endregion

        #region -- Private helpers --

        private void BindLangugesPickerView()
        {
            _languagePickerSource = new LanguagePickerDataSource(LanguagePickerView);
            LanguagePickerView.DataSource = _languagePickerSource;
            this.CreateBinding(_languagePickerSource).For(x => x.ItemsSource).To<SettingsViewModel>(vm => vm.Languages).Apply();
            this.CreateBinding(_languagePickerSource).For(x => x.SelectedItem).To<SettingsViewModel>(vm => vm.SelectedLanguageName).Apply();
            _languagePickerSource.SelectedItemChanged += OnSelectedItemChanged;
        }

        private void OnSelectedItemChanged(object sender, System.EventArgs e)
        {
            foreach (var vc in NavigationController.ViewControllers)
            {
                if (vc is ILocalazibleViewController viewController)
                {
                    viewController.SetLocalizableStrings();
                }
            }
        }

        #endregion
    }
}

