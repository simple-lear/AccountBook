using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBook.Model
{
    public class AccountTypeModel
    {
        private static AccountTypeModel instance;
        private static readonly object obj = new object();

        public static AccountTypeModel GetInstance()
        {
            if (instance == null)
            {
                lock (obj)
                {
                    if (instance == null)
                    {
                        instance = new AccountTypeModel();
                    }
                }
            }
            return instance;
        }

        public List<string> GetIncomeTypeAll()
        {
            List<string> all = new List<string>();
            all.Add("工资");
            all.Add("兼职");
            all.Add("理财");
            all.Add("其它");
            return all;
        }

        public List<string> GetExpendTypeAll()
        {
            return new List<string>()
            {
                "生活",
                "餐饮",
                "购物",
                "其它"
            };
        }
    }
}
