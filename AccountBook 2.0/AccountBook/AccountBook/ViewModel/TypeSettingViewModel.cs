using AccountBook.Models;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows;

namespace AccountBook.ViewModel
{
    public class TypeSettingViewModel
    {
        public TypeSettingViewModel()
        {
            
        }

        public ObservableCollection<AccountExpendTypeModel> GetExpendAllType
        {
            get
            {
                return AppData.ExpendTypeAllList;
            }
        }

        public ObservableCollection<AccountIncomeTypeModel> GetIncomeAllType
        {
            get
            {
                return AppData.IncomeTypeAllList;
            }
        }


        public RelayCommand DeleteExpendTypeCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MessageBox.Show("敬请期待...");
                });
            }
        }

        public RelayCommand DeleteIncomeTypeCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MessageBox.Show("敬请期待...");
                });
            }
        }

    }
}
