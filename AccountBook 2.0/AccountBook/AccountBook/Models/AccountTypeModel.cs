using AccountBook.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBook.Models
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

        /// <summary>
        /// 类型名
        /// </summary>
        private string typeName;

        public string TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }


    }
}
