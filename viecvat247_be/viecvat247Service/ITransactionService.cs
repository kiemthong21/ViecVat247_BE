using BusinessObject;
using BussinessObject.DTO;
using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace viecvat247Service
{
    public interface ITransactionService
    {
        Transaction getTransactionBySecureHash(string? secureHash);
        void CreateTransaction(Transaction transactionCreate);
        PaginatedList<Transaction> GetAllTransaction(int? transactionType, DateTime? startDate, DateTime? endDate, int pageIndex, int pageSize, string? orderBy);
        PaginatedList<Transaction> getAllJobsByCustomerId(int? customerId, int? transactionType, DateTime? startDate, DateTime? endDate, int pageIndex, int pageSize, string? orderBy);
    }
}
