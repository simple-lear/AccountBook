using AccountBook.DAL.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using AccountBook.Models;
using System.Collections.ObjectModel;

namespace AccountBook.DAL
{
    public class AccountDal
    {
        private AccountDal()
        {

        }

        private static AccountDal _accountDal;

        public static AccountDal GetInstance()
        {
            if(_accountDal == null)
            {
                _accountDal = new AccountDal();
            }
            return _accountDal;
        }
        //获取所有的收入数据
        public ObservableCollection<AccountIncomeModel> GetIncomeAll()
        {
            DataTable res = SqlHelper.ExecuteTable("SELECT * FROM AccountIncome;");
            ObservableCollection<AccountIncomeModel> accountIncomeList = ToIncomeModelList(res);
            return accountIncomeList;
        }

        //获取所有的支出数据
        public ObservableCollection<AccountExpendModel> GetExpendAll()
        {
            DataTable res = SqlHelper.ExecuteTable("SELECT * FROM AccountExpend;");
            ObservableCollection<AccountExpendModel> accountExpendList = ToExpendModelList(res);
            return accountExpendList;
        }

        /// <summary>
        /// 根据类型查询收入
        /// </summary>
        public ObservableCollection<AccountIncomeModel> GetTypeIncome(string type)
        {
            DataTable res = SqlHelper.ExecuteTable("SELECT * FROM AccountIncome WHERE AccountType = @AccountType",
                new SqlParameter("@AccountType", type));
            ObservableCollection<AccountIncomeModel> accountIncomeList = ToIncomeModelList(res);
            return accountIncomeList;
        }

        /// <summary>
        /// 根据类型查询支出
        /// </summary>
        public ObservableCollection<AccountExpendModel> GetTypeExpend(string type)
        {
            DataTable res = SqlHelper.ExecuteTable("SELECT * FROM AccountExpend WHERE AccountType = @AccountType",
                new SqlParameter("@AccountType", type));
            ObservableCollection<AccountExpendModel> accountExpendList = ToExpendModelList(res);
            return accountExpendList;
        }


        public ObservableCollection<AccountExpendTypeModel> GetExpendAllType()
        {
            DataTable res = SqlHelper.ExecuteTable("SELECT Id,AccountTypeName FROM AccountExpendType;");
            ObservableCollection<AccountExpendTypeModel> accountIncomeTypes = new ObservableCollection<AccountExpendTypeModel>();
            foreach (DataRow row in res.Rows)
            {
                AccountExpendTypeModel accountExpendTypeModel = new AccountExpendTypeModel();
                accountExpendTypeModel.Id = Convert.ToInt32(row["Id"]);
                accountExpendTypeModel.ExpendTypeName = row["AccountTypeName"].ToString();
                accountIncomeTypes.Add(accountExpendTypeModel);
            }
            return accountIncomeTypes;
        }

        public ObservableCollection<AccountIncomeTypeModel> GetIncomeAllType()
        {
            DataTable res = SqlHelper.ExecuteTable("SELECT Id,AccountTypeName FROM AccountIncomeType;");
            ObservableCollection<AccountIncomeTypeModel> accountIncomeTypes = new ObservableCollection<AccountIncomeTypeModel>();
            foreach (DataRow row in res.Rows)
            {
                AccountIncomeTypeModel accountExpendTypeModel = new AccountIncomeTypeModel();
                accountExpendTypeModel.Id = Convert.ToInt32(row["Id"]);
                accountExpendTypeModel.IncomeTypeName = row["AccountTypeName"].ToString();
                accountIncomeTypes.Add(accountExpendTypeModel);
            }
            return accountIncomeTypes;
        }

        /// <summary>
        /// 添加支出类型
        /// </summary>
        /// <param name="id"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public int InsertExpendType(int id,string typeName)
        {
            return SqlHelper.ExecuteNonQuery("INSERT INTO AccountExpendType(Id,AccountTypeName) VALUES(@Id,@TypeName);",
                new SqlParameter("@Id", id),
                new SqlParameter("@TypeName", typeName));
        }

        /// <summary>
        /// 添加收入类型
        /// </summary>
        /// <param name="id"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public int InsertIncomeType(int id, string typeName)
        {
            return SqlHelper.ExecuteNonQuery("INSERT INTO AccountIncomeType(Id,AccountTypeName) VALUES(@Id,@TypeName);",
                new SqlParameter("@Id", id),
                new SqlParameter("@TypeName", typeName));
        }

