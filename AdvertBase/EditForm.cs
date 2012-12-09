using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using MySql.Data.MySqlClient;

namespace AdvertBase
{
    public partial class EditForm : Form
    {
        public string id;
        public int level;
        public int userID;
        private string dbname, server, dbuser, dbpass, dbPort;

        public EditForm(string idS)
        {
            id = idS;
            InitializeComponent();
            FileStream f = new FileStream("properties.xml", FileMode.OpenOrCreate);

            XmlTextReader settings = new XmlTextReader(f);
            while (settings.Read())
            {
                if (settings.NodeType == XmlNodeType.Element)
                {
                    if (settings.Name.Equals("server"))
                    {
                        server = settings.GetAttribute("servername");
                        dbname = settings.GetAttribute("dbname");
                        dbuser = settings.GetAttribute("dbuser");
                        dbpass = settings.GetAttribute("dbpass");
                        dbPort = settings.GetAttribute("dbport");
                    }
                }
            }
            f.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //int count = cardList.Rows.Count;
            DateTime now = DateTime.Now;

            //EditControl[] cont = this.EditPanel

            string CommandText = "update ria_rim.ob set DATEPOST='" + DateTime.Now.ToString("yyyy-MM-dd") + "', K_WORD='" + EditPanel.k_word.Text + "', STRING_OB='" + EditPanel.string_ob.Text + "', ADRES= '" + EditPanel.phone.Text + "', TELEPHON='" + EditPanel.comment.Text + "', cost='" + EditPanel.cost.Text + "', costStr='', KOL_P='" + EditPanel.kol_p.Text + "', KOD_R='" + EditPanel.kod_r.Text + "',KOD_PR='" + EditPanel.kod_pr.Text + "',KOD_PPR='" + EditPanel.kod_ppr.Text + "',KOD_PPPR='" + EditPanel.kod_pppr.Text + "' where ID_OB = '" + id + "'";

            //string CommandText = "";
            string Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;

            //Переменная Connect - это строка подключения в которой:
            //БАЗА - Имя базы в MySQL
            //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
            //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
            //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL
            try
            {
                MySqlConnection myConnection = new MySqlConnection(Connect);
                MySqlCommand myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open(); //Устанавливаем соединение с базой данных.
                myCommand.ExecuteNonQuery();

                myConnection.Close(); //Обязательно закрываем соединение!
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //int count = cardList.Rows.Count;
            DateTime now = DateTime.Now;

            string CommandText = "delete from ria_rim.ob where ID_OB='" + id + "'";
            string Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;

            //Переменная Connect - это строка подключения в которой:
            //БАЗА - Имя базы в MySQL
            //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
            //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
            //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL
            try
            {
                MySqlConnection myConnection = new MySqlConnection(Connect);
                MySqlCommand myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open(); //Устанавливаем соединение с базой данных.
                myCommand.ExecuteNonQuery();

                myConnection.Close(); //Обязательно закрываем соединение!
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}