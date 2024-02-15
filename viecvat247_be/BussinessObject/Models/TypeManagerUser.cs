using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessObject.Models
{
    public partial class TypeManagerUser
    {
        public int TypeManagerUserId { get; set; }
        public int TypeManagerId { get; set; }
        public int Uid { get; set; }

        public virtual TypeManager TypeManager { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
