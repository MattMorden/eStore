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
    
    public partial class Order
    {
        public Order()
        {
            this.OrderLineitems = new HashSet<OrderLineitem>();
        }
    
        public int OrderID { get; set; }
        public System.DateTime OrderDate { get; set; }
        public decimal OrderAmount { get; set; }
        public string UserID { get; set; }
        public byte[] Timer { get; set; }
    
        public virtual ICollection<OrderLineitem> OrderLineitems { get; set; }
    }
}
