using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using AdvertBase;
using AdvertBaseServer;
using System.IO;
using System.Xml;
using System.Collections;

namespace AdvertBaseServer
{
    
    public partial class Statistics : Form
    {
        private string dbname, server, dbuser, dbpass, dbPort;
        public Statistics()
        {
            InitializeComponent();
        }
        public Statistics(DateTime date)
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

        
            string CommandText = "";
            string Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;
            //Переменная Connect - это строка подключения в которой:
            //БАЗА - Имя базы в MySQL
            //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
            //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
            //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL
            try
            {
                
                    MySqlConnection myConnection = new MySqlConnection(Connect);
                    MySqlCommand myCommand;
                   // myConnection.Open(); //Устанавливаем соединение с базой данных.
                    MySqlDataReader MyDataReader;
                    for (int i = 0; i < 5; i++)
                    {
                        CommandText = "select count(*) from ria_rim.ob where kol_p='" + i.ToString() + "' and datepost='" + date.ToString("yyyy-MM-dd") + "'";
                        myCommand = new MySqlCommand(CommandText, myConnection);
                        myConnection.Open();
                        MyDataReader = myCommand.ExecuteReader();
                        Control[] controls = countByDate1.Controls.Find("count"+i.ToString()+"0",true);
                        
                        
                        while (MyDataReader.Read())
                        {
                            controls[0].Text = MyDataReader.GetString(0).ToString();
                        }
                        myConnection.Close(); //Обязательно закрываем соединение!
                        controls = countByDate1.Controls.Find("count" + i.ToString() + "1", true);
                        controls[0].Text = date.ToString("yyyy-MM-dd");
                    }

                    CommandText = "select count(*) from ria_rim.ob where datepost='" + date.ToString("yyyy-MM-dd") + "'";
                    
                    myCommand = new MySqlCommand(CommandText, myConnection);
                    myConnection.Open();
                    MyDataReader = myCommand.ExecuteReader();
                    while (MyDataReader.Read())
                    {
                        this.Text = MyDataReader.GetString(0).ToString() + " объявлений в базе за " + date.ToString("yyyy-MM-dd");
                    }
                    myConnection.Close(); //Обязательно закрываем соединение!                
   
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
           

        
        }

        private void Statistics_Load(object sender, EventArgs e)
        {

        }
    }
}
