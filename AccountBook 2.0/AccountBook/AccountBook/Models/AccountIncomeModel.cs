using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBook.Models
{
    public class AccountIncomeModel
    {
        public int Id { get; set; }
        public string IncomeDescription { get; set; }
        public string IncomeType { get; set; }
        public decimal IncomeMoney { get; set; }
        public string IncomeDateTime { get; set; }
    }
}
