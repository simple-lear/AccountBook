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
    public class IncomeDetailViewModel
    {
        //获取所有的收入
        public ObservableCollection<AccountIncomeModel> GetAllIncomes
        {
            get
            {
                return AccountDal.GetInstance().GetIncomeAll();
            }
        }

        //全部收入
        public RelayCommand ShowIncomeAllCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    AppData.Instance.IncomeDetailView.incomeDetailDataGrid.ItemsSource = AppData.IncomeList;
                });
            }
        }

        //删除
        public RelayCommand DeleteCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    int selectRowCount = AppData.Instance.IncomeDetailView.incomeDetailDataGrid.SelectedIndex + 1;
                    try
                    {
                        GetAllIncomes.RemoveAt(selectRowCount - 1);
                        AccountDal.GetInstance().IncomeRemove(selectRowCount);
                        MessageBox.Show("删除成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                        AppData.incomeListCount = GetAllIncomes.Count;  //获取集合中元素的数量
                        UpdateId(selectRowCount);
                        AppData.Instance.IncomeDetailView.incomeDetailDataGrid.ItemsSource = AccountDal.GetInstance().GetIncomeAll();
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show($"删除失败,{exception.Message}");
                    }
                });
            }
        }

        private void UpdateId(int index)
        {
            for (int i = index - 1; i < GetAllIncomes.Count; ++i)
            {
                int oldId = GetAllIncomes[i].Id;
                GetAllIncomes[i].Id = GetAllIncomes[i].Id - 1;
                int newId = oldId - 1;
                AccountDal.GetInstance().UpdateAccountIncomeId(oldId, newId);
            }
        }

        //选择时间
        public RelayCommand SelectBeginDateChanged
        {
            get
            {
                return new RelayCommand(() =>
                {
                    AppData.Instance.IncomeDetailView.endDatePicker.IsEnabled = true;
                    var selectDate = AppData.Instance.IncomeDetailView.startDatePicker.SelectedDate;
                    var currentDate = DateTime.Now.Date;    //只获取日期不获取时间
                    if (selectDate > currentDate)
                    {
                        AppData.Instance.IncomeDetailView.startDatePicker.SelectedDate = currentDate;
                    }
                });
            }
        }

        //选择时间
        public RelayCommand SelectEndDateChanged
        {
            get
            {
                return new RelayCommand(() =>
                {
                    var selectDate = AppData.Instance.IncomeDetailView.endDatePicker.SelectedDate;
                    var currentDate = DateTime.Now.Date;    //只获取日期不获取时间
                    if (selectDate > currentDate)
                    {
                        AppData.Instance.IncomeDetailView.endDatePicker.SelectedDate = currentDate;
                    }

                    var startSelectDateTime = AppData.Instance.IncomeDetailView.startDatePicker.SelectedDate;
                    var endSelectDateTime = AppData.Instance.IncomeDetailView.endDatePicker.SelectedDate;
                    if (startSelectDateTime == null)
                    {
                        MessageBox.Show("开始时间为空"); return;
                    }
                    var startSelectDate = startSelectDateTime.ToString().Replace("0:00:00", "00:00:00");
                    var endSelectDate = endSelectDateTime.ToString().Replace("0:00:00", "23:59:59");
                    var beginDate = startSelectDate.Replace("/", "-");
                    var endDate = endSelectDate.Replace("/", "-");
                    var expendDateList = AccountDal.GetInstance().GetIncomeBeginEndDateTime(beginDate, endDate);
                    AppData.Instance.IncomeDetailView.incomeDetailDataGrid.ItemsSource = expendDateList;
                });
            }
        }



        /// <summary>
        /// 按月份查询数据
        /// </summary>
        public RelayCommand InputMonthTextCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    try
                    {
                        AppData.Instance.IncomeDetailView.InputYearTxt.Text = null;
                        AppData.Instance.IncomeDetailView.endDatePicker.Text = null;
                        if (AppData.Instance.IncomeDetailView.InputMonthTxt.Text == "")
                        {
                            AppData.Instance.IncomeDetailView.incomeDetailDataGrid.ItemsSource = AppData.IncomeList;
                            return;
                        }
                        int month = Convert.ToInt32(AppData.Instance.IncomeDetailView.InputMonthTxt.Text);
                        if (month > 12 || month < 1)
                        {
                            MessageBox.Show($"出错!\r\n输入的值不合法!", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        ObservableCollection<AccountIncomeModel> incomeList = AccountDal.GetInstance().GetIncomeMonth(month);
                        AppData.Instance.IncomeDetailView.incomeDetailDataGrid.ItemsSource = incomeList;
                    }
                    catch
                    {
                        MessageBox.Show($"出错!\r\n输入的值不合法!", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                });
            }
        }

        /// <summary>
        /// 按年份查询数据
        /// </summary>
        public RelayCommand InputYearTextCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    //清空月输入框
                    AppData.Instance.IncomeDetailView.InputMonthTxt.Text = null;
                    AppData.Instance.IncomeDetailView.endDatePicker.Text = null;

                    if (AppData.Instance.IncomeDetailView.InputYearTxt.Text == "")
                    {
                        AppData.Instance.IncomeDetailView.incomeDetailDataGrid.ItemsSource = AppData.IncomeList;
                        return;
                    }

                    int result;
                    bool isOk = int.TryParse(AppData.Instance.IncomeDetailView.InputYearTxt.Text,out result);
                    if(isOk)
                    {
                        if (result < 1 || result > 9999)
                        {
                            MessageBox.Show($"出错!\r\n输入的值不合法!", "错误", MessageBoxButton.OK, MessageBoxImage.Error); return;
                        }
                        ObservableCollection<AccountIncomeModel> incomeList = AccountDal.GetInstance().GetIncomeYear(result);
                        AppData.Instance.IncomeDetailView.incomeDetailDataGrid.ItemsSource = incomeList;
                    }
                    else
                    {
                        AppData.Instance.IncomeDetailView.InputYearTxt.Text = null;
                        MessageBox.Show($"出错!\r\n输入的值不合法!", "错误", MessageBoxButton.OK, MessageBoxImage.Error);return;
                    }    
                });
            }
        }

    }
}
