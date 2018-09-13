using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JVideoStore.Models
{
    public class Rental
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Customer Customer {get; set; }

        [Required]
        public Movie Movies { get; set; }

        public DateTime DateRented { get; set; }

        public DateTime? DateReturned { get; set; }

    }
}