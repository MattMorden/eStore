using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;



namespace EStoreDAL
{
        public class ProductService : BaseService<Product>
        {
            public ProductService(AppDbContext ctx) : base(ctx) { }
        }




}

