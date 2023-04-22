using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AccountBook.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            AppData.Instance.MainWindow = this;
            AppData.Instance.MainWindow.contentControl.Content = new HomeView();    
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //MessageBoxResult result = MessageBox.Show("确定要退出吗", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
            ////关闭窗口
            //if (result == MessageBoxResult.Yes)
            //{
            //    e.Cancel = false;
            //}
            ////不关闭窗口
            //if (result == MessageBoxResult.No)
            //{
            //    e.Cancel = true;
            //}
        }
    }
}
