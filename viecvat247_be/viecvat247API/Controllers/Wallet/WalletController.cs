using AutoMapper;
using BusinessObject;
using BussinessObject.DTO;
using BussinessObject.Models;
using DataAccess.ControllerDAO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using viecvat247Service;
using viecvat247Service.Service;

namespace viecvat247API.Controllers.Wallet
{
    [Route("api/Staff")]
    [ApiController]
    public class WalletController : ControllerBase
    {

        //cc
        private ITransactionService _transactionService = new TransactionService();
        private ICustomerService _customerService = new CustomerService();
        private IAuthenService _authenService = new AuthenService();

        private readonly IMapper _mapper;
        public WalletController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost("Deposit")]
        public IActionResult deposit(DepositeDTO deposite)
        {
            try
            {
                var cid = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
                var cus = _customerService.GetCustomerById(Int32.Parse(cid.ToString()));
                var trans = _transactionService.getTransactionBySecureHash(deposite.SecureHash);
                if (cus == null)
                {
                    return Ok(new { message = "user_not_exist" });
                }
                if (deposite.Epoint <= 0)
                {
                    return Ok(new { message = "epoint_greater_than_0" });
                }
                if (!deposite.TransactionStatus.Equals("00") || trans != null)
                {
                    return Ok(new { message = "payment_fail" });
                }
                Transaction transactionCreate = _mapper.Map<Transaction>(deposite);
                transactionCreate.Paymentdate = DateTime.Now;
                transactionCreate.ReceiverId = int.Parse(cid);
                transactionCreate.Detail = deposite.OrderInfo;
                transactionCreate.Status = 1;
                transactionCreate.Note = "nap_tien_" + deposite.Epoint;
                transactionCreate.TransactionType = 1;
                transactionCreate.OldBalance = cus.Epoint;
                transactionCreate.NewBalance = cus.Epoint + deposite.Epoint;
                _transactionService.CreateTransaction(transactionCreate);
                cus.Epoint = cus.Epoint + deposite.Epoint;
                _customerService.UpdateCustomer(cus);
                return Ok(new { message = "deposite_successful" });


            }
            catch (Exception)
            {

                return BadRequest(new { error = "error" });
            }

        }


        [Authorize]
        [HttpPost("Withdraw")]
        public IActionResult withdraw(WithdrawDTO withdraw)
        {
            try
            {
                var cid = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
                var cus = _customerService.GetCustomerById(Int32.Parse(cid.ToString()));
                if (!cus.Cemail.ToLower().Equals(withdraw.Email.ToLower()))
                {
                    return Ok(new { message = "user_not_exist" });
                }
                LoginDTO LoginDTO = new LoginDTO();
                LoginDTO.Email = withdraw.Email;
                LoginDTO.Password = withdraw.Password;
                var cusWithDraw = _authenService.GetCustomerByLogin(LoginDTO);
                if (cus == null || cusWithDraw == null)
                {
                    return Ok(new { message = "user_not_exist" });
                }
                if (withdraw.Epoint < 10000 )
                {
                    return Ok(new { message = "epoint_greater_than_or_equals_10000" });
                }
                if (withdraw.Epoint > cusWithDraw.Epoint || cusWithDraw.Epoint == null)
                {
                    return Ok(new { message = "insufficient_balance" });
                }
                Transaction transactionCreate = new Transaction();
                transactionCreate.Epoint = withdraw.Epoint;
                transactionCreate.Paymentdate = DateTime.Now;
                transactionCreate.CustomerId = cusWithDraw.Cid;
                transactionCreate.Detail = withdraw.BankAccountName + "_rut_tien_tu_he_thong";
                transactionCreate.Status = 1;
                transactionCreate.Note = "rut_tien_"+withdraw.BankAccountNumber+"_" + withdraw.Epoint;
                transactionCreate.TransactionType = 2;
                transactionCreate.BankCode = withdraw.BankCode;
                transactionCreate.OldBalance = cusWithDraw.Epoint;
                transactionCreate.NewBalance = cusWithDraw.Epoint - withdraw.Epoint;
                _transactionService.CreateTransaction(transactionCreate);
                cus.Epoint = cus.Epoint - withdraw.Epoint;
                _customerService.UpdateCustomer(cus);
                return Ok(new { message = "withDraw_successful" });


            }
            catch (Exception)
            {

                return BadRequest(new { error = "error" });
            }

        }

        [AllowAnonymous]
        [HttpGet("ListAllTransactions")]
        public IActionResult getAllTransaction([FromQuery] int? customerId, int? transactionType, DateTime? startDate, DateTime? endDate, string? orderBy,
            int pageIndex, int pageSize)
        {
            if (!string.IsNullOrWhiteSpace(customerId.ToString()))
            {
                var cus = _customerService.GetCustomerById(int.Parse(customerId.ToString()));
                if (cus == null)
                {
                    return Ok(new { message = "user_not_exist" });
                }

            }
            PaginatedList<Transaction> transactions = _transactionService.getAllJobsByCustomerId(customerId, transactionType, startDate, endDate, pageIndex, pageSize, orderBy);
            return Ok(new { totalItems = transactions.Totalsize, totalPage = transactions.TotalPages, Transactions = _mapper.Map<List<TransactionDTO>>(transactions) });
        }

        [Authorize]
        [HttpGet("ListAllTransactionsByCustomer")]
        public IActionResult getAllJobsByCustomerId([FromQuery] int? transactionType, DateTime? startDate, DateTime? endDate, string? orderBy,
            int pageIndex, int pageSize)
        {

            var cid = User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
            var cus = _customerService.GetCustomerById(Int32.Parse(cid.ToString()));
            if (cus == null)
            {
                return Ok(new { message = "user_not_exist" });
            }
            PaginatedList<Transaction> transactions = _transactionService.getAllJobsByCustomerId(int.Parse(cid), transactionType, startDate, endDate, pageIndex, pageSize, orderBy);
            return Ok(new { totalItems = transactions.Totalsize, totalPage = transactions.TotalPages, Transactions = _mapper.Map<List<TransactionDTO>>(transactions) });
        }



    }
}
