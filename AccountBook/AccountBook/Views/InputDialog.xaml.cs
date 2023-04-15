using AccountBook.BLL;
using AccountBook.Model;
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
using System.Windows.Shapes;

namespace AccountBook.Views
{
    /// <summary>
    /// InputDialog.xaml 的交互逻辑
    /// </summary>
    public partial class InputDialog : Window
    {
        List<string> incomeList = AccountTypeModel.GetInstance().GetIncomeTypeAll();
        List<string> expendList = AccountTypeModel.GetInstance().GetExpendTypeAll();
        System.DateTime selectDateTime;

        public InputDialog()
        {
            InitializeComponent();
            this.inExComBox.SelectedIndex = 0;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("确定是退出吗", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
            //关闭窗口
            if (result == MessageBoxResult.Yes)
                e.Cancel = false;
            //不关闭窗口
            if (result == MessageBoxResult.No)
                e.Cancel = true;
        }

        private void LocaleDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //检测选择的时间是否合理
            var dateTime = this.LocaleDatePicker.SelectedDate;
            if (dateTime > System.DateTime.Now)
            {
                this.LocaleDatePicker.SelectedDate = System.DateTime.Now;
            }
        }

        private void inComeComboBox_Selected(object sender, RoutedEventArgs e)
        {
            this.typeCombobox.Items.Clear();
            foreach (var incomeItem in incomeList)
            {
                this.typeCombobox.Items.Add(incomeItem);
            }
            this.typeCombobox.SelectedItem = "其它";
        }

        private void expendComboBox_Selected(object sender, RoutedEventArgs e)
        {
            this.typeCombobox.Items.Clear();
            foreach (var expendItem in expendList)
            {
                this.typeCombobox.Items.Add(expendItem);
            }
            this.typeCombobox.SelectedItem = "其它";
        }

        private void WithSecondsTimePicker_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            var dateTimeText = this.LocaleDatePicker.Text + " " + this.WithSecondsTimePicker.Text;
            string subDateTimeText = dateTimeText.Remove(dateTimeText.Length - 3, 3);
            //替换字符串为正确的时间格式
            string replaceStr = null;
            if (subDateTimeText.IndexOf("/") != -1)
            {
                replaceStr = subDateTimeText.Replace("/", "-");
            }
            this.selectDateTime = Convert.ToDateTime(replaceStr);
            //MessageBox.Show(replaceStr);
            //MessageBox.Show($"时间:{Convert.ToDateTime(replaceStr)}");
        }

        private void insertBtn_Click(object sender, RoutedEventArgs e)
        {
            //var str = new StringBuilder($"收入/支出:{this.inExComBox.Text}\n" + $"描述:{this.descriptionTxt.Text}\n" + $"插入时间:{this.selectDateTime}\n" + $"类型:{this.typeCombobox.Text}" + $"金额:{this.moneyTextBox.Text}");
            //MessageBox.Show(str.ToString());
            if (this.inExComBox.Text == "收入")
            {
                AccountIncomeModel accountIncome = new AccountIncomeModel();
                accountIncome.收入描述 = this.descriptionTxt.Text;
                accountIncome.收入类型 = this.typeCombobox.Text;
                accountIncome.收入金额 = Convert.ToDecimal(this.moneyTextBox.Text);
                accountIncome.时间 = this.selectDateTime;
                var message = AccountBll.GetInstance().InsertAccountIncomeModel(accountIncome);
                MessageBox.Show(message);
            }
            else if (this.inExComBox.Text == "支出")
            {
                AccountExpendModel accountExpend = new AccountExpendModel();
                accountExpend.支出描述 = this.descriptionTxt.Text;
                accountExpend.支出类型 = this.typeCombobox.Text;
                accountExpend.支出金额 = Convert.ToDecimal(this.moneyTextBox.Text);
                accountExpend.时间 = this.selectDateTime;
                var message = AccountBll.GetInstance().InsertAccountExpendModel(accountExpend);
                MessageBox.Show(message);
            }
        }
    }
}
