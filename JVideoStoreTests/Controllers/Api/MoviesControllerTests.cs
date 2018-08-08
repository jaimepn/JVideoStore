using Microsoft.VisualStudio.TestTools.UnitTesting;
using JVideoStore.Controllers.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JVideoStore.Controllers.Api.Tests
{
    [TestClass()]
    public class MoviesControllerTests
    {
        [TestMethod()]
        public void GetMoviesTest()
        {
            var sss = new MoviesController();
            sss.GetMovies();

            Assert.Fail();
        }
    }
}