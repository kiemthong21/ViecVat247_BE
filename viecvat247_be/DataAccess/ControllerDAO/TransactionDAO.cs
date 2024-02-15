using BusinessObject;
using BussinessObject.DTO;
using BussinessObject.Models;
using BussinessObject.Viecvat247Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ControllerDAO
{
    public class TransactionDAO
    {
        public static Transaction GetTransactionBySecureHash(string? secureHash)
        {
            var transaction = new Transaction();

            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    transaction = context.Transactions.SingleOrDefault(c => c.SecureHash.Equals(secureHash));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return transaction;
        }

        

        public static Job CreateJob(Job jobCreate)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    context.Jobs.Add(jobCreate);
                    context.SaveChanges();
                    return jobCreate;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void CreateTransaction(Transaction transactionCreate)
        {
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    context.Transactions.Add(transactionCreate);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static PaginatedList<Transaction> GetAllTransaction(int? transactionType, DateTime? startDate, DateTime? endDate, int pageIndex, int pageSize, string? orderBy)
        {
            var Transactions = new List<Transaction>();
            int count = 0;
            IQueryable<Transaction> query = null;
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    query = context.Transactions
                        .Include(j => j.Customer)
                        .Include(j => j.Receiver)
                        .Include(j => j.Job);

                    if (!string.IsNullOrWhiteSpace(transactionType.ToString()))
                    {
                        query = query.Where(e => e.TransactionType == transactionType);

                    }
                    if (!string.IsNullOrWhiteSpace(startDate.ToString()) && string.IsNullOrWhiteSpace(endDate.ToString()))
                    {
                        query = query.Where(e => e.Paymentdate > startDate);
                    }
                    else if (!string.IsNullOrWhiteSpace(endDate.ToString()) && string.IsNullOrWhiteSpace(startDate.ToString()))
                    {
                        query = query.Where(e => e.Paymentdate < endDate);
                    }
                    else if (!string.IsNullOrWhiteSpace(endDate.ToString()) && !string.IsNullOrWhiteSpace(startDate.ToString()))
                    {
                        query = query.Where(e => e.Paymentdate > startDate && e.Paymentdate < endDate);
                    }

                    if (!string.IsNullOrWhiteSpace(orderBy))
                    {
                        switch (orderBy)
                        {
                            case "Epoint desc":
                                query = query.OrderByDescending(e => e.Epoint);
                                break;
                            case "PaymentDate":
                                query = query.OrderBy(e => e.Paymentdate);
                                break;
                            case "PaymentDate desc":
                                query = query.OrderByDescending(e => e.Paymentdate);
                                break;
                            default:
                                query = query.OrderBy(e => e.Epoint);
                                break;
                        }
                    }
                    query = query.Where(e => e.Status == 1);
                    count = query.Count();
                    if (pageIndex != 0 && pageSize != 0)
                    {
                        query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    }
                    Transactions = query.ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return new PaginatedList<Transaction>(Transactions, count, pageIndex, pageSize);

        }


        public static PaginatedList<Transaction> getAllJobsByCustomerId(int? customerId, int? transactionType, DateTime? startDate, DateTime? endDate, int pageIndex, int pageSize, string? orderBy)
        {
            var Transactions = new List<Transaction>();
            int count = 0;
            IQueryable<Transaction> query = null;
            try
            {
                using (var context = new Viecvat247DBcontext())
                {
                    query = context.Transactions
                        .Include(j => j.Customer)
                        .Include(j => j.Receiver)
                        .Include(j => j.Job);

                    if (!string.IsNullOrWhiteSpace(transactionType.ToString()))
                    {
                        query = query.Where(e => e.TransactionType == transactionType);

                    }
                    if (!string.IsNullOrWhiteSpace(startDate.ToString()) && string.IsNullOrWhiteSpace(endDate.ToString()))
                    {
                        query = query.Where(e => e.Paymentdate > startDate);
                    }
                    else if (!string.IsNullOrWhiteSpace(endDate.ToString()) && string.IsNullOrWhiteSpace(startDate.ToString()))
                    {
                        query = query.Where(e => e.Paymentdate < endDate);
                    }
                    else if (!string.IsNullOrWhiteSpace(endDate.ToString()) && !string.IsNullOrWhiteSpace(startDate.ToString()))
                    {
                        query = query.Where(e => e.Paymentdate > startDate && e.Paymentdate < endDate);
                    }

                    if (!string.IsNullOrWhiteSpace(orderBy))
                    {
                        switch (orderBy)
                        {
                            case "Epoint desc":
                                query = query.OrderByDescending(e => e.Epoint);
                                break;
                            case "PaymentDate":
                                query = query.OrderBy(e => e.Paymentdate);
                                break;
                            case "PaymentDate desc":
                                query = query.OrderByDescending(e => e.Paymentdate);
                                break;
                            default:
                                query = query.OrderBy(e => e.Epoint);
                                break;
                        }
                    }
                    query = query.Where(e => e.Status == 1);
                    if (!string.IsNullOrWhiteSpace(customerId.ToString()))
                    {
                        query = query.Where(e => e.CustomerId == customerId || e.ReceiverId == customerId);

                    }
                    count = query.Count();
                    if (pageIndex != 0 && pageSize != 0)
                    {
                        query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    }
                    Transactions = query.ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return new PaginatedList<Transaction>(Transactions, count, pageIndex, pageSize);
        }

    }
}
