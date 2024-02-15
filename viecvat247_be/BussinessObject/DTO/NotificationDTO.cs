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
    public class NotificationDTO
    {
        public int NotificationId { get; set; }
        public string? Description { get; set; }
        public DateTime? Timestamp { get; set; }
        public int? JobId { get; set; }
        public int? Status { get; set; }
        public int? Aid { get; set; }
    }
}
