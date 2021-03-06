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

namespace AdvertBase
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

            
                string CommandText = "select * from ads_paper.cataloglist order by idcatalogList";
                string Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port="+dbPort;
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

                    int id = MyDataReader.GetInt32(0); //Получаем строку
                    string name = MyDataReader.GetString(1); //Получаем строку

                    catalogsComboBox.Items.Insert(id - 1, name);

                }
                MyDataReader.Close();
                myConnection.Close(); //Обязательно закрываем соединение!
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
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
                    Microsoft.Office.Interop.Word.Application wdApp = new Microsoft.Office.Interop.Word.Application();
                    Microsoft.Office.Interop.Word.Document wdDoc = new Microsoft.Office.Interop.Word.Document();
                    wdDoc = wdApp.Documents.Add();
                    wdDoc.SpellingChecked = false;
                    
                    wdDoc.ShowSpellingErrors = false;
                    wdApp.Selection.Font.Name = "Times New Roman";
                    wdApp.Selection.Font.Size = 12;
                    wdApp.ActiveDocument.Select();

                    string CommandText = "select * from `ads_paper`.`catalogItems` order by R1,R2,R3,R4";
                    string Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port="+dbPort;

                MySqlConnection myConnection = new MySqlConnection(Connect);
                MySqlCommand myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open(); //Устанавливаем соединение с базой данных.
                MySqlDataReader MyDataReader;

                bool state = false;
                MyDataReader = myCommand.ExecuteReader();
                int[] catID = new int[5];
                while (MyDataReader.Read())
                {
                    catID[0] = MyDataReader.GetInt32(0);
                    catID[1] = MyDataReader.GetInt32(2);
                    catID[2] = MyDataReader.GetInt32(3);
                    catID[3] = MyDataReader.GetInt32(4);
                    catID[4] = MyDataReader.GetInt32(5);
                    string name = MyDataReader.GetString(1); //Получаем строку
                    
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
                            }
                            else
                            {
                                wdApp.Selection.TypeText(catID[1].ToString() + "." + catID[2].ToString());
                            }
                            
                            wdApp.Selection.TypeText(" "+name);
                            wdApp.Selection.TypeParagraph();
                            state = true;

                        }
                        else
                        {
                            //if (state == true)
                            //{
                            //    wdApp.Selection.TypeParagraph();
                            //}
                            
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
                wdApp.ActiveDocument.SaveAs("Объявления.doc");
                wdDoc.Close();
                //wdApp.Documents.Close();
                wdApp.Quit();
                MessageBox.Show("Выгрузка завершена, выгружено: "+col.ToString()+" объявлений");
                //mainForm f = new mainForm();
                    //f.catalogSelector.Select(1, 1);
                   /* TreeNodeCollection nodes = f.catalogList.Nodes;
                    foreach (TreeNode groupeNode in nodes)
                    {
                        PrintRecursive(groupeNode, ref wdApp);
                    }

                    ReplaceTextWord(ref wdApp, "@@@-", "@@@");
                    ReplaceTextWord(ref wdApp, "@@@,", "@@@");
                    ReplaceTextWord(ref wdApp, "@@@ -", "@@@");
                    ReplaceTextWord(ref wdApp, "@@@ ", "@@@");
                    ReplaceTextWord(ref wdApp, ",,", ",");

                    //col++;


                    MessageBox.Show("Выгружено объявлений: " + col.ToString());
                    col = 0;
                    rubNum = 0;
              /* }
                else if (catalogsComboBox.SelectedIndex == 0)
                {
                    Microsoft.Office.Interop.Word.Application wdApp = new Microsoft.Office.Interop.Word.Application();
                    Microsoft.Office.Interop.Word.Document wdDoc = new Microsoft.Office.Interop.Word.Document();
                    wdDoc = wdApp.Documents.Add();
                    wdDoc.SpellingChecked = false;
                    wdDoc.ShowSpellingErrors = false;
                    wdApp.ActiveDocument.Select();
                    mainForm f = new mainForm();
                    f.catalogSelector.Select(0, 1);
                    TreeNodeCollection nodes = f.catalogList.Nodes;
                    foreach (TreeNode groupeNode in nodes)
                    {
                        PrintRecursiveRIO(groupeNode, ref wdApp);
                    }

                    ReplaceTextWord(ref wdApp, "@@@-", "@@@");
                    ReplaceTextWord(ref wdApp, "@@@,", "@@@");
                    ReplaceTextWord(ref wdApp, "@@@ -", "@@@");
                    ReplaceTextWord(ref wdApp, "@@@ ", "@@@");
                    wdApp.ActiveDocument.SaveAs("РИО.doc");
                    wdDoc.Close();
                    //wdApp.Documents.Close();
                    wdApp.Quit();
                    //col++;


                    MessageBox.Show("Выгружено объявлений: " + col.ToString());
                    col = 0;
                    rubNum = 0;
                }
                else
                {
                    Microsoft.Office.Interop.Word.Application wdApp = new Microsoft.Office.Interop.Word.Application();
                    Microsoft.Office.Interop.Word.Document wdDoc = new Microsoft.Office.Interop.Word.Document();
                    wdDoc = wdApp.Documents.Add();
                    wdDoc.SpellingChecked = false;
                    wdDoc.ShowSpellingErrors = false;
                    wdApp.ActiveDocument.Select();
                    mainForm f = new mainForm();
                    f.catalogSelector.Select(0,1);
                    f.catalogSelector.SelectedIndex = 0;
                    TreeNodeCollection nodes = f.catalogList.Nodes;
                    foreach (TreeNode groupeNode in nodes)
                    {
                        PrintRecursiveOB(groupeNode, ref wdApp);
                    }

                    ReplaceTextWord(ref wdApp, "@@@-", "@@@");
                    ReplaceTextWord(ref wdApp, "@@@,", "@@@");
                    ReplaceTextWord(ref wdApp, "@@@ -", "@@@");
                    ReplaceTextWord(ref wdApp, "@@@ ", "@@@");
                    //wdApp.Selection.Select();
                    //wdApp.Selection.Font.
                    wdApp.ActiveDocument.SaveAs("Объявления.doc");
                    wdDoc.Close();
                    //wdApp.Documents.Close();
                    wdApp.Quit();
                    //col++;


                    MessageBox.Show("Выгружено объявлений: " + col.ToString());
                    col = 0;
                    rubNum = 0;
                }*/
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }

        //private void PrintRecursive(TreeNode treeNode, ref Microsoft.Office.Interop.Word.Application wdApp)
        //{
           
        //    // Print the node.
        //    wdApp.Selection.Font.Bold = 0;
        //    if (treeNode.Name != "Node0")
        //    {
        //            idRub = treeNode.Name;
        //            //wdApp.Selection.Select();
        //            //wdApp.Selection.Characters.Last.Select();
        //            if (treeNode.Name == "1" || treeNode.Name == "19" || treeNode.Name == "35")
        //            {
        //                rubNum++;
        //                wdApp.Selection.TypeText("-" + rubNum.ToString() + "-");
        //                wdApp.Selection.TypeParagraph();
        //                wdApp.Selection.TypeText(treeNode.Text);
        //                wdApp.Selection.TypeParagraph();
        //                wdApp.Selection.TypeParagraph();
        //            }
        //            else
        //            {
        //                if (treeNode.Name == "40")
        //                {
        //                    rubNum++;
        //                    wdApp.Selection.TypeText("-" + rubNum.ToString() + "-");
        //                    wdApp.Selection.TypeParagraph();
        //                    wdApp.Selection.TypeText(treeNode.Text);
        //                    wdApp.Selection.TypeParagraph();
        //                    wdApp.Selection.TypeParagraph();

        //                }
        //                else
        //                {
        //                    wdApp.Selection.TypeText("@@" + treeNode.Text);
                            
        //                    wdApp.Selection.TypeParagraph();
        //                    wdApp.Selection.TypeParagraph();
        //                }
        //                string CommandText = "select * from " + dbname + ".rubrikator3 where ID_RUBRIKATOR ='" + treeNode.Name.ToString() + "'";
        //                string Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass;

        //                MySqlConnection myConnection = new MySqlConnection(Connect);
        //                MySqlCommand myCommand = new MySqlCommand(CommandText, myConnection);
        //                myConnection.Open(); //Устанавливаем соединение с базой данных.
        //                MySqlDataReader MyDataReader;


        //                MyDataReader = myCommand.ExecuteReader();
        //                int[] catID = new int[5];
        //                while (MyDataReader.Read())
        //                {
        //                    catID[0] = MyDataReader.GetInt32(0);
        //                    catID[1] = MyDataReader.GetInt32(1);                           
        //                    catID[2] = MyDataReader.GetInt32(2);
        //                    catID[3] = MyDataReader.GetInt32(3);
        //                    catID[4] = MyDataReader.GetInt32(4);                           
        //                   // string name = MyDataReader.GetString(5); //Получаем строку
        //                }
        //                MyDataReader.Close();
        //                myConnection.Close();

        //                if (treeNode.Name == "8")
        //                {
        //                    catID[1] = 1;
        //                    catID[2] = 4;
        //                    catID[3] = 0;
        //                    catID[4] = 0;
        //                    PrintAds8(catID, ref wdApp);

        //                }
        //                else if (treeNode.Name == "9")
        //                {
        //                    catID[1] = 1;
        //                    catID[2] = 5;
        //                    catID[3] = 0;
        //                    catID[4] = 0;
        //                    PrintAds9(catID, ref wdApp);
        //                }
        //                else if (treeNode.Name == "40")
        //                {
        //                    PrintAdsRSU(19,3, ref wdApp);
        //                }
        //                else
        //                {                            
        //                    PrintAds(catID, ref wdApp);
        //                }
        //            }
                    
        //        }

        //        //wdApp.Selection.TypeText("l");
        //        // Print each node recursively.
        //        foreach (TreeNode tn in treeNode.Nodes)
        //        {
        //            PrintRecursive(tn, ref wdApp);
        //        }           
        //}

       

        //private void PrintRecursiveOB(ref Microsoft.Office.Interop.Word.Application wdApp)
        //{


        //        string CommandText = "select * from " + dbname + ".rubrikator where ID_RUBRIKATOR ='" + treeNode.Name.ToString() + "'";
        //        string Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass;

        //        MySqlConnection myConnection = new MySqlConnection(Connect);
        //        MySqlCommand myCommand = new MySqlCommand(CommandText, myConnection);
        //        myConnection.Open(); //Устанавливаем соединение с базой данных.
        //        MySqlDataReader MyDataReader;


        //        MyDataReader = myCommand.ExecuteReader();
        //        int[] catID = new int[5];
        //        while (MyDataReader.Read())
        //        {
        //            catID[0] = MyDataReader.GetInt32(0);
        //            catID[1] = MyDataReader.GetInt32(1);
        //            catID[2] = MyDataReader.GetInt32(2);
        //            catID[3] = MyDataReader.GetInt32(3);
        //            catID[4] = MyDataReader.GetInt32(4);
        //            string name = MyDataReader.GetString(5); //Получаем строку
        //        }
        //        MyDataReader.Close();
        //        myConnection.Close();

        //        if (catID[1] == 0)
        //        {
        //            rubNum++;
        //            wdApp.Selection.TypeText("@@" + rubNum.ToString() + "");
        //            wdApp.Selection.TypeParagraph();
        //            wdApp.Selection.TypeText(treeNode.Text);
        //            wdApp.Selection.TypeParagraph();
        //            wdApp.Selection.TypeParagraph();
        //        }
        //        else
        //        {
        //            wdApp.Selection.TypeParagraph();
        //            wdApp.Selection.TypeText(treeNode.Text);
        //            wdApp.Selection.TypeParagraph();
        //            wdApp.Selection.TypeParagraph();

        //        }
        //        PrintAdsOB(catID, ref wdApp);

        //    }

        //    //wdApp.Selection.TypeText("l");
        //    // Print each node recursively.
        //    foreach (TreeNode tn in treeNode.Nodes)
        //    {
        //        PrintRecursiveOB(ref wdApp);
        //    }
        //}

        private void PrintAdsRIO(int[] catalogNum, ref Microsoft.Office.Interop.Word.Application wdApp)
        {

            string CommandText = "select * from " + dbname + ".ob where `KOD_R` = '" + catalogNum[1] + "' AND `KOD_PR` = '" + catalogNum[2] + "' and `KOD_PPR` = '" + catalogNum[3] + "' AND `KOD_PPPR` = '" + catalogNum[4] + "' AND `KOL_P` > '0' ORDER BY K_WORD";
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
                string date; //Получаем дату
                string phone; //Получаем строку
                string header; //Получаем строку
                string text; //Получаем строку
                int cost; //Получаем строку
                string catalog; //Получаем строку
                int toshow; //Получаем строку
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
                        //wdApp.Selection.Font.Bold = 1;
                        wdApp.Selection.ClearFormatting();
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
           
            
            
           
            //wdApp.Selection.TypeText("l");
            // Print each node recursively.
           
             }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

            }

        }

        private void PrintAdsOB(int[] catalogNum, ref Microsoft.Office.Interop.Word.Application wdApp)
        {

            string CommandText = "select * from " + dbname + ".ob where `KOD_R` = '" + catalogNum[1] + "' AND `KOD_PR` = '" + catalogNum[2] + "' and `KOD_PPR` = '" + catalogNum[3] + "' AND `KOD_PPPR` = '" + catalogNum[4] + "' AND `KOL_P` > '0' ORDER BY K_WORD";
            string Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port="+dbPort;
            //todo: Проверить по выписаным ошибкам
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
                string date; //Получаем дату
                string phone; //Получаем строку
                string header; //Получаем строку
                string text; //Получаем строку
                int cost; //Получаем строку
                string catalog; //Получаем строку
                int toshow; //Получаем строку
                string id;
                string costStr;
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




                //wdApp.Selection.TypeText("l");
                // Print each node recursively.

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
                string date; //Получаем дату
                string phone; //Получаем строку
                string header; //Получаем строку
                string text; //Получаем строку
                int cost; //Получаем строку
                string catalog; //Получаем строку
                int toshow; //Получаем строку
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
                    //toshow = MyDataReader.GetUInt16(9); //Получаем строку
                    //catalog = MyDataReader.GetString(11); //Получаем строку


                   // wdApp.Selection.ClearFormatting();
                   // wdApp.Selection.Select();
                   // wdApp.Selection.Characters.Last.Select();
                    wdApp.Selection.Font.Bold = 1;
                    wdApp.Selection.TypeText("@@@" + cost.ToString("N", nfi));
                    wdApp.Selection.TypeText(" ");
                    //wdApp.Selection.Font.Bold = 0;
                   // wdApp.Selection.Select();
                   // wdApp.Selection.Characters.Last.Select();
                  //  wdApp.Selection.ClearFormatting();
                    wdApp.Selection.Font.Bold = 0;
                    wdApp.Selection.TypeText(" руб." + costStr);
                    //wdApp.Selection.Font.Bold = 1;
                    wdApp.Selection.TypeParagraph();
                  //  wdApp.Selection.Select();
                  //  wdApp.Selection.Characters.Last.Select();
                  //  wdApp.Selection.Font.Bold = 0;
                  //  wdApp.Selection.ClearFormatting();
                    wdApp.Selection.TypeText(header);
                  //  wdApp.Selection.Font.Bold = 0;
                  //  wdApp.Selection.Select();
                  //  wdApp.Selection.Characters.Last.Select();
                  //  wdApp.Selection.Font.Bold = 0;
                  //  wdApp.Selection.ClearFormatting();
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
                string date; //Получаем дату
                string phone; //Получаем строку
                string header; //Получаем строку
                string text; //Получаем строку
                int cost; //Получаем строку
                string catalog; //Получаем строку
                int toshow; //Получаем строку
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
                string date; //Получаем дату
                string phone; //Получаем строку
                string header; //Получаем строку
                string text; //Получаем строку
                int cost; //Получаем строку
                string catalog; //Получаем строку
                int toshow; //Получаем строку
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
                string date; //Получаем дату
                string phone; //Получаем строку
                string header; //Получаем строку
                string text; //Получаем строку
                string cost; //Получаем строку
                string catalog; //Получаем строку
                int toshow; //Получаем строку

                //StringBuilder t = new StringBuilder(text);
                //t.

                MyDataReader = myCommand.ExecuteReader();

                while (MyDataReader.Read())
                {

                    header = MyDataReader.GetString(12); //Получаем строку
                    text = MyDataReader.GetString(13); //Получаем строку                    
                    phone = MyDataReader.GetString(14); //Получаем строку
                    //date = MyDataReader.GetString(2); //Получаем дату
                    //cost = MyDataReader.GetString(20); //Получаем строку
                    //toshow = MyDataReader.GetUInt16(9); //Получаем строку
                    //catalog = MyDataReader.GetString(11); //Получаем строку

                    //wdApp.Selection.Font.Bold = 1;
                    //wdApp.Selection.Select();
                   // wdApp.Selection.Characters.Last.Select();
                    //wdApp.Selection.TypeText(cost);
                  ///  wdApp.Selection.ClearFormatting();
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
            Microsoft.Office.Interop.Word.Application wdApp = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document wdDoc = new Microsoft.Office.Interop.Word.Document();
            wdDoc = wdApp.Documents.Add();
            
            string CommandText = "select * from " + dbname + ".ob where KOD_R=1 and KOD_PR=6 and KOD_PPR =5 and cost < 1800000 and cost > 1300000 and KOL_P>0" ;
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
                string date; //Получаем дату
                string phone; //Получаем строку
                string header; //Получаем строку
                string text; //Получаем строку
                int cost; //Получаем строку
                string catalog; //Получаем строку
                int toshow; //Получаем строку
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
                    wdApp.Selection.TypeText("" + cost.ToString("N", nfi));
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


                wdApp.ActiveDocument.SaveAs("Квартиры.doc");
                wdDoc.Close();
                //wdApp.Documents.Close();
                wdApp.Quit();

                //wdApp.Selection.TypeText("l");
                // Print each node recursively.

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }

        private void ExportForm_Load(object sender, EventArgs e)
        {
            try
            {

                string CommandText = "select count(*) from " + dbname + ".ob where kol_p >0";
                string Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port="+dbPort;

                MySqlConnection myConnection = new MySqlConnection(Connect);
                MySqlCommand myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open(); //Устанавливаем соединение с базой данных.

                MySqlConnection myConnection2 = new MySqlConnection(Connect);
                myConnection2.Open();

                MySqlDataReader MyDataReader;
                 MyDataReader = myCommand.ExecuteReader();

                 while (MyDataReader.Read())
                 {
                     this.Text = MyDataReader.GetString(0).ToString() + " объявлений в базе";
                 }

        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
           
            //todo: Расписать сколько повторов у скольки объявлений за каждый день. Добавлять повторы к выбранным объявлениям.
        }
    }
}
