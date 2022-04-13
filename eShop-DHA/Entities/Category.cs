using System;
using System.Collections.Generic;

namespace eShop_DHA.Entities
{
    public  class Category
    {
        public DateTime? CreatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public string? Name { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? ExternalId { get; set; }
        public string? SfId { get; set; }
        public int Id { get; set; }
        
        public virtual ICollection<Product>? Products { get; set; }

    }
}
