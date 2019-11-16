using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;


namespace Updating_with_Database.Pages
{
    [IgnoreAntiforgeryToken]
    public class TableViewModel : PageModel
    {
        public Collection<Tuple<int, string, int>> OutputList { get; private set; } = new Collection<Tuple<int, string, int>>();
        public string Check { get; private set; } = "";

        public void OnGet()
        {
            OutputList = Sqlretriever();
        }
        public class MethodData
        {
            public string id { get; set; }
            public string value { get; set; }

        }
       
        
        public ActionResult OnPostIncrement(MethodData stuff)
        {
            /**
             * Currently increments value from webpage on id from webpage
             * Should implement some sort of check for a server update to check if the id matches one that exists + value hasn't changed since webpage opened.
             */
            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = sqlconnectionbuilder();
            int newval = int.Parse(stuff.value);
            try
            {
                connection.Open();
                Check = "Lord Help me";
                newval++;
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE testpeople SET Increment = " + newval + " WHERE idTestPeople = " + stuff.id + ";";

                command.ExecuteNonQuery();
                connection.Close();
            }
            catch(Exception E)
            {
                Check = E.ToString();
            }
            return new JsonResult("Id:"+stuff.id + " Set to new value:"+newval);
        }

        


        private Collection<Tuple<int, string, int>> Sqlretriever()
        {

            Collection<Tuple<int, string, int>> tempoutput = new Collection<Tuple<int, string, int>>();

            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = sqlconnectionbuilder();

            try
            {
                connection.Open();
                Check = "Success";
                MySqlCommand command = new MySqlCommand("SELECT idTestPeople,Name,Increment FROM testpeople;", connection);
                MySqlDataReader reader;

                reader = command.ExecuteReader();
                while (reader.Read())

                {
                    Tuple<int, String, int> temptuple = Tuple.Create(int.Parse(reader.GetValue(0).ToString()),
                            reader.GetValue(1).ToString(),
                            int.Parse(reader.GetValue(2).ToString()));
                    tempoutput.Add(temptuple);
                }

                connection.Close();
            }
            catch (Exception E)
            {
                Check = E.ToString();
            }

            return tempoutput;
        }

        private string sqlconnectionbuilder()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "localhost";
            builder.UserID = "Me";
            builder.Password = "";
            builder.InitialCatalog = "testing_1pagewebsite";

            return builder.ConnectionString;
        }



    }
}