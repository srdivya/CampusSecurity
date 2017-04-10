using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CampusSecurity.Models
{
    public class UIModel
    {
        public List<SelectListItem> University { get; set; }
        public List<SelectListItem> Location { get; set; }
        public List<SelectListItem> Year { get; set; }
        public List<SelectListItem> Type { get; set; }
        public List<SelectListItem> SubType { get; set; }
    }
}