        public int InsertAccountIncomeModel(AccountIncomeModel income)
        {
            return SqlHelper.ExecuteNonQuery("insert into AccountIncome(AccountIncomeId,AccountDescription,AccountType,IncomeMoney,InsertDate) values(@AccountIncomeId,@AccountDescription,@AccountType,@IncomeMoney,@InsertDateTime)",
                new SqlParameter("@AccountIncomeId",income.Id),
                new SqlParameter("AccountDescription", income.IncomeDescription),
                new SqlParameter("@AccountType", income.IncomeType),
                new SqlParameter("@IncomeMoney", income.IncomeMoney),
                new SqlParameter("@InsertDateTime", income.IncomeDateTime));
        }

        public int InsertAccountExpendModel(AccountExpendModel expend)
        {
            return SqlHelper.ExecuteNonQuery("insert into AccountExpend(AccountExpendId,AccountDescription,AccountType,ExpendMoney,InsertDate) values(@AccountExpendId,@AccountDescription,@AccountType,@ExpendMoney,@InsertDateTime)",
                new SqlParameter("@AccountExpendId",expend.Id),
                new SqlParameter("AccountDescription", expend.ExpendDescription),
                new SqlParameter("@AccountType", expend.ExpendType),
                new SqlParameter("@ExpendMoney", expend.ExpendMoney),
                new SqlParameter("@InsertDateTime", expend.ExpendDateTime));
        }

        public int UpdateAccountExpendId(int oldId,int newId)
        {
            return SqlHelper.ExecuteNonQuery("UPDATE AccountExpend SET AccountExpendId = @NewId WHERE AccountExpendId = @OldId",
                new SqlParameter("@NewId",newId),
                new SqlParameter("@OldId",oldId));
        }

        public int UpdateAccountIncomeId(int oldId,int newId)
        {
            return SqlHelper.ExecuteNonQuery("UPDATE AccountIncome SET AccountIncomeId = @NewId WHERE AccountIncomeId = @OldId",
                new SqlParameter("@NewId", newId),
                new SqlParameter("@OldId", oldId));
        }


        public int IncomeRemove(int id)
        {
            int count = SqlHelper.ExecuteNonQuery("DELETE FROM AccountIncome WHERE AccountIncomeId = @Id", new SqlParameter("@Id", id));
            return count;
        }

        public int ExpendRemove(int id)
        {
            int count = SqlHelper.ExecuteNonQuery("DELETE FROM AccountExpend WHERE AccountExpendId = @Id", new SqlParameter("@Id", id)); 
            return count;
        }

        /// <summary>
        /// 用于更新收入表的Id
        /// </summary>
        /// <param name="id"></param>
        public void IncomeUpdateId(int id)
        {
            SqlHelper.ExecuteNonQuery("DBCC CHECKIDENT('AccountIncome',RESEED,@Id);", new SqlParameter("@Id", id - 1));
        }

        public void ExpendUpdateId(int id)
        {
            SqlHelper.ExecuteNonQuery("DBCC CHECKIDENT('AccountExpend',RESEED,@Id);", new SqlParameter("@Id", id - 1));
        }

        /// <summary>
        /// 取支出topCount条数据，用于分页
        /// </summary>
        public ObservableCollection<AccountExpendModel> GetExpendTop(int pageIndex,int pageSize)
        {
            DataTable res = SqlHelper.ExecuteTable(
                "SELECT TOP(@PageSize) * FROM AccountExpend WHERE AccountExpendId = NOT IN(SELECT TOP(@PageSize * (@PageIndex - 1)) AccountExpendId FROM AccountExpend;)",
                new SqlParameter("@PageSize",pageSize),new SqlParameter("@PageIndex",pageIndex));
            ObservableCollection<AccountExpendModel> accountExpendList = ToExpendModelList(res);
            return accountExpendList;
        }

        /// <summary>
        /// 取收入topCount条数据，用于分页
        /// </summary>
        public ObservableCollection<AccountIncomeModel> GetIncomeTop(int topCount)
        {
            DataTable res = SqlHelper.ExecuteTable("SELECT TOP @Count * FROM AccountIncome;", new SqlParameter("@Count", topCount));
            ObservableCollection<AccountIncomeModel> accountIncomeList = ToIncomeModelList(res);
            return accountIncomeList;
        }

        /// <summary>
        /// 根据月份查询所有支出
        /// </summary>
        /// <param name="month">月份</param>
        /// <returns></returns>
        public ObservableCollection<AccountExpendModel> GetExpendMonth(int month)
        {
            DataTable res = SqlHelper.ExecuteTable("SELECT * FROM AccountExpend WHERE MONTH(InsertDate) = @Month;",new SqlParameter("@Month",month));
            ObservableCollection<AccountExpendModel> accountExpendList = ToExpendModelList(res);
            return accountExpendList;
        }

