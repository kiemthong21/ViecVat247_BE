using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.DTO
{
    public class EditStaffDTO
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public string[]? TypeManagers { get; set; }
    }
}
