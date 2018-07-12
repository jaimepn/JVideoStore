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
    public class MoviesController : ApiController
    {

        private readonly MyContext _context;

        public MoviesController()
        {
            _context = new MyContext();
        }

        //GET
        public IEnumerable<Movie> GetMovies()
        {
            return _context.Movies.ToList();
        }

        //GET
        public MovieDto GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var movieDto = new MovieDto
            {
                Id = movie.Id,
                DateAdded = movie.DateAdded,
                GenreId = movie.GenreId,
                Name = movie.Name,
                NumberInStock = movie.NumberInStock,
                ReleaseDate = movie.ReleaseDate
            };

            return movieDto;
        }

        //POST
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
                return BadRequest();

            var movie = new Movie
            {
                DateAdded = movieDto.DateAdded,
                GenreId = movieDto.GenreId,
                Name = movieDto.Name,
                NumberInStock = movieDto.NumberInStock,
                ReleaseDate = movieDto.ReleaseDate
            };

            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movieDto.Id), movieDto);
        }


        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
                return BadRequest();

            var movieDB = _context.Movies.Single(m => m.Id == id);

            if (movieDB == null)
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();

            movieDB.Name = movieDto.Name;
            movieDB.GenreId = movieDto.GenreId;
            movieDB.DateAdded = movieDto.DateAdded;
            movieDB.NumberInStock = movieDto.NumberInStock;
            movieDB.ReleaseDate = movieDto.ReleaseDate;

            _context.SaveChanges();

            return Ok();
        }


        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movieDB = _context.Movies.Single(m => m.Id == id);

            if (movieDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Movies.Remove(movieDB);
            _context.SaveChanges();

            return Ok();

        }

    }
}
