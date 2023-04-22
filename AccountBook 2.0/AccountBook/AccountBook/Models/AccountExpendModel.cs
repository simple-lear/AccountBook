using System;

namespace AccountBook.Models
{
    public class AccountExpendModel
    {
        public int Id { get; set; }
        public string ExpendDescription { get; set; }
        public string ExpendType { get; set; }
        public decimal ExpendMoney { get; set; }
        public string ExpendDateTime { get; set; }
    }
}
