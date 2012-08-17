using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using System.Xml;
 

namespace DatabaseMover
{
    public partial class Form1 : Form
    {

        private string dbname, server, dbuser, dbpass;
        //private string count;
        public Form1()
        {
            InitializeComponent();
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dataGridView1.AutoResizeRows();
            dataGridView1.AutoResizeColumns();
            try
            {
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
                        }
                    }
                }
                f.Close();
                string CommandText = "select * from ria_rim.ob where KOD_R =1 and KOL_P >0 and (STRING_OB like '%%р.%%' or K_WORD like '%%р.%%') and cost<1 limit 500";
                string Connect = "server=" + server + ";Database=ria_rim;User Id=" + dbuser + ";Password=" + dbpass;
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
                string id;
                string head;
                string date;
                string text="";
                string cost="";
                while (MyDataReader.Read())
                {
                    id = MyDataReader.GetValue(0).ToString();
                    head = MyDataReader.GetValue(12).ToString();
                    date = MyDataReader.GetValue(2).ToString();
                    if(!MyDataReader.IsDBNull(13))
                    {
                        text = MyDataReader.GetValue(13).ToString();
                    }

                    
                        cost = MyDataReader.GetValue(22).ToString();
                   
                    dataGridView1.Rows.Add(date,head, text, cost,0,id);
                }
                MyDataReader.Close();
                myConnection.Close(); //Обязательно закрываем соединение!
            }
            catch (MySqlException except)
            {
                MessageBox.Show(except.Number.ToString());
                MessageBox.Show(except.Message);
            }

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {

                string CommandText = "";
                string Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass;
                //Переменная Connect - это строка подключения в которой:
                //БАЗА - Имя базы в MySQL
                //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
                //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
                //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL
                MySqlConnection myConnection = new MySqlConnection(Connect);
                MySqlCommand myCommand = new MySqlCommand(CommandText, myConnection);

                myConnection = new MySqlConnection(Connect);
                myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open(); //Устанавливаем соединение с базой данных.
                string id="";
                string head="";
                string text = "";
                string cost = "";
                string check = "0";
                int count = 0;
                count = dataGridView1.Rows.Count-1;
                DataGridViewRow row;
                for (int i = 0; i < count; i++)
                
                {
                    row = dataGridView1.Rows[i];
                    //count=row.Cells[5].Value.ToString();
                    if ((row.Cells[3].Value.ToString() != "") && (row.Cells[3].Value.ToString() != null))
                    {
                        check = "0";
                        //if (row.Cells[3].Value.ToString() != "0")
                        //{
                            check = "1";
                            cost =  AddSlashes(row.Cells[3].Value.ToString());
                            if (row.Cells[2].Value.ToString() != "" && (row.Cells[2].Value.ToString() != null))
                            {
                                text = AddSlashes(row.Cells[2].Value.ToString());
                            }
                            else
                            {
                                text = "";
                            }
                            if (row.Cells[5].Value.ToString() != "" && (row.Cells[5].Value.ToString() != null))
                            {
                                id = AddSlashes(row.Cells[5].Value.ToString());

                            }
                            else
                            {
                                id = "";
                            }
                            if (row.Cells[1].Value.ToString() != "" && (row.Cells[1].Value.ToString() != null))
                            {
                                head = row.Cells[1].Value.ToString();
                            
                                //cost = row.Cells[0].Value.ToString();
                                head =  AddSlashes(head.TrimEnd(' ','-'));
                            }
                            else
                            {
                                head = "";
                            }
                            CommandText = "`K_WORD`='" + head + "', `STRING_OB` = '" + text + "', `costStr` = '" + cost + "' where `ID_OB` = '" + id + "'";
                            //myCommand.CommandText = CommandText;
                            //myCommand.ExecuteNonQuery();
                            updateQuerry(myCommand, CommandText);

                        //}

                    }
                }
                myConnection.Close(); //Обязательно закрываем соединение! 
                loaddata(numericUpDown1.Value);
            
                        }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
                //MessageBox.Show(except.Data.);

            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            try
            {
                string CommandText = "update ria_rim.ob set `STRING_OB` = '" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "', `cost` = '" + dataGridView1.CurrentRow.Cells[2].Value.ToString() + "', `checked` = '1'  where `id_ob` = '" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
                string Connect = "Database=ria_rim.ob" + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass;
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
                loaddata(numericUpDown1.Value);
                //dataGridView1.CurrentRow.
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);

            }
        }

        private void loaddata(decimal offset)
        {
            try
            {

                dataGridView1.Rows.Clear();
                string CommandText = "select * from ria_rim.ob where KOD_R =1 and KOL_P >0 and (STRING_OB like '%%р.%%' or K_WORD like '%%р.%%') and cost<1 limit " + (offset * 500).ToString() + ", 500";
                string Connect = "server=" + server + ";Database=ria_rim;User Id=" + dbuser + ";Password=" + dbpass;
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
                string id;
                string head;
                string date;
                string text = "";
                string cost = "";
                while (MyDataReader.Read())
                {
                    id = MyDataReader.GetValue(0).ToString();
                    head = MyDataReader.GetValue(12).ToString();
                    date = MyDataReader.GetValue(2).ToString();
                    if (!MyDataReader.IsDBNull(13))
                    {
                        text = MyDataReader.GetValue(13).ToString();
                    }

                    //if (!MyDataReader.IsDBNull(20))
                    //{
                        cost = MyDataReader.GetValue(22).ToString();
                    //}
                    dataGridView1.Rows.Add(date, head, text, cost, 0, id);
                }
                MyDataReader.Close();
                myConnection.Close(); //Обязательно закрываем соединение!
            }
            catch (MySqlException except)
            {
                MessageBox.Show(except.Number.ToString());
                MessageBox.Show(except.Message);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            loaddata(numericUpDown1.Value);
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.BeginEdit(false);
        }
        private void updateQuerry(MySqlCommand myCommand, string setItems)
        {
            try
            {
                string CommandText = "update ria_rim.ob set " + setItems;
                myCommand.CommandText = CommandText;
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException except)
            {
                MessageBox.Show(except.Message + setItems);

            }
        }
        /// <summary>

        /// Returns a string with backslashes before characters that need to be quoted

        /// </summary>

        /// <param name="InputTxt">Text string need to be escape with slashes</param>

        public string AddSlashes(string InputTxt)
        {

            // List of characters handled:

            // \000 null

            // \010 backspace

            // \011 horizontal tab

            // \012 new line

            // \015 carriage return

            // \032 substitute

            // \042 double quote

            // \047 single quote

            // \134 backslash

            // \140 grave accent

            string Result = InputTxt;



            try
            {

                Result = System.Text.RegularExpressions.Regex.Replace(InputTxt, @"[\000\010\011\012\015\032\042\047\134\140]", "\\$0");

            }

            catch (Exception Ex)
            {

                // handle any exception here

                Console.WriteLine(Ex.Message);

            }

            return Result;

        }

    }
}
