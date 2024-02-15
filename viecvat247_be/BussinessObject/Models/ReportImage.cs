using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessObject.Models
{
    public class ReportImage
    {
        public int ReportImageId { get; set; }
        public int ReportId { get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        [MaxLength]
        public string? Image { get; set; }
        public int? ImageType { get; set; }
        public virtual Report? Report { get; set; }

    }
}
