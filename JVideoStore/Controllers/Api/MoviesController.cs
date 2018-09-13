using JVideoStore.Dtos;
using JVideoStore.Migrations;
using JVideoStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public IHttpActionResult GetMovies()
        {
            var movies = _context.Movies.Include(g => g.Genre).ToList();
            var moviesDto = new List<MovieDto>();

            foreach (Movie movie in movies)
            {
                var genredto = new GenreDto
                {
                    Id = movie.Genre.Id,
                    Name = movie.Genre.Name
                };

                MovieDto moviedto = new MovieDto
                {
                    Id = movie.Id,
                    Name = movie.Name,
                    Genre = genredto,
                    GenreId = movie.GenreId,
                    NumberInStock = movie.NumberInStock,
                    ReleaseDate = movie.ReleaseDate
                };

                moviesDto.Add(moviedto);
            }
            return Ok(moviesDto);
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
        [Authorize(Roles = "canManageMovies")]
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

        [Authorize(Roles = "canManageMovies")]
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

        [Authorize(Roles = "canManageMovies")]
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
