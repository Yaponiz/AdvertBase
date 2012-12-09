using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;

//using System.Linq;
using System.Windows.Forms;
using System.Xml;
using MySql.Data.MySqlClient;

namespace AdvertBaseServer
{
    public partial class ExportForm : Form
    {
        public object Missing;
        public int col = 0;
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
                string CommandText = "select * from `ads_paper`.`catalogitems` order by R1,R2,R3,R4";
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
                    int id = MyDataReader.GetInt32(0); //Получаем строку
                    string R1 = MyDataReader.GetString(2); //Получаем строку
                    string R2 = MyDataReader.GetString(3); //Получаем строку
                    string R3 = MyDataReader.GetString(4); //Получаем строку
                    string R4 = MyDataReader.GetString(5); //Получаем строку
                    string name = MyDataReader.GetString(1); //Получаем строку

                    mainCatalog.Rows.Add(id, name, R1, R2, R3, R4);
                }
                MyDataReader.Close();
                myConnection.Close(); //Обязательно закрываем соединение!

                updateDateTable();
                loadCatalogs();
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

            CommandText = "select datepost, count(*), kol_p from ria_rim.ob group by datepost, kol_p order by datepost desc";
            myCommand.CommandText = CommandText;

            myConnection.Open(); //Устанавливаем соединение с базой данных.

            MyDataReader = myCommand.ExecuteReader();
            DateTime date;
            while (MyDataReader.Read())
            {
                //date = MyDataReader.GetDateTime(0);
                //    CommandText = "select count(*) from ria_rim.ob where DATEPOST = '" + date.ToString("yyyy-MM-dd") + "' and KOL_P ='" + MyDataReader.GetString(1) + "'";
                //    myCommand3.CommandText = CommandText;
                //    MyDataReader3 = myCommand3.ExecuteReader();
                //    while (MyDataReader3.Read())
                //    {
                dataGridView1.Rows.Add(MyDataReader.GetDateTime(0).ToString("yyyy-MM-dd"), MyDataReader.GetString(1), MyDataReader.GetString(2));

                //}
                //MyDataReader3.Close();
            }
            MyDataReader.Close();
            myConnection.Close(); //Обязательно закрываем соединение!

