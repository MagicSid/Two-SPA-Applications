using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Angular_SPA.Models
{
    public class TestPeopleCollection
    {
        Collection<TestPeopleModel> TestpeopleModels { get; set; }

        public Collection<TestPeopleModel> GetPeople()
        {
            return TestpeopleModels;
        }

        public TestPeopleCollection()
        {
            Collection<TestPeopleModel> testpeoples = new Collection<TestPeopleModel>();
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = Sqlconnectionbuilder();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT * FROM testpeople";
            try
            {
                con.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    TestPeopleModel tempperson = new TestPeopleModel(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                    testpeoples.Add(tempperson);
                }
                reader.Close();
                con.Close();
                
            }
            catch (Exception E)
            {
                Debug.WriteLine(E.Message);
                
            }
            this.TestpeopleModels = testpeoples;
        }

        private string Sqlconnectionbuilder()
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
