using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.DTO
{
    public class ReportImageDTO
    {
        public int ReportImageId { get; set; }
        public int ReportId { get; set; }
        public string? Image { get; set; }
        public int? ImageType { get; set; }
    }
}
