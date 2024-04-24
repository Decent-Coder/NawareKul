using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NawareKulCore.Models
{
    public class AncestorInfoDTO
    {
        public int FatherId { get; set; }
        public String FatherName { get; set; }
        public int GrandFatherId { get; set; }
        public string GrandFatherName { get; set; }
        public int SuperGrandFatherId { get; set; }
        public string SuperGrandFatherName { get; set; }
    }
}