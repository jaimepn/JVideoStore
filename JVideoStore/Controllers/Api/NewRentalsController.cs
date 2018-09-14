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

            var customer = _context.Customers.SingleOrDefault(c => c.Id == newRental.CustomerId);
            if (customer == null)
                return BadRequest("Customer not valid!");

            var movies = _context.Movies.Where(m => newRental.MovieIds.Contains(m.Id)).ToList();
            if (movies.Count != newRental.MovieIds.Count)
                return BadRequest("One or more movies are invalid!");


            foreach (var movie in movies)
            {

                if (movie.NumberAvailable == 0)
                    return BadRequest("No stock for a given movie!");

                movie.NumberAvailable--;
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
