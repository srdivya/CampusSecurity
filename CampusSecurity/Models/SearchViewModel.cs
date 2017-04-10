using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampusSecurity.Models
{
    public class SearchViewModel
    {
        public List<Object> generalList { get; set; }
        //pagination
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int PagerCount { get; set; }
    }
}