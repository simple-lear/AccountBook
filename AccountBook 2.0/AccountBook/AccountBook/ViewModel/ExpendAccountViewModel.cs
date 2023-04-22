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
    public class ExpendAccountViewModel
    {
        public RelayCommand SubmitCommand
        {
            get
            {
                return new RelayCommand(InsertExpendAccount);
            }
        }

        public ObservableCollection<string> ExpendTypes
        {
            get
            {
                var query = from expendTypeName in AppData.ExpendTypeAllList
                            select expendTypeName.ExpendTypeName;
                ObservableCollection<string> expendTypes = new ObservableCollection<string>();
                foreach(var expendType in query)
                {
                    expendTypes.Add(expendType);
                }
                return expendTypes;
            }
        }

        //插入
        public void InsertExpendAccount()
        {
            //这里输入空依然会报错，需要用正则表达式验证
            if (string.IsNullOrEmpty(AppData.Instance.ExpendAccountView.moneyTxt.Text))
            {
                MessageBox.Show("请输入金额!", "失败", MessageBoxButton.OK); return;
            }
            decimal money = Convert.ToDecimal(AppData.Instance.ExpendAccountView.moneyTxt.Text);
            string accountType = AppData.Instance.ExpendAccountView.comboBoxType.Text;
            string selectDate = AppData.Instance.ExpendAccountView.LocaleDatePicker.Text;
            string selectTime = AppData.Instance.ExpendAccountView.PresetTimePicker.Text;
            if (string.IsNullOrEmpty(selectDate) || string.IsNullOrEmpty(selectTime))
            {
                MessageBox.Show("请选择日期或时间"); return;
            }
            DateTime dateTime = DataTimeFormat(selectDate, selectTime);
            string expendDateTime = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            string explainTxt = AppData.Instance.ExpendAccountView.explainTxt.Text;

            AccountExpendModel accountExpendModel = new AccountExpendModel();
            accountExpendModel.Id = AppData.ExpendList.Count + 1;
            accountExpendModel.ExpendMoney = money;
            accountExpendModel.ExpendType = accountType;
            accountExpendModel.ExpendDateTime = expendDateTime;
            accountExpendModel.ExpendDescription = explainTxt;

            int count = AccountDal.GetInstance().InsertAccountExpendModel(accountExpendModel);
            if(count > 0)
            {
                MessageBox.Show("插入成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("插入失败", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            AppData.ExpendList.Add(accountExpendModel); //添加一条数据
        }

        /// <summary>
        /// 把日期时间字符串转成正确的格式
        /// </summary>
        /// <param name="date"></param>
        /// <param name="time"></param>
        public DateTime DataTimeFormat(string date, string time)
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
