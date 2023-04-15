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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AccountBook.Views
{
    /// <summary>
    /// ShowData.xaml 的交互逻辑
    /// </summary>
    public partial class ShowData : UserControl
    {
        //获取收入数据
        private List<AccountIncomeModel> incomeList = AccountBll.GetInstance().GetIncomeAll();
        ////获取支出数据
        private List<AccountExpendModel> expendList = AccountBll.GetInstance().GetExpendAll();

        private List<AccountIncomeModel> incomeTypeList = new List<AccountIncomeModel>();

        private List<AccountExpendModel> expendTypeList = new List<AccountExpendModel>();

        public List<AccountIncomeModel> IncomeList { get { return incomeList; } }

        public List<AccountExpendModel> ExpendList { get { return expendList; } }

        private static ShowData _instance;

        public static ShowData GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ShowData();
            }
            return _instance;
        }

        private ShowData()
        {
            InitializeComponent();
            this.showIncomeDataGrid.DataContext = incomeList;
            this.showExpendDataGrid.DataContext = expendList;

            //this.showIncomeDataGrid.ItemsSource = incomeList;
            //报索引错误的原因有可能是数据还没有初始化完成
            //this.showIncomeDataGrid.Columns[0].Header = "第几个";

            var incomeTypeList = AccountBll.GetInstance().GetIncomeTypeAll();
            foreach (var type in incomeTypeList)
            {
                this.typeIncomeComboBox.Items.Add(type);
            }

            var expendTypeList = AccountBll.GetInstance().GetExpendTypeAll();
            foreach (var type in expendTypeList)
            {
                this.typeExpendComboBox.Items.Add(type);
            }

        }

        private void typeIncomeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //选择改变时，根据选择的类型查询数据并显示
            var type = this.typeIncomeComboBox.SelectedItem.ToString();
            this.incomeTypeList.Clear();
            this.incomeTypeList = AccountBll.GetInstance().GetTypeIncome(type);
            this.showIncomeDataGrid.ItemsSource = this.incomeTypeList;
        }

        private void typeExpendComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var type = this.typeExpendComboBox.SelectedItem.ToString();
            this.expendTypeList.Clear();
            this.expendTypeList = AccountBll.GetInstance().GetTypeExpend(type);
            this.showExpendDataGrid.ItemsSource = this.expendTypeList;
        }

        private void calcuExpend_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"总支出:{AccountBll.GetInstance().GetExpendMoneySum()}");
        }

        private void calcuIncome_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"总收入:{AccountBll.GetInstance().GetIncomeMoneySum()}");
        }

        private void showIncomeAllBtn_Click(object sender, RoutedEventArgs e)
        {
            this.showIncomeDataGrid.ItemsSource = AccountBll.GetInstance().GetIncomeAll();
        }

        private void showExpendAllBtn_Click(object sender, RoutedEventArgs e)
        {
            this.showExpendDataGrid.ItemsSource = AccountBll.GetInstance().GetExpendAll();
        }

        private void removeIncomeIdBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(this.txtIncomeId.Text);
                var message = AccountBll.GetInstance().IncomeRemove(id);
                MessageBox.Show(message);
            }
            catch (Exception exception)
            {
                MessageBox.Show("请输入合法的Id");
                return;
            }
        }

        private void removeExpendIdBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(this.txtExpendId.Text);
                var message = AccountBll.GetInstance().ExpendRemove(id);
                MessageBox.Show(message);
            }
            catch (Exception)
            {
                MessageBox.Show("请输入合法的Id");
                return;
            }
        }
    }
}
