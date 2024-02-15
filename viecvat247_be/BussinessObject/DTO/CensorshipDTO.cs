using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BussinessObject.DTO
{
    public class CensorshipDTO
    {
        [Required(ErrorMessage = "Status is require"), DefaultValue(0)]
        public int? Status { get; set; }

        public string? Note { get; set; }
    }
}
