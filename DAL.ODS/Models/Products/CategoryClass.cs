using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DAL.ODS.Models.Products
{
    public class CategoryClass : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }        
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; } = true;

        //[JsonIgnore] 
        //Remove circular references during serialization
        public virtual ICollection<ProductClass> Products { get; set; } = new HashSet<ProductClass>();
    }
}
