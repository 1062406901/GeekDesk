﻿using System.Collections.ObjectModel;
using System.ComponentModel;

namespace GeekDesk.ViewModel.Temp
{
    public class SearchIconList
    {

        private static ObservableCollection<IconInfo> iconList = new ObservableCollection<IconInfo>();

        public static ObservableCollection<IconInfo> IconList
        {
            get
            {
                return iconList;
            }
            set
            {
                iconList = value;
                OnPropertyChanged("IconList");
            }
        }


        public static event PropertyChangedEventHandler PropertyChanged;
        private static void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }
    }
}
