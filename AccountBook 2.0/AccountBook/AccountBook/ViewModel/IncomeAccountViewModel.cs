using AccountBook.DAL;
using AccountBook.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AccountBook.ViewModel
{
    public class IncomeAccountViewModel
    {
        public RelayCommand SubmitCommand 
        {
            get
            {
                return new RelayCommand(InsertIncomeAccount);
            }
        }

        public ObservableCollection<string> IcomeTypes 
        {
            get
            {
                var query = from incomeTypeName in AppData.IncomeTypeAllList
                            select incomeTypeName.IncomeTypeName;
                ObservableCollection<string> incomeTypes = new ObservableCollection<string>();
                foreach (var incomeType in query)
                {
                    incomeTypes.Add(incomeType);
                }
                return incomeTypes;
            }
        }


        public IncomeAccountViewModel() 
        {
        }

        //插入
        public void InsertIncomeAccount()
        {
            //这里输入空依然会报错，需要用正则表达式验证
            if (string.IsNullOrEmpty(AppData.Instance.IncomeAccountView.moneyTxt.Text))
            {
                MessageBox.Show("请输入金额!", "失败", MessageBoxButton.OK); return;
            }
            decimal money = Convert.ToDecimal(AppData.Instance.IncomeAccountView.moneyTxt.Text);
            string accountType = AppData.Instance.IncomeAccountView.comboBoxType.Text;
            string selectDate = AppData.Instance.IncomeAccountView.LocaleDatePicker.Text;
            string selectTime = AppData.Instance.IncomeAccountView.PresetTimePicker.Text;
            if (string.IsNullOrEmpty(selectDate) || string.IsNullOrEmpty(selectTime))
            {
                MessageBox.Show("请选择日期或时间");return;
            }
            DateTime dateTime = DataTimeFormat(selectDate, selectTime);
            string incomeDateTime = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            string explainTxt = AppData.Instance.IncomeAccountView.explainTxt.Text;
            
            AccountIncomeModel accountIncomeModel = new AccountIncomeModel();
            accountIncomeModel.Id = AppData.IncomeList.Count + 1;
            accountIncomeModel.IncomeMoney = money;
            accountIncomeModel.IncomeType = accountType;
            accountIncomeModel.IncomeDateTime = incomeDateTime;
            accountIncomeModel.IncomeDescription = explainTxt;

            int count = AccountDal.GetInstance().InsertAccountIncomeModel(accountIncomeModel);
            if(count > 0)
            {
                MessageBox.Show("插入成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("插入失败","错误",MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 把日期时间字符串转成正确的格式
        /// </summary>
        /// <param name="date"></param>
        /// <param name="time"></param>
        public DateTime DataTimeFormat(string date,string time)
        {
            var dateTimeText = date + " " + time;
            //替换字符串为正确的时间格式
            string replaceStr = null;
            if (dateTimeText.IndexOf("/") != -1)
            {
                replaceStr = dateTimeText.Replace("/", "-");
            }
            return Convert.ToDateTime(replaceStr);
        }

    }
}
