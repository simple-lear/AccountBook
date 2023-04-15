using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBook.Model
{
    public class AccountIncomeModel
    {
        public int Id { get; set; }
        public string 收入描述 { get; set; }
        public string 收入类型 { get; set; }
        public decimal 收入金额 { get; set; }
        public System.DateTime 时间 { get; set; }
    }
}
