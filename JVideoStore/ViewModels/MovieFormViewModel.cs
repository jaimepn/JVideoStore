using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JVideoStore.Models;

namespace JVideoStore.ViewModels
{
    public class MovieFormViewModel
    {

        public Movie Movie { get; set; }
        public IEnumerable<Genre> Genres { get; set; }


    }
}