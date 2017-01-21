using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eStoreWebsite.Models
{
    public class CartItemDTO
    {
        //
        //  Auto Implemented Properties
        //
        public int Qty { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Graphic { get; set; }
        public decimal Msrp { get; set; }
    }
}