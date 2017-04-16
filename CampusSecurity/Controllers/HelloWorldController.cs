using CampusSecurity.Models;
using Oracle.ManagedDataAccess.Client;
using System.Web;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcMovie.Controllers
{
    public class HelloWorldController : Controller
    {
        // 
        // GET: /HelloWorld/ 
        static string connectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.cise.ufl.edu)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl)));User Id = menon; Password = Zxqw29!!;";
        OracleConnection connection = new OracleConnection(connectionString);
        public string Index()
        {
            return "This is my <b>default</b> action...";
        }

        // 
        // GET: /HelloWorld/Welcome/ 

        public ActionResult Welcome()
        {
            return View();//"This is the Welcome action method...";
        }
        [HttpGet]
        public ActionResult Search()
        {
            UIModel objUI = new UIModel();
           //LINQ query executed first time search is loaded. List of uni returned, converted to select list and added to objIU object
            objUI.University = getUniversity().Select(c => new SelectListItem
            {
                Text = c.ToString(),
                Value = c.ToString()
            }).ToList();
            List<int> lstYear = new List<int> { 2011, 2012, 2013, 2014, 2015 };
            objUI.Year = lstYear.Select(c => new SelectListItem
            {
                Text = c.ToString(),
                Value = c.ToString()
            }).ToList();
            List<String> lstLoc = new List<string> { "Non-campus", "On-campus", "Public Property", "Reported by Local Police", "Residence Hall" };
            List<String> lstType = new List<string> { "Criminal Offense", "Discipline", "Arrests","Violence Against Women", "Hate Crime" };
            objUI.Location = lstLoc.Select(c => new SelectListItem
            {
                Text = c.ToString(),
                Value = c.ToString()
            }).ToList();
            lstType.Sort();
            objUI.Type = lstType.Select(c => new SelectListItem
            {
                Text = c.ToString(),
                Value = c.ToString()
            }).ToList();
            return View(new Tuple<UIModel,SearchViewModel>(objUI,null));
        }




        [HttpGet]
        public ActionResult AdvSearch()
        {
            UIModel objUI = new UIModel();
            //LINQ query executed first time search is loaded. List of uni returned, converted to select list and added to objIU object
            objUI.University = getUniversity().Select(c => new SelectListItem
            {
                Text = c.ToString(),
                Value = c.ToString()
            }).ToList();
            List<int> lstYear = new List<int> { 2011, 2012, 2013, 2014, 2015 };
            objUI.Year = lstYear.Select(c => new SelectListItem
            {
                Text = c.ToString(),
                Value = c.ToString()
            }).ToList();
            List<String> lstLoc = new List<string> { "Non-campus", "On-campus", "Public Property", "Reported by Local Police", "Residence Hall" };
            List<String> lstType = new List<string> { "Criminal Offense", "Discipline", "Arrests", "Violence Against Women", "Hate Crime" };
            objUI.Location = lstLoc.Select(c => new SelectListItem
            {
                Text = c.ToString(),
                Value = c.ToString()
            }).ToList();
            lstType.Sort();
            objUI.Type = lstType.Select(c => new SelectListItem
            {
                Text = c.ToString(),
                Value = c.ToString()
            }).ToList();
            return View(new Tuple<UIModel, SearchViewModel>(objUI, null));
        }









        private List<string> getUniversity()
        {
            List<String> lstUni = new List<string>();
            using (connection)
            {
                connection.Open();
                string sql = "Select DISTINCT Name as University from LOCATIONYEAR order by NAME asc";
                OracleCommand cmd = new OracleCommand(sql, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lstUni.Add(reader.GetString(0));
                }
                connection.Close();
            }
            return lstUni;
        }

        public ActionResult loadGrid(String Uni, int Year, String Location, String Type, String SubType)
        {
            SearchViewModel model = new SearchViewModel();
            model.PageSize = 25;
            int columns = 0;
            String sql = "Select ";
            if (SubType.Equals("All"))
            {
                sql = sql + "*";
            }
            else
            {
                sql = sql + SubType;
            }
            int flag = 0;
            Type = (Type == "Criminal Offense") ? "Criminal_Offense" : Type;
            sql = sql + " from " + Type + " where id in (select id from locationyear where name = '" + Uni + "' AND year = " + Year + " AND location = '" + Location + "')";
            //var temp;
            //Console.WriteLine(sql);
            if (Type == "Discipline")
            {
                flag = 1;
                var temp = new System.Collections.Generic.List<Discipline>();
                model.generalList = temp.Cast<object>().ToList();

            }
            else if (Type == "Arrests")
            {
                flag = 2;
                var temp = new System.Collections.Generic.List<Arrests>();
                model.generalList = temp.Cast<object>().ToList();
            }
            else if (Type == "Criminal_Offense")
            {
                flag = 3;
                var temp = new System.Collections.Generic.List<Criminal_Offense>();
                model.generalList = temp.Cast<object>().ToList();

            }

            else if (Type == ("VAWA"))
            {
                flag = 4;
                var temp = new System.Collections.Generic.List<VAWA>();
                model.generalList = temp.Cast<object>().ToList();
            }
            //new System.Collections.Generic.List<Discipline>();
            //model.Location.Add(new LocYear(2014, "UF", "FL", "Campus"));
            model.PageSize = 25;
            List<string> tempList = new List<string>();
            int class_type = 0;
            using (connection)
            {

                connection.Open();
                //string sql = "Select NAME, STATE, YEAR, LOCATION FROM LOCATIONYEAR WHERE STATE = 'FL'";
                OracleCommand cmd = new OracleCommand(sql, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    switch (flag)
                    {
                        case 1:
                            Discipline di = new Discipline(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3));
                            tempList.Add("id"); tempList.Add("drug"); tempList.Add("weapon"); tempList.Add("liquor");
                            class_type = 1;
                            columns = 4;
                            model.generalList.Add(di);
                            break;
                        case 2:
                            Arrests ar = new Arrests(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3));
                            tempList.Add("id"); tempList.Add("drug"); tempList.Add("weapon"); tempList.Add("liquor");
                            columns = 4;
                            class_type = 2;
                            model.generalList.Add(ar);
                            break;
                        case 3:
                            Criminal_Offense co = new Criminal_Offense(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetInt32(9));
                            tempList.Add("id"); tempList.Add("burgla"); tempList.Add("murd"); tempList.Add("vehic"); tempList.Add("neg_m"); tempList.Add("robbe"); tempList.Add("forcib"); tempList.Add("nonfor"); tempList.Add("agg_a"); tempList.Add("arson");
                            columns = 10;
                            class_type = 3;
                            model.generalList.Add(co);
                            break;
                        case 4:
                            VAWA va = new VAWA(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3));
                            tempList.Add("id"); tempList.Add("stalk"); tempList.Add("dating"); tempList.Add("domest");
                            columns = 4;
                            class_type = 4;
                            model.generalList.Add(va);
                            break;
                    }
                    //;

                }
                connection.Close(); 
            }
            ViewBag.NbColumns = columns;
            ViewBag.Tlist = tempList;
            ViewBag.Class_Type = class_type;
            return View("loadGrid",new Tuple<SearchViewModel, Discipline>(model, null));
            //return Json(model);
        }
        

        [HttpPost]
        public ActionResult Search(UIModel input)//string University)
        {
            SearchViewModel model = new SearchViewModel();
            //model.Location = new List<LocYear>();
            //model.Location.Add(new LocYear(2014, "abc", "dbs","dsh"));
            //model.Location.Add(new LocYear(2014, "UF", "FL", "Campus"));
            model.PageSize = 25;
            ViewBag.model = model;
            return View(model);
        }
        //public ActionResult AdvSearch()
        //{
        //    return View();
        //}

    }
}
