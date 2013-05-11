using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using MySql.Data.MySqlClient;
using System.Text;

namespace AdvertBase
{
    public partial class EditControl : UserControl
    {
        public int level;
        public int userID;
        public int selected = 0;
        public int lastSelected = 0;
        private string dbname, server, dbuser, dbpass, dbPort;
        public mainForm mf;
        public EditControl()
        {
            InitializeComponent();
            FileStream f = new FileStream("properties.xml", FileMode.OpenOrCreate);
            mf = this.ParentForm as mainForm;
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

        //public EditControl(string id)
        //{
        //    InitializeComponent();
        //    FileStream f = new FileStream("properties.xml", FileMode.OpenOrCreate);
        //    XmlTextReader settings = new XmlTextReader(f);
        //    while (settings.Read())
        //    {
        //        if (settings.NodeType == XmlNodeType.Element)
        //        {
        //            if (settings.Name.Equals("server"))
        //            {
        //                server = settings.GetAttribute("servername");
        //                dbname = settings.GetAttribute("dbname");
        //                dbuser = settings.GetAttribute("dbuser");
        //                dbpass = settings.GetAttribute("dbpass");
        //                dbPort = settings.GetAttribute("dbport");
        //            }
        //        }
        //    }
        //    f.Close();
        //    string CommandText = "select * from ria_rim.ob where ID_OB='" + id + "'";
        //    string Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;

        //    //Переменная Connect - это строка подключения в которой:
        //    //БАЗА - Имя базы в MySQL
        //    //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
        //    //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
        //    //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL

        //    try
        //    {
        //        MySqlConnection myConnection = new MySqlConnection(Connect);
        //        MySqlCommand myCommand = new MySqlCommand(CommandText, myConnection);
        //        myConnection.Open(); //Устанавливаем соединение с базой данных.
        //        myCommand.ExecuteNonQuery();
        //        MySqlDataReader MyDataReader;
        //        MyDataReader = myCommand.ExecuteReader();

        //        MyDataReader.Read();

        //        //KOD_R-KOD_PPPR

        //        kod_r.Text = MyDataReader.GetInt32(4).ToString();

        //        kod_pr.Text = MyDataReader.GetInt32(5).ToString();

        //        kod_ppr.Text = MyDataReader.GetInt32(6).ToString();

        //        kod_pppr.Text = MyDataReader.GetInt32(7).ToString();

        //        //KOD_R-KOD_PPPR

        //        k_word.Text = MyDataReader.GetString(12);

        //        string_ob.Text = MyDataReader.GetString(13);

        //        phone.Text = MyDataReader.GetString(14);

        //        secPhone.Text = MyDataReader.GetString(15);
        //        cost.Text = MyDataReader.GetString(20);

        //        kol_p.Value = MyDataReader.GetDecimal(16);

        //        //richTextBox9.Text = MyDataReader.GetString(14);

        //        //f.Show();
        //        MyDataReader.Close();
        //        myConnection.Close(); //Обязательно закрываем соединение!
        //    }
        //    catch (Exception except)
        //    {
        //        MessageBox.Show(except.Message);
        //    }
        //}

        private void cardPanel_Enter(object sender, EventArgs e)
        {
            this.BackColor = Color.Silver;
            selected = 1;
        }

        private void cardPanel_Leave(object sender, EventArgs e)
        {
            this.BackColor = Color.DarkGray;
            selected = 0;
        }

        private void EditControl_Enter(object sender, EventArgs e)
        {
            this.BackColor = Color.Silver;
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

        public void SetSelected()
        {
            this.BackColor = Color.LightGray;
            selected = 1;
        }

        public void UnSetSelected()
        {
            this.BackColor = Color.Gray;
            selected = 0;
        }

        public bool SaveAndClose()
        {
            try
            {
                DateTime now = DateTime.Now;
                StringBuilder textForErrors = new StringBuilder("");
                //cardList.Rows.Add(count, DateTime.Now, cardPhone.Text, cardAddress.Text, cardCost.Text, cardHeader.Text, "");
                string CommandText = "";// "insert into ria_rim.ob (DATEPOST, K_WORD, STRING_OB, ADRES, TELEPHON, cost, costStr, KOL_P, KOD_R,KOD_PR,KOD_PPR,KOD_PPPR) Values ('" + DateTime.Now.ToString("yyyy-MM-dd") + "', '" + cardHeader.Text + "', '" + cardText.Text + "', '" + cardPhone.Text + "', '" + cardComment.Text + "', '" + cardCost.Text + "', '" + "" + "', '" + cardToShow.Value.ToString() + "', '" + KOD_R1.Text + "', '" + KOD_R2.Text + "', '" + KOD_R3.Text + "', '" + KOD_R4.Text + "')";
                string Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;

                //Переменная Connect - это строка подключения в которой:
                //БАЗА - Имя базы в MySQL
                //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
                //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
                //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL
                if (k_word.Text == "")
                {
                    textForErrors.Append("Заголовок не задан");
                    textForErrors.AppendLine();
                }

                if (phone.Text == "")
                {
                    textForErrors.Append("Номер телефона не задан");
                    textForErrors.AppendLine();
                }

                if (textForErrors.Length > 0)
                {
                    MessageBox.Show(textForErrors.ToString(), "Не заполнены поля", MessageBoxButtons.OK);
                    return false;
                }

                if (string_ob.Text == "")
                {
                    DialogResult res = MessageBox.Show("Текст не задан, продолжить?", "", MessageBoxButtons.YesNo);
                    if (res == DialogResult.No)
                    {
                        return false;
                    }
                }

                CommandText = "insert into ria_rim.ob (DATEPOST, K_WORD, STRING_OB, ADRES, TELEPHON, KOL_P, KOD_R,KOD_PR,KOD_PPR,KOD_PPPR) Values ('" + DateTime.Now.ToString("yyyy-MM-dd") + "', '" + k_word.Text + "', '" + string_ob.Text + "', '" + phone.Text + "', '" + comment.Text + "', '" + kol_p.Value.ToString() + "', '" + kod_r.Text + "', '" + kod_pr.Text + "', '" + kod_ppr.Text + "', '" + kod_pppr.Text + "')";
                MySqlConnection myConnection = new MySqlConnection(Connect);
                MySqlCommand myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open(); //Устанавливаем соединение с базой данных.
                myCommand.ExecuteNonQuery();

                myConnection.Close(); //Обязательно закрываем соединение!
                this.Dispose();
                return true;
            }
            catch (Exception excp)
            {
                return false;
            }

        }

        private void k_word_TextChanged(object sender, EventArgs e)
        {

        }

        public bool loadCard(string id = "0")
        {
            if (id != "0")
            {
                string CommandText = "select * from ria_rim.ob where ID_OB='" + id + "'";
                string Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" +
                                 dbpass + ";Port=" + dbPort;

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

                    return true;
                }
                catch (Exception except)
                {
                    MessageBox.Show(except.Message);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void kod_pppr_ValueChanged(object sender, EventArgs e)
        {
            if ((kod_r.Text == "1") && (kod_pr.Text == "2") && (kod_ppr.Text == "1"))
            {
                switch (kod_pppr.Value.ToString())
                {
                    case "1":
                        {
                            townList.Items.Clear();
                            string[] towns = new string[] { "пос. Первомайский", "с. Алханчурт", "с. Верхний Кани", "с. Верхняя Саниба", "с. Восход", "с. Гизель", "с. Даргавс", "с. Дачное", "с. Джимара", "с. Донгарон", "с. Ир", "с. Какадур", "с. Камбилеевское", "с. Кармадон", "с. Кобан", "с. Комгарон", "с. Куртат", "с. Ламардон", "с. Майское", "с. Михайловское", "с. Нижний Кани", "с. Нижняя Саниба", "с. Ногир", "с. Октябрьское", "с. Старая Саниба", "с. Сунжа", "с. Тарское", "с. Тменикау", "с. Фазикау", "с. Чермен", "ст. Архонская" };
                            townList.Items.AddRange(towns);
                            townList.Show();
                            break;
                        }
                    case "2":
                        {
                            townList.Items.Clear();
                            string[] towns = new string[] { "г. Беслан", "пос. Долаково", "с. Батако", "с. Брут", "с. Заманкул", "с. Зилга", "с. Новый Батако", "с. Ольгинское", "с. Раздзог", "с. Фарн", "с. Хумалаг", "с. Цалык" };
                            townList.Items.AddRange(towns);
                            townList.Show();
                            break;
                        }

                    case "3":
                        {
                            townList.Items.Clear();
                            string[] towns = new string[] { "г. Алагир", "п. Бурон", "п. Верхний Фиагдон", "п. Рамоново", "п. Холст", "п. Верхний Згид", "п. Нижний Згид", "п. Садон", "пгт Цементный Завод", "с. Абайтикау", "с. Архон", "с. Бад", "с. Барзикау", "с. Биз", "с. Верхний Бирагзанг", "с. Верхний Зарамаг", "с. Верхний Унал", "с. Верхний Цей", "с. Горная Карца", "с. Гусыра", "с. Дагом", "с. Дайкау", "с. Даллагкау", "с. Дзивгис", "с. Дзуарикау", "с. Дзуарикау", "с. Донисар", "с. Зинцар", "с. Кодахджин", "с. Красный ход", "с. Лац", "с. Лисри", "с. Майрамадаг", "с. Мизур", "с. Нижний Бирагзанг", "с. Нижний Унал", "с. Нижний Цей", "с. Ногкау", "с. Суадаг", "с. Тагардон", "с. Урикау", "с. Урсдон", "с. Харисджин", "с. Хидикус", "с. Хукали", "с. Цаликово", "с. Цамад", "с. Цмити", "с. Црау", "с. Варце", "с. Гори", "с. Елгона", "с. Ецина", "с. Згил", "с. Зригатта", "с. Калак", "с. Камсхо", "с. Клиат", "с. Курайтта", "с. Нар", "с. Нижний Зарамаг", "с. Потыфаз", "с. Регах", "с. Сагол", "с. Сатат", "с. Саубын", "с. Сахсат", "с. Синдзисар", "с. Слас", "с. Тапанкау", "с. Тиб", "с. Тибели", "с. Тибсли", "с. Хаталдон", "с. Ход", "с. Худисан", "с. Цасем", "с. Цемса", "с. Цми" };

                            townList.Show();
                            break;
                        }
                    case "4":
                        {
                            townList.Items.Clear();
                            string[] towns = new string[] { "г. Ардон", "п. Бекан", "с. Кадгарон", "с. Кирово", "с. Коста", "с. Красногор", "с. Мичурино", "с. Нарт", "с. Рассвет", "с. Фиагдон", "с. Цмити" };
                            townList.Show();
                            break;
                        }
                    case "5":
                        {
                            townList.Items.Clear();
                            string[] towns = new string[] { "с. Дарг-Кох", "ст. Змейская", "с. Иран", "с. Карджин", "с. Комсомольское", "с. Ставд-Дурт", "с. Эльхотово" };
                            townList.Show();
                            break;
                        }
                    case "6":
                        {
                            townList.Items.Clear();
                            string[] towns = new string[] { "г. Дигорас.", "Дур-Дур", "с. Карман-Синдзикау", "с. Кора", "с. Мостиздах", "с. Урсдон", "ст. Николаевская" };
                            townList.Show();
                            break;
                        }
                    case "7":
                        {
                            townList.Items.Clear();
                            string[] towns = new string[] { "с. Ахсарисар", "с. Калух", "с. Галиат", "с. Камунта", "с. Дунта", "с. Дзинага", "с. Ногкау", "с. Ахсау", "с. Мацута", "с. Нижний Задалеск", "с. Верхний Задалеск", "с. Нижний Нар", "с. Верхний Нар", "с. Ханазс. Лезгор", "с. Донифарс", "с. Лескен", "с. Махческ", "с. Вакац", "с. Казахта", "с. Калнахта", "с. Камата", "с. Фаснал", "с. Фараскатта", "с. Новый Урух", "с. Дзагепбарз", "с. Советское", "с. Средний Урух", "с. Стур-Дигора", "с. Куссус. Моска", "с. Одола", "с. Сурх-Дигора", "с. Толдзгун", "с. Хазнидон", "с. Чикола" };
                            townList.Show();
                            break;
                        }
                    case "8":
                        {
                            townList.Items.Clear();
                            string[] towns = new string[] { "г. Моздок", "пос. Дружба", "пос. Калининский", "пос. Луковский", "пос. Любы Кондратенко", "пос. Мирный", "пос. Осетинский", "пос. Притеречный", "пос. Садовый", "пос. Советский", "пос. Тельмана", "пос. Теркум", "пос. Черноярский", "с. Весёлое", "с. Виноградное", "с. Елбаево", "с. Киевское", "с. Кизляр", "с. Комарово", "с. Кусово", "с. Малгобек", "с. Малый Малгобек", "с. Ново-Георгиевское", "с. Октябрьское", "с. Предгорное", "с. Раздольное", "с. Сухотское", "с. Троицкое", "с. Хурикау", "ст. Луковская", "ст. Ново-Осетинская", "ст. Павлодольская", "ст. Терская", "ст. Черноярская" };
                            townList.Show();
                            break;
                        }
                    default:
                        townList.Hide();
                        break;
                }
            }
            else
            {
                automobilesList.Hide();
            }
        }

        public void kod_r_ValueChanged(object sender, EventArgs e)
        {
            if (kod_r.Text == "2")
            {
                automobilesList.Show();
            }
            else
            {
                automobilesList.Hide();
            }
        }

        private void townList_TextChanged(object sender, EventArgs e)
        {

        }

        private void townList_SelectedIndexChanged(object sender, EventArgs e)
        {
            k_word.Text = townList.Text;
        }
    }
}