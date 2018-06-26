using JVideoStore.Migrations;
using JVideoStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JVideoStore.Controllers.Api
{
    public class CustomersController : ApiController
    {

        private MyContext _context;

        public CustomersController()
        {
            _context = new MyContext();
        }

        //GET Api/Customers
        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

        //GET Api/Customers/1
        public Customer GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return customer;
        }

        //POST Api/Customers
        [HttpPost]
        public Customer CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return customer;
        }

        //PUT Api/Customers/1
        [HttpPut]
        public void UpdateCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerDB = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            customerDB.Name = customer.Name;
            customerDB.MembershipTypeId = customer.MembershipTypeId;
            customerDB.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            customerDB.BirthDate = customer.BirthDate;

            _context.SaveChanges();
        }

        //DELETE /Api/Customers/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerDB = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Customers.Remove(customerDB);
            _context.SaveChanges();
        }
    }
}
