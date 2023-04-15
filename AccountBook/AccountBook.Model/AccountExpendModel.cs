using System;

namespace AccountBook.Model
{
    public class AccountExpendModel
    {
        public int Id { get; set; }
        public string 支出描述 { get; set; }
        public string 支出类型 { get; set; }
        public decimal 支出金额 { get; set; }
        public System.DateTime 时间 { get; set; }
    }
}
