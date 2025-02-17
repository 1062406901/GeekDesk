﻿using GeekDesk.Constant;
using GeekDesk.Util;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace GeekDesk.ViewModel
{

    [Serializable]
    public class MenuInfo : INotifyPropertyChanged
    {


        private string menuName;
        private string menuId;
        private MenuTypeEnum menuType = MenuTypeEnum.NORMAL;
        private string relationPath;
        private Visibility menuEdit = Visibility.Collapsed;
        private Visibility notMenuEdit = Visibility.Visible;
        private string menuGeometry;  //菜单几何图标
        private string geometryColor; //几何图标颜色
        private ObservableCollection<IconInfo> iconList = new ObservableCollection<IconInfo>();
        private bool isEncrypt;  //是否加密

        public string RelationPath
        {
            get => relationPath;
            set
            {
                relationPath = value;
                OnPropertyChanged("RelationPath");
            }
        }

        public MenuTypeEnum MenuType
        {
            get => menuType;
            set
            {
                menuType = value;
                OnPropertyChanged("MenuType");
            }
        }

        public bool IsEncrypt
        {
            get
            {
                return isEncrypt;
            }
            set
            {
                isEncrypt = value;
                OnPropertyChanged("IsEncrypt");
            }
        }

        public string MenuGeometry
        {
            get
            {
                if (menuGeometry == null)
                {
                    return Constants.DEFAULT_MENU_GEOMETRY;
                }
                return menuGeometry;
            }
            set
            {
                menuGeometry = value;
                OnPropertyChanged("MenuGeometry");
            }
        }

        public string GeometryColor
        {
            get
            {
                if (geometryColor == null)
                {
                    return Constants.DEFAULT_MENU_GEOMETRY_COLOR;
                }
                return geometryColor;
            }
            set
            {
                geometryColor = value;
                OnPropertyChanged("GeometryColor");
            }
        }

        public string MenuName
        {
            get
            {
                return menuName;
            }
            set
            {
                menuName = value;
                OnPropertyChanged("MenuName");
            }
        }

        public string MenuId
        {
            get
            {
                return menuId;
            }
            set
            {
                menuId = value;
                OnPropertyChanged("MenuId");
            }
        }

        public Visibility MenuEdit
        {
            get
            {
                return menuEdit;
            }
            set
            {
                menuEdit = value;
                if (menuEdit == Visibility.Visible)
                {
                    NotMenuEdit = Visibility.Collapsed;
                }
                else
                {
                    NotMenuEdit = Visibility.Visible;
                }
                OnPropertyChanged("MenuEdit");
            }
        }

        public Visibility NotMenuEdit
        {
            get
            {
                return notMenuEdit;
            }
            set
            {
                notMenuEdit = value;
                OnPropertyChanged("NotMenuEdit");
            }
        }

        public ObservableCollection<IconInfo> IconList
        {
            get
            {
                //如果是关联文件夹类型，实时读取
                if (menuType == MenuTypeEnum.RELATION_FOLDER)
                {
                    DirectoryInfo dir = new DirectoryInfo(RelationPath);
                    if (dir.Exists)
                    {
                        ObservableCollection<IconInfo> relationIconInfo = new ObservableCollection<IconInfo>();
                        var folders = dir.GetDirectories();
                        foreach (var directoryInfo in folders)
                        {
                            relationIconInfo.Add(CommonCode.GetIconInfoByPath(directoryInfo.FullName));
                        }
                        var files = dir.EnumerateFiles().Where(f => MainWindow.appData.AppConfig.FilterExt.Contains(f.Extension.Replace(".", "")));
                        foreach (var fileInfo in files)
                        {
                            relationIconInfo.Add(CommonCode.GetIconInfoByPath(fileInfo.FullName));
                        }

                        return relationIconInfo;
                    }
                }
                return iconList;
            }
            set
            {
                iconList = value;
                OnPropertyChanged("IconList");
            }
        }

        [field: NonSerializedAttribute()]
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            CommonCode.SaveAppData(MainWindow.appData, Constants.DATA_FILE_PATH);
        }
    }
}
