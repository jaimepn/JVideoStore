using AutoMapper;
using JVideoStore.Dtos;
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
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return _context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);
        }

        //GET Api/Customers/1
        public CustomerDto GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var customerdto = Mapper.Map<Customer, CustomerDto>(customer);
            return customerdto;
        }

        //POST Api/Customers
        [HttpPost]
        public CustomerDto CreateCustomer(CustomerDto customerdto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customer = Mapper.Map<CustomerDto, Customer>(customerdto);

            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerdto.Id = customer.Id;

            return customerdto;
        }

        //PUT Api/Customers/1
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerdto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerDB = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(customerdto, customerDB);

            //customerDB.Name = customer.Name;
            //customerDB.MembershipTypeId = customer.MembershipTypeId;
            //customerDB.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            //customerDB.BirthDate = customer.BirthDate;

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
