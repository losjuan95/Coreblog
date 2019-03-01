using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using coreblog.Models;
using System.Web.Mvc;
using System.Configuration;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Configuration;
using PagedList;
using PagedList.Mvc;

namespace coreblog.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(int? page, string searchStr)
        {
            var pageSize = 3;
            var pageNumber = (page ?? 1);
            ViewBag.Search = searchStr;
            var blogList = IndexSearch(searchStr);

            return View(blogList.ToPagedList(pageNumber, pageSize));
        }

        public IQueryable<BlogPost> IndexSearch(string searchStr)
        {

            IQueryable<BlogPost> result = null;
            if (searchStr != null)
            {
                result = db.BlogPosts.AsQueryable();
                result = result.Where(p => p.Title.Contains(searchStr) || 
                        p.Body.Contains(searchStr) || 
                        p.Abstract.Contains(searchStr) ||
                        p.Comments.Any(c => c.Body.Contains(searchStr) || 
                                c.Author.FirstName.Contains(searchStr) || 
                                c.Author.LastName.Contains(searchStr) || 
                                c.Author.DisplayName.Contains(searchStr) || 
                                c.Author.Email.Contains(searchStr)));
            }
            else
            {
                result = db.BlogPosts.AsQueryable();
            }

            return result.OrderByDescending(p => p.Created);


        }


            public ActionResult BlogPosts()
        {
            
            return View(db.BlogPosts.ToList().OrderByDescending(b => b.Created));

        }
            
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        //httpget
        public ActionResult Contact()
        {
            email_model model = new email_model();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(email_model model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var body = "<p>Email From:<bold>{0}({1})</p><p>Message:</p><p>{2}</p>";
                    var from = "CoreBlog<anyemailher@host.com>";

                    model.Body = "this is a message from your Blog site. The name and the email of the contacting person is above."+ model.Body;
                    var email = new MailMessage(from, WebConfigurationManager.AppSettings["emailto"])
                    {
                        Subject = model.Subject,
                        Body = string.Format(body, model.FromName, model.FromEmail, model.Body),
                        IsBodyHtml = true
                    };

                    var svc = new PersonalEmail();
                    await svc.SendAsync(email);

                    return View(new email_model());
     
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await Task.FromResult(0);

                }
            }
            return View(model);

        }
    }
}