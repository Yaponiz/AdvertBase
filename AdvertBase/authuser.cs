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
    public partial class authuser : Form
    {
        public int level = -1;
        public int userID;
        private string dbname, server, dbuser, dbpass, dbPort;
        public authuser()
        {
            InitializeComponent();
            
            FileStream f2 = new FileStream("properties.xml", FileMode.OpenOrCreate);
            XmlTextWriter settings;
            if (f2.Length < 2)
            {
                settings = new XmlTextWriter(f2, Encoding.Default);
                settings.WriteStartDocument();
                settings.WriteStartElement("server");
                addAtributeToXml(settings, "servername", server);
                addAtributeToXml(settings, "dbname", dbname);
                addAtributeToXml(settings, "dbuser", dbuser);
                addAtributeToXml(settings, "dbpass", dbpass);
                addAtributeToXml(settings, "dbport", dbPort);
                settings.WriteEndElement();
                settings.WriteEndDocument();
                settings.Close();
            }
            f2.Close();
            
                FileStream f1 = new FileStream("properties.xml", FileMode.OpenOrCreate);
                XmlTextReader settingsR = new XmlTextReader(f1);
                while (settingsR.Read())
                {
                    if (settingsR.NodeType == XmlNodeType.Element)
                    {
                        if (settingsR.Name.Equals("server"))
                        {
                            server = settingsR.GetAttribute("servername");
                            dbname = settingsR.GetAttribute("dbname");
                            dbuser = settingsR.GetAttribute("dbuser");
                            dbpass = settingsR.GetAttribute("dbpass");
                            dbPort = settingsR.GetAttribute("dbport");
                        }
                    }
                }
                settingsR.Close();
            
            f1.Close();
           
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

               
                string CommandText = "select accessLevel, idusers from ads_paper.users where `userName` ='" + login.Text + "' and `password` = '" + pass.Text+ "'";
                string Connect = "Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass+";Port="+dbPort;
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

               if (MyDataReader.Read())
                {

                    if (!MyDataReader.IsDBNull(0))
                    {
                                      
                        level = MyDataReader.GetInt32(0); //Получаем строку  
                        userID = MyDataReader.GetInt32(1);
                    }
                    else
                    {
                        level = -1;                       
                    }

                }
                MyDataReader.Close();
                myConnection.Close(); //Обязательно закрываем соединение!     

                if (level != -1)
                {                    
                    this.Hide();
                    mainForm f = new mainForm();
                    f.level = level;
                    f.Show();
                    
                }
                else
                {
                    MessageBox.Show("Неверное введены имя пользователя или пароль");
                    Application.Exit();
                }
            }
            catch (MySqlException ex)
            {

                MessageBox.Show(ex.ErrorCode.ToString() + "  " + ex.Message);
                this.Hide();
                mainForm f = new mainForm();
                f.level = 0;
                f.userID = userID;
                f.Show();
            }
        }

        private void pass_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }

        public void addAtributeToXml(XmlTextWriter t, string name, string text)
        {
            if (text == null)
            {
                text = "";
            }
            t.WriteStartAttribute(name);
            t.WriteString(text);
            t.WriteEndAttribute();
        }
    }
}
