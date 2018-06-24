using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using JVideoStore.Models;

namespace JVideoStore.ViewModels
{
    public class MovieFormViewModel
    {

        //Use required properties individually (best practice!)
        //public Movie Movie { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        //Only used if EDIT, hence nullable
        public int? Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Genre")]
        [Required]
        public int? GenreId { get; set; }

        [Display(Name = "Release Date")]
        [Required]
        public DateTime? ReleaseDate { get; set; }

        [Display(Name = "Number in Stock")]
        [Range(1, 20)]
        [Required]
        public byte? NumberInStock { get; set; }

        public string Title
        {
            get
            {
                return Id != 0 ? "Edit Movie" : "New Movie";
            }
        }

        //Constructor for NEW mode
        public MovieFormViewModel()
        {
            Id = 0;
        }

        //Constructor for EDIT mode
        public MovieFormViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            ReleaseDate = movie.ReleaseDate;
            NumberInStock = movie.NumberInStock;
            GenreId = movie.GenreId;
        }



    }
}
