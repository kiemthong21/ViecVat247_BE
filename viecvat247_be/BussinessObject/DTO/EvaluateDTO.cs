using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.DTO
{
    public class EvaluateDTO
    {
        [Required]
        public int rate { get; set; } = 10;

        public string? comment { get; set; }
    }
}
