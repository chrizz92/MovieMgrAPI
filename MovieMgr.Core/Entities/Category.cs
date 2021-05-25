using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MovieMgr.Core.Entities
{
    public class Category: EntityObject
    {
        [Display(Name = "Kategorie"), Required]
        public String CategoryName { get; set; }
        //public List<Movie> Movies{ get; set; }
    }
}
