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
            //var movies = _context.Movies.Include(m => m.Genre).ToList();
            if (User.IsInRole("canManageMovies"))                
                return View("List");
            else
                return View("ReadOnlyList");
        }


        public ViewResult Details(int Id)
        {

            var movie = _context.Movies.Include(mbox => mbox.Genre).SingleOrDefault(m2 => m2.Id == Id);
            return View(movie);
        }

  
        [Authorize(Roles = "canManageMovies")]
        public ActionResult New()
        {
            var model = new MovieFormViewModel
            {
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", model);
        }


        [Authorize(Roles = "canManageMovies")]
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
                return HttpNotFound();

            var model = new MovieFormViewModel(movie)
            {
                //Movie = _context.Movies.Single(m => m.Id == Id),
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", model);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        //ASP maps individual properties to a Movie object
        {

            if (!ModelState.IsValid)
            {
                var model2 = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
               };

                return View("MovieForm", model2);
            }

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }
    }
}