using AccountBook.DAL;
using AccountBook.Models;
using AccountBook.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace AccountBook.Views
{
    /// <summary>
    /// ExpendDetail.xaml 的交互逻辑
    /// </summary>
    public partial class ExpendDetailView : UserControl
    {
        public ExpendDetailView()
        {
            InitializeComponent();
            AppData.Instance.ExpendDetailView = this;
        }

        private void InputMonthTxt_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void InputYearTxt_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
        
        /// <summary>
        /// 失去焦点时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputMonthTxt_LostFocus(object sender, RoutedEventArgs e)
        {
            this.InputMonthTxt.Text = null;
        }

        private void InputYearTxt_LostFocus(object sender, RoutedEventArgs e)
        {
            this.InputYearTxt.Text = null;
        }
    }
}