            myConnection3.Close();
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);
        }

        private void updateStatistics()
        {
            string datePart;
            if (dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") != dateTimePicker2.Value.Date.ToString("yyyy-MM-dd"))
            {
                datePart = "(date(datepost) between '" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyy-MM-dd") + "') and";
            }
            else
            {
                datePart = "datepost ='" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "' and ";
            }

            string CommandText = "select NUM_PK, count(*) from `ria_rim`.`ob` where " + datePart + " (time(timepost) between '" + dateTimePicker1.Value.ToString("HH:mm:ss") + "' and '" + dateTimePicker2.Value.ToString("HH:mm:ss") + "') group by num_PK";
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

            while (MyDataReader.Read())
            {
                int kom_num = MyDataReader.GetInt32(0); //Получаем строку
                int kol = MyDataReader.GetInt32(1); //Получаем строку
                statisticsByPC.Rows.Add(kom_num.ToString(), kol.ToString(), "", "");
            }
            MyDataReader.Close();
            myConnection.Close(); //Обязательно закрываем соединение!
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool checkCount(int[] catalogNum)
        {
            try
            {
                string CommandText = "select * from " + dbname + ".ob where `KOD_R` = '" + catalogNum[1] + "' AND `KOL_P` > '0' ";
                if (catalogNum[2] != 0)
                {
                    CommandText = CommandText + " AND `KOD_PR` = '" + catalogNum[2] + "'";
                }

                if (catalogNum[3] != 0)
                {
                    CommandText = CommandText + " and `KOD_PPR` = '" + catalogNum[3] + "'";
                }

                if (catalogNum[4] != 0)
                {
                    CommandText = CommandText + " AND `KOD_PPPR` = '" + catalogNum[4] + "'";
                }

                string Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;
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
                    Microsoft.Office.Interop.Word.Range rang = wdDoc.Range();
                    wdDoc.SpellingChecked = false;

                    wdDoc.ShowSpellingErrors = false;
                    wdApp.Selection.Font.Name = "Times New Roman";
                    wdApp.Selection.Font.Size = 12;
                    wdApp.ActiveDocument.Select();

                    string CommandText = "select * from `ads_paper`.`catalogitems` order by idcatalogItems";
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

                        //parentID = MyDataReader.GetInt32(6);
                        //real = MyDataReader.GetInt32(7);
                        //sum = MyDataReader.GetInt32(8);
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

        private void PrintAdsRIO(int[] catalogNum)
        {
            try
            {
                // StreamWriter f = new StreamWriter("log.txt");

                string CommandText = "select * from `ads_paper`.`" + catalogsComboBox.Text + "` order by id";
                string CommandText2 = "";
                string Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;

                //Переменная Connect - это строка подключения в которой:
                //БАЗА - Имя базы в MySQL
                //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
                //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
                //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL

                MySqlConnection myConnection = new MySqlConnection(Connect);
                MySqlConnection myConnection2 = new MySqlConnection(Connect);
                MySqlCommand myCommand = new MySqlCommand(CommandText, myConnection);
                MySqlCommand myCommand2 = new MySqlCommand(CommandText2, myConnection2);
                myConnection.Open(); //Устанавливаем соединение с базой данных.

                // Gets a NumberFormatInfo associated with the en-US culture.
                NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

                nfi.NumberDecimalDigits = 0;
                nfi.NumberGroupSeparator = " ";

                nfi.PositiveSign = "";
                MySqlDataReader MyDataReader;
                MySqlDataReader MyDataReader2;

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
                int countOfLines = 0;
                while (MyDataReader.Read())
                {

                    if (MyDataReader.GetString(3) != "")
                    {
                        richTextBox1.DeselectAll();
                        richTextBox1.SelectionFont = new System.Drawing.Font("Times New Roman", 12, System.Drawing.FontStyle.Bold); ;
                        richTextBox1.AppendText(MyDataReader.GetString(3) + Environment.NewLine);

                        //wdApp.Selection.TypeText(MyDataReader.GetString(3));


                        //wdApp.Selection.TypeParagraph();
                    }
                    else
                    {
                        richTextBox1.AppendText(Environment.NewLine);
                    }

                    if (MyDataReader.GetString(4) != "")
                    {
                        myConnection2.Open(); //Устанавливаем соединение с базой данных для получения объявлений.
                        CommandText2 = "select * from ria_rim.ob where kol_p>0 and (KOD_R='" + MyDataReader.GetString(4) + "' and KOD_PR='" + MyDataReader.GetString(5) + "' and kod_ppr='" + MyDataReader.GetString(6) + "' and kod_pppr='" + MyDataReader.GetString(7) + "') order by k_word";
                        myCommand2.CommandText = CommandText2;
                        MyDataReader2 = myCommand2.ExecuteReader();
                        while (MyDataReader2.Read())
                        {
                            header = MyDataReader2.GetString(12); //Получаем строку
                            text = MyDataReader2.GetString(13); //Получаем строку
                            phone = MyDataReader2.GetString(14); //Получаем строку
                            id = MyDataReader2.GetString(0); //Получаем id

                            //toshow = MyDataReader.GetUInt16(9); //Получаем строку
                            //catalog = MyDataReader.GetString(11); //Получаем строку
                            richTextBox1.SelectionFont = new System.Drawing.Font("Times New Roman", 12, System.Drawing.FontStyle.Bold); ;
                            //wdApp.Selection.Font.Bold = 1;

                            richTextBox1.AppendText(header);
                            //wdApp.Selection.TypeText(header);

                            //wdApp.Selection.TypeText(" ");
                            richTextBox1.SelectionFont = new System.Drawing.Font("Times New Roman", 12, System.Drawing.FontStyle.Regular);
                            //wdApp.Selection.Font.Bold = 0;
                            if (text != " ")
                            {
                                richTextBox1.AppendText(" " + text);
                                //wdApp.Selection.TypeText(" " + text);
                            }

                            richTextBox1.AppendText(" " + phone+Environment.NewLine);
                            //wdApp.Selection.TypeText(" " + phone);
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
                        MyDataReader2.Close();
                        myConnection2.Close();
                    }
                }

                MyDataReader.Close();
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
                bool aut = false;
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

                if (!aut)
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
                //todo: брать из базы данных
                string[] listOfAuto = {
            "Alfa",
            "Audi",
            "BMW",
            "Great Wall",
            "Daihatsu",
            "Daewoo",
            "Kia",
            "Chrysler",
            "Lexus",
            "Mazda",
            "Mercedes-Benz",
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
            "Hyundai",
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
            "Volvo",
            "ВАЗ",
            "ГАЗ",
            "Москвич",
        };
                foreach (string auto in listOfAuto)
                {
                    string CommandText = "select * from " + dbname + ".ob where `KOD_R` = '" + catalogNum[1] + "' AND `KOD_PR` = '" + catalogNum[2] + "' and `KOD_PPR` = '" + catalogNum[3] + "' AND `KOD_PPPR` = '" + catalogNum[4] + "' AND `KOL_P` > '0' AND K_WORD like '%%" + auto + "%%' ORDER BY K_WORD";
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
                    wdApp.Selection.TypeText(header + " ");

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
            listBox1.Items.Add(monthCalendar1.SelectionRange.Start.ToShortDateString() + " - " + monthCalendar1.SelectionRange.End.ToShortDateString());
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

                   // Microsoft.Office.Interop.Word.Application wdApp = new Microsoft.Office.Interop.Word.Application();
                   // Microsoft.Office.Interop.Word.Document wdDoc = new Microsoft.Office.Interop.Word.Document();
                    //wdDoc = wdApp.Documents.Add();

                   // wdDoc.SpellingChecked = false;

                   // wdDoc.ShowSpellingErrors = false;
                   // wdApp.Selection.Font.Name = "Times New Roman";
                  //  wdApp.Selection.Font.Size = 12;
                   // wdApp.ActiveDocument.Select();

                    int[] catID = new int[5];
                    int parentID;
                    int real;
                    int sum;
                    PrintAdsRIO(catID);

                  //  ReplaceTextWord(ref wdApp, "  ", " ");
                    //wdApp.ActiveDocument.SaveAs(saveFileDialog1.FileName + ".doc");
                   // wdDoc.Close();

                    //wdApp.Documents.Close();
                  //  wdApp.Quit();
                    MessageBox.Show("Выгрузка завершена, выгружено: " + col.ToString() + " объявлений");
    
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

                myCommand.ExecuteNonQuery();
                myConnection.Close();
                UpdateTable(date, index, (int.Parse(dataGridView1.Rows[index].Cells[2].Value.ToString()) + i));
                getCount();
                dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void UpdateTable(DateTime date, int index, int kol)
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

            CommandText = "select distinct DATEPOST from ria_rim.ob";
            myCommand.CommandText = CommandText;

            dataGridView1.Rows.RemoveAt(index);
            MyDataReader = myCommand.ExecuteReader();

            CommandText = "select count(*) from ria_rim.ob where DATEPOST = '" + date.ToString("yyyy-MM-dd") + "' and KOL_P ='" + (kol).ToString() + "'";
            myCommand3.CommandText = CommandText;
            MyDataReader3 = myCommand3.ExecuteReader();
            while (MyDataReader3.Read())
            {
                dataGridView1.Rows.Add(date, MyDataReader3.GetString(0), (kol).ToString());
            }
            MyDataReader3.Close();

            MyDataReader.Close();
            myConnection.Close(); //Обязательно закрываем соединение!
            myConnection2.Close();
            myConnection3.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            updateCount(-1);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (DialogResult.OK == saveFileDialog1.ShowDialog())
            {
                richTextBox1.SaveFile(saveFileDialog1.FileName);
            }
        }

        private void addCard_Click(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                string CommandText = "TRUNCATE TABLE `ads_paper`.`" + catalogSelector.Text + "`";
                string Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;

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
                myCommand.ExecuteNonQuery();

                int count = dataGridView2.Rows.Count - 1;
                DataGridViewRow row;
                int bold = -1;
                for (int i = 0; i < count; i++)
                {
                    if (dataGridView2.Rows[i].Cells[1].Value != null)
                    {
                        if ((bool)dataGridView2.Rows[i].Cells[1].Value == true)
                            bold = 1;
                        else
                            bold = -1;
                    }

                    CommandText = "INSERT INTO `ads_paper`.`" + catalogSelector.Text + "` (`bold`, `prefix`, `name`, `KOD_R`, `KOD_PR`, `KOD_PPR`, `KOD_PPPR`) VALUES ('" + bold.ToString() + "', '" + dataGridView2.Rows[i].Cells[2].Value + "', '" + dataGridView2.Rows[i].Cells[3].Value + "', '" + dataGridView2.Rows[i].Cells[4].Value + "', '" + dataGridView2.Rows[i].Cells[5].Value + "', '" + dataGridView2.Rows[i].Cells[6].Value + "', '" + dataGridView2.Rows[i].Cells[7].Value + "');";

                    //myCommand.CommandText = CommandText;
                    //myCommand.ExecuteNonQuery();
                    myCommand.CommandText = CommandText;
                    myCommand.ExecuteNonQuery();
                }
                myConnection.Close(); //Обязательно закрываем соединение!
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            saveMainCatalog();
        }

        private void saveMainCatalog()
        {
            try
            {
                string CommandText = "drop table if exists `ads_paper`.`catalogItems`";
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
                myConnection.Close();

                CommandText = "CREATE TABLE `ads_paper`.`catalogitems` (" +
  "`idcatalogItems` int(10) unsigned NOT NULL AUTO_INCREMENT," +
  "`name` varchar(100) NOT NULL," +
  "`R1` int(10) unsigned NOT NULL," +
  "`R2` int(10) unsigned NOT NULL," +
  "`R3` int(10) unsigned NOT NULL," +
  "`R4` int(10) unsigned NOT NULL," +
  "PRIMARY KEY (`idcatalogItems`)," +
   "UNIQUE KEY `idcatalogItems_UNIQUE` (`idcatalogItems`)" +
") ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8 COMMENT='Список рубрик'";
                Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;

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
                saveMainCatalogItem();
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }

        private void saveMainCatalogItem()
        {
            try
            {
                string CommandText = "";
                string Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;

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
                int count = mainCatalog.Rows.Count - 1;
                DataGridViewRow row;
                for (int i = 0; i < count; i++)
                {
                    row = mainCatalog.Rows[i];

                    CommandText = "INSERT INTO `ads_paper`.`catalogitems` (`name`, `R1`, `R2`, `R3`, `R4`) VALUES ('" + mainCatalog.Rows[i].Cells[1].Value + "', " + mainCatalog.Rows[i].Cells[2].Value + ", " + mainCatalog.Rows[i].Cells[3].Value + ", " + mainCatalog.Rows[i].Cells[4].Value + ", " + mainCatalog.Rows[i].Cells[5].Value + ");";

                    //myCommand.CommandText = CommandText;
                    //myCommand.ExecuteNonQuery();
                    myCommand.CommandText = CommandText;
                    myCommand.ExecuteNonQuery();
                }
                myConnection.Close(); //Обязательно закрываем соединение!

                //wdApp.Selection.TypeText("l");
                // Print each node recursively.
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
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

        private void button14_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedCellCollection cells = mainCatalog.SelectedCells;
            foreach (DataGridViewCell cell in cells)
            {
                mainCatalog.Rows[cell.RowIndex].Selected = true;
            }

            DataGridViewSelectedRowCollection rows = mainCatalog.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                dataGridView2.Rows.Add("", true, "", row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString(), row.Cells[4].Value.ToString(), row.Cells[5].Value.ToString());
            }
        }

        private void catalogSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CommandText = "select * from ads_paper.cataloglist";
            string Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;

            //Переменная Connect - это строка подключения в которой:
            //БАЗА - Имя базы в MySQL
            //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
            //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
            //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL
            //sortedTree.Nodes.Add(catalogSelector.Text);

            MySqlConnection myConnection = new MySqlConnection(Connect);
            MySqlCommand myCommand = new MySqlCommand(CommandText, myConnection);
            MySqlDataReader MyDataReader;

            CommandText = "select * from ads_paper." + catalogSelector.Text + "";
            myCommand = new MySqlCommand(CommandText, myConnection);
            myConnection.Open(); //Устанавливаем соединение с базой данных.

            dataGridView2.Rows.Clear();
            MyDataReader = myCommand.ExecuteReader();

            while (MyDataReader.Read())
            {
                dataGridView2.Rows.Add(MyDataReader.GetString(0), MyDataReader.GetBoolean(1), MyDataReader.GetString(2), MyDataReader.GetString(3),
                    MyDataReader.GetString(4), MyDataReader.GetString(5), MyDataReader.GetString(6), MyDataReader.GetString(7));
            }
            MyDataReader.Close();
            myConnection.Close(); //Обязательно закрываем соединение!

            // sortedTree.ExpandAll();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                string CommandText = "drop table if exists `ads_paper`.`" + catalogSelector.Text + "`";
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

                myCommand.CommandText = "delete from ads_paper.cataloglist where catalogName = '" + catalogSelector.Text + "'";
                myCommand.ExecuteNonQuery();
                myConnection.Close();
                loadCatalogs();
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string CommandText = "";
            string Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;

            //Переменная Connect - это строка подключения в которой:
            //БАЗА - Имя базы в MySQL
            //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
            //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
            //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL
            try
            {
                CommandText = "insert into `ads_paper`.`cataloglist` (`catalogName`) VALUES ('" + catalogName.Text + "');";
                MySqlConnection myConnection = new MySqlConnection(Connect);
                MySqlCommand myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open(); //Устанавливаем соединение с базой данных.
                myCommand.ExecuteNonQuery();
                CommandText = "CREATE TABLE ads_paper." + catalogName.Text + " (`id` INT NOT NULL AUTO_INCREMENT,  `bold` TINYINT(1)  NULL ,  `Prefix` VARCHAR(45) NULL ,  `Name` VARCHAR(45) NULL ,  `KOD_R` VARCHAR(45) NULL ,  `KOD_PR` VARCHAR(45) NULL ,  `KOD_PPR` VARCHAR(45) NULL ,  `KOD_PPPR` VARCHAR(45) NULL ,  `reserved` VARCHAR(45) NULL ,  PRIMARY KEY (`id`) );";
                myCommand.CommandText = CommandText;
                myCommand.ExecuteNonQuery();
                myConnection.Close(); //Обязательно закрываем соединение!
                loadCatalogs();
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }

        private void loadCatalogs()
        {
            string CommandText = "select * from `ads_paper`.`cataloglist`";
            string Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;

            //Переменная Connect - это строка подключения в которой:
            //БАЗА - Имя базы в MySQL
            //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
            //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
            //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL
            try
            {
                catalogSelector.Items.Clear();
                MySqlConnection myConnection = new MySqlConnection(Connect);
                MySqlCommand myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open(); //Устанавливаем соединение с базой данных.
                MySqlDataReader MyDataReader;
                MyDataReader = myCommand.ExecuteReader();

                while (MyDataReader.Read())
                {
                    catalogSelector.Items.Add(MyDataReader.GetValue(1).ToString());
                }
                MyDataReader.Close();
                myConnection.Close(); //Обязательно закрываем соединение!
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }

        private void tabPage2_DoubleClick(object sender, EventArgs e)
        {
        }

        private void button6_Click_2(object sender, EventArgs e)
        {
            updateStatistics();
        }
    }
}