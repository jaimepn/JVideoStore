using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using JVideoStore.Migrations;
using JVideoStore.Models;
using JVideoStore.ViewModels;
using System.Data.Entity;
using System;

namespace JVideoStore.Controllers
{
    public class MoviesController : Controller
    {

        private MyContext _context;

        public MoviesController()
        {
            _context = new MyContext();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _context.Dispose();
        }

        //private IEnumerable<Movie> GetMovies()
        //{
        //    return new List<Movie>
        //    {
        //        new Movie { Id = 1, Name = "Shrek" },
        //        new Movie { Id = 2, Name = "Wall-e" }
        //    };
        //}

        public ViewResult Index()
        {
            //var movies = GetMovies();
            var movies = _context.Movies.Include(m => m.Genre).ToList();

            return View(movies);
        }


        public ViewResult Details(int Id)
        {

            var movie = _context.Movies.Include(mbox => mbox.Genre).SingleOrDefault(m2 => m2.Id == Id);
            return View(movie);
        }

        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };
            var customers = new List<Customer>
            {
                new Customer { Name = "Customer 1" },
                new Customer { Name = "Customer 2" }
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
        }


        public ActionResult New()
        {
            var model = new MovieFormViewModel
            {
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", model);
        }

        public ActionResult Edit(int Id)
        {
            var model = new MovieFormViewModel
            {
                Movie = _context.Movies.Single(m => m.Id == Id),
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", model);
        }


        [HttpPost]
        public ActionResult Save(MovieFormViewModel model)
        {

            if (model.Movie.Id == 0)
            {
                //New Movie
                var movie = model.Movie;
                movie.DateAdded = DateTime.Now;

                _context.Movies.Add(movie);
            }
            else
            {
                //Edit Movie
                var movie = _context.Movies.Single(m => m.Id == model.Movie.Id);
                movie.Name = model.Movie.Name;
                movie.ReleaseDate = model.Movie.ReleaseDate;
                movie.GenreId = model.Movie.GenreId;
                movie.NumberInStock = model.Movie.NumberInStock;
            }
            
            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }
    }
}