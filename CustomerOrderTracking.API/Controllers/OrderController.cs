using CustomerOrderTracking.Business;
using CustomerOrderTracking.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerOrderTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        public OrderManager orderManager = new OrderManager();

        
        [HttpGet]
        public List<Order> Get()
        {
            return orderManager.List();
        }

       
        [HttpPost]
        public ErrorMessages Post([FromBody] Order order)
        {
            return orderManager.Insert(order) > 0 ? new ErrorMessages("S", "Success") : new ErrorMessages("E", "Error");

        }

        [HttpGet("{id}")]
        public Order Get(int id)
        {
            Order order = orderManager.Find(x => x.Id == id);

            if (order != null)
                return order;
            throw new ArgumentException("Order Not Found");
        }

       
        [HttpPut]
        public ErrorMessages Put([FromBody] Order order)
        {

            Order order_ = orderManager.Find(x => x.Id == order.Id);
            order_.CustomerId = order.CustomerId;
            order_.Address = order.Address;

            return orderManager.Update(order_) > 0 ? new ErrorMessages("S", "Success") : new ErrorMessages("E", "Error");

        }

       
        [HttpDelete("{id}")]
        public ErrorMessages Delete(int id)
        {
            Order order = orderManager.Find(x => x.Id == id);

            if (order != null)
            {
                orderManager.Delete(order);
                return new ErrorMessages("S", "Success");
            }
            return new ErrorMessages("E", "Error");
        }
    }
}
