using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using JVideoStore.Models;

namespace JVideoStore.Migrations
{
    public class MyContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// default project context
        /// </summary>
        public MyContext() : base("JVideoStoreConnectionString")
        {
            //optional but I use: Configuration.LazyLoadingEnabled = false;
            //optional but I use: Configuration.ProxyCreationEnabled = false;
        }

        public static MyContext Create()
        {
            return new MyContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //remove cascade deletions; we handle our deletions manually in the code
            //optional but I use: modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //optional but I use: modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

        //add a dbset for each model class that you want to make a table for
        //you do NOT need to do this for the Identity provided classes
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}