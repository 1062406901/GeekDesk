﻿using GeekDesk.Control.UserControls.Backlog;
using GeekDesk.ViewModel;
using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GeekDesk.Control.Windows
{
    /// <summary>
    /// BacklogWindow.xaml 的交互逻辑
    /// </summary>
    public partial class BacklogWindow
    {
        private static BacklogControl backlog = new BacklogControl();
        private AppData appData = MainWindow.appData;
        private BacklogWindow()
        {
            InitializeComponent();
            RightCard.Content = backlog;
            backlog.BacklogList.ItemsSource = appData.ExeBacklogList;
            this.Topmost = true;
        }


        /// <summary>
        /// 移动窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DragMove(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        /// <summary>
        /// 点击关闭按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MemuClick(object sender, RoutedEventArgs e)
        {
            SideMenuItem smi = sender as SideMenuItem;
            switch (smi.Tag.ToString())
            {
                case "History":
                    backlog.BacklogList.ItemsSource = appData.HiBacklogList;
                    break;
                default:
                    backlog.BacklogList.ItemsSource = appData.ExeBacklogList;
                    break;
            }
        }

        /// <summary>
        /// 新建待办
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateBacklog_BtnClick(object sender, RoutedEventArgs e)
        {
            BacklogInfoWindow.ShowNone();
        }


        private static System.Windows.Window window = null;
        public static void Show()
        {
            if (window == null || !window.Activate())
            {
                window = new BacklogWindow();
            }
            window.Show();
        }

        
    }
}