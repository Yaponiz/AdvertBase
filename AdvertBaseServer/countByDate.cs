using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AdvertBaseServer
{
    public partial class countByDate : UserControl
    {
        private string dbname, server, dbuser, dbpass, dbPort;

        public countByDate()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string CommandText = "";
                string Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;

                MySqlConnection myConnection = new MySqlConnection(Connect);
                MySqlCommand myCommand;
                Control[] controls = this.Controls.Find("count01", true);
                CommandText = "update ria_rim.ob set kol_p='1' where kol_p='0' and datepost='" + controls[0].Text + "'";

                // myConnection.Open(); //Устанавливаем соединение с базой данных.
                MySqlDataReader MyDataReader;

                myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open();
                myCommand.ExecuteNonQuery();

                myConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}