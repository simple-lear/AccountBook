using AccountBook.DAL.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using AccountBook.Model;

namespace AccountBook.DAL
{
    public class AccountDal
    {
        //获取所有的收入数据
        public List<AccountIncomeModel> GetIncomeAll()
        {
            DataTable res = SqlHelper.ExecuteTable("SELECT * FROM AccountIncome;");
            List<AccountIncomeModel> accountIncomeList = ToIncomeModelList(res);
            return accountIncomeList;
        }

        //获取所有的支出数据
        public List<AccountExpendModel> GetExpendAll()
        {
            DataTable res = SqlHelper.ExecuteTable("SELECT * FROM AccountExpend;");
            List<AccountExpendModel> accountExpendList = ToExpendModelList(res);
            return accountExpendList;
        }

        /// <summary>
        /// 根据类型查询收入
        /// </summary>
        public List<AccountIncomeModel> GetTypeIncome(string type)
        {
            DataTable res = SqlHelper.ExecuteTable("SELECT * FROM AccountIncome WHERE AccountType = @AccountType",
                new SqlParameter("@AccountType", type));
            List<AccountIncomeModel> accountIncomeList = ToIncomeModelList(res);
            return accountIncomeList;
        }

        /// <summary>
        /// 根据类型查询支出
        /// </summary>
        public List<AccountExpendModel> GetTypeExpend(string type)
        {
            DataTable res = SqlHelper.ExecuteTable("SELECT * FROM AccountExpend WHERE AccountType = @AccountType",
                new SqlParameter("@AccountType", type));
            List<AccountExpendModel> accountExpendList = ToExpendModelList(res);
            return accountExpendList;
        }

        /// <summary>
        /// 获取所有的收入类型
        /// </summary>
        /// <returns></returns>
        public List<string> GetIncomeTypeAll()
        {
            DataTable res = SqlHelper.ExecuteTable("SELECT DISTINCT AccountType FROM AccountIncome;");
            List<string> accountIncomeTypes = new List<string>();
            foreach (DataRow row in res.Rows)
            {
                accountIncomeTypes.Add(row["AccountType"].ToString());  //将查询的类型添加到集合进行返回
            }
            return accountIncomeTypes;
        }

        /// <summary>
        /// 获取所有的支出类型
        /// </summary>
        /// <returns></returns>
        public List<string> GetExpendTypeAll()
        {
            DataTable res = SqlHelper.ExecuteTable("SELECT DISTINCT AccountType FROM AccountExpend;");
            List<string> accountIncomeTypes = new List<string>();
            foreach (DataRow row in res.Rows)
            {
                accountIncomeTypes.Add(row["AccountType"].ToString());  //将查询的类型添加到集合进行返回
            }
            return accountIncomeTypes;
        }

        private AccountIncomeModel ToIncomeModel(DataRow row)
        {
            AccountIncomeModel model = new AccountIncomeModel();
            model.Id = Convert.ToInt32(row["AccountIncomeId"]);
            model.收入描述 = row["AccountDescription"].ToString();
            model.收入类型 = row["AccountType"].ToString();
            model.收入金额 = Convert.ToDecimal(row["IncomeMoney"]);
            model.时间 = Convert.ToDateTime(row["InsertDate"]);
            return model;
        }

        public int InsertAccountIncomeModel(AccountIncomeModel income)
        {
            return SqlHelper.ExecuteNonQuery("insert into AccountIncome(AccountDescription,AccountType,IncomeMoney,InsertDate) values(@AccountDescription,@AccountType,@IncomeMoney,@InsertDateTime)",
                new SqlParameter("AccountDescription", income.收入描述),
                new SqlParameter("@AccountType", income.收入类型),
                new SqlParameter("@IncomeMoney", income.收入金额),
                new SqlParameter("@InsertDateTime", income.时间));
        }

        public int InsertAccountExpendModel(AccountExpendModel expend)
        {
            return SqlHelper.ExecuteNonQuery("insert into AccountExpend(AccountDescription,AccountType,ExpendMoney,InsertDate) values(@AccountDescription,@AccountType,@ExpendMoney,@InsertDateTime)",
                new SqlParameter("AccountDescription", expend.支出描述),
                new SqlParameter("@AccountType", expend.支出类型),
                new SqlParameter("@ExpendMoney", expend.支出金额),
                new SqlParameter("@InsertDateTime", expend.时间));
        }

        public int IncomeRemove(int id)
        {
            return SqlHelper.ExecuteNonQuery("DELETE FROM AccountIncome WHERE AccountIncomeId = @Id", new SqlParameter("@Id", id));
        }

        public int ExpendRemove(int id)
        {
            return SqlHelper.ExecuteNonQuery("DELETE FROM AccountExpend WHERE AccountExpendId = @Id", new SqlParameter("@Id", id));
        }

        private AccountExpendModel ToExpendModel(DataRow row)
        {
            AccountExpendModel model = new AccountExpendModel();
            model.Id = Convert.ToInt32(row["AccountExpendId"]);
            model.支出描述 = row["AccountDescription"].ToString();
            model.支出类型 = row["AccountType"].ToString();
            model.支出金额 = Convert.ToDecimal(row["ExpendMoney"]);
            model.时间 = Convert.ToDateTime(row["InsertDate"]);
            return model;
        }

        private List<AccountIncomeModel> ToIncomeModelList(DataTable table)
        {
            List<AccountIncomeModel> accountList = new List<AccountIncomeModel>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                AccountIncomeModel model = ToIncomeModel(row);
                accountList.Add(model);
            }
            return accountList;
        }

        private List<AccountExpendModel> ToExpendModelList(DataTable table)
        {
            List<AccountExpendModel> accountList = new List<AccountExpendModel>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                AccountExpendModel model = ToExpendModel(row);
                accountList.Add(model);
            }
            return accountList;
        }
    }
}
