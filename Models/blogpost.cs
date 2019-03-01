using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace coreblog.Models
{
    public class BlogPost
    {
        //put a data annotation below one of these lines if you want to put a max or a minimum of a length\/

            //you can also fire off custom messages


        public int Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Slug { get; set; }
        [AllowHtml]
        public string Body { get; set; }

        public string MediaURL { get; set; }
        public bool Published { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public BlogPost()
        {
            this.Comments = new HashSet<Comment>();
        }

    }
}