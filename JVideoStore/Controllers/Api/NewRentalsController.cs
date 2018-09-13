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
    public class NewRentalsController : ApiController
    {

        private readonly MyContext _context;

        public NewRentalsController()
        {
            _context = new MyContext();
        }

        //GET Api/Rentals
        [HttpGet]
        public IHttpActionResult GetRentals()
        {
            var movieIds = new List<int>
            {
                2,
                3
            };
            var newRental = new NewRentalDto
            {
                CustomerId = 4,
                MovieIds = movieIds
            };

            return Ok(newRental);
        }


        //POST Api/Rentals
        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRental)
        {

            var customer = _context.Customers.Single(c => c.Id == newRental.CustomerId);

            var movies = _context.Movies.Where(m => newRental.MovieIds.Contains(m.Id));

            foreach (var movie in movies)
            {
                //var movie = _context.Movies.FirstOrDefault(m => m.Id == movieId);

                var rental = new Rental
                {
                    Customer = customer,
                    Movies = movie,
                    DateRented = DateTime.Now
                };

                _context.Rentals.Add(rental);                
            }

            _context.SaveChanges();

            return Ok();
        }


    }
}
