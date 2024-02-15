using BussinessObject.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using viecvat247Service;
using viecvat247Service.Service;


namespace viecvat247API.Controllers.Staff
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private IOtherService _otherService = new OtherService();

        

        [AllowAnonymous]
        [HttpGet("GetStatisticsDaskboard/{year}")]
        public IActionResult GetStatisticsDaskboard(int year)
        {
            try
            {
                DateTime yearNow = DateTime.Now;
                int numberYearNow = yearNow.Year;
                if (year > numberYearNow)
                {
                    return Ok(new { message = "year_large_now" });
                }
                DashboardDTO dashboardDTO = _otherService.GetStatisticsDaskboard(year);
                dashboardDTO.chartDTOs = _otherService.GetChart(year);
                return Ok(dashboardDTO);
            }
            catch (Exception)
            {
                return BadRequest(new { error = "error" });
            }
        }

        
    }
}
