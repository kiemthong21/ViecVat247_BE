using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.DTO
{
    public class DepositeDTO
    {
        public long? Epoint { get; set; }
        public string? OrderInfo { get; set; }
        public string? BankCode { get; set; }
        public string? BankTranNo { get; set; }
        public string? CardType { get; set; }
        public string? TransactionNo { get; set; }
        public string? TransactionStatus { get; set; }
        public string? TxnRef { get; set; }
        public string? SecureHash { get; set; }
    }
}
