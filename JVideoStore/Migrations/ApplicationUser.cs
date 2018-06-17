using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JVideoStore.Migrations
{
    public class ApplicationUser : IdentityUser
    {
        //only application user fields that are specific to your application
        //i.e: any fields that the default AspNetUsers table does not have
        public DateTime? InactiveDate { get; set; }
    }
}