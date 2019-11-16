using System;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Angular_SPA.Models
{
    public class TestPeopleModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Increment { get; set; }

        public TestPeopleModel(int id,string name, int increment)
        {
            this.Id = id;
            this.Name = name;
            this.Increment = increment;
        }

        public void UpdateIncrement()
        {
            this.Increment++;
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = sqlconnectionbuilder();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "UPDATE testpeople SET Increment ="+Increment + " WHERE idTestPeople="+Id+";";
            con.Open();
            command.ExecuteNonQuery();
            con.Close();
            //RefreshIncrement();
        }

        [Obsolete("Method is deprecated and no longer needed.")]
        public void RefreshIncrement() {
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = sqlconnectionbuilder();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT Increment FROM testpeople WHERE idTestPeople="+Id;
            con.Open();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                this.Increment = reader.GetInt32(0);
            }
            reader.Close();
            con.Close();

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
