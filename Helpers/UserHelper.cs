using coreblog.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace coreblog.Helpers
{
    public class UserHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        public static string GetDisplayName()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            return db.Users.Find(userId).DisplayName;
        }
    }
}




