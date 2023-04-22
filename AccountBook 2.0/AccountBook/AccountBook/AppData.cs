using AccountBook.DAL;
using AccountBook.Models;
using AccountBook.Views;
using AccountBook.Views.TypeViews;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBook
{
    public class AppData : ObservableObject
    {
        public static AppData Instance = new Lazy<AppData>(() => new AppData()).Value;

		private string systemName = "记账簿";

		public string SystemName
		{
			get { return systemName; }
			set { systemName = value; RaisePropertyChanged(); }
		}

        /// <summary>
        /// 主窗体
        /// </summary>
        public MainWindow MainWindow { get; set; } = null;

        /// <summary>
        /// 收入记账
        /// </summary>
        public IncomeAccountView IncomeAccountView { get; set; }

        /// <summary>
        /// 支出记账
        /// </summary>        
        public ExpendAccountView ExpendAccountView { get; set; }

        /// <summary>
        /// 支出类型
        /// </summary>
        public ExpendTypeView ExpendTypeView { get; set; }

        /// <summary>
        /// 收入类型
        /// </summary>
        public IncomeTypeView IncomeTypeView { get; set; }

        /// <summary>
        /// 获取所有的支出
        /// </summary>
        public static ObservableCollection<AccountExpendModel> ExpendList = AccountDal.GetInstance().GetExpendAll();

        /// <summary>
        /// 获取所有的收入
        /// </summary>
        public static ObservableCollection<AccountIncomeModel> IncomeList = AccountDal.GetInstance().GetIncomeAll();

        public static ObservableCollection<AccountExpendTypeModel> ExpendTypeAllList = AccountDal.GetInstance().GetExpendAllType();

        public static ObservableCollection<AccountIncomeTypeModel> IncomeTypeAllList = AccountDal.GetInstance().GetIncomeAllType(); 

        public ExpendDetailView ExpendDetailView { get; set; } = null;

        public IncomeDetailView IncomeDetailView { get; set; } = null;

        public static int expendListCount = 1;  //从1开始插入

        public static int incomeListCount = 1;

        //获取支出和收入的类型

        private AppData() { }	


	}
}
