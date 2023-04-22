/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:AccountBook"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace AccountBook.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<HomeViewModel>();
            SimpleIoc.Default.Register<IncomeAccountViewModel>();
            SimpleIoc.Default.Register<ExpendAccountViewModel>();
            SimpleIoc.Default.Register<TypeSettingViewModel>();
            SimpleIoc.Default.Register<ExpendDetailViewModel>();
            SimpleIoc.Default.Register<IncomeDetailViewModel>();
            SimpleIoc.Default.Register<AccountDetailViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        //������ҳ
        public HomeViewModel Home
        {
            get
            {
                return ServiceLocator.Current.GetInstance<HomeViewModel>();
            }
        }

        //�����������ҳ �˴���ע���޷���AppData
        public IncomeAccountViewModel IncomeAccount
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IncomeAccountViewModel>();
            }
        }

        //����֧������ҳ
        public ExpendAccountViewModel ExpendAccount
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ExpendAccountViewModel>();
            }
        }

        //�����������
        public TypeSettingViewModel TypeSetting
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TypeSettingViewModel>();
            }
        }

        //����֧����ϸ
        public ExpendDetailViewModel ExpendDetail
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ExpendDetailViewModel>();
            }
        }

        //����������ϸ
        public IncomeDetailViewModel IncomeDetail
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IncomeDetailViewModel>();
            }
        }


        public AccountDetailViewModel AccountDetail
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AccountDetailViewModel>();
            }
        }


        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}