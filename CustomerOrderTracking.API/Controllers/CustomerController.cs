using CustomerOrderTracking.Business;
using CustomerOrderTracking.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CustomerOrderTracking.API.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public CustomerManager customerManager = new CustomerManager();
        public OrderManager orderManager = new OrderManager();
        private ProductManager productManager = new ProductManager();
        private OrderDetailManager orderDetailManager = new OrderDetailManager();


        [HttpPost]
        public ErrorMessages Post([FromBody] Customer customer)
        {
            return customerManager.Insert(customer) > 0 ? new ErrorMessages("S", "Success") : new ErrorMessages("E", "Error");

        }

     
        [HttpGet]
        public List<Customer> Get()
        {
            return customerManager.List();
        }

     
        [HttpGet("{id}")]
        public Customer Get(int id)
        {
            Customer customer = customerManager.Find(x => x.Id == id);

            if (customer != null)
                return customer;
            throw new ArgumentException("Customer Not Found");
        }

        
        [HttpPut]
        public ErrorMessages Put([FromBody] Customer customer) //Diğerlerini de ez RR:
        {
            Customer customer_ = customerManager.Find(x => x.Id == customer.Id);
            customer_.Name = customer.Name;
            customer_.Address = customer.Address;
            return customerManager.Update(customer_) > 0 ? new ErrorMessages("S", "Success") : new ErrorMessages("E", "Error");

        }

       
        [HttpDelete("{id}")]
        public ErrorMessages Delete(int id)
        {
            Customer customer = customerManager.Find(x => x.Id == id);

            if (customer != null)
            {
                customerManager.Delete(customer);
                return new ErrorMessages("S", "Success");
            }
            return new ErrorMessages("E", "Error");
        }

     
        [HttpPost("{customerId}/orders")]
        public ErrorMessages GetCustomerOrders(Order order)
        {
            Customer customer = customerManager.Find(x => x.Id == order.CustomerId);
            if (customer != null)
            {
                foreach (var item in order.OrderDetails)
                {
                    Product product = productManager.Find(x => x.Id == item.ProductId);
                    product.Quantity -= item.NumberOfProducts;
                    productManager.Update(product);
                    item.CreatedOn = DateTime.Now;
                    item.ModifiedOn = DateTime.Now;
                }
                return orderManager.Insert(order) > 0 ? new ErrorMessages("S", "Success") : new ErrorMessages("E", "Error");
            }
            throw new ArgumentException("Customer Not Found");

        }

      
        [HttpGet("{customerId}/orders")]
        public List<Order> GetCustomerOrders(int customerId)
        {
            List<Order> orders = orderManager.List(x => x.CustomerId == customerId);
            List<Order> listOrders = new List<Order>();
            if (orders.Count != 0)
            {
                foreach (var order in orders)
                {
                    List<OrderDetails> orderDetailList = orderDetailManager.List(x => x.OrderId == order.Id);
                    if (orderDetailList != null)
                    {
                        List<Product> productList = new List<Product>();
                        foreach (var item in orderDetailList)
                        {
                            if (order.Id == item.OrderId)
                            {
                                if(!listOrders.Contains(order))
                                listOrders.Add(order);
                            }
                            item.Product = productManager.Find(x => x.Id == item.ProductId);
                            item.Order = null;
                        }
                    }
                    order.Customer = null;
                }
            }
            return listOrders;
        }


        [HttpGet("{customerId}/orders/{orderId}/lineItems")]
        public List<OrderDetails> GetCustomerOrderItems(int customerId, int orderId)
        {
            List<OrderDetails> orderDetailList = new List<OrderDetails>();
            List<Order> orders = orderManager.List(x => x.CustomerId == customerId && x.Id == orderId);
            if (orders.Count != 0)
            {
                foreach (var order in orders)
                {
                    orderDetailList = orderDetailManager.List(x => x.OrderId == order.Id);
                    if (orderDetailList != null)
                    {
                        List<Product> productList = new List<Product>();
                        foreach (var item in orderDetailList)
                        {
                            item.Product = productManager.Find(x => x.Id == item.ProductId);
                            item.Order = null;
                        }
                    }
                }
            }
            else
                throw new ArgumentException("Customer or Order Not Found");

            return orderDetailList;
        }


       
        [HttpGet("{customerId}/orders/{orderId}/lineItem/{itemId}")]
        public OrderDetails GetCustomerOrderItem(int customerId, int orderId, int itemId)
        {
            List<OrderDetails> orderDetailList = new List<OrderDetails>();
            List<Order> orders = orderManager.List(x => x.CustomerId == customerId && x.Id == orderId);
            if (orders.Count != 0)
            {
                foreach (var order in orders)
                {
                    orderDetailList = orderDetailManager.List(x => x.OrderId == order.Id);
                    if (orderDetailList != null)
                    {
                        List<Product> productList = new List<Product>();
                        foreach (var item in orderDetailList)
                        {
                            item.Product = productManager.Find(x => x.Id == item.ProductId);
                            item.Order = null;
                        }
                    }
                }
            }
            else
                throw new ArgumentException("Customer or Order Not Found");
            if(itemId> orderDetailList.Count)
                throw new ArgumentException("itemId should be smaller than customer's order's item count!");

            OrderDetails orderDetailsItem = orderDetailList[itemId - 1];

            return orderDetailsItem;
        }




    }
}
