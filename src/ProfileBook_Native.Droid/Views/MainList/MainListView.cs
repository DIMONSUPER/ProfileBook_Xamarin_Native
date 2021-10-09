using System;
using Android.OS;
using ProfileBook_Native.Core.ViewModels.MainList;

namespace ProfileBook_Native.Droid.Views.MainList
{
    public class MainListView : BaseActivity<MainListViewModel>
    {
        #region -- Overrides --

        protected override int ActivityLayoutId ;

        protected override void OnStart()
        {
            base.OnStart();

        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
        }

        #endregion

        #region -- Private helpers --

        private void SetLocalazableStrings()
        {
        }

        #endregion
    }
}
