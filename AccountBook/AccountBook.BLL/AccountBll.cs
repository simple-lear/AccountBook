using AccountBook.DAL;
using AccountBook.Model;
using System;
using System.Collections.Generic;

namespace AccountBook.BLL
{
    public class AccountBll
    {
        AccountDal accountDal = new AccountDal();

        private static AccountBll instance;

        private AccountBll()
        {

        }

        public static AccountBll GetInstance()
        {
            if (instance == null)
            {
                instance = new AccountBll();
            }
            return instance;
        }

        /// <summary>
        /// 根据类型查询收入
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<AccountIncomeModel> GetTypeIncome(string type)
        {
            return accountDal.GetTypeIncome(type);
        }

        public List<AccountExpendModel> GetTypeExpend(string type)
        {
            return accountDal.GetTypeExpend(type);
        }

        //计算收入总金额
        public decimal GetIncomeMoneySum()
        {
            decimal sum = 0.0M;
            foreach (var icome in accountDal.GetIncomeAll())
            {
                sum = sum + icome.收入金额;
            }
            return sum;
        }

        //计算支出总金额
        public decimal GetExpendMoneySum()
        {
            decimal sum = 0.0M;
            foreach (var expend in accountDal.GetExpendAll())
            {
                sum = sum + expend.支出金额;
            }
            return sum;
        }

        /// <summary>
        /// 按Id删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string IncomeRemove(int id)
        {
            if (accountDal.IncomeRemove(id) > 0)
            {
                return "删除成功";
            }
            return "删除失败";
        }

        /// <summary>
        /// 按Id删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string ExpendRemove(int id)
        {
            if (accountDal.ExpendRemove(id) > 0)
            {
                return "删除成功";
            }
            return "删除失败";
        }

        //获取收入的所有类型
        public List<string> GetIncomeTypeAll()
        {
            return accountDal.GetIncomeTypeAll();
        }

        /// <summary>
        /// 获取支出的所有类型
        /// </summary>
        /// <returns></returns>
        public List<string> GetExpendTypeAll()
        {
            return accountDal.GetExpendTypeAll();
        }

        public List<AccountIncomeModel> GetIncomeAll()
        {
            return accountDal.GetIncomeAll();
        }

        public List<AccountExpendModel> GetExpendAll()
        {
            return accountDal.GetExpendAll();
        }

        //添加一条数据
        public string InsertAccountIncomeModel(AccountIncomeModel income)
        {
            if (accountDal.InsertAccountIncomeModel(income) != 0)
            {
                return "插入成功";
            }
            return "插入失败";
        }

        public string InsertAccountExpendModel(AccountExpendModel expend)
        {
            if (accountDal.InsertAccountExpendModel(expend) != 0)
            {
                return "插入成功";
            }
            return "插入失败";
        }
    }
}
