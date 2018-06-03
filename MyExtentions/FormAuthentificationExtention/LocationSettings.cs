using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyExtentions.FormAuthentificationExtention
{
    public class LocationSettings
    {
        public string ServerAddress { get; set; }

        public string Database { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        public void Init()
        {
            LocationSettings Settings = new LocationSettings();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = this.ServerAddress;
            builder.InitialCatalog = this.Database;
            builder.UserID = this.User;
            builder.Password = this.Password;
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {

                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error in Connection. "
                        + Environment.NewLine
                        + "Please Check the Error Message." + Environment.NewLine
                        + "Please take a screen shot and send to System Administrator" + Environment.NewLine + Environment.NewLine
                        + ex.Message, "Error Occurred");
                }
                connection.Close();
            }

        }
        
    }
}
