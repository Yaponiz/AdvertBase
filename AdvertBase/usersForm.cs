using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AdvertBase;
using MySql.Data.MySqlClient;
using System.IO;
using System.Xml;

namespace AdvertBase
{
    public partial class usersForm : Form
    {
        private string dbname, server, dbuser, dbpass, dbPort;
        public usersForm()
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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            string CommandText = "drop table `ads_paper`.`users`";
            string Connect = "Database=" + "ads_paper" + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port="+dbPort;
            //Переменная Connect - это строка подключения в которой:
            //БАЗА - Имя базы в MySQL
            //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
            //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
            //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL


            MySqlConnection myConnection = new MySqlConnection(Connect);
            MySqlCommand myCommand = new MySqlCommand(CommandText, myConnection);
            myConnection.Open(); //Устанавливаем соединение с базой данных.



            myCommand.ExecuteNonQuery();
            myConnection.Close();

            CommandText = "CREATE  TABLE IF NOT EXISTS `ads_paper`.`users` ("+
  "`idusers` INT UNSIGNED NOT NULL AUTO_INCREMENT ,"+
  "`userName` VARCHAR(45) NOT NULL ,"+
  "`password` VARCHAR(45) NOT NULL ,"+
  "`accessLevel` INT UNSIGNED NOT NULL ,"+
  "PRIMARY KEY (`idusers`) ,"+
  "UNIQUE INDEX `userName_UNIQUE` (`userName` ASC) )"+
"ENGINE = InnoDB, "+
"COMMENT = 'Список пользователей, паролей и прав доступа' ";
            Connect = "Database=ads_paper;Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port="+dbPort;
            //Переменная Connect - это строка подключения в которой:
            //БАЗА - Имя базы в MySQL
            //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
            //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
            //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL


            myConnection = new MySqlConnection(Connect);
            myCommand = new MySqlCommand(CommandText, myConnection);
            myConnection.Open(); //Устанавливаем соединение с базой данных.
            myCommand.ExecuteNonQuery();
            myConnection.Close();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    CommandText = "insert into ads_paper.users (userName, password, accessLevel) values ('" + row.Cells[1].Value.ToString() + "', '" + row.Cells[2].Value.ToString() + "', '" + row.Cells[3].Value.ToString() + "')";
                    Connect = "Database=ads_paper;Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port="+dbPort;
                    //Переменная Connect - это строка подключения в которой:
                    //БАЗА - Имя базы в MySQL
                    //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
                    //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
                    //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL


                    myConnection = new MySqlConnection(Connect);
                    myCommand = new MySqlCommand(CommandText, myConnection);
                    myConnection.Open(); //Устанавливаем соединение с базой данных.



                    myCommand.ExecuteNonQuery();



                    myConnection.Close(); //Обязательно закрываем соединение!  
                }
            }
        }

        private void usersForm_Load(object sender, EventArgs e)
        {
            string CommandText = "select * from ads_paper.users";
            string Connect = "Database=" + "ads_paper" + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port="+dbPort;
            //Переменная Connect - это строка подключения в которой:
            //БАЗА - Имя базы в MySQL
            //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
            //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
            //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL


            MySqlConnection myConnection = new MySqlConnection(Connect);
            MySqlCommand myCommand = new MySqlCommand(CommandText, myConnection);
            myConnection.Open(); //Устанавливаем соединение с базой данных.
            MySqlDataReader MyDataReader;


            MyDataReader = myCommand.ExecuteReader();

            while (MyDataReader.Read())
            {

                string id = MyDataReader.GetString(0); //Получаем строку
                string name = MyDataReader.GetString(1); //Получаем строку
                string pass = MyDataReader.GetString(2); //Получаем строку
                string access = MyDataReader.GetString(3); //Получаем строку

                dataGridView1.Rows.Add(id,name,pass,access);
            }
            MyDataReader.Close();
            myConnection.Close(); //Обязательно закрываем соединение!     
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
        }
    }
}
