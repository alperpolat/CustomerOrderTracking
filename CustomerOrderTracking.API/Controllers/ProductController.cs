using CustomerOrderTracking.Business;
using CustomerOrderTracking.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ProductOrderTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ProductManager productManager = new ProductManager();

      
        [HttpGet]
        public List<Product> Get()
        {
            return productManager.List();
        }

       
    [HttpPost]
        public ErrorMessages Post([FromBody] Product product)
        {
            return productManager.Insert(product) > 0 ? new ErrorMessages("S", "Success") : new ErrorMessages("E", "Error");

        }

      
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            Product product = productManager.Find(x => x.Id == id);

            if (product != null)
                return product;
            throw new ArgumentException("Product Not Found");
        }

      
        [HttpPut]
        public ErrorMessages Put([FromBody] Product product)
        {
            Product product_ = productManager.Find(x => x.Id == product.Id);
            product_.Barcode = product.Barcode;
            product_.Description = product.Description;
            product_.Quantity = product.Quantity;
            product_.Price = product.Price;

            return productManager.Update(product) > 0 ? new ErrorMessages("S", "Success") : new ErrorMessages("E", "Error");

        }

      
        [HttpDelete("{id}")]
        public ErrorMessages Delete(int id)
        {
            Product product = productManager.Find(x => x.Id == id);

            if (product != null)
            {
                productManager.Delete(product);
                return new ErrorMessages("S", "Success");
            }
            return new ErrorMessages("E", "Error");
        }

    }
}
