using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace MovieMgr.Core.Entities
{
    public class Movie: EntityObject
    {
        [Display(Name = "Titel"), Required]
        public string Title { get; set; }
        [ForeignKey("Category_Id")]
        public Category Category { get; set; }
        [Display(Name = "Kategorie")]
        public int Category_Id { get; set; }
        [Display(Name = "Dauer [min]"), Range(1, 1000)]
        public int Duration { get; set; } //in Minuten
        [Display(Name = "Jahr"), Range(1000, 9999)]
        public int Year { get; set; }
    }
}
