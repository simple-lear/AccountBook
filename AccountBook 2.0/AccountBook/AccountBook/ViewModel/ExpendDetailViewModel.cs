using AccountBook.DAL;
using AccountBook.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace AccountBook.ViewModel
{
    public class ExpendDetailViewModel
    {
        private ObservableCollection<AccountExpendModel> _getAllExpends = AppData.ExpendList;
        
        //获取所有的支出数据
        public ObservableCollection<AccountExpendModel> GetAllExpends
        {
            get
            {
                return _getAllExpends;
            }
        }

        //删除
        public RelayCommand DeleteCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    int selectRowCount = AppData.Instance.ExpendDetailView.expendDetailDataGrid.SelectedIndex + 1;
                    try
                    {
                        GetAllExpends.RemoveAt(selectRowCount - 1);
                        AccountDal.GetInstance().ExpendRemove(selectRowCount);
                        MessageBox.Show("删除成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                        AppData.expendListCount = GetAllExpends.Count;  //获取集合中元素的数量
                        UpdateId(selectRowCount);
                        AppData.Instance.ExpendDetailView.expendDetailDataGrid.ItemsSource = AccountDal.GetInstance().GetExpendAll();
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
            for(int i = index - 1; i < GetAllExpends.Count;++i)
            {
                int oldId = GetAllExpends[i].Id;
                GetAllExpends[i].Id = GetAllExpends[i].Id - 1;
                int newId = oldId - 1;
                AccountDal.GetInstance().UpdateAccountExpendId(oldId, newId);
            }
        }

        //全部支出
        public RelayCommand ShowExpendAllCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    AppData.Instance.ExpendDetailView.expendDetailDataGrid.ItemsSource = AccountDal.GetInstance().GetExpendAll();
                });
            }
        }

        //选择时间
        public RelayCommand SelectBeginDateChanged
        {
            get
            {
                return new RelayCommand(() =>
                {
                    AppData.Instance.ExpendDetailView.endDatePicker.IsEnabled = true;
                    var selectDate = AppData.Instance.ExpendDetailView.startDatePicker.SelectedDate;
                    var currentDate = DateTime.Now.Date;    //只获取日期不获取时间
                    if(selectDate > currentDate)
                    {
                        AppData.Instance.ExpendDetailView.startDatePicker.SelectedDate = currentDate;
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
                    var selectDate = AppData.Instance.ExpendDetailView.endDatePicker.SelectedDate;
                    var currentDate = DateTime.Now.Date;    //只获取日期不获取时间
                    if (selectDate > currentDate)
                    {
                        AppData.Instance.ExpendDetailView.endDatePicker.SelectedDate = currentDate;
                    }
                    
                    var startSelectDateTime = AppData.Instance.ExpendDetailView.startDatePicker.SelectedDate;
                    var endSelectDateTime = AppData.Instance.ExpendDetailView.endDatePicker.SelectedDate;
                    if(startSelectDateTime == null)
                    {
                        MessageBox.Show("开始时间为空");return;
                    }
                    var startSelectDate = startSelectDateTime.ToString().Replace("0:00:00","00:00:00");
                    var endSelectDate = endSelectDateTime.ToString().Replace("0:00:00","23:59:59");
                    var beginDate = startSelectDate.Replace("/","-");
                    var endDate = endSelectDate.Replace("/","-");
                    var expendDateList = AccountDal.GetInstance().GetExpendBeginEndDateTime(beginDate, endDate);
                    AppData.Instance.ExpendDetailView.expendDetailDataGrid.ItemsSource = expendDateList;
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
                        AppData.Instance.ExpendDetailView.InputYearTxt.Text = null;
                        AppData.Instance.ExpendDetailView.endDatePicker.Text = null;
                        if (AppData.Instance.ExpendDetailView.InputMonthTxt.Text == "")
                        {
                            AppData.Instance.ExpendDetailView.expendDetailDataGrid.ItemsSource = AppData.ExpendList;
                            return;
                        }
                        int month = Convert.ToInt32(AppData.Instance.ExpendDetailView.InputMonthTxt.Text);
                        if(month > 12 || month < 1)
                        {
                            MessageBox.Show($"出错!\r\n输入的值不合法!", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        ObservableCollection<AccountExpendModel> expendList = AccountDal.GetInstance().GetExpendMonth(month);
                        AppData.Instance.ExpendDetailView.expendDetailDataGrid.ItemsSource = expendList;
                    }
                    catch
                    {
                        MessageBox.Show($"出错!\r\n输入的值不合法!","错误",MessageBoxButton.OK,MessageBoxImage.Error);
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
                    try
                    {
                        if (AppData.Instance.ExpendDetailView.InputYearTxt.Text == "")
                        {
                            AppData.Instance.ExpendDetailView.expendDetailDataGrid.ItemsSource = AppData.ExpendList;
                            return;
                        }
                        int year = Convert.ToInt32(AppData.Instance.ExpendDetailView.InputYearTxt.Text);
                        if(year < 1 || year > 9999)
                        {
                            MessageBox.Show($"出错!\r\n输入的值不合法!", "错误", MessageBoxButton.OK, MessageBoxImage.Error);return;
                        }
                        ObservableCollection<AccountExpendModel> expendList = AccountDal.GetInstance().GetExpendYear(year);
                        AppData.Instance.ExpendDetailView.expendDetailDataGrid.ItemsSource = expendList;
                    }
                    catch
                    {
                        MessageBox.Show($"出错!\r\n输入的值不合法!", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                });
            }
        }
    }
}
