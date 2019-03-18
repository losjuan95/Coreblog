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
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        [MaxLength(30), MinLength(5)]
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Slug { get; set; }

        [MaxLength(2500), MinLength(1)]
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