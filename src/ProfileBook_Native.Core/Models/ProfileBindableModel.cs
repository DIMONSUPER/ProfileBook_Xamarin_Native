using System;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace ProfileBook_Native.Core.Models
{
    public class ProfileBindableModel : MvxNotifyPropertyChanged
    {
        #region -- Public properties --

        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private int _userId;
        public int UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private string _profileImage;
        public string ProfileImage
        {
            get => _profileImage;
            set => SetProperty(ref _profileImage, value);
        }

        private string _nickName;
        public string NickName
        {
            get => _nickName;
            set => SetProperty(ref _nickName, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        private IMvxCommand _tapCommad;
        public IMvxCommand TapCommad
        {
            get => _tapCommad;
            set => SetProperty(ref _tapCommad, value);
        }

        private IMvxCommand _editCommad;
        public IMvxCommand EditCommad
        {
            get => _editCommad;
            set => SetProperty(ref _editCommad, value);
        }

        private IMvxCommand _deleteCommad;
        public IMvxCommand DeleteCommand
        {
            get => _deleteCommad;
            set => SetProperty(ref _deleteCommad, value);
        }

        #endregion
    }
}
