using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Xml;
using System.IO;


namespace AdvertBase
{
    public partial class checkForm : Form
    {
        private string dbname, server, dbuser, dbpass, dbPort;
        public checkForm()
        {
            try
            {
                //Microsoft.
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
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void проверитьВсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string CommandText = "update ria_rim.ob set gram_prov = '1'";
                string Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;
                //Переменная Connect - это строка подключения в которой:
                //БАЗА - Имя базы в MySQL
                //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
                //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
                //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL
                MySqlConnection myConnection = new MySqlConnection(Connect);
                MySqlCommand myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open(); //Устанавливаем соединение с базой данных.
                
                myCommand.ExecuteNonQuery();              
                
                myConnection.Close(); //Обязательно закрываем соединение!
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void checkForm_Load(object sender, EventArgs e)
        {
            try
            {
                
                string CommandText = "select k_word, string_ob, adres, id_ob from ria_rim.ob where gram_prov = '0'";
                string Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;
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
                    
                    checkCard ck = new checkCard(MyDataReader.GetValue(0).ToString() + ", " + MyDataReader.GetValue(1).ToString() + " " + MyDataReader.GetValue(2).ToString(), MyDataReader.GetInt32(3));
                    flowLayoutPanel1.Controls.Add(ck);
                }
                MyDataReader.Close();
                myConnection.Close(); //Обязательно закрываем соединение!

                CommandText = "select count(*) from ria_rim.ob where gram_prov = '0'";
                //Переменная Connect - это строка подключения в которой:
                //БАЗА - Имя базы в MySQL
                //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
                //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
                //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL
                myConnection = new MySqlConnection(Connect);
                myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open(); //Устанавливаем соединение с базой данных.
                ;
                MyDataReader = myCommand.ExecuteReader();
                while (MyDataReader.Read())
                {
                    this.Text = "Проверка объявлений. Не проверено:" + MyDataReader.GetValue(0).ToString() + " объявлений";                    
                }
                MyDataReader.Close();
                myConnection.Close(); //Обязательно закрываем соединение!
                
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
    }
}
