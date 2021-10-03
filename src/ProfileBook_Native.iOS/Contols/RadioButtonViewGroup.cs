using System.Collections.Generic;
using System.ComponentModel;

namespace ProfileBook_Native.iOS.Contols
{
    public class RadioButtonViewGroup : INotifyPropertyChanged
    {
        public RadioButtonViewGroup(int selectedIndex = 0, params CustomSelectedItemView[] radioButtons)
        {
            Items = new();
            CreateItems(radioButtons);

            Items[selectedIndex].IsSelected = true;
        }

        #region -- Public properties --

        public List<CustomSelectedItemView> Items { get; }

        private int _selectedIndex;

        public event PropertyChangedEventHandler PropertyChanged;

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                if (_selectedIndex != value)
                {
                    _selectedIndex = value;
                    Items[_selectedIndex].IsSelected = true;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedIndex)));
                }
            }
        }

        #endregion

        #region -- Private helpers --

        private void CreateItems(CustomSelectedItemView[] radioButtons)
        {
            foreach (var item in radioButtons)
            {
                item.Selected += OnItemSelected;
                Items.Add(item);
            }
        }

        private void OnItemSelected(object sender, System.EventArgs e)
        {
            for (var i = 0; i < Items.Count; i++)
            {
                if (Items[i] == sender)
                {
                    SelectedIndex = i;
                }
                else
                {
                    Items[i].IsSelected = false;
                }
            }
        }

        #endregion
    }
}
