using BusinessObject;
using BussinessObject.DTO;
using BussinessObject.Models;
using DataAccess.ControllerDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace viecvat247Service.Service
{
    public class TransactionService : ITransactionService
    {
        public void CreateTransaction(Transaction transactionCreate)
        => TransactionDAO.CreateTransaction(transactionCreate);

        public PaginatedList<Transaction> getAllJobsByCustomerId(int? customerId, int? transactionType, DateTime? startDate, DateTime? endDate, int pageIndex, int pageSize, string? orderBy)
        => TransactionDAO.getAllJobsByCustomerId(customerId,transactionType, startDate, endDate, pageIndex, pageSize, orderBy);

        public PaginatedList<Transaction> GetAllTransaction(int? transactionType, DateTime? startDate, DateTime? endDate, int pageIndex, int pageSize, string? orderBy)
        => TransactionDAO.GetAllTransaction(transactionType,startDate,endDate,pageIndex,pageSize,orderBy);

        public Transaction getTransactionBySecureHash(string? secureHash)
            => TransactionDAO.GetTransactionBySecureHash(secureHash);
        
    }
}
