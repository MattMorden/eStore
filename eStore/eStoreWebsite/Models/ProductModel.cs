using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EStoreDAL;
using System.Threading.Tasks;

namespace eStoreWebsite.Models
{
    public class ProductModel
    {
        private ProductService _dal;

        public string ProdCode { get; set; }
        public string ProdName { get; set; }
        public string Description { get; set; }
        public string Graphic { get; set; }
        public decimal Msrp { get; set; }
        public decimal CostPrice { get; set; }
        public int Qob { get; set; }
        public int Qoh { get; set; }

        public ProductModel()
        {
            _dal = new ProductService(new AppDbContext());
        }


        public async Task<List<ProductModel>> GetAllAsync()
        {
            List<ProductModel> models = new List<ProductModel>();
            ICollection<Product> prdsData = await _dal.GetAllAsync();

            foreach (Product prod in prdsData)
            {
                ProductModel model = new ProductModel();
                model.ProdCode = prod.ProductID;
                model.ProdName = prod.ProductName;
                model.Graphic = prod.GraphicName;
                model.CostPrice = prod.CostPrice;
                model.Msrp = prod.MSRP;
                model.Qob = prod.QtyOnBackOrder;
                model.Qoh = prod.QtyOnHand;
                model.Description = prod.Description;
                models.Add(model);
            }
            return models;
        }

    }
}