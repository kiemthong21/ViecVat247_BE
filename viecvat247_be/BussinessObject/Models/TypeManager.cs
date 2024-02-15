using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessObject.Models
{
    public partial class TypeManager
    {
        public int TypeManagerId { get; set; }
        public string TypeManagerName { get; set; }
        public int? Status { get; set; }
        public virtual ICollection<TypeManagerUser> TypeManagerUsers { get; set; }
    }
}
