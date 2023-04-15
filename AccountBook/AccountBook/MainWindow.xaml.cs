using AccountBook.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Input;

namespace AccountBook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(!this.startUpStankPanel.Children.Contains(HomePage.GetInstance()))
            {
                this.startUpStankPanel.Children.Add(HomePage.GetInstance());
            }
        }

        private void StackPanel_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            Views.InputDialog inputDialog = new Views.InputDialog();
            inputDialog.ShowDialog();
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("确定是退出吗", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
            //关闭窗口
            if (result == MessageBoxResult.Yes)
                e.Cancel = false;
            //不关闭窗口
            if (result == MessageBoxResult.No)
                e.Cancel = true;
        }

        private void StackPanel_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            //判断ShowData在不在Stackpanel中
            if (!this.addPageStackPanel.Children.Contains(ShowData.GetInstance()))
            {
                this.addPageStackPanel.Children.Add(ShowData.GetInstance());
            }
        }
    }
}
