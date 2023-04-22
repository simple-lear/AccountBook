using AccountBook.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Windows.Controls;

namespace AccountBook.ViewModel
{
    
    public class MainViewModel : ViewModelBase
    {
        public AppData AppData { get; private set; } = AppData.Instance;    //用于MainWindow.xaml的部分绑定

        public MainViewModel()
        {
           
        }

        //实现界面跳转，和前台MainWindow.xaml绑定
        public RelayCommand<RadioButton> SelectViewCommand
        {
            get
            {
                return new RelayCommand<RadioButton>((arg) =>
                {
                    if(!(arg is RadioButton radioButton))
                    {
                        return;
                    }
                    if (string.IsNullOrEmpty(radioButton.Content.ToString()))
                    {
                        return;
                    }
                    
                    switch(radioButton.Content.ToString())
                    {
                        case "首页":
                            AppData.Instance.MainWindow.contentControl.Content = new HomeView();
                            break;
                        case "收入记账":
                            AppData.Instance.MainWindow.contentControl.Content = new IncomeAccountView();
                            break;
                        case "支出记账":
                            AppData.Instance.MainWindow.contentControl.Content = new ExpendAccountView();
                            break;
                        case "类别设置":
                            AppData.Instance.MainWindow.contentControl.Content = new TypeSettingView();
                            break;
                        case "支出明细":
                            AppData.Instance.MainWindow.contentControl.Content = new ExpendDetailView();
                            break;
                        case "收入明细":
                            AppData.Instance.MainWindow.contentControl.Content = new IncomeDetailView();
                            break;
                        case "记账明细":
                            AppData.Instance.MainWindow.contentControl.Content = new AccountDetailView();
                            break;
                    }
                });
            }
        }
    }
}