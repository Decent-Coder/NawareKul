using NawareKulCore.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NawareKulCore.Models
{
    public class AboutKulvruttant
    {
        [Display(Name = "AboutKulvruttantTitle", ResourceType = typeof(Resource))]
        public string Title { get; }


        [Display(Name = "About_Description", ResourceType = typeof(Resource))]
        public string Description { get; }
    }
}