using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampusSecurity.Models
{
    public class AdvSearchViewModel
    {
        public List<String> generalList { get; set; }
        public List<int> idList { get; set; }
        //pagination
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int PagerCount { get; set; }
    }
}