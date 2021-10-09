using Android.Content;
using Android.Views;
using MvvmCross.Platforms.Android.Binding.Views;

namespace ProfileBook_Native.Droid.Views.MainList
{
    public class ProfileListViewAdapter : MvxAdapter
    {
        public ProfileListViewAdapter(Context context) : base(context)
        {
        }

        #region -- Overrides --

        protected override IMvxListItemView CreateBindableView(object dataContext, ViewGroup parent, int templateId)
        {
            return new ProfileListViewItem(Context, BindingContext.LayoutInflaterHolder, dataContext, parent, templateId);
        }

        #endregion
    }
}
