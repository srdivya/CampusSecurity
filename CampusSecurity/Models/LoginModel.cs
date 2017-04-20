using Oracle.ManagedDataAccess.Client;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace CampusSecurity.Models
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        
        /// <summary>
        /// Checks if user with given password exists in the database
        /// </summary>
        /// <param name="_username">User name</param>
        /// <param name="_password">User password</param>
        /// <returns>True if user exist and password is correct</returns>
        public bool IsValid(string _username, string _password)
        {

            string connectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.cise.ufl.edu)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl)));User Id = menon; Password = Zxqw29!!;";
            OracleConnection connection = new OracleConnection(connectionString);
            string sql = "Select * from login";// where username = '"+model.username +"' and password = '"+model.password+"'";
            connection.Open();
            int RowCount;
            String user, pass;
            OracleCommand cmd = new OracleCommand(sql, connection);
            cmd.CommandType = System.Data.CommandType.Text;
            OracleDataAdapter orada = new OracleDataAdapter(cmd.CommandText, connection);
            DataTable dt = new DataTable();
            orada.Fill(dt);
            RowCount = dt.Rows.Count;
            for (int i = 0; i < RowCount; i++)
            {
                user = dt.Rows[i]["UserName"].ToString();
                pass = dt.Rows[i]["Password"].ToString();
                if (user == _username && pass == _password)
                {
                    return true;
                    //return RedirectToAction("Reporting",new Tuple<UIModel, SearchViewModel>(null,null));
                }
                else
                    return false;
            }
            return false;
        }
    }
}