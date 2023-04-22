using AccountBook.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Windows.Controls;

namespace AccountBook.ViewModel
{
    
    public class MainViewModel : ViewModelBase
    {
        public AppData AppData { get; private set; } = AppData.Instance;    //����MainWindow.xaml�Ĳ��ְ�

        public MainViewModel()
        {
           
        }

        //ʵ�ֽ�����ת����ǰ̨MainWindow.xaml��
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
                        case "��ҳ":
                            AppData.Instance.MainWindow.contentControl.Content = new HomeView();
                            break;
                        case "�������":
                            AppData.Instance.MainWindow.contentControl.Content = new IncomeAccountView();
                            break;
                        case "֧������":
                            AppData.Instance.MainWindow.contentControl.Content = new ExpendAccountView();
                            break;
                        case "�������":
                            AppData.Instance.MainWindow.contentControl.Content = new TypeSettingView();
                            break;
                        case "֧����ϸ":
                            AppData.Instance.MainWindow.contentControl.Content = new ExpendDetailView();
                            break;
                        case "������ϸ":
                            AppData.Instance.MainWindow.contentControl.Content = new IncomeDetailView();
                            break;
                        case "������ϸ":
                            AppData.Instance.MainWindow.contentControl.Content = new AccountDetailView();
                            break;
                    }
                });
            }
        }
    }
}