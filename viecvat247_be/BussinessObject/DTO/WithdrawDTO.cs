using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.DTO
{
    public class WithdrawDTO
    {
        public long? Epoint { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? BankCode { get; set; }
        public string? BankAccountNumber { get; set; }
        public string? BankAccountName { get; set; }
    }
}