        /// <summary>
        /// 根据月份查询所有收入
        /// </summary>
        /// <param name="month">月份</param>
        /// <returns></returns>
        public ObservableCollection<AccountIncomeModel> GetIncomeMonth(int month)
        {
            DataTable res = SqlHelper.ExecuteTable("SELECT * FROM AccountIncome WHERE MONTH(InsertDate) = @Month;", new SqlParameter("@Month", month));
            ObservableCollection<AccountIncomeModel> accountIncomeList = ToIncomeModelList(res);
            return accountIncomeList;
        }

        /// <summary>
        /// 根据年份查询所有支出
        /// </summary>
        /// <param name="year">年份</param>
        /// <returns></returns>
        public ObservableCollection<AccountExpendModel> GetExpendYear(int year)
        {
            DataTable res = SqlHelper.ExecuteTable("SELECT * FROM AccountExpend WHERE YEAR(InsertDate) = @Year;", new SqlParameter("@Year", year));
            ObservableCollection<AccountExpendModel> accountExpendList = ToExpendModelList(res);
            return accountExpendList;
        }

        /// <summary>
        /// 根据年份查询所有收入
        /// </summary>
        /// <param name="year">年份</param>
        /// <returns></returns>
        public ObservableCollection<AccountIncomeModel> GetIncomeYear(int year)
        {
            DataTable res = SqlHelper.ExecuteTable("SELECT * FROM AccountIncome WHERE YEAR(InsertDate) = @Year;", new SqlParameter("@Year", year));
            ObservableCollection<AccountIncomeModel> accountIncomeList = ToIncomeModelList(res);
            return accountIncomeList;
        }

        /// <summary>
        /// 查询范围内的数据，包括起始和结束的数据
        /// </summary>
        /// <param name="beginDateTime">起始时间</param>
        /// <param name="endDateTime">结束时间</param>
        /// <returns></returns>
        public ObservableCollection<AccountExpendModel> GetExpendBeginEndDateTime(string beginDateTime,string endDateTime)
        {
            DataTable res =
                SqlHelper.ExecuteTable("SELECT * FROM AccountExpend WHERE InsertDate >= @BeginDateTime and InsertDate <= @EndDateTime",
                new SqlParameter("@BeginDateTime", beginDateTime),
                new SqlParameter("@EndDateTime",endDateTime));
            ObservableCollection<AccountExpendModel> accountExpendList = ToExpendModelList(res);
            return accountExpendList;
        }

        /// <summary>
        /// 查询范围内的数据，包括起始和结束的数据
        /// </summary>
        /// <param name="beginDateTime">起始时间</param>
        /// <param name="endDateTime">结束时间</param>
        /// <returns></returns>
        public ObservableCollection<AccountIncomeModel> GetIncomeBeginEndDateTime(string beginDateTime, string endDateTime)
        {
            DataTable res =
                SqlHelper.ExecuteTable("SELECT * FROM AccountIncome WHERE InsertDate >= @BeginDateTime and InsertDate <= @EndDateTime",
                new SqlParameter("@BeginDateTime", beginDateTime),
                new SqlParameter("@EndDateTime", endDateTime));
            ObservableCollection<AccountIncomeModel> accountIncomeList = ToIncomeModelList(res);
            return accountIncomeList;
        }

        private AccountIncomeModel ToIncomeModel(DataRow row)
        {
            AccountIncomeModel model = new AccountIncomeModel();
            model.Id = Convert.ToInt32(row["AccountIncomeId"]);
            model.IncomeDescription = row["AccountDescription"].ToString();
            model.IncomeType = row["AccountType"].ToString();
            model.IncomeMoney = Convert.ToDecimal(row["IncomeMoney"]);
            //处理日期时间
            var incomeTempDateTime = Convert.ToDateTime(row["InsertDate"]);
            model.IncomeDateTime = incomeTempDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            return model;
        }

        private AccountExpendModel ToExpendModel(DataRow row)
        {
            AccountExpendModel model = new AccountExpendModel();
            model.Id = Convert.ToInt32(row["AccountExpendId"]);
            model.ExpendDescription = row["AccountDescription"].ToString();
            model.ExpendType = row["AccountType"].ToString();
            model.ExpendMoney = Convert.ToDecimal(row["ExpendMoney"]);
            //处理日期时间
            var expendTempDateTime = Convert.ToDateTime(row["InsertDate"]);
            model.ExpendDateTime = expendTempDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            return model;
        }

        private ObservableCollection<AccountIncomeModel> ToIncomeModelList(DataTable table)
        {
            ObservableCollection<AccountIncomeModel> accountList = new ObservableCollection<AccountIncomeModel>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                AccountIncomeModel model = ToIncomeModel(row);
                accountList.Add(model);
            }
            return accountList;
        }

        private ObservableCollection<AccountExpendModel> ToExpendModelList(DataTable table)
        {
            ObservableCollection<AccountExpendModel> accountList = new ObservableCollection<AccountExpendModel>();
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
