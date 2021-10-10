using System.ComponentModel;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using ProfileBook_Native.Core;
using ProfileBook_Native.Core.Enums;
using ProfileBook_Native.Core.Resources.Strings;
using ProfileBook_Native.Core.ViewModels.Settings;

namespace ProfileBook_Native.Droid.Views.Settings
{
    [Activity]
    public class SettingsView : BaseActivity<SettingsViewModel>, INotifyPropertyChanged
    {
        private TextView _sortByTextView;
        private RadioGroup _sortRadioGroup;
        private RadioButton _dateRadioButton;
        private RadioButton _nameRadioButton;
        private RadioButton _nicknameRadioButton;
        private CheckBox _darkThemeCheckBox;
        private TextView _languagesTextView;
        private NumberPicker _languagesNumberPicker;

        #region -- INotifyPropertyChanged implementation --

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region -- Public properties --

        private ESortOption _selectedSortOption;
        public ESortOption SelectedSortOption
        {
            get => _selectedSortOption;
            set
            {
                if (_selectedSortOption != value)
                {
                    _selectedSortOption = value;
                    _sortRadioGroup.Check(_sortRadioGroup.GetChildAt((int)value).Id);
                    PropertyChanged?.Invoke(this, new(nameof(SelectedSortOption)));
                }
            }
        }

        private ETheme _currentTheme;
        public ETheme CurrentTheme
        {
            get => _currentTheme;
            set
            {
                if (_currentTheme != value)
                {
                    _currentTheme = value;
                    _darkThemeCheckBox.Checked = value == ETheme.Dark;
                    PropertyChanged?.Invoke(this, new(nameof(CurrentTheme)));
                }
            }
        }

        private string _selectedLanguageName;
        public string SelectedLanguageName
        {
            get => _selectedLanguageName;
            set
            {
                if (_selectedLanguageName != value)
                {
                    _selectedLanguageName = value;
                    _languagesNumberPicker.Value = ViewModel.Languages.IndexOf(value);
                    PropertyChanged?.Invoke(this, new(nameof(SelectedLanguageName)));
                }
            }
        }

        #endregion

        #region -- Overrides --

        protected override int ActivityLayoutId => Resource.Layout.SettingsView;

        public override void Finish()
        {
            base.Finish();
            OverridePendingTransition(Resource.Animation.slide_in_left, Resource.Animation.slide_out_right);
            _sortRadioGroup.CheckedChange -= OnSortRadioGroupCheckedChange;
            _darkThemeCheckBox.CheckedChange -= OnDarkThemeCheckedChange;
            _languagesNumberPicker.ValueChanged -= OnLanguageChanged;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                OnBackPressed();
            }

            return base.OnOptionsItemSelected(item);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            OverridePendingTransition(Resource.Animation.slide_in_right, Resource.Animation.slide_out_left);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            _sortByTextView = FindViewById<TextView>(Resource.Id.sort_by_text_view);
            _sortRadioGroup = FindViewById<RadioGroup>(Resource.Id.sort_radio_group);
            _dateRadioButton = FindViewById<RadioButton>(Resource.Id.date_radio_button);
            _nameRadioButton = FindViewById<RadioButton>(Resource.Id.name_radio_button);
            _nicknameRadioButton = FindViewById<RadioButton>(Resource.Id.nickname_radio_button);
            _darkThemeCheckBox = FindViewById<CheckBox>(Resource.Id.dark_theme_check_box);
            _languagesTextView = FindViewById<TextView>(Resource.Id.languages_text_view);
            _languagesNumberPicker = FindViewById<NumberPicker>(Resource.Id.languages_number_picker);

            _languagesNumberPicker.MinValue = 0;
            _languagesNumberPicker.MaxValue = 2;
            _languagesNumberPicker.DescendantFocusability = DescendantFocusability.BlockDescendants;
            _languagesNumberPicker.SetDisplayedValues(ViewModel.Languages.ToArray());

            var set = this.CreateBindingSet<SettingsView, SettingsViewModel>();
            set.Bind().For(x => x.SelectedSortOption).To(vm => vm.SelectedSortOption);
            set.Bind().For(x => x.CurrentTheme).To(vm => vm.CurrentTheme);
            set.Bind().For(x => x.SelectedLanguageName).To(vm => vm.SelectedLanguageName);
            set.Apply();

            SetLocalazableStrings();

            _sortRadioGroup.CheckedChange += OnSortRadioGroupCheckedChange;
            _darkThemeCheckBox.CheckedChange += OnDarkThemeCheckedChange;
            _languagesNumberPicker.ValueChanged += OnLanguageChanged;
        }

        #endregion

        #region -- Private helpers --

        private void SetLocalazableStrings()
        {
            SupportActionBar.Title = Strings.Settings;
            _sortByTextView.Text = Strings.SortBy;
            _dateRadioButton.Text = Strings.Date;
            _nameRadioButton.Text = Strings.Name;
            _nicknameRadioButton.Text = Strings.NickName;
            _darkThemeCheckBox.Text = Strings.Dark;
            _languagesTextView.Text = Strings.ChooseLanguage;
        }

        private void OnLanguageChanged(object sender, NumberPicker.ValueChangeEventArgs e)
        {
            SelectedLanguageName = ViewModel.Languages[e.NewVal];

            SetLocalazableStrings();
        }

        private void OnDarkThemeCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            CurrentTheme = e.IsChecked ? ETheme.Dark : ETheme.Light;
        }

        private void OnSortRadioGroupCheckedChange(object sender, RadioGroup.CheckedChangeEventArgs e)
        {
            SelectedSortOption = e.CheckedId switch
            {
                Resource.Id.date_radio_button => ESortOption.ByDate,
                Resource.Id.name_radio_button => ESortOption.ByName,
                Resource.Id.nickname_radio_button => ESortOption.ByNickname,
                _ => throw new System.NotImplementedException(),
            };
        }

        #endregion
    }
}
