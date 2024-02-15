using AutoMapper;
using BusinessObject;
using BussinessObject.DTO;
using BussinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using viecvat247Service;
using viecvat247Service.Service;

namespace viecvat247API.Controllers.Staff
{
    [Route("api/Staff")]
    [ApiController]
    public class ManageCustomerController : ControllerBase
    {
        private ICustomerService _customerService = new CustomerService();
        private IOtherService _otherService = new OtherService();
        private readonly IMapper _mapper;
        public ManageCustomerController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("GetCustomers")]
        public IActionResult GetCustomers([FromQuery] string? searchValue, string? orderBy, string? status, string? role,
            int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                if (pageIndex < 1 || pageSize < 1)
                {
                    BadRequest(new { error = "error" });
                }
                PaginatedList<Customer> cus = _customerService.GetCustomers(searchValue, pageIndex, pageSize, orderBy, role, status);
                var cusDTO = cus.Select(e => _mapper.Map<CustomerDTO>(e));
                return Ok(new { totalItems = cus.Totalsize, totalPages = cus.TotalPages, customers = cusDTO });
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }

        [Authorize]
        [HttpPut("BanOrBanRemoveCustomer/{id}")]
        public IActionResult BanOrBanRemoveCustomer(int id)
        {
            try
            {
                var cus = _customerService.GetCustomerById(id);
                if (cus == null)
                {
                    return Ok(new { message = "not_found" });
                }
                else if (cus.Status == 3)
                {
                    cus.Status = 1;
                    _customerService.UpdateCustomer(cus);
                    return Ok(new { message = "ban_remove_successful" });
                }
                else 
                {
                    cus.Status = 3;
                    _customerService.UpdateCustomer(cus);
                    return Ok(new { message = "ban_successful" });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }

        [Authorize]
        [HttpGet("GetCustomer/{id}")]
        public IActionResult GetCustomerById(int id)
        {
            try
            {
                var cus = _customerService.GetCustomerById(id);
                if (cus == null)
                {
                    return Ok(new { message = "not_found" });
                }
                else
                {
                    return Ok(_mapper.Map<CustomerDTO>(cus));
                }
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }


    }
}
