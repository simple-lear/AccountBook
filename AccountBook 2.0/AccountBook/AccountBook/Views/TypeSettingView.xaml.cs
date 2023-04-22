using AccountBook.Views.TypeViews;
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
    /// TypeSettingView.xaml 的交互逻辑
    /// </summary>
    public partial class TypeSettingView : UserControl
    {
        public TypeSettingView()
        {
            InitializeComponent();
            this.typeSettingControl.Content = new ExpendTypeView();
        }

        private void expendBtn_Click(object sender, RoutedEventArgs e)
        {
            this.typeSettingControl.Content = new ExpendTypeView();
        }

        private void IncomeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.typeSettingControl.Content = new IncomeTypeView();
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    RadioButton radioButton = new RadioButton();
        //    radioButton.Width = 95;
        //    radioButton.FontSize = 18;
        //    Thickness thickness = new Thickness();
        //    thickness.Left = 10;
        //    thickness.Top = 10;
        //    thickness.Right = 0;
        //    thickness.Bottom = 0;
        //    radioButton.Margin = thickness;
        //    radioButton.Content = "你是";
        //    radioButton.Style = (Style)FindResource("ToggleButtonStyle");

        //    this.wrapPanel.Children.Add(radioButton);
        //}
    }
}
