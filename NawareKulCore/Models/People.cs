using NawareKulCore.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NawareKulCore.Models
{
    public class People
    {
        [Display(Name = "F_Name", ResourceType = typeof(Resource))]
        public string Name { get; set; }

        [Display(Name = "F_Education", ResourceType = typeof(Resource))]
        public string Education { get; set; }

        [Display(Name = "F_Work", ResourceType = typeof(Resource))]
        public string Work { get; set; }

        [Display(Name = "F_MainWork", ResourceType = typeof(Resource))]
        public string MainWork { get; set; }

        [Display(Name = "F_Informatio", ResourceType = typeof(Resource))]
        public string Information { get; set; }

        
        public string SrcPath { get; set; }

    }
}