﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Microsoft.Office.Interop.Word;
using AdvertBase;
using System.IO;
using System.Xml;
using System.Globalization;

namespace AdvertBaseServer
{
    public partial class ExportForm : Form
    {
        public object Missing;
        public int col=0;
        public int rubNum = 0;
        private string dbname, server, dbuser, dbpass, dbPort;
        public string idRub;
        public ExportForm()
        {
            InitializeComponent();
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
                            dbPort = settings.GetAttribute("dbport");
                        }
                    }
                }
                f.Close();


                updateDateTable();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void updateDateTable()
        {
            string CommandText = "select * from ads_paper.cataloglist order by idcatalogList";
            string Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;
            //Переменная Connect - это строка подключения в которой:
            //БАЗА - Имя базы в MySQL
            //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
            //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
            //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL

            MySqlConnection myConnection2 = new MySqlConnection(Connect);
            MySqlCommand myCommand2 = new MySqlCommand(CommandText, myConnection2);
            myConnection2.Open(); //Устанавливаем соединение с базой данных.
            MySqlConnection myConnection3 = new MySqlConnection(Connect);
            MySqlCommand myCommand3 = new MySqlCommand(CommandText, myConnection3);
            myConnection3.Open(); //Устанавливаем соединение с базой данных.
            MySqlConnection myConnection = new MySqlConnection(Connect);
            MySqlCommand myCommand = new MySqlCommand(CommandText, myConnection);
            myConnection.Open(); //Устанавливаем соединение с базой данных.
            MySqlDataReader MyDataReader;
            MySqlDataReader MyDataReader2;
            MySqlDataReader MyDataReader3;


            MyDataReader = myCommand.ExecuteReader();
            int i = 0;
            while (MyDataReader.Read())
            {

                int id = MyDataReader.GetInt32(0); //Получаем строку
                string name = MyDataReader.GetString(1); //Получаем строку

                catalogsComboBox.Items.Insert(i, name);
                i++;

            }
            MyDataReader.Close();
            myConnection.Close(); //Обязательно закрываем соединение!

            CommandText = "select distinct DATEPOST from ria_rim.ob";
            myCommand.CommandText = CommandText;

            myConnection.Open(); //Устанавливаем соединение с базой данных.

            MyDataReader = myCommand.ExecuteReader();
            DateTime date;
            while (MyDataReader.Read())
            {
                date = MyDataReader.GetDateTime(0);
                CommandText = "select distinct KOL_P from ria_rim.ob where DATEPOST = '" + date.ToString("yyyy-MM-dd") + "'";
                myCommand2.CommandText = CommandText;
                MyDataReader2 = myCommand2.ExecuteReader();
                while (MyDataReader2.Read())
                {
                    CommandText = "select count(*) from ria_rim.ob where DATEPOST = '" + date.ToString("yyyy-MM-dd") + "' and KOL_P ='" + MyDataReader2.GetString(0) + "'";
                    myCommand3.CommandText = CommandText;
                    MyDataReader3 = myCommand3.ExecuteReader();
                    while (MyDataReader3.Read())
                    {
                        dataGridView1.Rows.Add(date, MyDataReader3.GetString(0), MyDataReader2.GetString(0));
                    }
                    MyDataReader3.Close();
                }
                MyDataReader2.Close();
            }
            MyDataReader.Close();
            myConnection.Close(); //Обязательно закрываем соединение!
            myConnection2.Close();
            myConnection3.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool checkCount(int[] catalogNum)
        {
            try
            {
                string CommandText = "select * from " + dbname + ".ob where `KOD_R` = '" + catalogNum[1] + "' AND `KOL_P` > '0' " ;
                if (catalogNum[2] != 0)
                {
                    CommandText = CommandText + " AND `KOD_PR` = '" + catalogNum[2]+"'";
                }
                
                if (catalogNum[3] != 0)
                {
                    CommandText = CommandText + " and `KOD_PPR` = '" + catalogNum[3] + "'";
                }

                if (catalogNum[4] != 0)
                {
                    CommandText = CommandText + " AND `KOD_PPPR` = '" + catalogNum[4] + "'";
                }

                string Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port="+dbPort;
                bool result = false;
                MySqlConnection myConnection = new MySqlConnection(Connect);
                MySqlCommand myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open(); //Устанавливаем соединение с базой данных.
                MySqlDataReader MyDataReader;


                MyDataReader = myCommand.ExecuteReader();
                if (MyDataReader.Read())
                {
                    result = true;
                }
                MyDataReader.Close();
                myConnection.Close();

                return result;

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
               /* if (catalogsComboBox.SelectedIndex == 1)
                 {*/
                col = 0;
                if (DialogResult.OK == saveFileDialog1.ShowDialog())
                {
                    Microsoft.Office.Interop.Word.Application wdApp = new Microsoft.Office.Interop.Word.Application();
                    Microsoft.Office.Interop.Word.Document wdDoc = new Microsoft.Office.Interop.Word.Document();
                    wdDoc = wdApp.Documents.Add();
                                        
                    wdDoc.SpellingChecked = false;

                    wdDoc.ShowSpellingErrors = false;
                    wdApp.Selection.Font.Name = "Times New Roman";
                    wdApp.Selection.Font.Size = 12;
                    wdApp.ActiveDocument.Select();

                    string CommandText = "select * from `ads_paper`.`" + catalogsComboBox.Text + "` order by r1,r2,r3,r4";
                    string Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;

                    MySqlConnection myConnection = new MySqlConnection(Connect);
                    MySqlCommand myCommand = new MySqlCommand(CommandText, myConnection);
                    myConnection.Open(); //Устанавливаем соединение с базой данных.
                    MySqlDataReader MyDataReader;

                    bool state = false;
                    MyDataReader = myCommand.ExecuteReader();

                    int[] catID = new int[5];
                    int parentID;
                    int real;
                    int sum;

                    while (MyDataReader.Read())
                    {
                        catID[0] = MyDataReader.GetInt32(0);
                        catID[1] = MyDataReader.GetInt32(2);
                        catID[2] = MyDataReader.GetInt32(3);
                        catID[3] = MyDataReader.GetInt32(4);
                        catID[4] = MyDataReader.GetInt32(5);
                        string name = MyDataReader.GetString(1); //Получаем строку
                        parentID = MyDataReader.GetInt32(6);
                        real = MyDataReader.GetInt32(7);
                        sum = MyDataReader.GetInt32(8);
                        if (checkCount(catID))
                        {
                            wdApp.Selection.TypeParagraph();
                            if (catID[2] == 0)
                            {
                                wdApp.Selection.TypeText("@@" + catID[1].ToString());
                                wdApp.Selection.TypeParagraph();
                                wdApp.Selection.TypeParagraph();
                                wdApp.Selection.TypeText(name);
                                wdApp.Selection.TypeParagraph();
                                wdApp.Selection.TypeParagraph();

                            }
                            else if (catID[3] == 0)
                            {
                                if (catID[1] == 1 || catID[1] == 4 || catID[1] == 5 || catID[1] == 11 || catID[1] == 18)
                                {
                                    wdApp.Selection.TypeText("-" + catID[1].ToString() + "." + catID[2].ToString() + "-");
                                    wdApp.Selection.TypeParagraph();
                                }
                                else
                                {
                                    wdApp.Selection.TypeText(catID[1].ToString() + "." + catID[2].ToString());
                                }

                                wdApp.Selection.TypeText(" " + name);
                                wdApp.Selection.TypeParagraph();
                                state = true;

                            }
                            else
                            {

                                wdApp.Selection.TypeText(name);
                                wdApp.Selection.TypeParagraph();
                                wdApp.Selection.TypeParagraph();
                                state = false;
                            }

                            PrintAdsOB(catID, ref wdApp);

                        }
                    }
                    MyDataReader.Close();
                    myConnection.Close();
                    ReplaceTextWord(ref wdApp, "  ", " ");
                    wdApp.ActiveDocument.SaveAs(saveFileDialog1.FileName+".doc");
                    wdDoc.Close();
                    //wdApp.Documents.Close();
                    wdApp.Quit();
                    MessageBox.Show("Выгрузка завершена, выгружено: " + col.ToString() + " объявлений");
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }



        private void PrintAdsRIO(int[] catalogNum, ref Microsoft.Office.Interop.Word.Application wdApp)
        {
            try
            {
                wdApp.Selection.TypeText("@@ 1");
                wdApp.Selection.TypeParagraph();wdApp.Selection.TypeParagraph();
                wdApp.Selection.TypeText("НЕДВИЖИМОСТЬ");
    wdApp.Selection.TypeParagraph();wdApp.Selection.TypeParagraph();
 wdApp.Selection.TypeText("ПРОДАЮ");
    wdApp.Selection.TypeParagraph();wdApp.Selection.TypeParagraph();
 wdApp.Selection.TypeText("ВНЕ РСО-АЛАНИЯ");
    wdApp.Selection.TypeParagraph();wdApp.Selection.TypeParagraph();
                string CommandText = "select * from " + dbname + ".ob where `KOD_R` = '1' AND `KOD_PR` = '1' and `KOD_PPR` = '1' AND `KOL_P` > '0' ORDER BY K_WORD";
            string Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port="+dbPort;

            //Переменная Connect - это строка подключения в которой:
            //БАЗА - Имя базы в MySQL
            //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
            //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
            //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL
            
                MySqlConnection myConnection = new MySqlConnection(Connect);
                MySqlCommand myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open(); //Устанавливаем соединение с базой данных.

                MySqlConnection myConnection2 = new MySqlConnection(Connect);
                MySqlCommand updateCommand = new MySqlCommand(CommandText, myConnection2);
                myConnection2.Open();
                // Gets a NumberFormatInfo associated with the en-US culture.
                NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

                nfi.NumberDecimalDigits = 0;
                nfi.NumberGroupSeparator = " ";

                nfi.PositiveSign = "";
                MySqlDataReader MyDataReader;
                //string date; //Получаем дату
                string phone; //Получаем строку
                string header; //Получаем строку
                string text; //Получаем строку
                int cost; //Получаем строку
                //string catalog; //Получаем строку
                //int toshow; //Получаем строку
                string id;
                string costStr;
                MyDataReader = myCommand.ExecuteReader();

                while (MyDataReader.Read())
                {

                    header = MyDataReader.GetString(12); //Получаем строку
                    text = MyDataReader.GetString(13); //Получаем строку                    
                    phone = MyDataReader.GetString(14); //Получаем строку
                    id = MyDataReader.GetString(0); //Получаем id
                    if (!MyDataReader.IsDBNull(20))
                    {
                        cost = MyDataReader.GetInt32(20); //Получаем строку
                    }
                    else
                    {
                        cost = 0;
                    }

                    if (!MyDataReader.IsDBNull(21))
                    {
                        costStr = MyDataReader.GetString(22);//Получаем строку
                    }
                    else
                    { 
                        costStr="";
                    }
                    //toshow = MyDataReader.GetUInt16(9); //Получаем строку
                    //catalog = MyDataReader.GetString(11); //Получаем строку


                        wdApp.Selection.ClearFormatting();
                        wdApp.Selection.Select();
                        wdApp.Selection.Characters.Last.Select();
                        wdApp.Selection.Font.Bold = 1;
                        wdApp.Selection.TypeText(header);
                        if (cost != 0)
                        {
                            wdApp.Selection.TypeText(" "+cost.ToString("N", nfi));
                            wdApp.Selection.TypeText(" ");
                            wdApp.Selection.TypeText(" руб." + costStr);
                        }
                        wdApp.Selection.TypeText(" ");
                        wdApp.Selection.Font.Bold = 0;
                        wdApp.Selection.ClearFormatting();
                        wdApp.Selection.Select();
                        wdApp.Selection.Characters.Last.Select();
                        wdApp.Selection.Font.Bold = 0;
                        wdApp.Selection.TypeText("- " + text + " ");                        
                        wdApp.Selection.TypeText(" " + phone);
                        wdApp.Selection.Font.Bold = 0;
                        wdApp.Selection.TypeParagraph();
                        //wdApp.Selection.TypeParagraph();
                        CommandText = "UPDATE `" + dbname + "`.`ob` SET `secondRub`='" + idRub + "' WHERE `ID_OB`='" + id + "'";
                        //MessageBox.Show(CommandText);
                        updateCommand.CommandText = CommandText;
                        int res = updateCommand.ExecuteNonQuery();
                        col++;
                        if (checkBox2.Checked)
                        {
                            //CommandText = "UPDATE `ads_paper`.`cards` SET `toshow`=" + (toshow - 1).ToString() + " WHERE `date`='" + date + "'";                        
                            //MessageBox.Show(CommandText);

                            //MySqlCommand updateCommand = new MySqlCommand(CommandText, myConnection2);
                            //int res = updateCommand.ExecuteNonQuery();
                        }
                   
                }
                
                MyDataReader.Close();
                myConnection2.Close();
                myConnection.Close(); //Обязательно закрываем соединение!
                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                wdApp.Selection.TypeText("ПО РЕСПУБЛИКЕ");
                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
               
                CommandText = "select * from " + dbname + ".ob where `KOD_R` = '1' AND `KOD_PR` = '2' and `KOD_PPR` = '1' AND `KOL_P` > '0' ORDER BY K_WORD";
                Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;

                //Переменная Connect - это строка подключения в которой:
                //БАЗА - Имя базы в MySQL
                //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
                //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
                //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL

                myConnection = new MySqlConnection(Connect);
                myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open(); //Устанавливаем соединение с базой данных.

                myConnection2 = new MySqlConnection(Connect);
                updateCommand = new MySqlCommand(CommandText, myConnection2);
                myConnection2.Open();
                // Gets a NumberFormatInfo associated with the en-US culture.
                nfi = new CultureInfo("en-US", false).NumberFormat;

                nfi.NumberDecimalDigits = 0;
                nfi.NumberGroupSeparator = " ";

                nfi.PositiveSign = "";
              
                MyDataReader = myCommand.ExecuteReader();

                while (MyDataReader.Read())
                {

                    header = MyDataReader.GetString(12); //Получаем строку
                    text = MyDataReader.GetString(13); //Получаем строку                    
                    phone = MyDataReader.GetString(14); //Получаем строку
                    id = MyDataReader.GetString(0); //Получаем id
                    if (!MyDataReader.IsDBNull(20))
                    {
                        cost = MyDataReader.GetInt32(20); //Получаем строку
                    }
                    else
                    {
                        cost = 0;
                    }

                    if (!MyDataReader.IsDBNull(21))
                    {
                        costStr = MyDataReader.GetString(22);//Получаем строку
                    }
                    else
                    {
                        costStr = "";
                    }
                    //toshow = MyDataReader.GetUInt16(9); //Получаем строку
                    //catalog = MyDataReader.GetString(11); //Получаем строку


                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 1;
                    //wdApp.Selection.Font.Bold = 1;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.TypeText(header);
                    if (cost != 0)
                    {
                        wdApp.Selection.TypeText(" " + cost.ToString("N", nfi));
                        wdApp.Selection.TypeText(" ");
                        wdApp.Selection.TypeText(" руб." + costStr);
                    }
                    wdApp.Selection.TypeText(" ");
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeText("- " + text + " ");
                    wdApp.Selection.TypeText(" " + phone);
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeParagraph();
                    //wdApp.Selection.TypeParagraph();
                    CommandText = "UPDATE `" + dbname + "`.`ob` SET `secondRub`='" + idRub + "' WHERE `ID_OB`='" + id + "'";
                    //MessageBox.Show(CommandText);
                    updateCommand.CommandText = CommandText;
                    int res = updateCommand.ExecuteNonQuery();
                    col++;
                    if (checkBox2.Checked)
                    {
                        //CommandText = "UPDATE `ads_paper`.`cards` SET `toshow`=" + (toshow - 1).ToString() + " WHERE `date`='" + date + "'";                        
                        //MessageBox.Show(CommandText);

                        //MySqlCommand updateCommand = new MySqlCommand(CommandText, myConnection2);
                        //int res = updateCommand.ExecuteNonQuery();
                    }

                }

                MyDataReader.Close();
                myConnection2.Close();
                myConnection.Close(); //Обязательно закрываем соединение!

                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                wdApp.Selection.TypeText("В САДОВОДЧЕСКИХ ТОВАРИЩЕСТВАХ");
                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();

                CommandText = "select * from " + dbname + ".ob where `KOD_R` = '1' AND `KOD_PR` = '3' and `KOD_PPR` = '1' AND `KOL_P` > '0' ORDER BY K_WORD";
                Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;

                //Переменная Connect - это строка подключения в которой:
                //БАЗА - Имя базы в MySQL
                //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
                //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
                //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL

                myConnection = new MySqlConnection(Connect);
                myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open(); //Устанавливаем соединение с базой данных.

                myConnection2 = new MySqlConnection(Connect);
                updateCommand = new MySqlCommand(CommandText, myConnection2);
                myConnection2.Open();
                // Gets a NumberFormatInfo associated with the en-US culture.
                nfi = new CultureInfo("en-US", false).NumberFormat;

                nfi.NumberDecimalDigits = 0;
                nfi.NumberGroupSeparator = " ";

                nfi.PositiveSign = "";

                MyDataReader = myCommand.ExecuteReader();

                while (MyDataReader.Read())
                {

                    header = MyDataReader.GetString(12); //Получаем строку
                    text = MyDataReader.GetString(13); //Получаем строку                    
                    phone = MyDataReader.GetString(14); //Получаем строку
                    id = MyDataReader.GetString(0); //Получаем id
                    if (!MyDataReader.IsDBNull(20))
                    {
                        cost = MyDataReader.GetInt32(20); //Получаем строку
                    }
                    else
                    {
                        cost = 0;
                    }

                    if (!MyDataReader.IsDBNull(21))
                    {
                        costStr = MyDataReader.GetString(22);//Получаем строку
                    }
                    else
                    {
                        costStr = "";
                    }
                    //toshow = MyDataReader.GetUInt16(9); //Получаем строку
                    //catalog = MyDataReader.GetString(11); //Получаем строку


                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 1;
                    //wdApp.Selection.Font.Bold = 1;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.TypeText(header);
                    if (cost != 0)
                    {
                        wdApp.Selection.TypeText(" " + cost.ToString("N", nfi));
                        wdApp.Selection.TypeText(" ");
                        wdApp.Selection.TypeText(" руб." + costStr);
                    }
                    wdApp.Selection.TypeText(" ");
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeText("- " + text + " ");
                    wdApp.Selection.TypeText(" " + phone);
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeParagraph();
                    //wdApp.Selection.TypeParagraph();
                    CommandText = "UPDATE `" + dbname + "`.`ob` SET `secondRub`='" + idRub + "' WHERE `ID_OB`='" + id + "'";
                    //MessageBox.Show(CommandText);
                    updateCommand.CommandText = CommandText;
                    int res = updateCommand.ExecuteNonQuery();
                    col++;
                    if (checkBox2.Checked)
                    {
                        //CommandText = "UPDATE `ads_paper`.`cards` SET `toshow`=" + (toshow - 1).ToString() + " WHERE `date`='" + date + "'";                        
                        //MessageBox.Show(CommandText);

                        //MySqlCommand updateCommand = new MySqlCommand(CommandText, myConnection2);
                        //int res = updateCommand.ExecuteNonQuery();
                    }

                }

                MyDataReader.Close();
                myConnection2.Close();
                myConnection.Close(); //Обязательно закрываем соединение!

                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                wdApp.Selection.TypeText("1-КОМНАТНЫЕ КВАРТИРЫ");
                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();

                CommandText = "select * from " + dbname + ".ob where `KOD_R` = '1' AND `KOD_PR` = '6' and `KOD_PPR` = '1' AND `KOL_P` > '0' ORDER BY K_WORD";
                Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;

                //Переменная Connect - это строка подключения в которой:
                //БАЗА - Имя базы в MySQL
                //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
                //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
                //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL

                myConnection = new MySqlConnection(Connect);
                myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open(); //Устанавливаем соединение с базой данных.

                myConnection2 = new MySqlConnection(Connect);
                updateCommand = new MySqlCommand(CommandText, myConnection2);
                myConnection2.Open();
                // Gets a NumberFormatInfo associated with the en-US culture.
                nfi = new CultureInfo("en-US", false).NumberFormat;

                nfi.NumberDecimalDigits = 0;
                nfi.NumberGroupSeparator = " ";

                nfi.PositiveSign = "";

                MyDataReader = myCommand.ExecuteReader();

                while (MyDataReader.Read())
                {

                    header = MyDataReader.GetString(12); //Получаем строку
                    text = MyDataReader.GetString(13); //Получаем строку                    
                    phone = MyDataReader.GetString(14); //Получаем строку
                    id = MyDataReader.GetString(0); //Получаем id
                    if (!MyDataReader.IsDBNull(20))
                    {
                        cost = MyDataReader.GetInt32(20); //Получаем строку
                    }
                    else
                    {
                        cost = 0;
                    }

                    if (!MyDataReader.IsDBNull(21))
                    {
                        costStr = MyDataReader.GetString(22);//Получаем строку
                    }
                    else
                    {
                        costStr = "";
                    }
                    //toshow = MyDataReader.GetUInt16(9); //Получаем строку
                    //catalog = MyDataReader.GetString(11); //Получаем строку


                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 1;
                    //wdApp.Selection.Font.Bold = 1;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.TypeText(header);
                    if (cost != 0)
                    {
                        wdApp.Selection.TypeText(" " + cost.ToString("N", nfi));
                        wdApp.Selection.TypeText(" ");
                        wdApp.Selection.TypeText(" руб." + costStr);
                    }
                    wdApp.Selection.TypeText(" ");
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeText("- " + text + " ");
                    wdApp.Selection.TypeText(" " + phone);
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeParagraph();
                    //wdApp.Selection.TypeParagraph();
                    CommandText = "UPDATE `" + dbname + "`.`ob` SET `secondRub`='" + idRub + "' WHERE `ID_OB`='" + id + "'";
                    //MessageBox.Show(CommandText);
                    updateCommand.CommandText = CommandText;
                    int res = updateCommand.ExecuteNonQuery();
                    col++;
                    if (checkBox2.Checked)
                    {
                        //CommandText = "UPDATE `ads_paper`.`cards` SET `toshow`=" + (toshow - 1).ToString() + " WHERE `date`='" + date + "'";                        
                        //MessageBox.Show(CommandText);

                        //MySqlCommand updateCommand = new MySqlCommand(CommandText, myConnection2);
                        //int res = updateCommand.ExecuteNonQuery();
                    }

                }

                MyDataReader.Close();
                myConnection2.Close();
                myConnection.Close(); //Обязательно закрываем соединение!

                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                wdApp.Selection.TypeText("1,5-КОМНАТНЫЕ КВАРТИРЫ");
                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();

                CommandText = "select * from " + dbname + ".ob where `KOD_R` = '1' AND `KOD_PR` = '6' and `KOD_PPR` = '2' AND `KOL_P` > '0' ORDER BY K_WORD";
                Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;

                //Переменная Connect - это строка подключения в которой:
                //БАЗА - Имя базы в MySQL
                //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
                //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
                //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL

                myConnection = new MySqlConnection(Connect);
                myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open(); //Устанавливаем соединение с базой данных.

                myConnection2 = new MySqlConnection(Connect);
                updateCommand = new MySqlCommand(CommandText, myConnection2);
                myConnection2.Open();
                // Gets a NumberFormatInfo associated with the en-US culture.
                nfi = new CultureInfo("en-US", false).NumberFormat;

                nfi.NumberDecimalDigits = 0;
                nfi.NumberGroupSeparator = " ";

                nfi.PositiveSign = "";

                MyDataReader = myCommand.ExecuteReader();

                while (MyDataReader.Read())
                {

                    header = MyDataReader.GetString(12); //Получаем строку
                    text = MyDataReader.GetString(13); //Получаем строку                    
                    phone = MyDataReader.GetString(14); //Получаем строку
                    id = MyDataReader.GetString(0); //Получаем id
                    if (!MyDataReader.IsDBNull(20))
                    {
                        cost = MyDataReader.GetInt32(20); //Получаем строку
                    }
                    else
                    {
                        cost = 0;
                    }

                    if (!MyDataReader.IsDBNull(21))
                    {
                        costStr = MyDataReader.GetString(22);//Получаем строку
                    }
                    else
                    {
                        costStr = "";
                    }
                    //toshow = MyDataReader.GetUInt16(9); //Получаем строку
                    //catalog = MyDataReader.GetString(11); //Получаем строку


                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 1;
                    //wdApp.Selection.Font.Bold = 1;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.TypeText(header);
                    if (cost != 0)
                    {
                        wdApp.Selection.TypeText(" " + cost.ToString("N", nfi));
                        wdApp.Selection.TypeText(" ");
                        wdApp.Selection.TypeText(" руб." + costStr);
                    }
                    wdApp.Selection.TypeText(" ");
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeText("- " + text + " ");
                    wdApp.Selection.TypeText(" " + phone);
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeParagraph();
                    //wdApp.Selection.TypeParagraph();
                    CommandText = "UPDATE `" + dbname + "`.`ob` SET `secondRub`='" + idRub + "' WHERE `ID_OB`='" + id + "'";
                    //MessageBox.Show(CommandText);
                    updateCommand.CommandText = CommandText;
                    int res = updateCommand.ExecuteNonQuery();
                    col++;
                    if (checkBox2.Checked)
                    {
                        //CommandText = "UPDATE `ads_paper`.`cards` SET `toshow`=" + (toshow - 1).ToString() + " WHERE `date`='" + date + "'";                        
                        //MessageBox.Show(CommandText);

                        //MySqlCommand updateCommand = new MySqlCommand(CommandText, myConnection2);
                        //int res = updateCommand.ExecuteNonQuery();
                    }

                }

                MyDataReader.Close();
                myConnection2.Close();
                myConnection.Close(); //Обязательно закрываем соединение!
                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                wdApp.Selection.TypeText("2-КОМНАТНЫЕ КВАРТИРЫ");
                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();

                CommandText = "select * from " + dbname + ".ob where `KOD_R` = '1' AND `KOD_PR` = '6' and `KOD_PPR` = '3' AND `KOL_P` > '0' ORDER BY K_WORD";
                Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;

                //Переменная Connect - это строка подключения в которой:
                //БАЗА - Имя базы в MySQL
                //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
                //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
                //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL

                myConnection = new MySqlConnection(Connect);
                myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open(); //Устанавливаем соединение с базой данных.

                myConnection2 = new MySqlConnection(Connect);
                updateCommand = new MySqlCommand(CommandText, myConnection2);
                myConnection2.Open();
                // Gets a NumberFormatInfo associated with the en-US culture.
                nfi = new CultureInfo("en-US", false).NumberFormat;

                nfi.NumberDecimalDigits = 0;
                nfi.NumberGroupSeparator = " ";

                nfi.PositiveSign = "";

                MyDataReader = myCommand.ExecuteReader();

                while (MyDataReader.Read())
                {

                    header = MyDataReader.GetString(12); //Получаем строку
                    text = MyDataReader.GetString(13); //Получаем строку                    
                    phone = MyDataReader.GetString(14); //Получаем строку
                    id = MyDataReader.GetString(0); //Получаем id
                    if (!MyDataReader.IsDBNull(20))
                    {
                        cost = MyDataReader.GetInt32(20); //Получаем строку
                    }
                    else
                    {
                        cost = 0;
                    }

                    if (!MyDataReader.IsDBNull(21))
                    {
                        costStr = MyDataReader.GetString(22);//Получаем строку
                    }
                    else
                    {
                        costStr = "";
                    }
                    //toshow = MyDataReader.GetUInt16(9); //Получаем строку
                    //catalog = MyDataReader.GetString(11); //Получаем строку


                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 1;
                    //wdApp.Selection.Font.Bold = 1;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.TypeText(header);
                    if (cost != 0)
                    {
                        wdApp.Selection.TypeText(" " + cost.ToString("N", nfi));
                        wdApp.Selection.TypeText(" ");
                        wdApp.Selection.TypeText(" руб." + costStr);
                    }
                    wdApp.Selection.TypeText(" ");
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeText("- " + text + " ");
                    wdApp.Selection.TypeText(" " + phone);
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeParagraph();
                    //wdApp.Selection.TypeParagraph();
                    CommandText = "UPDATE `" + dbname + "`.`ob` SET `secondRub`='" + idRub + "' WHERE `ID_OB`='" + id + "'";
                    //MessageBox.Show(CommandText);
                    updateCommand.CommandText = CommandText;
                    int res = updateCommand.ExecuteNonQuery();
                    col++;
                    if (checkBox2.Checked)
                    {
                        //CommandText = "UPDATE `ads_paper`.`cards` SET `toshow`=" + (toshow - 1).ToString() + " WHERE `date`='" + date + "'";                        
                        //MessageBox.Show(CommandText);

                        //MySqlCommand updateCommand = new MySqlCommand(CommandText, myConnection2);
                        //int res = updateCommand.ExecuteNonQuery();
                    }

                }

                MyDataReader.Close();
                myConnection2.Close();
                myConnection.Close(); //Обязательно закрываем соединение!

                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                wdApp.Selection.TypeText("2,5-КОМНАТНЫЕ КВАРТИРЫ");
                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();

                CommandText = "select * from " + dbname + ".ob where `KOD_R` = '1' AND `KOD_PR` = '6' and `KOD_PPR` = '4' AND `KOL_P` > '0' ORDER BY K_WORD";
                Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;

                //Переменная Connect - это строка подключения в которой:
                //БАЗА - Имя базы в MySQL
                //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
                //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
                //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL

                myConnection = new MySqlConnection(Connect);
                myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open(); //Устанавливаем соединение с базой данных.

                myConnection2 = new MySqlConnection(Connect);
                updateCommand = new MySqlCommand(CommandText, myConnection2);
                myConnection2.Open();
                // Gets a NumberFormatInfo associated with the en-US culture.
                nfi = new CultureInfo("en-US", false).NumberFormat;

                nfi.NumberDecimalDigits = 0;
                nfi.NumberGroupSeparator = " ";

                nfi.PositiveSign = "";

                MyDataReader = myCommand.ExecuteReader();

                while (MyDataReader.Read())
                {

                    header = MyDataReader.GetString(12); //Получаем строку
                    text = MyDataReader.GetString(13); //Получаем строку                    
                    phone = MyDataReader.GetString(14); //Получаем строку
                    id = MyDataReader.GetString(0); //Получаем id
                    if (!MyDataReader.IsDBNull(20))
                    {
                        cost = MyDataReader.GetInt32(20); //Получаем строку
                    }
                    else
                    {
                        cost = 0;
                    }

                    if (!MyDataReader.IsDBNull(21))
                    {
                        costStr = MyDataReader.GetString(22);//Получаем строку
                    }
                    else
                    {
                        costStr = "";
                    }
                    //toshow = MyDataReader.GetUInt16(9); //Получаем строку
                    //catalog = MyDataReader.GetString(11); //Получаем строку


                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 1;
                    //wdApp.Selection.Font.Bold = 1;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.TypeText(header);
                    if (cost != 0)
                    {
                        wdApp.Selection.TypeText(" " + cost.ToString("N", nfi));
                        wdApp.Selection.TypeText(" ");
                        wdApp.Selection.TypeText(" руб." + costStr);
                    }
                    wdApp.Selection.TypeText(" ");
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeText("- " + text + " ");
                    wdApp.Selection.TypeText(" " + phone);
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeParagraph();
                    //wdApp.Selection.TypeParagraph();
                    CommandText = "UPDATE `" + dbname + "`.`ob` SET `secondRub`='" + idRub + "' WHERE `ID_OB`='" + id + "'";
                    //MessageBox.Show(CommandText);
                    updateCommand.CommandText = CommandText;
                    int res = updateCommand.ExecuteNonQuery();
                    col++;
                    if (checkBox2.Checked)
                    {
                        //CommandText = "UPDATE `ads_paper`.`cards` SET `toshow`=" + (toshow - 1).ToString() + " WHERE `date`='" + date + "'";                        
                        //MessageBox.Show(CommandText);

                        //MySqlCommand updateCommand = new MySqlCommand(CommandText, myConnection2);
                        //int res = updateCommand.ExecuteNonQuery();
                    }

                }

                MyDataReader.Close();
                myConnection2.Close();
                myConnection.Close(); //Обязательно закрываем соединение!

                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                wdApp.Selection.TypeText("3-КОМНАТНЫЕ КВАРТИРЫ");
                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();

                CommandText = "select * from " + dbname + ".ob where `KOD_R` = '1' AND `KOD_PR` = '6' and `KOD_PPR` = '5' AND `KOL_P` > '0' ORDER BY K_WORD";
                Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;

                //Переменная Connect - это строка подключения в которой:
                //БАЗА - Имя базы в MySQL
                //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
                //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
                //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL

                myConnection = new MySqlConnection(Connect);
                myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open(); //Устанавливаем соединение с базой данных.

                myConnection2 = new MySqlConnection(Connect);
                updateCommand = new MySqlCommand(CommandText, myConnection2);
                myConnection2.Open();
                // Gets a NumberFormatInfo associated with the en-US culture.
                nfi = new CultureInfo("en-US", false).NumberFormat;

                nfi.NumberDecimalDigits = 0;
                nfi.NumberGroupSeparator = " ";

                nfi.PositiveSign = "";

                MyDataReader = myCommand.ExecuteReader();

                while (MyDataReader.Read())
                {

                    header = MyDataReader.GetString(12); //Получаем строку
                    text = MyDataReader.GetString(13); //Получаем строку                    
                    phone = MyDataReader.GetString(14); //Получаем строку
                    id = MyDataReader.GetString(0); //Получаем id
                    if (!MyDataReader.IsDBNull(20))
                    {
                        cost = MyDataReader.GetInt32(20); //Получаем строку
                    }
                    else
                    {
                        cost = 0;
                    }

                    if (!MyDataReader.IsDBNull(21))
                    {
                        costStr = MyDataReader.GetString(22);//Получаем строку
                    }
                    else
                    {
                        costStr = "";
                    }
                    //toshow = MyDataReader.GetUInt16(9); //Получаем строку
                    //catalog = MyDataReader.GetString(11); //Получаем строку


                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 1;
                    //wdApp.Selection.Font.Bold = 1;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.TypeText(header);
                    if (cost != 0)
                    {
                        wdApp.Selection.TypeText(" " + cost.ToString("N", nfi));
                        wdApp.Selection.TypeText(" ");
                        wdApp.Selection.TypeText(" руб." + costStr);
                    }
                    wdApp.Selection.TypeText(" ");
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeText("- " + text + " ");
                    wdApp.Selection.TypeText(" " + phone);
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeParagraph();
                    //wdApp.Selection.TypeParagraph();
                    CommandText = "UPDATE `" + dbname + "`.`ob` SET `secondRub`='" + idRub + "' WHERE `ID_OB`='" + id + "'";
                    //MessageBox.Show(CommandText);
                    updateCommand.CommandText = CommandText;
                    int res = updateCommand.ExecuteNonQuery();
                    col++;
                    if (checkBox2.Checked)
                    {
                        //CommandText = "UPDATE `ads_paper`.`cards` SET `toshow`=" + (toshow - 1).ToString() + " WHERE `date`='" + date + "'";                        
                        //MessageBox.Show(CommandText);

                        //MySqlCommand updateCommand = new MySqlCommand(CommandText, myConnection2);
                        //int res = updateCommand.ExecuteNonQuery();
                    }

                }

                MyDataReader.Close();
                myConnection2.Close();
                myConnection.Close(); //Обязательно закрываем соединение!

                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                wdApp.Selection.TypeText("4-КОМНАТНЫЕ КВАРТИРЫ");
                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();

                CommandText = "select * from " + dbname + ".ob where `KOD_R` = '1' AND `KOD_PR` = '6' and `KOD_PPR` = '6' AND `KOL_P` > '0' ORDER BY K_WORD";
                Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;

                //Переменная Connect - это строка подключения в которой:
                //БАЗА - Имя базы в MySQL
                //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
                //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
                //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL

                myConnection = new MySqlConnection(Connect);
                myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open(); //Устанавливаем соединение с базой данных.

                myConnection2 = new MySqlConnection(Connect);
                updateCommand = new MySqlCommand(CommandText, myConnection2);
                myConnection2.Open();
                // Gets a NumberFormatInfo associated with the en-US culture.
                nfi = new CultureInfo("en-US", false).NumberFormat;

                nfi.NumberDecimalDigits = 0;
                nfi.NumberGroupSeparator = " ";

                nfi.PositiveSign = "";

                MyDataReader = myCommand.ExecuteReader();

                while (MyDataReader.Read())
                {

                    header = MyDataReader.GetString(12); //Получаем строку
                    text = MyDataReader.GetString(13); //Получаем строку                    
                    phone = MyDataReader.GetString(14); //Получаем строку
                    id = MyDataReader.GetString(0); //Получаем id
                    if (!MyDataReader.IsDBNull(20))
                    {
                        cost = MyDataReader.GetInt32(20); //Получаем строку
                    }
                    else
                    {
                        cost = 0;
                    }

                    if (!MyDataReader.IsDBNull(21))
                    {
                        costStr = MyDataReader.GetString(22);//Получаем строку
                    }
                    else
                    {
                        costStr = "";
                    }
                    //toshow = MyDataReader.GetUInt16(9); //Получаем строку
                    //catalog = MyDataReader.GetString(11); //Получаем строку


                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 1;
                    //wdApp.Selection.Font.Bold = 1;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.TypeText(header);
                    if (cost != 0)
                    {
                        wdApp.Selection.TypeText(" " + cost.ToString("N", nfi));
                        wdApp.Selection.TypeText(" ");
                        wdApp.Selection.TypeText(" руб." + costStr);
                    }
                    wdApp.Selection.TypeText(" ");
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeText("- " + text + " ");
                    wdApp.Selection.TypeText(" " + phone);
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeParagraph();
                    //wdApp.Selection.TypeParagraph();
                    CommandText = "UPDATE `" + dbname + "`.`ob` SET `secondRub`='" + idRub + "' WHERE `ID_OB`='" + id + "'";
                    //MessageBox.Show(CommandText);
                    updateCommand.CommandText = CommandText;
                    int res = updateCommand.ExecuteNonQuery();
                    col++;
                    if (checkBox2.Checked)
                    {
                        //CommandText = "UPDATE `ads_paper`.`cards` SET `toshow`=" + (toshow - 1).ToString() + " WHERE `date`='" + date + "'";                        
                        //MessageBox.Show(CommandText);

                        //MySqlCommand updateCommand = new MySqlCommand(CommandText, myConnection2);
                        //int res = updateCommand.ExecuteNonQuery();
                    }

                }

                MyDataReader.Close();
                myConnection2.Close();
                myConnection.Close(); //Обязательно закрываем соединение!

                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                wdApp.Selection.TypeText("КВАРТИРЫ ИЗ 5 И БОЛЕЕ КОМНАТ");
                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();

                CommandText = "select * from " + dbname + ".ob where `KOD_R` = '1' AND `KOD_PR` = '6' and `KOD_PPR` = '7' AND `KOL_P` > '0' ORDER BY K_WORD";
                Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;

                //Переменная Connect - это строка подключения в которой:
                //БАЗА - Имя базы в MySQL
                //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
                //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
                //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL

                myConnection = new MySqlConnection(Connect);
                myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open(); //Устанавливаем соединение с базой данных.

                myConnection2 = new MySqlConnection(Connect);
                updateCommand = new MySqlCommand(CommandText, myConnection2);
                myConnection2.Open();
                // Gets a NumberFormatInfo associated with the en-US culture.
                nfi = new CultureInfo("en-US", false).NumberFormat;

                nfi.NumberDecimalDigits = 0;
                nfi.NumberGroupSeparator = " ";

                nfi.PositiveSign = "";

                MyDataReader = myCommand.ExecuteReader();

                while (MyDataReader.Read())
                {

                    header = MyDataReader.GetString(12); //Получаем строку
                    text = MyDataReader.GetString(13); //Получаем строку                    
                    phone = MyDataReader.GetString(14); //Получаем строку
                    id = MyDataReader.GetString(0); //Получаем id
                    if (!MyDataReader.IsDBNull(20))
                    {
                        cost = MyDataReader.GetInt32(20); //Получаем строку
                    }
                    else
                    {
                        cost = 0;
                    }

                    if (!MyDataReader.IsDBNull(21))
                    {
                        costStr = MyDataReader.GetString(22);//Получаем строку
                    }
                    else
                    {
                        costStr = "";
                    }
                    //toshow = MyDataReader.GetUInt16(9); //Получаем строку
                    //catalog = MyDataReader.GetString(11); //Получаем строку


                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 1;
                    //wdApp.Selection.Font.Bold = 1;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.TypeText(header);
                    if (cost != 0)
                    {
                        wdApp.Selection.TypeText(" " + cost.ToString("N", nfi));
                        wdApp.Selection.TypeText(" ");
                        wdApp.Selection.TypeText(" руб." + costStr);
                    }
                    wdApp.Selection.TypeText(" ");
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeText("- " + text + " ");
                    wdApp.Selection.TypeText(" " + phone);
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeParagraph();
                    //wdApp.Selection.TypeParagraph();
                    CommandText = "UPDATE `" + dbname + "`.`ob` SET `secondRub`='" + idRub + "' WHERE `ID_OB`='" + id + "'";
                    //MessageBox.Show(CommandText);
                    updateCommand.CommandText = CommandText;
                    int res = updateCommand.ExecuteNonQuery();
                    col++;
                    if (checkBox2.Checked)
                    {
                        //CommandText = "UPDATE `ads_paper`.`cards` SET `toshow`=" + (toshow - 1).ToString() + " WHERE `date`='" + date + "'";                        
                        //MessageBox.Show(CommandText);

                        //MySqlCommand updateCommand = new MySqlCommand(CommandText, myConnection2);
                        //int res = updateCommand.ExecuteNonQuery();
                    }

                }

                MyDataReader.Close();
                myConnection2.Close();
                myConnection.Close(); //Обязательно закрываем соединение!

                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                wdApp.Selection.TypeText("ДОМА");
                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();

                CommandText = "select * from " + dbname + ".ob where `KOD_R` = '1' AND (`KOD_PR` = '4' OR `KOD_PR` = '5') AND `KOL_P` > '0' ORDER BY K_WORD";
                Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;

                //Переменная Connect - это строка подключения в которой:
                //БАЗА - Имя базы в MySQL
                //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
                //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
                //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL

                myConnection = new MySqlConnection(Connect);
                myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open(); //Устанавливаем соединение с базой данных.

                myConnection2 = new MySqlConnection(Connect);
                updateCommand = new MySqlCommand(CommandText, myConnection2);
                myConnection2.Open();
                // Gets a NumberFormatInfo associated with the en-US culture.
                nfi = new CultureInfo("en-US", false).NumberFormat;

                nfi.NumberDecimalDigits = 0;
                nfi.NumberGroupSeparator = " ";

                nfi.PositiveSign = "";

                MyDataReader = myCommand.ExecuteReader();

                while (MyDataReader.Read())
                {

                    header = MyDataReader.GetString(12); //Получаем строку
                    text = MyDataReader.GetString(13); //Получаем строку                    
                    phone = MyDataReader.GetString(14); //Получаем строку
                    id = MyDataReader.GetString(0); //Получаем id
                    if (!MyDataReader.IsDBNull(20))
                    {
                        cost = MyDataReader.GetInt32(20); //Получаем строку
                    }
                    else
                    {
                        cost = 0;
                    }

                    if (!MyDataReader.IsDBNull(21))
                    {
                        costStr = MyDataReader.GetString(22);//Получаем строку
                    }
                    else
                    {
                        costStr = "";
                    }
                    //toshow = MyDataReader.GetUInt16(9); //Получаем строку
                    //catalog = MyDataReader.GetString(11); //Получаем строку


                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 1;
                    //wdApp.Selection.Font.Bold = 1;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.TypeText(header);
                    if (cost != 0)
                    {
                        wdApp.Selection.TypeText(" " + cost.ToString("N", nfi));
                        wdApp.Selection.TypeText(" ");
                        wdApp.Selection.TypeText(" руб." + costStr);
                    }
                    wdApp.Selection.TypeText(" ");
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeText("- " + text + " ");
                    wdApp.Selection.TypeText(" " + phone);
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeParagraph();
                    //wdApp.Selection.TypeParagraph();
                    CommandText = "UPDATE `" + dbname + "`.`ob` SET `secondRub`='" + idRub + "' WHERE `ID_OB`='" + id + "'";
                    //MessageBox.Show(CommandText);
                    updateCommand.CommandText = CommandText;
                    int res = updateCommand.ExecuteNonQuery();
                    col++;
                    if (checkBox2.Checked)
                    {
                        //CommandText = "UPDATE `ads_paper`.`cards` SET `toshow`=" + (toshow - 1).ToString() + " WHERE `date`='" + date + "'";                        
                        //MessageBox.Show(CommandText);

                        //MySqlCommand updateCommand = new MySqlCommand(CommandText, myConnection2);
                        //int res = updateCommand.ExecuteNonQuery();
                    }

                }

                MyDataReader.Close();
                myConnection2.Close();
                myConnection.Close(); //Обязательно закрываем соединение!

                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                wdApp.Selection.TypeText("ДРУГАЯ НЕДВИЖИМОСТЬ ВО ВЛАДИКАВКАЗЕ");
                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();

                CommandText = "select * from " + dbname + ".ob where `KOD_R` = '1' AND (`KOD_PR` = '12' OR `KOD_PR` = '13' OR `KOD_PR` = '14') AND `KOL_P` > '0' ORDER BY K_WORD";
                Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;

                //Переменная Connect - это строка подключения в которой:
                //БАЗА - Имя базы в MySQL
                //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
                //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
                //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL

                myConnection = new MySqlConnection(Connect);
                myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open(); //Устанавливаем соединение с базой данных.

                myConnection2 = new MySqlConnection(Connect);
                updateCommand = new MySqlCommand(CommandText, myConnection2);
                myConnection2.Open();
                // Gets a NumberFormatInfo associated with the en-US culture.
                nfi = new CultureInfo("en-US", false).NumberFormat;

                nfi.NumberDecimalDigits = 0;
                nfi.NumberGroupSeparator = " ";

                nfi.PositiveSign = "";

                MyDataReader = myCommand.ExecuteReader();

                while (MyDataReader.Read())
                {

                    header = MyDataReader.GetString(12); //Получаем строку
                    text = MyDataReader.GetString(13); //Получаем строку                    
                    phone = MyDataReader.GetString(14); //Получаем строку
                    id = MyDataReader.GetString(0); //Получаем id
                    if (!MyDataReader.IsDBNull(20))
                    {
                        cost = MyDataReader.GetInt32(20); //Получаем строку
                    }
                    else
                    {
                        cost = 0;
                    }

                    if (!MyDataReader.IsDBNull(21))
                    {
                        costStr = MyDataReader.GetString(22);//Получаем строку
                    }
                    else
                    {
                        costStr = "";
                    }
                    //toshow = MyDataReader.GetUInt16(9); //Получаем строку
                    //catalog = MyDataReader.GetString(11); //Получаем строку


                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 1;
                    //wdApp.Selection.Font.Bold = 1;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.TypeText(header);
                    if (cost != 0)
                    {
                        wdApp.Selection.TypeText(" " + cost.ToString("N", nfi));
                        wdApp.Selection.TypeText(" ");
                        wdApp.Selection.TypeText(" руб." + costStr);
                    }
                    wdApp.Selection.TypeText(" ");
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeText("- " + text + " ");
                    wdApp.Selection.TypeText(" " + phone);
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeParagraph();
                    //wdApp.Selection.TypeParagraph();
                    CommandText = "UPDATE `" + dbname + "`.`ob` SET `secondRub`='" + idRub + "' WHERE `ID_OB`='" + id + "'";
                    //MessageBox.Show(CommandText);
                    updateCommand.CommandText = CommandText;
                    int res = updateCommand.ExecuteNonQuery();
                    col++;
                    if (checkBox2.Checked)
                    {
                        //CommandText = "UPDATE `ads_paper`.`cards` SET `toshow`=" + (toshow - 1).ToString() + " WHERE `date`='" + date + "'";                        
                        //MessageBox.Show(CommandText);

                        //MySqlCommand updateCommand = new MySqlCommand(CommandText, myConnection2);
                        //int res = updateCommand.ExecuteNonQuery();
                    }

                }

                MyDataReader.Close();
                myConnection2.Close();
                myConnection.Close(); //Обязательно закрываем соединение!

                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                wdApp.Selection.TypeText("МЕНЯЮ");
                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                wdApp.Selection.TypeText("ВНЕ РСО-АЛАНИЯ");
                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                CommandText = "select * from " + dbname + ".ob where `KOD_R` = '1' AND `KOD_PR` = '1' AND `KOD_PPR` = '2'  AND `KOL_P` > '0' ORDER BY K_WORD";
                Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;

                //Переменная Connect - это строка подключения в которой:
                //БАЗА - Имя базы в MySQL
                //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
                //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
                //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL

                myConnection = new MySqlConnection(Connect);
                myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open(); //Устанавливаем соединение с базой данных.

                myConnection2 = new MySqlConnection(Connect);
                updateCommand = new MySqlCommand(CommandText, myConnection2);
                myConnection2.Open();
                // Gets a NumberFormatInfo associated with the en-US culture.
                nfi = new CultureInfo("en-US", false).NumberFormat;

                nfi.NumberDecimalDigits = 0;
                nfi.NumberGroupSeparator = " ";

                nfi.PositiveSign = "";

                MyDataReader = myCommand.ExecuteReader();

                while (MyDataReader.Read())
                {

                    header = MyDataReader.GetString(12); //Получаем строку
                    text = MyDataReader.GetString(13); //Получаем строку                    
                    phone = MyDataReader.GetString(14); //Получаем строку
                    id = MyDataReader.GetString(0); //Получаем id
                    if (!MyDataReader.IsDBNull(20))
                    {
                        cost = MyDataReader.GetInt32(20); //Получаем строку
                    }
                    else
                    {
                        cost = 0;
                    }

                    if (!MyDataReader.IsDBNull(21))
                    {
                        costStr = MyDataReader.GetString(22);//Получаем строку
                    }
                    else
                    {
                        costStr = "";
                    }
                    //toshow = MyDataReader.GetUInt16(9); //Получаем строку
                    //catalog = MyDataReader.GetString(11); //Получаем строку


                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 1;
                    //wdApp.Selection.Font.Bold = 1;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.TypeText(header);
                    if (cost != 0)
                    {
                        wdApp.Selection.TypeText(" " + cost.ToString("N", nfi));
                        wdApp.Selection.TypeText(" ");
                        wdApp.Selection.TypeText(" руб." + costStr);
                    }
                    wdApp.Selection.TypeText(" ");
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeText("- " + text + " ");
                    wdApp.Selection.TypeText(" " + phone);
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeParagraph();
                    //wdApp.Selection.TypeParagraph();
                    CommandText = "UPDATE `" + dbname + "`.`ob` SET `secondRub`='" + idRub + "' WHERE `ID_OB`='" + id + "'";
                    //MessageBox.Show(CommandText);
                    updateCommand.CommandText = CommandText;
                    int res = updateCommand.ExecuteNonQuery();
                    col++;
                    if (checkBox2.Checked)
                    {
                        //CommandText = "UPDATE `ads_paper`.`cards` SET `toshow`=" + (toshow - 1).ToString() + " WHERE `date`='" + date + "'";                        
                        //MessageBox.Show(CommandText);

                        //MySqlCommand updateCommand = new MySqlCommand(CommandText, myConnection2);
                        //int res = updateCommand.ExecuteNonQuery();
                    }

                }

                MyDataReader.Close();
                myConnection2.Close();
                myConnection.Close(); //Обязательно закрываем соединение!

                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                wdApp.Selection.TypeText("ВНЕ РСО-АЛАНИЯ");
                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                CommandText = "select * from " + dbname + ".ob where `KOD_R` = '1' AND `KOD_PR` = '2' AND `KOD_PPR` = '2' AND `KOL_P` > '0' ORDER BY K_WORD";
                Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;

                //Переменная Connect - это строка подключения в которой:
                //БАЗА - Имя базы в MySQL
                //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
                //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
                //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL

                myConnection = new MySqlConnection(Connect);
                myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open(); //Устанавливаем соединение с базой данных.

                myConnection2 = new MySqlConnection(Connect);
                updateCommand = new MySqlCommand(CommandText, myConnection2);
                myConnection2.Open();
                // Gets a NumberFormatInfo associated with the en-US culture.
                nfi = new CultureInfo("en-US", false).NumberFormat;

                nfi.NumberDecimalDigits = 0;
                nfi.NumberGroupSeparator = " ";

                nfi.PositiveSign = "";

                MyDataReader = myCommand.ExecuteReader();

                while (MyDataReader.Read())
                {

                    header = MyDataReader.GetString(12); //Получаем строку
                    text = MyDataReader.GetString(13); //Получаем строку                    
                    phone = MyDataReader.GetString(14); //Получаем строку
                    id = MyDataReader.GetString(0); //Получаем id
                    if (!MyDataReader.IsDBNull(20))
                    {
                        cost = MyDataReader.GetInt32(20); //Получаем строку
                    }
                    else
                    {
                        cost = 0;
                    }

                    if (!MyDataReader.IsDBNull(21))
                    {
                        costStr = MyDataReader.GetString(22);//Получаем строку
                    }
                    else
                    {
                        costStr = "";
                    }
                    //toshow = MyDataReader.GetUInt16(9); //Получаем строку
                    //catalog = MyDataReader.GetString(11); //Получаем строку


                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 1;
                    //wdApp.Selection.Font.Bold = 1;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.TypeText(header);
                    if (cost != 0)
                    {
                        wdApp.Selection.TypeText(" " + cost.ToString("N", nfi));
                        wdApp.Selection.TypeText(" ");
                        wdApp.Selection.TypeText(" руб." + costStr);
                    }
                    wdApp.Selection.TypeText(" ");
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeText("- " + text + " ");
                    wdApp.Selection.TypeText(" " + phone);
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeParagraph();
                    //wdApp.Selection.TypeParagraph();
                    CommandText = "UPDATE `" + dbname + "`.`ob` SET `secondRub`='" + idRub + "' WHERE `ID_OB`='" + id + "'";
                    //MessageBox.Show(CommandText);
                    updateCommand.CommandText = CommandText;
                    int res = updateCommand.ExecuteNonQuery();
                    col++;
                    if (checkBox2.Checked)
                    {
                        //CommandText = "UPDATE `ads_paper`.`cards` SET `toshow`=" + (toshow - 1).ToString() + " WHERE `date`='" + date + "'";                        
                        //MessageBox.Show(CommandText);

                        //MySqlCommand updateCommand = new MySqlCommand(CommandText, myConnection2);
                        //int res = updateCommand.ExecuteNonQuery();
                    }

                }

                MyDataReader.Close();
                myConnection2.Close();
                myConnection.Close(); //Обязательно закрываем соединение!

                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                wdApp.Selection.TypeText("ВО ВЛАДИКАВКАЗЕ");
                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                CommandText = "select * from " + dbname + ".ob where `KOD_R` = '1' AND (`KOD_PR` = '7' OR `KOD_PR` = '8') AND `KOL_P` > '0' ORDER BY K_WORD";
                Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;

                //Переменная Connect - это строка подключения в которой:
                //БАЗА - Имя базы в MySQL
                //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
                //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
                //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL

                myConnection = new MySqlConnection(Connect);
                myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open(); //Устанавливаем соединение с базой данных.

                myConnection2 = new MySqlConnection(Connect);
                updateCommand = new MySqlCommand(CommandText, myConnection2);
                myConnection2.Open();
                // Gets a NumberFormatInfo associated with the en-US culture.
                nfi = new CultureInfo("en-US", false).NumberFormat;

                nfi.NumberDecimalDigits = 0;
                nfi.NumberGroupSeparator = " ";

                nfi.PositiveSign = "";

                MyDataReader = myCommand.ExecuteReader();

                while (MyDataReader.Read())
                {

                    header = MyDataReader.GetString(12); //Получаем строку
                    text = MyDataReader.GetString(13); //Получаем строку                    
                    phone = MyDataReader.GetString(14); //Получаем строку
                    id = MyDataReader.GetString(0); //Получаем id
                    if (!MyDataReader.IsDBNull(20))
                    {
                        cost = MyDataReader.GetInt32(20); //Получаем строку
                    }
                    else
                    {
                        cost = 0;
                    }

                    if (!MyDataReader.IsDBNull(21))
                    {
                        costStr = MyDataReader.GetString(22);//Получаем строку
                    }
                    else
                    {
                        costStr = "";
                    }
                    //toshow = MyDataReader.GetUInt16(9); //Получаем строку
                    //catalog = MyDataReader.GetString(11); //Получаем строку


                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 1;
                    //wdApp.Selection.Font.Bold = 1;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.TypeText(header);
                    if (cost != 0)
                    {
                        wdApp.Selection.TypeText(" " + cost.ToString("N", nfi));
                        wdApp.Selection.TypeText(" ");
                        wdApp.Selection.TypeText(" руб." + costStr);
                    }
                    wdApp.Selection.TypeText(" ");
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeText("- " + text + " ");
                    wdApp.Selection.TypeText(" " + phone);
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeParagraph();
                    //wdApp.Selection.TypeParagraph();
                    CommandText = "UPDATE `" + dbname + "`.`ob` SET `secondRub`='" + idRub + "' WHERE `ID_OB`='" + id + "'";
                    //MessageBox.Show(CommandText);
                    updateCommand.CommandText = CommandText;
                    int res = updateCommand.ExecuteNonQuery();
                    col++;
                    if (checkBox2.Checked)
                    {
                        //CommandText = "UPDATE `ads_paper`.`cards` SET `toshow`=" + (toshow - 1).ToString() + " WHERE `date`='" + date + "'";                        
                        //MessageBox.Show(CommandText);

                        //MySqlCommand updateCommand = new MySqlCommand(CommandText, myConnection2);
                        //int res = updateCommand.ExecuteNonQuery();
                    }

                }

                MyDataReader.Close();
                myConnection2.Close();
                myConnection.Close(); //Обязательно закрываем соединение!

                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                wdApp.Selection.TypeText("СДАМ В АРЕНДУ");
                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                CommandText = "select * from " + dbname + ".ob where (`KOD_R` = '1' AND (`KOD_PR` = '1' OR `KOD_PR` = '2' OR `KOD_PR` = '3' OR `KOD_PR` = '13') AND `KOD_PPR` = '3') OR (`KOD_R` = '1' OR `KOD_PR` = '9') AND `KOL_P` > '0' ORDER BY K_WORD";
                Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;

                //Переменная Connect - это строка подключения в которой:
                //БАЗА - Имя базы в MySQL
                //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
                //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
                //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL

                myConnection = new MySqlConnection(Connect);
                myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open(); //Устанавливаем соединение с базой данных.

                myConnection2 = new MySqlConnection(Connect);
                updateCommand = new MySqlCommand(CommandText, myConnection2);
                myConnection2.Open();
                // Gets a NumberFormatInfo associated with the en-US culture.
                nfi = new CultureInfo("en-US", false).NumberFormat;

                nfi.NumberDecimalDigits = 0;
                nfi.NumberGroupSeparator = " ";

                nfi.PositiveSign = "";

                MyDataReader = myCommand.ExecuteReader();

                while (MyDataReader.Read())
                {

                    header = MyDataReader.GetString(12); //Получаем строку
                    text = MyDataReader.GetString(13); //Получаем строку                    
                    phone = MyDataReader.GetString(14); //Получаем строку
                    id = MyDataReader.GetString(0); //Получаем id
                    if (!MyDataReader.IsDBNull(20))
                    {
                        cost = MyDataReader.GetInt32(20); //Получаем строку
                    }
                    else
                    {
                        cost = 0;
                    }

                    if (!MyDataReader.IsDBNull(21))
                    {
                        costStr = MyDataReader.GetString(22);//Получаем строку
                    }
                    else
                    {
                        costStr = "";
                    }
                    //toshow = MyDataReader.GetUInt16(9); //Получаем строку
                    //catalog = MyDataReader.GetString(11); //Получаем строку


                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 1;
                    //wdApp.Selection.Font.Bold = 1;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.TypeText(header);
                    if (cost != 0)
                    {
                        wdApp.Selection.TypeText(" " + cost.ToString("N", nfi));
                        wdApp.Selection.TypeText(" ");
                        wdApp.Selection.TypeText(" руб." + costStr);
                    }
                    wdApp.Selection.TypeText(" ");
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeText("- " + text + " ");
                    wdApp.Selection.TypeText(" " + phone);
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeParagraph();
                    //wdApp.Selection.TypeParagraph();
                    CommandText = "UPDATE `" + dbname + "`.`ob` SET `secondRub`='" + idRub + "' WHERE `ID_OB`='" + id + "'";
                    //MessageBox.Show(CommandText);
                    updateCommand.CommandText = CommandText;
                    int res = updateCommand.ExecuteNonQuery();
                    col++;
                    if (checkBox2.Checked)
                    {
                        //CommandText = "UPDATE `ads_paper`.`cards` SET `toshow`=" + (toshow - 1).ToString() + " WHERE `date`='" + date + "'";                        
                        //MessageBox.Show(CommandText);

                        //MySqlCommand updateCommand = new MySqlCommand(CommandText, myConnection2);
                        //int res = updateCommand.ExecuteNonQuery();
                    }

                }

                MyDataReader.Close();
                myConnection2.Close();
                myConnection.Close(); //Обязательно закрываем соединение!


                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                wdApp.Selection.TypeText("ДРУГАЯ НЕДВИЖИМОСТЬ");
                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                CommandText = "select * from " + dbname + ".ob where `KOD_R` = '1' AND `KOD_PR` = '14' AND `KOD_PPR` = '3' AND `KOL_P` > '0' ORDER BY K_WORD";
                Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;

                //Переменная Connect - это строка подключения в которой:
                //БАЗА - Имя базы в MySQL
                //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
                //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
                //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL

                myConnection = new MySqlConnection(Connect);
                myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open(); //Устанавливаем соединение с базой данных.

                myConnection2 = new MySqlConnection(Connect);
                updateCommand = new MySqlCommand(CommandText, myConnection2);
                myConnection2.Open();
                // Gets a NumberFormatInfo associated with the en-US culture.
                nfi = new CultureInfo("en-US", false).NumberFormat;

                nfi.NumberDecimalDigits = 0;
                nfi.NumberGroupSeparator = " ";

                nfi.PositiveSign = "";

                MyDataReader = myCommand.ExecuteReader();

                while (MyDataReader.Read())
                {

                    header = MyDataReader.GetString(12); //Получаем строку
                    text = MyDataReader.GetString(13); //Получаем строку                    
                    phone = MyDataReader.GetString(14); //Получаем строку
                    id = MyDataReader.GetString(0); //Получаем id
                    if (!MyDataReader.IsDBNull(20))
                    {
                        cost = MyDataReader.GetInt32(20); //Получаем строку
                    }
                    else
                    {
                        cost = 0;
                    }

                    if (!MyDataReader.IsDBNull(21))
                    {
                        costStr = MyDataReader.GetString(22);//Получаем строку
                    }
                    else
                    {
                        costStr = "";
                    }
                    //toshow = MyDataReader.GetUInt16(9); //Получаем строку
                    //catalog = MyDataReader.GetString(11); //Получаем строку


                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 1;
                    //wdApp.Selection.Font.Bold = 1;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.TypeText(header);
                    if (cost != 0)
                    {
                        wdApp.Selection.TypeText(" " + cost.ToString("N", nfi));
                        wdApp.Selection.TypeText(" ");
                        wdApp.Selection.TypeText(" руб." + costStr);
                    }
                    wdApp.Selection.TypeText(" ");
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeText("- " + text + " ");
                    wdApp.Selection.TypeText(" " + phone);
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeParagraph();
                    //wdApp.Selection.TypeParagraph();
                    CommandText = "UPDATE `" + dbname + "`.`ob` SET `secondRub`='" + idRub + "' WHERE `ID_OB`='" + id + "'";
                    //MessageBox.Show(CommandText);
                    updateCommand.CommandText = CommandText;
                    int res = updateCommand.ExecuteNonQuery();
                    col++;
                    if (checkBox2.Checked)
                    {
                        //CommandText = "UPDATE `ads_paper`.`cards` SET `toshow`=" + (toshow - 1).ToString() + " WHERE `date`='" + date + "'";                        
                        //MessageBox.Show(CommandText);

                        //MySqlCommand updateCommand = new MySqlCommand(CommandText, myConnection2);
                        //int res = updateCommand.ExecuteNonQuery();
                    }

                }

                MyDataReader.Close();
                myConnection2.Close();
                myConnection.Close(); //Обязательно закрываем соединение!

                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                wdApp.Selection.TypeText("ВОЗЬМУ В АРЕНДУ (СНИМУ)");
                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                wdApp.Selection.TypeText("ЖИЛЬЕ");
                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                CommandText = "select * from " + dbname + ".ob where (`KOD_R` = '1' AND (`KOD_PR` = '1' OR `KOD_PR` = '2' OR `KOD_PR` = '3' OR `KOD_PR` = '13') AND `KOD_PPR` = '4') OR (`KOD_R` = '1' OR `KOD_PR` = '10') AND `KOL_P` > '0' ORDER BY K_WORD";
                Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;

                //Переменная Connect - это строка подключения в которой:
                //БАЗА - Имя базы в MySQL
                //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
                //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
                //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL

                myConnection = new MySqlConnection(Connect);
                myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open(); //Устанавливаем соединение с базой данных.

                myConnection2 = new MySqlConnection(Connect);
                updateCommand = new MySqlCommand(CommandText, myConnection2);
                myConnection2.Open();
                // Gets a NumberFormatInfo associated with the en-US culture.
                nfi = new CultureInfo("en-US", false).NumberFormat;

                nfi.NumberDecimalDigits = 0;
                nfi.NumberGroupSeparator = " ";

                nfi.PositiveSign = "";

                MyDataReader = myCommand.ExecuteReader();

                while (MyDataReader.Read())
                {

                    header = MyDataReader.GetString(12); //Получаем строку
                    text = MyDataReader.GetString(13); //Получаем строку                    
                    phone = MyDataReader.GetString(14); //Получаем строку
                    id = MyDataReader.GetString(0); //Получаем id
                    if (!MyDataReader.IsDBNull(20))
                    {
                        cost = MyDataReader.GetInt32(20); //Получаем строку
                    }
                    else
                    {
                        cost = 0;
                    }

                    if (!MyDataReader.IsDBNull(21))
                    {
                        costStr = MyDataReader.GetString(22);//Получаем строку
                    }
                    else
                    {
                        costStr = "";
                    }
                    //toshow = MyDataReader.GetUInt16(9); //Получаем строку
                    //catalog = MyDataReader.GetString(11); //Получаем строку


                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 1;
                    //wdApp.Selection.Font.Bold = 1;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.TypeText(header);
                    if (cost != 0)
                    {
                        wdApp.Selection.TypeText(" " + cost.ToString("N", nfi));
                        wdApp.Selection.TypeText(" ");
                        wdApp.Selection.TypeText(" руб." + costStr);
                    }
                    wdApp.Selection.TypeText(" ");
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeText("- " + text + " ");
                    wdApp.Selection.TypeText(" " + phone);
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeParagraph();
                    //wdApp.Selection.TypeParagraph();
                    CommandText = "UPDATE `" + dbname + "`.`ob` SET `secondRub`='" + idRub + "' WHERE `ID_OB`='" + id + "'";
                    //MessageBox.Show(CommandText);
                    updateCommand.CommandText = CommandText;
                    int res = updateCommand.ExecuteNonQuery();
                    col++;
                    if (checkBox2.Checked)
                    {
                        //CommandText = "UPDATE `ads_paper`.`cards` SET `toshow`=" + (toshow - 1).ToString() + " WHERE `date`='" + date + "'";                        
                        //MessageBox.Show(CommandText);

                        //MySqlCommand updateCommand = new MySqlCommand(CommandText, myConnection2);
                        //int res = updateCommand.ExecuteNonQuery();
                    }

                }

                MyDataReader.Close();
                myConnection2.Close();
                myConnection.Close(); //Обязательно закрываем соединение!

                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                wdApp.Selection.TypeText("ДРУГАЯ НЕДВИЖИМОСТЬ");
                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                CommandText = "select * from " + dbname + ".ob where `KOD_R` = '1' AND `KOD_PR` = '14' AND `KOD_PPR` = '4' AND `KOL_P` > '0' ORDER BY K_WORD";
                Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;

                //Переменная Connect - это строка подключения в которой:
                //БАЗА - Имя базы в MySQL
                //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
                //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
                //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL

                myConnection = new MySqlConnection(Connect);
                myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open(); //Устанавливаем соединение с базой данных.

                myConnection2 = new MySqlConnection(Connect);
                updateCommand = new MySqlCommand(CommandText, myConnection2);
                myConnection2.Open();
                // Gets a NumberFormatInfo associated with the en-US culture.
                nfi = new CultureInfo("en-US", false).NumberFormat;

                nfi.NumberDecimalDigits = 0;
                nfi.NumberGroupSeparator = " ";

                nfi.PositiveSign = "";

                MyDataReader = myCommand.ExecuteReader();

                while (MyDataReader.Read())
                {

                    header = MyDataReader.GetString(12); //Получаем строку
                    text = MyDataReader.GetString(13); //Получаем строку                    
                    phone = MyDataReader.GetString(14); //Получаем строку
                    id = MyDataReader.GetString(0); //Получаем id
                    if (!MyDataReader.IsDBNull(20))
                    {
                        cost = MyDataReader.GetInt32(20); //Получаем строку
                    }
                    else
                    {
                        cost = 0;
                    }

                    if (!MyDataReader.IsDBNull(21))
                    {
                        costStr = MyDataReader.GetString(22);//Получаем строку
                    }
                    else
                    {
                        costStr = "";
                    }
                    //toshow = MyDataReader.GetUInt16(9); //Получаем строку
                    //catalog = MyDataReader.GetString(11); //Получаем строку


                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 1;
                    //wdApp.Selection.Font.Bold = 1;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.TypeText(header);
                    if (cost != 0)
                    {
                        wdApp.Selection.TypeText(" " + cost.ToString("N", nfi));
                        wdApp.Selection.TypeText(" ");
                        wdApp.Selection.TypeText(" руб." + costStr);
                    }
                    wdApp.Selection.TypeText(" ");
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeText("- " + text + " ");
                    wdApp.Selection.TypeText(" " + phone);
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeParagraph();
                    //wdApp.Selection.TypeParagraph();
                    CommandText = "UPDATE `" + dbname + "`.`ob` SET `secondRub`='" + idRub + "' WHERE `ID_OB`='" + id + "'";
                    //MessageBox.Show(CommandText);
                    updateCommand.CommandText = CommandText;
                    int res = updateCommand.ExecuteNonQuery();
                    col++;
                    if (checkBox2.Checked)
                    {
                        //CommandText = "UPDATE `ads_paper`.`cards` SET `toshow`=" + (toshow - 1).ToString() + " WHERE `date`='" + date + "'";                        
                        //MessageBox.Show(CommandText);

                        //MySqlCommand updateCommand = new MySqlCommand(CommandText, myConnection2);
                        //int res = updateCommand.ExecuteNonQuery();
                    }

                }

                MyDataReader.Close();
                myConnection2.Close();
                myConnection.Close(); //Обязательно закрываем соединение!


                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                wdApp.Selection.TypeText("ПОКУПАЮ");
                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                CommandText = "select * from " + dbname + ".ob where (`KOD_R` = '1' AND (`KOD_PR` = '1' OR `KOD_PR` = '2' OR `KOD_PR` = '3' OR `KOD_PR` = '13'  OR `KOD_PR` = '14') AND `KOD_PPR` = '5') OR (`KOD_R` = '1' OR `KOD_PR` = '11') OR (`KOD_R` = '1' OR `KOD_PR` = '12' AND `KOD_PPR` = '3') AND `KOL_P` > '0' ORDER BY K_WORD";
                Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;

                //Переменная Connect - это строка подключения в которой:
                //БАЗА - Имя базы в MySQL
                //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
                //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
                //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL

                myConnection = new MySqlConnection(Connect);
                myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open(); //Устанавливаем соединение с базой данных.

                myConnection2 = new MySqlConnection(Connect);
                updateCommand = new MySqlCommand(CommandText, myConnection2);
                myConnection2.Open();
                // Gets a NumberFormatInfo associated with the en-US culture.
                nfi = new CultureInfo("en-US", false).NumberFormat;

                nfi.NumberDecimalDigits = 0;
                nfi.NumberGroupSeparator = " ";

                nfi.PositiveSign = "";

                MyDataReader = myCommand.ExecuteReader();

                while (MyDataReader.Read())
                {

                    header = MyDataReader.GetString(12); //Получаем строку
                    text = MyDataReader.GetString(13); //Получаем строку                    
                    phone = MyDataReader.GetString(14); //Получаем строку
                    id = MyDataReader.GetString(0); //Получаем id
                    if (!MyDataReader.IsDBNull(20))
                    {
                        cost = MyDataReader.GetInt32(20); //Получаем строку
                    }
                    else
                    {
                        cost = 0;
                    }

                    if (!MyDataReader.IsDBNull(21))
                    {
                        costStr = MyDataReader.GetString(22);//Получаем строку
                    }
                    else
                    {
                        costStr = "";
                    }
                    //toshow = MyDataReader.GetUInt16(9); //Получаем строку
                    //catalog = MyDataReader.GetString(11); //Получаем строку


                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 1;
                    //wdApp.Selection.Font.Bold = 1;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.TypeText(header);
                    if (cost != 0)
                    {
                        wdApp.Selection.TypeText(" " + cost.ToString("N", nfi));
                        wdApp.Selection.TypeText(" ");
                        wdApp.Selection.TypeText(" руб." + costStr);
                    }
                    wdApp.Selection.TypeText(" ");
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeText("- " + text + " ");
                    wdApp.Selection.TypeText(" " + phone);
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeParagraph();
                    //wdApp.Selection.TypeParagraph();
                    CommandText = "UPDATE `" + dbname + "`.`ob` SET `secondRub`='" + idRub + "' WHERE `ID_OB`='" + id + "'";
                    //MessageBox.Show(CommandText);
                    updateCommand.CommandText = CommandText;
                    int res = updateCommand.ExecuteNonQuery();
                    col++;
                    if (checkBox2.Checked)
                    {
                        //CommandText = "UPDATE `ads_paper`.`cards` SET `toshow`=" + (toshow - 1).ToString() + " WHERE `date`='" + date + "'";                        
                        //MessageBox.Show(CommandText);

                        //MySqlCommand updateCommand = new MySqlCommand(CommandText, myConnection2);
                        //int res = updateCommand.ExecuteNonQuery();
                    }

                }

                MyDataReader.Close();
                myConnection2.Close();
                myConnection.Close(); //Обязательно закрываем соединение!

                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                wdApp.Selection.TypeText("@@ 2");
                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                wdApp.Selection.TypeText("АВТОРЫНОК");
                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                wdApp.Selection.TypeText("ПРОДАЮ");
                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                wdApp.Selection.TypeText("ЛЕГКОВЫЕ А/М И ВНЕДОРОЖНИКИ");
                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                CommandText = "select * from " + dbname + ".ob where `KOD_R` = '2' OR `KOD_PR` = '1' AND `KOD_PPR` = '1' AND `KOL_P` > '0' ORDER BY K_WORD";
                Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;

                //Переменная Connect - это строка подключения в которой:
                //БАЗА - Имя базы в MySQL
                //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
                //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
                //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL

                myConnection = new MySqlConnection(Connect);
                myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open(); //Устанавливаем соединение с базой данных.

                myConnection2 = new MySqlConnection(Connect);
                updateCommand = new MySqlCommand(CommandText, myConnection2);
                myConnection2.Open();
                // Gets a NumberFormatInfo associated with the en-US culture.
                nfi = new CultureInfo("en-US", false).NumberFormat;

                nfi.NumberDecimalDigits = 0;
                nfi.NumberGroupSeparator = " ";

                nfi.PositiveSign = "";

                MyDataReader = myCommand.ExecuteReader();

                while (MyDataReader.Read())
                {

                    header = MyDataReader.GetString(12); //Получаем строку
                    text = MyDataReader.GetString(13); //Получаем строку                    
                    phone = MyDataReader.GetString(14); //Получаем строку
                    id = MyDataReader.GetString(0); //Получаем id
                    if (!MyDataReader.IsDBNull(20))
                    {
                        cost = MyDataReader.GetInt32(20); //Получаем строку
                    }
                    else
                    {
                        cost = 0;
                    }

                    if (!MyDataReader.IsDBNull(21))
                    {
                        costStr = MyDataReader.GetString(22);//Получаем строку
                    }
                    else
                    {
                        costStr = "";
                    }
                    //toshow = MyDataReader.GetUInt16(9); //Получаем строку
                    //catalog = MyDataReader.GetString(11); //Получаем строку


                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 1;
                    //wdApp.Selection.Font.Bold = 1;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.TypeText(header);
                    if (cost != 0)
                    {
                        wdApp.Selection.TypeText(" " + cost.ToString("N", nfi));
                        wdApp.Selection.TypeText(" ");
                        wdApp.Selection.TypeText(" руб." + costStr);
                    }
                    wdApp.Selection.TypeText(" ");
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeText("- " + text + " ");
                    wdApp.Selection.TypeText(" " + phone);
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeParagraph();
                    //wdApp.Selection.TypeParagraph();
                    CommandText = "UPDATE `" + dbname + "`.`ob` SET `secondRub`='" + idRub + "' WHERE `ID_OB`='" + id + "'";
                    //MessageBox.Show(CommandText);
                    updateCommand.CommandText = CommandText;
                    int res = updateCommand.ExecuteNonQuery();
                    col++;
                    if (checkBox2.Checked)
                    {
                        //CommandText = "UPDATE `ads_paper`.`cards` SET `toshow`=" + (toshow - 1).ToString() + " WHERE `date`='" + date + "'";                        
                        //MessageBox.Show(CommandText);

                        //MySqlCommand updateCommand = new MySqlCommand(CommandText, myConnection2);
                        //int res = updateCommand.ExecuteNonQuery();
                    }

                }

                MyDataReader.Close();
                myConnection2.Close();
                myConnection.Close(); //Обязательно закрываем соединение!

                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                wdApp.Selection.TypeText("ДРУГИЕ ТРАНСПОРТНЫЕ СРЕДСТВА");
                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                CommandText = "select * from " + dbname + ".ob where (`KOD_R` = '2' OR `KOD_PR` = '2' AND `KOD_PPR` = '1') OR (`KOD_R` = '2' OR `KOD_PR` = '3' AND `KOD_PPR` = '1') OR(`KOD_R` = '2' OR `KOD_PR` = '4' AND `KOD_PPR` = '1') AND `KOL_P` > '0' ORDER BY K_WORD";
                Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;

                //Переменная Connect - это строка подключения в которой:
                //БАЗА - Имя базы в MySQL
                //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
                //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
                //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL

                myConnection = new MySqlConnection(Connect);
                myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open(); //Устанавливаем соединение с базой данных.

                myConnection2 = new MySqlConnection(Connect);
                updateCommand = new MySqlCommand(CommandText, myConnection2);
                myConnection2.Open();
                // Gets a NumberFormatInfo associated with the en-US culture.
                nfi = new CultureInfo("en-US", false).NumberFormat;

                nfi.NumberDecimalDigits = 0;
                nfi.NumberGroupSeparator = " ";

                nfi.PositiveSign = "";

                MyDataReader = myCommand.ExecuteReader();

                while (MyDataReader.Read())
                {

                    header = MyDataReader.GetString(12); //Получаем строку
                    text = MyDataReader.GetString(13); //Получаем строку                    
                    phone = MyDataReader.GetString(14); //Получаем строку
                    id = MyDataReader.GetString(0); //Получаем id
                    if (!MyDataReader.IsDBNull(20))
                    {
                        cost = MyDataReader.GetInt32(20); //Получаем строку
                    }
                    else
                    {
                        cost = 0;
                    }

                    if (!MyDataReader.IsDBNull(21))
                    {
                        costStr = MyDataReader.GetString(22);//Получаем строку
                    }
                    else
                    {
                        costStr = "";
                    }
                    //toshow = MyDataReader.GetUInt16(9); //Получаем строку
                    //catalog = MyDataReader.GetString(11); //Получаем строку


                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 1;
                    //wdApp.Selection.Font.Bold = 1;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.TypeText(header);
                    if (cost != 0)
                    {
                        wdApp.Selection.TypeText(" " + cost.ToString("N", nfi));
                        wdApp.Selection.TypeText(" ");
                        wdApp.Selection.TypeText(" руб." + costStr);
                    }
                    wdApp.Selection.TypeText(" ");
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeText("- " + text + " ");
                    wdApp.Selection.TypeText(" " + phone);
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeParagraph();
                    //wdApp.Selection.TypeParagraph();
                    CommandText = "UPDATE `" + dbname + "`.`ob` SET `secondRub`='" + idRub + "' WHERE `ID_OB`='" + id + "'";
                    //MessageBox.Show(CommandText);
                    updateCommand.CommandText = CommandText;
                    int res = updateCommand.ExecuteNonQuery();
                    col++;
                    if (checkBox2.Checked)
                    {
                        //CommandText = "UPDATE `ads_paper`.`cards` SET `toshow`=" + (toshow - 1).ToString() + " WHERE `date`='" + date + "'";                        
                        //MessageBox.Show(CommandText);

                        //MySqlCommand updateCommand = new MySqlCommand(CommandText, myConnection2);
                        //int res = updateCommand.ExecuteNonQuery();
                    }

                }

                MyDataReader.Close();
                myConnection2.Close();
                myConnection.Close(); //Обязательно закрываем соединение!

                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                wdApp.Selection.TypeText("ЗАПЧАСТИ И ПРИНАДЛЕЖНОСТИ");
                wdApp.Selection.TypeParagraph(); wdApp.Selection.TypeParagraph();
                CommandText = "select * from " + dbname + ".ob where (`KOD_R` = '2' OR `KOD_PR` = '5' AND `KOD_PPR` = '1') AND `KOL_P` > '0' ORDER BY K_WORD";
                Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;

                //Переменная Connect - это строка подключения в которой:
                //БАЗА - Имя базы в MySQL
                //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
                //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
                //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL

                myConnection = new MySqlConnection(Connect);
                myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open(); //Устанавливаем соединение с базой данных.

                myConnection2 = new MySqlConnection(Connect);
                updateCommand = new MySqlCommand(CommandText, myConnection2);
                myConnection2.Open();
                // Gets a NumberFormatInfo associated with the en-US culture.
                nfi = new CultureInfo("en-US", false).NumberFormat;

                nfi.NumberDecimalDigits = 0;
                nfi.NumberGroupSeparator = " ";

                nfi.PositiveSign = "";

                MyDataReader = myCommand.ExecuteReader();

                while (MyDataReader.Read())
                {

                    header = MyDataReader.GetString(12); //Получаем строку
                    text = MyDataReader.GetString(13); //Получаем строку                    
                    phone = MyDataReader.GetString(14); //Получаем строку
                    id = MyDataReader.GetString(0); //Получаем id
                    if (!MyDataReader.IsDBNull(20))
                    {
                        cost = MyDataReader.GetInt32(20); //Получаем строку
                    }
                    else
                    {
                        cost = 0;
                    }

                    if (!MyDataReader.IsDBNull(21))
                    {
                        costStr = MyDataReader.GetString(22);//Получаем строку
                    }
                    else
                    {
                        costStr = "";
                    }
                    //toshow = MyDataReader.GetUInt16(9); //Получаем строку
                    //catalog = MyDataReader.GetString(11); //Получаем строку


                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 1;
                    //wdApp.Selection.Font.Bold = 1;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.TypeText(header);
                    if (cost != 0)
                    {
                        wdApp.Selection.TypeText(" " + cost.ToString("N", nfi));
                        wdApp.Selection.TypeText(" ");
                        wdApp.Selection.TypeText(" руб." + costStr);
                    }
                    wdApp.Selection.TypeText(" ");
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Select();
                    wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeText("- " + text + " ");
                    wdApp.Selection.TypeText(" " + phone);
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeParagraph();
                    //wdApp.Selection.TypeParagraph();
                    CommandText = "UPDATE `" + dbname + "`.`ob` SET `secondRub`='" + idRub + "' WHERE `ID_OB`='" + id + "'";
                    //MessageBox.Show(CommandText);
                    updateCommand.CommandText = CommandText;
                    int res = updateCommand.ExecuteNonQuery();
                    col++;
                    if (checkBox2.Checked)
                    {
                        //CommandText = "UPDATE `ads_paper`.`cards` SET `toshow`=" + (toshow - 1).ToString() + " WHERE `date`='" + date + "'";                        
                        //MessageBox.Show(CommandText);

                        //MySqlCommand updateCommand = new MySqlCommand(CommandText, myConnection2);
                        //int res = updateCommand.ExecuteNonQuery();
                    }

                }

                MyDataReader.Close();
                myConnection2.Close();
                myConnection.Close(); //Обязательно закрываем соединение!

             }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

            }

        }

        private void PrintAdsOB(int[] catalogNum, ref Microsoft.Office.Interop.Word.Application wdApp)
        {
            try
            {
                bool aut=false;
                if (catalogNum[1] == 2)
                {
                  if (catalogNum[2] == 1)
                  {
                      if (catalogNum[3] == 1)
                      {
                          aut = true;
                          PrintAuto(catalogNum, ref wdApp);
                      }
                  }
                }

                if(!aut)
                {
                    string CommandText = "select * from " + dbname + ".ob where `KOD_R` = '" + catalogNum[1] + "' AND `KOD_PR` = '" + catalogNum[2] + "' and `KOD_PPR` = '" + catalogNum[3] + "' AND `KOD_PPPR` = '" + catalogNum[4] + "' AND `KOL_P` > '0' and DATEPOST='2012-01-01' ORDER BY K_WORD";
                    string Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;
                    //todo: Проверить по выписаным ошибкам
                    //Переменная Connect - это строка подключения в которой:
                    //БАЗА - Имя базы в MySQL
                    //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
                    //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
                    //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL

                    MySqlConnection myConnection = new MySqlConnection(Connect);
                    MySqlCommand myCommand = new MySqlCommand(CommandText, myConnection);
                    myConnection.Open(); //Устанавливаем соединение с базой данных.

                    MySqlConnection myConnection2 = new MySqlConnection(Connect);
                    MySqlCommand updateCommand = new MySqlCommand(CommandText, myConnection2);
                    myConnection2.Open();
                    // Gets a NumberFormatInfo associated with the en-US culture.
                    NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

                    nfi.NumberDecimalDigits = 0;
                    nfi.NumberGroupSeparator = " ";

                    nfi.PositiveSign = "";
                    MySqlDataReader MyDataReader;
                    //string date; //Получаем дату
                    string phone; //Получаем строку
                    string header; //Получаем строку
                    string text; //Получаем строку
                    //int cost; //Получаем строку
                    //string catalog; //Получаем строку
                    //int toshow; //Получаем строку
                    string id;
                    //string costStr;
                    MyDataReader = myCommand.ExecuteReader();

                    while (MyDataReader.Read())
                    {

                        header = MyDataReader.GetString(12); //Получаем строку
                        text = MyDataReader.GetString(13); //Получаем строку                    
                        phone = MyDataReader.GetString(14); //Получаем строку
                        id = MyDataReader.GetString(0); //Получаем id

                        //toshow = MyDataReader.GetUInt16(9); //Получаем строку
                        //catalog = MyDataReader.GetString(11); //Получаем строку


                        wdApp.Selection.Font.Bold = 1;

                        wdApp.Selection.TypeText(header);
                        //wdApp.Selection.TypeText(" ");
                        wdApp.Selection.Font.Bold = 0;
                        if (text != " ")
                        {
                            wdApp.Selection.TypeText(" " + text);
                        }
                        wdApp.Selection.TypeText(" " + phone);
                        wdApp.Selection.TypeParagraph();
                        //wdApp.Selection.TypeParagraph();

                        col++;
                        if (checkBox2.Checked)
                        {
                            //CommandText = "UPDATE `ads_paper`.`cards` SET `toshow`=" + (toshow - 1).ToString() + " WHERE `date`='" + date + "'";                        
                            //MessageBox.Show(CommandText);

                            //MySqlCommand updateCommand = new MySqlCommand(CommandText, myConnection2);
                            //int res = updateCommand.ExecuteNonQuery();
                        }

                    }
                    MyDataReader.Close();
                    myCommand.CommandText = "select * from " + dbname + ".ob where `KOD_R` = '" + catalogNum[1] + "' AND `KOD_PR` = '" + catalogNum[2] + "' and `KOD_PPR` = '" + catalogNum[3] + "' AND `KOD_PPPR` = '" + catalogNum[4] + "' AND `KOL_P` > '0' and NOT DATEPOST= '2012-01-01' ORDER BY K_WORD";
                    MyDataReader = myCommand.ExecuteReader();

                    while (MyDataReader.Read())
                    {

                        header = MyDataReader.GetString(12); //Получаем строку
                        text = MyDataReader.GetString(13); //Получаем строку                    
                        phone = MyDataReader.GetString(14); //Получаем строку
                        id = MyDataReader.GetString(0); //Получаем id

                        //toshow = MyDataReader.GetUInt16(9); //Получаем строку
                        //catalog = MyDataReader.GetString(11); //Получаем строку


                        wdApp.Selection.Font.Bold = 1;

                        wdApp.Selection.TypeText(header);
                        //wdApp.Selection.TypeText(" ");
                        wdApp.Selection.Font.Bold = 0;
                        if (text != " ")
                        {
                            wdApp.Selection.TypeText(" " + text);
                        }
                        wdApp.Selection.TypeText(" " + phone);
                        wdApp.Selection.TypeParagraph();
                        //wdApp.Selection.TypeParagraph();

                        col++;
                        if (checkBox2.Checked)
                        {
                            //CommandText = "UPDATE `ads_paper`.`cards` SET `toshow`=" + (toshow - 1).ToString() + " WHERE `date`='" + date + "'";                        
                            //MessageBox.Show(CommandText);

                            //MySqlCommand updateCommand = new MySqlCommand(CommandText, myConnection2);
                            //int res = updateCommand.ExecuteNonQuery();
                        }

                    }
                    MyDataReader.Close();
                    
                    myConnection2.Close();
                    myConnection.Close(); //Обязательно закрываем соединение!
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        private void PrintAuto(int[] catalogNum, ref Microsoft.Office.Interop.Word.Application wdApp)
        {
        try
            {
            string[] listOfAuto = {
            "ВАЗ",
            "ГАЗ", 
            "Москвич",
            "Alfa", "Audi","BMW", "Great Wall","Daihatsu","Daewoo","Kia","Chrysler","Lexus","Mazda","Mercedes-Benz",
            "Mitsubishi",
            "Nissan",        
            "Opel",
      
            "Peugeot",
 
            "Pontiac",
        
            "Renault",
            "Rover",
            
            "Citroen",
           
            "Subaru",
            
            "Toyota",
            
            "Fiat",
           
            "Ford",
          
            "Hyundai ",
            "Hafei",
           
            "Honda",
            
            "Chery",
            "Chevrolet",

            
            "Infiniti Series",
            
            "Rover",
            "Cadillac",
            "Dodge",

            "Ferrari",

            "Jeep",
      
            "Porshe",
            "Saab",
          
            "Suzuki",
            "Skoda ",
            "Volkswagen",
            "Volvo"
        };
            foreach (string auto in listOfAuto)
            {

                
                string CommandText = "select * from " + dbname + ".ob where `KOD_R` = '" + catalogNum[1] + "' AND `KOD_PR` = '" + catalogNum[2] + "' and `KOD_PPR` = '" + catalogNum[3] + "' AND `KOD_PPPR` = '" + catalogNum[4] + "' AND `KOL_P` > '0' AND K_WORD like '%%"+auto+"%%' ORDER BY K_WORD";
                string Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;
                //todo: Проверить по выписаным ошибкам
                //Переменная Connect - это строка подключения в которой:
                //БАЗА - Имя базы в MySQL
                //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
                //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
                //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL

                MySqlConnection myConnection = new MySqlConnection(Connect);
                MySqlCommand myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open(); //Устанавливаем соединение с базой данных.

                MySqlConnection myConnection2 = new MySqlConnection(Connect);
                MySqlCommand updateCommand = new MySqlCommand(CommandText, myConnection2);
                myConnection2.Open();
                // Gets a NumberFormatInfo associated with the en-US culture.
                NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

                nfi.NumberDecimalDigits = 0;
                nfi.NumberGroupSeparator = " ";

                nfi.PositiveSign = "";
                MySqlDataReader MyDataReader;
                //string date; //Получаем дату
                string phone; //Получаем строку
                string header; //Получаем строку
                string text; //Получаем строку
                //int cost; //Получаем строку
                //string catalog; //Получаем строку
                //int toshow; //Получаем строку
                string id;
                //string costStr;
                MyDataReader = myCommand.ExecuteReader();
                if (MyDataReader.HasRows)
                { 
                //wdApp.Selection.TypeParagraph();
                wdApp.Selection.TypeParagraph();
                wdApp.Selection.Font.Bold = 1; 
                wdApp.Selection.TypeText(auto);
                wdApp.Selection.Font.Bold = 0;
                wdApp.Selection.TypeParagraph();
                wdApp.Selection.TypeParagraph();

                while (MyDataReader.Read())
                {

                    header = MyDataReader.GetString(12); //Получаем строку
                    text = MyDataReader.GetString(13); //Получаем строку                    
                    phone = MyDataReader.GetString(14); //Получаем строку
                    id = MyDataReader.GetString(0); //Получаем id

                    //toshow = MyDataReader.GetUInt16(9); //Получаем строку
                    //catalog = MyDataReader.GetString(11); //Получаем строку


                    wdApp.Selection.Font.Bold = 1;

                    wdApp.Selection.TypeText(header);
                    //wdApp.Selection.TypeText(" ");
                    wdApp.Selection.Font.Bold = 0;
                    if (text != " ")
                    {
                        wdApp.Selection.TypeText(" " + text);
                    }
                    wdApp.Selection.TypeText(" " + phone);
                    wdApp.Selection.TypeParagraph();
                    //wdApp.Selection.TypeParagraph();

                    col++;
                    if (checkBox2.Checked)
                    {
                        //CommandText = "UPDATE `ads_paper`.`cards` SET `toshow`=" + (toshow - 1).ToString() + " WHERE `date`='" + date + "'";                        
                        //MessageBox.Show(CommandText);

                        //MySqlCommand updateCommand = new MySqlCommand(CommandText, myConnection2);
                        //int res = updateCommand.ExecuteNonQuery();
                    }
                }

                }
                MyDataReader.Close();
                myConnection2.Close();
                myConnection.Close(); //Обязательно закрываем соединение!     
            }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

            }
}
        private void PrintAds(int[] catalogNum, ref Microsoft.Office.Interop.Word.Application wdApp)
        {

            string CommandText = "select * from " + dbname + ".ob where `KOD_R` = '" + catalogNum[1] + "' AND `KOD_PR` = '" + catalogNum[2] + "' and `KOD_PPR` = '" + catalogNum[3] + "' AND `KOD_PPPR` = '" + catalogNum[4] + "' AND `KOL_P` > '0' and cost > '0' ORDER BY cost";
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

                MySqlConnection myConnection2 = new MySqlConnection(Connect);
                MySqlCommand updateCommand = new MySqlCommand(CommandText, myConnection2);
                myConnection2.Open();
                // Gets a NumberFormatInfo associated with the en-US culture.
                NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

                nfi.NumberDecimalDigits = 0;
                nfi.NumberGroupSeparator = " ";

                nfi.PositiveSign = "";
                MySqlDataReader MyDataReader;
                //string date; //Получаем дату
                string phone; //Получаем строку
                string header; //Получаем строку
                string text; //Получаем строку
                int cost; //Получаем строку
                //string catalog; //Получаем строку
                //int toshow; //Получаем строку
                string id;
                string costStr;
                MyDataReader = myCommand.ExecuteReader();

                while (MyDataReader.Read())
                {
                    header = MyDataReader.GetString(12); //Получаем строку
                    text = MyDataReader.GetString(13); //Получаем строку                    
                    phone = MyDataReader.GetString(14); //Получаем строку
                    id = MyDataReader.GetString(0); //Получаем id
                    cost = MyDataReader.GetInt32(20); //Получаем строку
                    costStr = MyDataReader.GetString(22); //Получаем строку
                    
                    wdApp.Selection.Font.Bold = 1;
                    wdApp.Selection.TypeText("@@@" + cost.ToString("N", nfi));
                    wdApp.Selection.TypeText(" ");
          
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeText(" руб." + costStr);
                    //wdApp.Selection.Font.Bold = 1;
                    wdApp.Selection.TypeParagraph();
                 
                    wdApp.Selection.TypeText(header);
               
                    wdApp.Selection.TypeText(", " + text + " ");
                  //  wdApp.Selection.Select();
                  //  wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.TypeText(" " + phone);
                 //   wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeParagraph();
                    //wdApp.Selection.TypeParagraph();
                    CommandText = "UPDATE `" + dbname + "`.`ob` SET `secondRub`='" + idRub + "' WHERE `ID_OB`='" + id + "'";
                    //MessageBox.Show(CommandText);
                    updateCommand.CommandText = CommandText;
                    int res = updateCommand.ExecuteNonQuery();
                    col++;
                    
                    if (checkBox2.Checked)
                    {
                        //CommandText = "UPDATE `ads_paper`.`cards` SET `toshow`=" + (toshow - 1).ToString() + " WHERE `date`='" + date + "'";                        
                        //MessageBox.Show(CommandText);

                        //MySqlCommand updateCommand = new MySqlCommand(CommandText, myConnection2);
                        //int res = updateCommand.ExecuteNonQuery();
                    }

                }

                MyDataReader.Close();
                myConnection2.Close();
                myConnection.Close(); //Обязательно закрываем соединение!




                //wdApp.Selection.TypeText("l");
                // Print each node recursively.

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

            }

        }
        private void PrintAds8(int[] catalogNum, ref Microsoft.Office.Interop.Word.Application wdApp)
        {

            string CommandText = "select * from " + dbname + ".ob where `KOD_R` = '" + catalogNum[1] + "' AND `KOD_PR` = '" + catalogNum[2] + "' AND `KOL_P` > '0' and cost > '0' ORDER BY cost";
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

                MySqlConnection myConnection2 = new MySqlConnection(Connect);
                MySqlCommand updateCommand = new MySqlCommand(CommandText, myConnection2);
                myConnection2.Open();
                // Gets a NumberFormatInfo associated with the en-US culture.
                NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

                nfi.NumberDecimalDigits = 0;
                nfi.NumberGroupSeparator = " ";

                nfi.PositiveSign = "";
                MySqlDataReader MyDataReader;
                //string date; //Получаем дату
                string phone; //Получаем строку
                string header; //Получаем строку
                string text; //Получаем строку
                int cost; //Получаем строку
                //string catalog; //Получаем строку
                //int toshow; //Получаем строку
                string id;
                MyDataReader = myCommand.ExecuteReader();

                while (MyDataReader.Read())
                {

                    header = MyDataReader.GetString(12); //Получаем строку
                    text = MyDataReader.GetString(13); //Получаем строку                    
                    phone = MyDataReader.GetString(14); //Получаем строку
                    id = MyDataReader.GetString(0); //Получаем id
                    cost = MyDataReader.GetInt32(20); //Получаем строку
                    //toshow = MyDataReader.GetUInt16(9); //Получаем строку
                    //catalog = MyDataReader.GetString(11); //Получаем строку


                   // wdApp.Selection.ClearFormatting();
                    //wdApp.Selection.Select();
                    //wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 1;
                    wdApp.Selection.TypeText("@@@" + cost.ToString("N", nfi));
                    wdApp.Selection.TypeText(" ");
                    wdApp.Selection.Font.Bold = 0;
                   // wdApp.Selection.Select();
                   // wdApp.Selection.Characters.Last.Select();
                   // wdApp.Selection.ClearFormatting();
                    //wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeText(" руб.");
                    //wdApp.Selection.Font.Bold = 1;
                    wdApp.Selection.TypeParagraph();
                   // wdApp.Selection.Select();
                   // wdApp.Selection.Characters.Last.Select();
                   // wdApp.Selection.Font.Bold = 0;
                   // wdApp.Selection.ClearFormatting();
                    wdApp.Selection.TypeText(header);
                  //  wdApp.Selection.Font.Bold = 0;
                  //  wdApp.Selection.Select();
                  //  wdApp.Selection.Characters.Last.Select();
                  //  wdApp.Selection.Font.Bold = 0;
                  //  wdApp.Selection.ClearFormatting();
                    wdApp.Selection.TypeText(" - " + text + " ");
                  //  wdApp.Selection.Select();
                  //  wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.TypeText(" " + phone);
                  //  wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeParagraph();
                   // wdApp.Selection.TypeParagraph();
                    CommandText = "UPDATE `" + dbname + "`.`ob` SET `secondRub`='" + idRub + "' WHERE `ID_OB`='" + id + "'";
                    //MessageBox.Show(CommandText);
                    updateCommand.CommandText = CommandText;
                    int res = updateCommand.ExecuteNonQuery();
                    col++;
                    if (checkBox2.Checked)
                    {
                        //CommandText = "UPDATE `ads_paper`.`cards` SET `toshow`=" + (toshow - 1).ToString() + " WHERE `date`='" + date + "'";                        
                        //MessageBox.Show(CommandText);

                        //MySqlCommand updateCommand = new MySqlCommand(CommandText, myConnection2);
                        //int res = updateCommand.ExecuteNonQuery();
                    }

                }

                MyDataReader.Close();
                myConnection2.Close();
                myConnection.Close(); //Обязательно закрываем соединение!




                //wdApp.Selection.TypeText("l");
                // Print each node recursively.

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

            }

        }
        private void PrintAds9(int[] catalogNum, ref Microsoft.Office.Interop.Word.Application wdApp)
        {

            string CommandText = "select * from " + dbname + ".ob where `KOD_R` = '" + catalogNum[1] + "' AND `KOD_PR` = '" + catalogNum[2] + "' AND `KOL_P` > '0' and cost > '0' ORDER BY cost";
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

                MySqlConnection myConnection2 = new MySqlConnection(Connect);
                MySqlCommand updateCommand = new MySqlCommand(CommandText, myConnection2);
                myConnection2.Open();
                // Gets a NumberFormatInfo associated with the en-US culture.
                NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

                nfi.NumberDecimalDigits = 0;
                nfi.NumberGroupSeparator = " ";

                nfi.PositiveSign = "";
                MySqlDataReader MyDataReader;
                //string date; //Получаем дату
                string phone; //Получаем строку
                string header; //Получаем строку
                string text; //Получаем строку
                int cost; //Получаем строку
                //string catalog; //Получаем строку
                //int toshow; //Получаем строку
                string id;
                MyDataReader = myCommand.ExecuteReader();

                while (MyDataReader.Read())
                {

                    header = MyDataReader.GetString(12); //Получаем строку
                    text = MyDataReader.GetString(13); //Получаем строку                    
                    phone = MyDataReader.GetString(14); //Получаем строку
                    id = MyDataReader.GetString(0); //Получаем id
                    cost = MyDataReader.GetInt32(20); //Получаем строку
                    //toshow = MyDataReader.GetUInt16(9); //Получаем строку
                    //catalog = MyDataReader.GetString(11); //Получаем строку


                  // wdApp.Selection.ClearFormatting();
                   // wdApp.Selection.Select();
                  //  wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 1;
                    wdApp.Selection.TypeText("@@@" + cost.ToString("N", nfi));
                    wdApp.Selection.TypeText(" ");
                   // wdApp.Selection.Font.Bold = 0;
                   // wdApp.Selection.Select();
                  //  wdApp.Selection.Characters.Last.Select();
                  //  wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeText(" руб.");
                    //wdApp.Selection.Font.Bold = 1;
                    wdApp.Selection.TypeParagraph();
                   // wdApp.Selection.Select();
                  //  wdApp.Selection.Characters.Last.Select();
                    
                  //  wdApp.Selection.ClearFormatting();
                  //  wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeText(header);
                  //  wdApp.Selection.Font.Bold = 0;
                  //  wdApp.Selection.Select();
                  //  wdApp.Selection.Characters.Last.Select();
                  //  wdApp.Selection.Font.Bold = 0;
                  //  wdApp.Selection.ClearFormatting();
                    wdApp.Selection.TypeText(" - " + text + " ");
                  //  wdApp.Selection.Select();
                  //  wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.TypeText(" " + phone);
                  //  wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeParagraph();
                    //wdApp.Selection.TypeParagraph();
                    CommandText = "UPDATE `" + dbname + "`.`ob` SET `secondRub`='" + idRub + "' WHERE `ID_OB`='" + id + "'";
                    //MessageBox.Show(CommandText);
                    updateCommand.CommandText = CommandText;
                    int res = updateCommand.ExecuteNonQuery();
                    col++;
                    if (checkBox2.Checked)
                    {
                        //CommandText = "UPDATE `ads_paper`.`cards` SET `toshow`=" + (toshow - 1).ToString() + " WHERE `date`='" + date + "'";                        
                        //MessageBox.Show(CommandText);

                        //MySqlCommand updateCommand = new MySqlCommand(CommandText, myConnection2);
                        //int res = updateCommand.ExecuteNonQuery();
                    }

                }

                MyDataReader.Close();
                myConnection2.Close();
                myConnection.Close(); //Обязательно закрываем соединение!




                //wdApp.Selection.TypeText("l");
                // Print each node recursively.

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

            }

        }

        private void PrintAdsRSU(int first, int second, ref Microsoft.Office.Interop.Word.Application wdApp)
        {

            string CommandText = "select * from " + dbname + ".ob where `KOD_R` = '" + first + "' AND `KOD_PR` = '" + second + "'  AND `KOL_P` > '0' ORDER BY cost";
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

                MySqlConnection myConnection2 = new MySqlConnection(Connect);
                myConnection2.Open();

                MySqlDataReader MyDataReader;
                //string date; //Получаем дату
                string phone; //Получаем строку
                string header; //Получаем строку
                string text; //Получаем строку
               //string cost; //Получаем строку
                //string catalog; //Получаем строку
                //int toshow; //Получаем строку

                //StringBuilder t = new StringBuilder(text);
                //t.

                MyDataReader = myCommand.ExecuteReader();

                while (MyDataReader.Read())
                {

                    header = MyDataReader.GetString(12); //Получаем строку
                    text = MyDataReader.GetString(13); //Получаем строку                    
                    phone = MyDataReader.GetString(14); //Получаем строку
             
                    wdApp.Selection.Font.Bold = 1;
                    //wdApp.Selection.TypeParagraph();
                    wdApp.Selection.TypeText(header+ " ");
                 //   wdApp.Selection.Select();
                 //   wdApp.Selection.Characters.Last.Select();
                 //   wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Font.Bold = 0;                    
                    wdApp.Selection.TypeText(text + " ");
                //    wdApp.Selection.Select();
                 //   wdApp.Selection.Characters.Last.Select();
                //    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeText(phone);
                   
                    wdApp.Selection.TypeParagraph();
                    //wdApp.Selection.TypeParagraph();

                    col++;
                    if (checkBox2.Checked)
                    {

                        //CommandText = "UPDATE `ads_paper`.`cards` SET `toshow`=" + (toshow - 1).ToString() + " WHERE `date`='" + date + "'";                        
                        //MessageBox.Show(CommandText);

                        //MySqlCommand updateCommand = new MySqlCommand(CommandText, myConnection2);
                        //int res = updateCommand.ExecuteNonQuery();

                    }
                }

                MyDataReader.Close();
                myConnection2.Close();
                myConnection.Close(); //Обязательно закрываем соединение!




                //wdApp.Selection.TypeText("l");
                // Print each node recursively.

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

            }

        }
        public bool ReplaceTextWord(ref Microsoft.Office.Interop.Word.Application wdApp, string replaceText, string text)
        {
            try
            {
                if (text == null)
                {
                    return false;
                }

                object replaceAll = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;

                wdApp.Selection.Find.ClearFormatting();
                wdApp.Selection.Find.Text = replaceText;
                wdApp.Selection.Find.Replacement.ClearFormatting();
                wdApp.Selection.Find.Replacement.Text = text;

                bool result = wdApp.Selection.Find.Execute(
                             ref Missing, ref Missing, ref Missing, ref Missing, ref Missing,
                             ref Missing, ref Missing, ref Missing, ref Missing, ref Missing,
                             ref replaceAll, ref Missing, ref Missing, ref Missing, ref Missing);
                if (!result)
                {
                    return result;
                }
                else
                {
                    throw new Exception("1");
                    //return false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(monthCalendar1.SelectionRange.Start.ToShortDateString() + " - "+ monthCalendar1.SelectionRange.End.ToShortDateString());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                /* if (catalogsComboBox.SelectedIndex == 1)
                  {*/
                col = 0;
                if (DialogResult.OK == saveFileDialog1.ShowDialog())
                {
                    Microsoft.Office.Interop.Word.Application wdApp = new Microsoft.Office.Interop.Word.Application();
                    Microsoft.Office.Interop.Word.Document wdDoc = new Microsoft.Office.Interop.Word.Document();
                    wdDoc = wdApp.Documents.Add();

                    wdDoc.SpellingChecked = false;

                    wdDoc.ShowSpellingErrors = false;
                    wdApp.Selection.Font.Name = "Times New Roman";
                    wdApp.Selection.Font.Size = 12;
                    wdApp.ActiveDocument.Select();

                  

                    int[] catID = new int[5];
                    int parentID;
                    int real;
                    int sum;
                    PrintAdsRIO(catID, ref wdApp);
                    
                    ReplaceTextWord(ref wdApp, "  ", " ");
                    wdApp.ActiveDocument.SaveAs(saveFileDialog1.FileName + ".doc");
                    wdDoc.Close();
                    //wdApp.Documents.Close();
                    wdApp.Quit();
                    MessageBox.Show("Выгрузка завершена, выгружено: " + col.ToString() + " объявлений");
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }

        }

        private void ExportForm_Load(object sender, EventArgs e)
        {
            try
            {

                getCount();


        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void getCount()
        {
            string CommandText = "select count(*) from " + dbname + ".ob where kol_p >0";
            string Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;

            MySqlConnection myConnection = new MySqlConnection(Connect);
            MySqlCommand myCommand = new MySqlCommand(CommandText, myConnection);
            myConnection.Open(); //Устанавливаем соединение с базой данных.


            MySqlDataReader MyDataReader;
            MyDataReader = myCommand.ExecuteReader();

            while (MyDataReader.Read())
            {
                this.Text = MyDataReader.GetString(0).ToString() + " объявлений в базе";
            }
            myConnection.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            //todo: Расписать сколько повторов у скольки объявлений за каждый день. Добавлять повторы к выбранным объявлениям.
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            Statistics f = new Statistics(monthCalendar1.SelectionStart);
            f.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            updateCount(1);
        }

        private void updateCount(int i)
        {
            try
            {

                int index = dataGridView1.SelectedCells[0].RowIndex;
                DateTime date = DateTime.Parse(dataGridView1.Rows[index].Cells[0].Value.ToString());

                string CommandText = "update " + dbname + ".ob set KOL_P = '" + (int.Parse(dataGridView1.Rows[index].Cells[2].Value.ToString()) + i).ToString() + "' where `DATEPOST`= '" + date.ToString("yyyy-MM-dd") + "' AND KOL_P = '" + dataGridView1.Rows[index].Cells[2].Value.ToString() + "'";
                string Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;

                MySqlConnection myConnection = new MySqlConnection(Connect);
                MySqlCommand myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open(); //Устанавливаем соединение с базой данных.

                MySqlDataReader MyDataReader;


                myCommand.ExecuteNonQuery();
                myConnection.Close();
                dataGridView1.Rows.Clear();
                updateDateTable();
                getCount();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            updateCount(-1);
        }
    }
}
