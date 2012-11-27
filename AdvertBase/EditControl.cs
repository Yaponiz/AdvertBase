using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using AdvertBase;
using System.IO;
using System.Xml;
using System.Collections;

namespace AdvertBase
{
    public partial class EditControl : UserControl
    {
        public int level;
        public int userID;
        public int selected = 0;
        public int lastSelected = 0;
        private string dbname, server, dbuser, dbpass, dbPort;
        public EditControl(string id)
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
            string CommandText = "select * from ria_rim.ob where ID_OB='"+id+"'";
            string Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port="+dbPort;
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
               
                MyDataReader.Read();
               
                    //KOD_R-KOD_PPPR
                    
                    kod_r.Text = MyDataReader.GetInt32(4).ToString();
                    
                    kod_pr.Text = MyDataReader.GetInt32(5).ToString();
                    
                    kod_ppr.Text = MyDataReader.GetInt32(6).ToString();
                    
                    kod_pppr.Text = MyDataReader.GetInt32(7).ToString();
                    //KOD_R-KOD_PPPR

                    
                    k_word.Text = MyDataReader.GetString(12);
                    
                    string_ob.Text = MyDataReader.GetString(13);
                    
                    phone.Text = MyDataReader.GetString(14);
                    
                    secPhone.Text = MyDataReader.GetString(15);
                    cost.Text = MyDataReader.GetString(20);
                    
                    kol_p.Value = MyDataReader.GetDecimal(16);
                    //richTextBox9.Text = MyDataReader.GetString(14);
                
                //f.Show();
                MyDataReader.Close();
                myConnection.Close(); //Обязательно закрываем соединение!
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }

        public EditControl()
        {
            InitializeComponent();
        }

        private void cardPanel_Enter(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            selected = 1;
        }

        private void cardPanel_Leave(object sender, EventArgs e)
        {
            this.BackColor = Color.DarkGray;
            selected = 0;
        }

        private void EditControl_Enter(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            selected = 1;
        }

        private void EditControl_Leave(object sender, EventArgs e)
        {
            this.BackColor = Color.DarkGray;
            selected = 0;
        }

        private void secPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void kod_r_TextChanged(object sender, EventArgs e)
        {
            // todo: брать из базы данных
            if (kod_r.Text == "2")
            {
                automobilesList.Show();
            }
            else
            {
                automobilesList.Hide();
            }
        }

        private void automobilesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            k_word.Text = automobilesList.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
