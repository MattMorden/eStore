//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EStoreDAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public Product()
        {
            this.OrderLineitems = new HashSet<OrderLineitem>();
        }
    
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string GraphicName { get; set; }
        public decimal CostPrice { get; set; }
        public decimal MSRP { get; set; }
        public int QtyOnHand { get; set; }
        public int QtyOnBackOrder { get; set; }
        public string Description { get; set; }
        public byte[] Timer { get; set; }
    
        public virtual ICollection<OrderLineitem> OrderLineitems { get; set; }
    }
}
