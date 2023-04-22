using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace AccountBook.Views
{
    /// <summary>
    /// IncomeDetailView.xaml 的交互逻辑
    /// </summary>
    public partial class IncomeDetailView : UserControl
    {
        public IncomeDetailView()
        {
            InitializeComponent();
            AppData.Instance.IncomeDetailView = this;
        }

        private void InputYearTxt_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            //使用正则表达式判断
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void InputMonthTxt_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void InputMonthTxt_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            this.InputMonthTxt.Text = null;
        }

        private void InputYearTxt_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            this.InputYearTxt.Text = null;
        }
    }
}
