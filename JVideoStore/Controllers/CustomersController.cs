using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JVideoStore.Models;
using JVideoStore.ViewModels;
using System.Data.Entity;
using JVideoStore.Migrations;

namespace JVideoStore.Controllers
{
    public class CustomersController : Controller
    {

      


        private MyContext _context;

        public CustomersController()
        {
            _context = new MyContext();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _context.Dispose();
        }

        //private IEnumerable<Customer> GetCustomers()
        //{
        //    return new List<Customer>
        //    {
        //        new Customer { Id = 1, Name = "John Smith" },
        //        new Customer { Id = 2, Name = "Mary Williams" }
        //    };
        //}


        public ViewResult Index()
        {
            //COMMENTED OUT AS THE VIEW NOW USES API CALLS TO RETRIEVE DATA
            //var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            //return View(customers);

            return View();
        }


        public ActionResult Details(int id)
        {
            //var customer = GetCustomers().SingleOrDefault(c => c.Id == id);
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }


        public ActionResult New()
        {
            var MembershipTypes = _context.MembershipTypes.ToList();
            var ViewModel = new CustomerFormViewModel
            {
                MembershipTypes = MembershipTypes
            };

            return View("CustomerForm", ViewModel);
        }


        [HttpPost]
        public ActionResult Save(CustomerFormViewModel Model)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                var viewModel = new CustomerFormViewModel
                {
                    Customer = Model.Customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }

            //Continue normally if modelstate is valid
            if (Model.Customer.Id == 0)
            {
                _context.Customers.Add(Model.Customer);                
            }
            else
            {
                var customerInDB = _context.Customers.Single(C => C.Id == Model.Customer.Id);
                customerInDB.Name = Model.Customer.Name;
                customerInDB.MembershipTypeId = Model.Customer.MembershipTypeId;
                customerInDB.BirthDate = Model.Customer.BirthDate;
                customerInDB.IsSubscribedToNewsletter = Model.Customer.IsSubscribedToNewsletter;


            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var ViewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", ViewModel);
        }

    }

}