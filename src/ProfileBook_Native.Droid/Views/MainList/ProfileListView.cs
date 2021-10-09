using Android.Content;
using Android.Util;
using MvvmCross.Platforms.Android.Binding.Views;

namespace ProfileBook_Native.Droid.Views.MainList
{
    public class ProfileListView : MvxListView
    {
        public ProfileListView(Context context, IAttributeSet attrs)
        : base(context, attrs, new ProfileListViewAdapter(context))
        {
        }
    }
}
