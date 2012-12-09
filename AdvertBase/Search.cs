using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using MySql.Data.MySqlClient;

namespace AdvertBase
{
    public partial class Search : Form
    {
        public int level;
        public int userID;
        private string dbname, server, dbuser, dbpass, dbPort;

        public Search()
        {
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

        private void button1_Click(object sender, EventArgs e)
        {
            string CommandText = "select * from ria_rim.ob where ADRES like '%%" + searchstringPhone.Text + "%%'";
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
                MySqlDataReader MyDataReader;
                MyDataReader = myCommand.ExecuteReader();
                SearcResults f = new SearcResults();
                while (MyDataReader.Read())
                {
                    f.cardList.Rows.Add(MyDataReader.GetString(0), MyDataReader.GetString(2), MyDataReader.GetString(12), MyDataReader.GetString(13), MyDataReader.GetString(14), MyDataReader.GetString(15), MyDataReader.GetString(1));
                }
                f.Show();

                MyDataReader.Close();
                myConnection.Close(); //Обязательно закрываем соединение!
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ErrorCode.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string CommandText = "select * from ria_rim.ob where K_WORD like '%%" + searchstringText + "%%' or STRING_OB like '%%" + searchstringText.Text + "%%'";
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
                MySqlDataReader MyDataReader;
                MyDataReader = myCommand.ExecuteReader();
                SearcResults f = new SearcResults();
                while (MyDataReader.Read())
                {
                    f.cardList.Rows.Add(MyDataReader.GetString(0), MyDataReader.GetString(2), MyDataReader.GetString(12), MyDataReader.GetString(13), MyDataReader.GetString(14), MyDataReader.GetString(15), MyDataReader.GetString(1));
                }
                f.Show();
                MyDataReader.Close();
                myConnection.Close(); //Обязательно закрываем соединение!
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }
    }
}