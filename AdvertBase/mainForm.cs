using System;
using System.Drawing;
using System.IO;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using MySql.Data.MySqlClient;
namespace AdvertBase
{
    public partial class mainForm : Form
    {
        public EditControl cards;
        
        public int level = 100;
        public int userID;
        public string selectedCard;
        public int cardsCount = 0;
        private string dbname, server, dbuser, dbpass, dbPort;
        public int startHeight = 100;
        public int startWidth = 400;
        public mainForm()
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
                int i = -1;
                catalogList.BackColor = System.Drawing.Color.DarkGray;
                while (MyDataReader.Read())
                {
                    int id = MyDataReader.GetInt32(0); //Получаем строку
                    string R1 = MyDataReader.GetString(2); //Получаем строку
                    string R2 = MyDataReader.GetString(3); //Получаем строку
                    string R3 = MyDataReader.GetString(4); //Получаем строку
                    string R4 = MyDataReader.GetString(5); //Получаем строку
                    string name = MyDataReader.GetString(1); //Получаем строку
                    
                    catalogList.Rows.Add(id, name, R1, R2, R3, R4);
                    i++;
                    catalogList.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.DarkGray;
                    if (R1 == "0")
                    {
                        catalogList.Rows[i].DefaultCellStyle.BackColor= System.Drawing.Color.DarkCyan;
                        continue;
                        
                    }
                    if (R2 == "0")
                    {
                        catalogList.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.DarkSeaGreen;
                        continue;
                    }
                    if (R3 == "0")
                    {
                        catalogList.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.ForestGreen;
                        continue;
                    }
                    if (R4 == "0")
                    {
                        catalogList.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.CadetBlue;
                        continue;
                    }
                }
                MyDataReader.Close();
                myConnection.Close(); //Обязательно закрываем соединение!
                //добавляем первую карточку
                EditControl card = new EditControl(this, "0");
                card.Name = "cardPane" + cardsCount.ToString();
                selectedCard = card.Name;
                card.Top = startHeight;
                card.Left = startWidth;
                cardsCount++;
                card.Show();
                card.Enter += this.cardName;
                selectedCard = card.Name;
                listOfCards.Controls.Add(card);
                listOfCards.Height += card.Height + 10;
               
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
        }
       private void addCard_Click(object sender, EventArgs e)
        {
            
            try
            {
                int count;
                bool res=false;                
                count = listOfCards.Controls.Count;
                for (int i = 0; i < count; count--)
                {                    
                    Type t = listOfCards.Controls[i].GetType();
                    if (t.Name == "EditControl")
                    {
                        EditControl cont = listOfCards.Controls[i] as EditControl;
                        res = cont.SaveAndClose();
                    }
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            templateEditor tempForm = new templateEditor();
            tempForm.Show();
        }
        private void mainForm_Load(object sender, EventArgs e)
        {
        }
        private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
        private void usersButton_Click(object sender, EventArgs e)
        {
            usersForm f = new usersForm();
            f.Show();
        }
        private void mainCatalogTree_Click(object sender, EventArgs e)
        {
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
         private void mainForm_Shown(object sender, EventArgs e)
        {
            if (level != 100)
            {                
                usersButton.Dispose();
            }
        }
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            try
            {
                properties dlg = new properties();
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    // Read the contents of testDialog's TextBox.
                    server = dlg.serverName.Text;
                    dbname = dlg.dbName.Text;
                    dbuser = dlg.dbuser.Text;
                    dbpass = dlg.dbpass.Text;
                    dbPort = dlg.dbPort.Text;
                    FileStream f = new FileStream("properties.xml", FileMode.Create);
                    XmlTextWriter settings = new XmlTextWriter(f, Encoding.Default);
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
                    f.Close();
                }
                dlg.Dispose();
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
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
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
        }
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            try
            {
                string CommandText = "select ID_OB,cost,costStr from `ria_rim`.`ob` where KOD_R = 1 and KOL_P>0 and cost <1 and not costStr=''";
                string CommandText2 = "";
                string Connect = "server=" + server + ";Database=ria_rim;User Id=" + dbuser + ";Password=" + dbpass;
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
                myConnection2.Open(); //Устанавливаем соединение с базой данных.
                MySqlDataReader MyDataReader;
                MyDataReader = myCommand.ExecuteReader();
                string id;
                string cost = "";
                string tempCost = "";
                string costStr = "";
                string tempCost2 = "";
                double costD = 0;
                //string torg = "(торг)";
                while (MyDataReader.Read())
                {
                    id = MyDataReader.GetValue(0).ToString();
                    costStr = MyDataReader.GetValue(2).ToString();
                    cost = MyDataReader.GetValue(1).ToString();
                    if (costStr.Contains("т.р."))
                    {
                        tempCost2 = costStr.Replace("1", "");
                        tempCost2 = tempCost2.Replace("2", "");
                        tempCost2 = tempCost2.Replace("3", "");
                        tempCost2 = tempCost2.Replace("4", "");
                        tempCost2 = tempCost2.Replace("5", "");
                        tempCost2 = tempCost2.Replace("6", "");
                        tempCost2 = tempCost2.Replace("7", "");
                        tempCost2 = tempCost2.Replace("8", "");
                        tempCost2 = tempCost2.Replace("9", "");
                        tempCost2 = tempCost2.Replace("0", "");
                        tempCost2 = tempCost2.Replace(" т.р.", "");
                        tempCost2 = tempCost2.TrimStart(',');
                        tempCost = costStr.Replace(" т.р.", "");
                        tempCost = tempCost.Replace("сут", "");
                        tempCost = tempCost.Replace("мес", "");
                        tempCost = tempCost.Replace("/", "");
                        tempCost = tempCost.Replace("+", "");
                        tempCost = tempCost.Replace(".", "");
                        //tempCost = tempCost.Replace(",", "");
                        tempCost = tempCost.Replace(" ", "");
                        tempCost = tempCost.Replace("-", "");
                        tempCost = tempCost.Replace("(торг)", "");
                        //tempCost = tempCost.Replace(",", ".");
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.TrimEnd(',');
                        tempCost = tempCost.TrimEnd(',');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.TrimEnd(',');
                        tempCost = tempCost.TrimEnd(',');
                        tempCost = tempCost.TrimEnd(',');
                        tempCost = tempCost.TrimStart(',');
                        if (double.TryParse(tempCost, out costD))
                        {
                            costD = costD * 1000;
                        }
                        if (costD != 0)
                        {
                            //    cost = cost;
                            //}
                            //else
                            //{
                            cost = costD.ToString();
                        }
                        CommandText2 = "update ria_rim.ob set cost ='" + cost + "', costStr = '" + tempCost2 + "' where ID_OB = '" + id + "'";
                        myCommand2.CommandText = CommandText2;
                        myCommand2.ExecuteNonQuery();
                    }
                    else if (costStr.Contains("млн.р."))
                    {
                        tempCost2 = costStr.Replace("1", "");
                        tempCost2 = tempCost2.Replace("2", "");
                        tempCost2 = tempCost2.Replace("3", "");
                        tempCost2 = tempCost2.Replace("4", "");
                        tempCost2 = tempCost2.Replace("5", "");
                        tempCost2 = tempCost2.Replace("6", "");
                        tempCost2 = tempCost2.Replace("7", "");
                        tempCost2 = tempCost2.Replace("8", "");
                        tempCost2 = tempCost2.Replace("9", "");
                        tempCost2 = tempCost2.Replace("0", "");
                        tempCost2 = tempCost2.Replace(" млн.р.", "");
                        tempCost2 = tempCost2.TrimStart(',');
                        tempCost = costStr.Replace(" т.р.", "");
                        tempCost = tempCost.Replace("сут", "");
                        tempCost = tempCost.Replace("мес", "");
                        tempCost = tempCost.Replace("/", "");
                        tempCost = tempCost.Replace("+", "");
                        tempCost = tempCost.Replace(".", "");
                        //tempCost = tempCost.Replace(",", "");
                        tempCost = tempCost.Replace("(торг)", "");
                        tempCost = tempCost.Replace(" ", "");
                        tempCost = tempCost.Replace("-", "");
                        //tempCost = tempCost.Replace(",", ".");
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.TrimEnd(',');
                        tempCost = tempCost.TrimEnd(',');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.TrimEnd(',');
                        tempCost = tempCost.TrimEnd(',');
                        tempCost = tempCost.TrimEnd(',');
                        tempCost = tempCost.TrimStart(',');
                        if (double.TryParse(tempCost, out costD))
                        {
                            costD = costD * 1000000;
                        }
                        if (costD != 0)
                        {
                            //    cost = cost;
                            //}
                            //else
                            //{
                            cost = costD.ToString();
                        }
                        CommandText2 = "update ria_rim.ob set cost ='" + cost + "', costStr = '" + tempCost2 + "' where ID_OB = '" + id + "'";
                        myCommand2.CommandText = CommandText2;
                        myCommand2.ExecuteNonQuery();
                    }
                    else
                    {
                        tempCost2 = costStr.Replace("1", "");
                        tempCost2 = tempCost2.Replace("2", "");
                        tempCost2 = tempCost2.Replace("3", "");
                        tempCost2 = tempCost2.Replace("4", "");
                        tempCost2 = tempCost2.Replace("5", "");
                        tempCost2 = tempCost2.Replace("6", "");
                        tempCost2 = tempCost2.Replace("7", "");
                        tempCost2 = tempCost2.Replace("8", "");
                        tempCost2 = tempCost2.Replace("9", "");
                        tempCost2 = tempCost2.Replace("0", "");
                        tempCost2 = tempCost2.Replace("р.", "");
                        tempCost2 = tempCost2.TrimStart(',');
                        tempCost = costStr.Replace(" т.р.", "");
                        tempCost = tempCost.Replace("сут", "");
                        tempCost = tempCost.Replace("мес", "");
                        tempCost = tempCost.Replace("/", "");
                        tempCost = tempCost.Replace("+", "");
                        tempCost = tempCost.Replace(".", "");
                        tempCost = tempCost.Replace("(торг)", "");
                        //tempCost = tempCost.Replace(",", "");
                        tempCost = tempCost.Replace(" ", "");
                        tempCost = tempCost.Replace("-", "");
                        //tempCost = tempCost.Replace(",", ".");
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.TrimEnd(',');
                        tempCost = tempCost.TrimEnd(',');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.TrimEnd(',');
                        tempCost = tempCost.TrimEnd(',');
                        tempCost = tempCost.TrimEnd(',');
                        tempCost = tempCost.TrimStart(',');
                        if (double.TryParse(tempCost, out costD))
                        {
                            costD = costD * 1;
                        }
                        if (costD != 0)
                        {
                            //    cost = cost;
                            //}
                            //else
                            //{
                            cost = costD.ToString();
                        }
                        CommandText2 = "update ria_rim.ob set cost ='" + cost + "', costStr = '" + tempCost2 + "' where ID_OB = '" + id + "'";
                        myCommand2.CommandText = CommandText2;
                        myCommand2.ExecuteNonQuery();
                    }
                }
                MyDataReader.Close();
                myConnection2.Close(); //Обязательно закрываем соединение!
                myConnection.Close(); //Обязательно закрываем соединение!
            }
            catch (Exception except)
            {
                //MessageBox.Show(exceptoString());
                MessageBox.Show(except.Message + except.Source.ToString());
            }
        }
        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            try
            {
                string CommandText = "select * from ria_rim.ob";
                string CommandText2 = "";
                string Connect = "server=" + server + ";Database=ria_rim;User Id=" + dbuser + ";Password=" + dbpass;
                int k1 = 0, k2 = 0, k3 = 0;
                //Переменная Connect - это строка подключения в которой:
                //БАЗА - Имя базы в MySQL
                //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
                //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
                //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL
                MySqlConnection myConnection = new MySqlConnection(Connect);
                MySqlConnection myConnection2 = new MySqlConnection(Connect);
                MySqlConnection myConnection3 = new MySqlConnection(Connect);
                MySqlCommand myCommand = new MySqlCommand(CommandText, myConnection);
                MySqlCommand myCommand2 = new MySqlCommand(CommandText2, myConnection2);
                MySqlCommand myCommand3 = new MySqlCommand(CommandText2, myConnection3);
                myConnection.Open(); //Устанавливаем соединение с базой данных.
                myConnection2.Open(); //Устанавливаем соединение с базой данных.
                myConnection3.Open(); //Устанавливаем соединение с базой данных.
                MySqlDataReader MyDataReader;
                MySqlDataReader MyDataReader2;
                MyDataReader = myCommand.ExecuteReader();
                string id;
                string KOL_P = "";
                string date = "";
                while (MyDataReader.Read())
                {
                    id = MyDataReader.GetValue(0).ToString();
                    date = MyDataReader.GetDateTime(2).ToString("yyyy-MM-dd");
                    KOL_P = MyDataReader.GetValue(16).ToString();
                    CommandText2 = "select ID_OB from ria_rim.ob where ID_OB = '" + id + "'";
                    myCommand2.CommandText = CommandText2;
                    MyDataReader2 = myCommand2.ExecuteReader();
                    k1++;
                    if (MyDataReader2.Read())
                    {
                        CommandText2 = "update ria_rim.ob set DATEPOST ='" + date + "', KOL_P='" + KOL_P + "' where ID_OB = '" + id + "'";
                        myCommand3.CommandText = CommandText2;
                        myCommand3.ExecuteNonQuery();
                        k2++;
                    }
                    else
                    {
                        CommandText2 = "INSERT INTO ria_rim.`ob` VALUES ('" + MyDataReader.GetValue(0).ToString() + "','" + MyDataReader.GetValue(1).ToString() + "','" + MyDataReader.GetDateTime(2).ToString("yyyy-MM-dd") + "','" + MyDataReader.GetValue(3).ToString() + "','" + MyDataReader.GetValue(4).ToString() + "','" + MyDataReader.GetValue(5).ToString() + "','" + MyDataReader.GetValue(6).ToString() + "','" + MyDataReader.GetValue(7).ToString() + "','" + MyDataReader.GetValue(8).ToString() + "','" + MyDataReader.GetValue(8).ToString() + "','" + AddSlashes(MyDataReader.GetValue(10).ToString()) + "','" + AddSlashes(MyDataReader.GetValue(11).ToString()) + "','" + AddSlashes(MyDataReader.GetValue(12).ToString()) + "','" + AddSlashes(MyDataReader.GetValue(13).ToString()) + "','" + AddSlashes(MyDataReader.GetValue(14).ToString()) + "','" + MyDataReader.GetValue(15).ToString() + "','" + MyDataReader.GetValue(16).ToString() + "','" + MyDataReader.GetValue(17).ToString() + "','" + MyDataReader.GetValue(18).ToString() + "','" + MyDataReader.GetValue(19).ToString() + "','0','','','','','')";
                        myCommand3.CommandText = CommandText2;
                        myCommand3.ExecuteNonQuery();
                        k3++;
                    }
                    MyDataReader2.Close();
                }
                MyDataReader.Close();
                myConnection2.Close(); //Обязательно закрываем соединение!
                myConnection.Close(); //Обязательно закрываем соединение!
                MessageBox.Show("Обработано объявлений:" + k1.ToString() + " Из них добавлено новых:" + k3.ToString() + " Обновлено старых:" + k2.ToString());
            }
            catch (Exception except)
            {
                //MessageBox.Show(exceptoString());
                MessageBox.Show(except.Message);
            }
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Search f = new Search();
            f.Show();
        }
        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            try
            {
                string CommandText = "select * from ria_rim.ob";
                string CommandText2 = "";
                string Connect = "server=" + server + ";Database=ria_rim;User Id=" + dbuser + ";Password=" + dbpass;
                int k1 = 0, k2 = 0;//, k3 = 0;
                //Переменная Connect - это строка подключения в которой:
                //БАЗА - Имя базы в MySQL
                //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
                //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
                //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL
                MySqlConnection myConnection = new MySqlConnection(Connect);
                MySqlConnection myConnection2 = new MySqlConnection(Connect);
                MySqlConnection myConnection3 = new MySqlConnection(Connect);
                MySqlCommand myCommand = new MySqlCommand(CommandText, myConnection);
                MySqlCommand myCommand2 = new MySqlCommand(CommandText2, myConnection2);
                MySqlCommand myCommand3 = new MySqlCommand(CommandText2, myConnection3);
                myConnection.Open(); //Устанавливаем соединение с базой данных.
                myConnection2.Open(); //Устанавливаем соединение с базой данных.
                myConnection3.Open(); //Устанавливаем соединение с базой данных.
                MySqlDataReader MyDataReader;
                MySqlDataReader MyDataReader2;
                MyDataReader = myCommand.ExecuteReader();
                string id;
                string KOL_P = "";
                string date = "";
                while (MyDataReader.Read())
                {
                    id = MyDataReader.GetValue(0).ToString();
                    date = MyDataReader.GetDateTime(2).ToString("yyyy-MM-dd");
                    KOL_P = MyDataReader.GetValue(16).ToString();
                    CommandText2 = "select ID_OB from ria_rim.ob where ID_OB = '" + id + "'";
                    myCommand2.CommandText = CommandText2;
                    MyDataReader2 = myCommand2.ExecuteReader();
                    k1++;
                    if (MyDataReader2.Read())
                    {
                        CommandText2 = "update ria_rim.ob set KOL_P='0' where ID_OB = '" + id + "'";
                        myCommand3.CommandText = CommandText2;
                        myCommand3.ExecuteNonQuery();
                        k2++;
                    }
                    MyDataReader2.Close();
                }
                MyDataReader.Close();
                myConnection2.Close(); //Обязательно закрываем соединение!
                myConnection.Close(); //Обязательно закрываем соединение!
                MessageBox.Show("Обработано объявлений:" + k1.ToString() + " Обновлено старых:" + k2.ToString());
            }
            catch (Exception except)
            {
                //MessageBox.Show(exceptoString());
                MessageBox.Show(except.Message);
            }
        }
        public void fillCard(string id)
        {
            string CommandText = "select * from ria_rim.ob where ID_OB = " + id;
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
                myCommand.ExecuteNonQuery();
                MySqlDataReader MyDataReader;
                MyDataReader = myCommand.ExecuteReader();
                while (MyDataReader.Read())
                {
                    //cardAddress.Text =
                }
                MyDataReader.Close();
                myConnection.Close(); //Обязательно закрываем соединение!
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (catalogList.CurrentRow.Cells[2].Value.ToString() != "18")
                {
                    EditControl card = new EditControl(this, "0");
                    card.Name = "cardPane" + cardsCount.ToString();
                    selectedCard = card.Name;
                    card.Top = startHeight;
                    card.Left = startWidth;
                    cardsCount++;
                    card.Show();
                    card.Enter += this.cardName;
                    selectedCard = card.Name;
                    listOfCards.Controls.Add(card);
                    listOfCards.Height += card.Height + 10;
                }
                else
                {
                    //EditControlJob card = new EditControlJob();
                    //card.Name = "cardPane" + cardsCount.ToString();
                    //card.Top = cardsView.Height;
                    //card.BackColor = Color.DarkGray;
                    //cardsCount++;
                    //selectedCard = card.Name;
                    //cardsView.Controls.Add(card);
                    //cardsView.Width = 615;
                    //cardsView.Height += card.Height;
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            //listOfCards.Controls[selectedCard].;
        }
        private void cardName(object sender, EventArgs e)
        {
            EditControl card = sender as EditControl;
            if (card.Name.Contains("cardPane"))
                selectedCard = card.Name;
        }
        private void cardComment_TextChanged(object sender, EventArgs e)
        {
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
        private void cardList_Click(object sender, EventArgs e)
        {
        }
        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            try
            {
                string CommandText = "select * from ria_rim.ob";
                string CommandText2 = "";
                string Connect = "server=" + server + ";Database=ria_rim;User Id=" + dbuser + ";Password=" + dbpass;
                int k1 = 0, k2 = 0, k3 = 0;
                //Переменная Connect - это строка подключения в которой:
                //БАЗА - Имя базы в MySQL
                //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
                //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
                //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL
                MySqlConnection myConnection = new MySqlConnection(Connect);
                MySqlConnection myConnection2 = new MySqlConnection(Connect);
                MySqlConnection myConnection3 = new MySqlConnection(Connect);
                MySqlCommand myCommand = new MySqlCommand(CommandText, myConnection);
                MySqlCommand myCommand2 = new MySqlCommand(CommandText2, myConnection2);
                MySqlCommand myCommand3 = new MySqlCommand(CommandText2, myConnection3);
                myConnection.Open(); //Устанавливаем соединение с базой данных.
                myConnection2.Open(); //Устанавливаем соединение с базой данных.
                myConnection3.Open(); //Устанавливаем соединение с базой данных.
                MySqlDataReader MyDataReader;
                MySqlDataReader MyDataReader2;
                MyDataReader = myCommand.ExecuteReader();
                string id;
                string KOL_P = "";
                string date = "";
                while (MyDataReader.Read())
                {
                    id = MyDataReader.GetValue(0).ToString();
                    date = MyDataReader.GetDateTime(2).ToString("yyyy-MM-dd");
                    KOL_P = MyDataReader.GetValue(16).ToString();
                    CommandText2 = "select ID_OB from ria_rim.ob where ID_OB = '" + id + "'";
                    myCommand2.CommandText = CommandText2;
                    MyDataReader2 = myCommand2.ExecuteReader();
                    k1++;
                    if (MyDataReader2.Read())
                    {
                        CommandText2 = "update ria_rim.ob set DATEPOST ='" + date + "', KOL_P='" + KOL_P + "' where ID_OB = '" + id + "'";
                        myCommand3.CommandText = CommandText2;
                        myCommand3.ExecuteNonQuery();
                        k2++;
                    }
                    else
                    {
                        CommandText2 = "INSERT INTO ria_rim.`ob` VALUES ('" + MyDataReader.GetValue(0).ToString() + "','" + MyDataReader.GetValue(1).ToString() + "','" + MyDataReader.GetDateTime(2).ToString("yyyy-MM-dd") + "','" + MyDataReader.GetValue(3).ToString() + "','" + MyDataReader.GetValue(4).ToString() + "','" + MyDataReader.GetValue(5).ToString() + "','" + MyDataReader.GetValue(6).ToString() + "','" + MyDataReader.GetValue(7).ToString() + "','" + MyDataReader.GetValue(8).ToString() + "','" + MyDataReader.GetValue(8).ToString() + "','" + AddSlashes(MyDataReader.GetValue(10).ToString()) + "','" + AddSlashes(MyDataReader.GetValue(11).ToString()) + "','" + AddSlashes(MyDataReader.GetValue(12).ToString()) + "','" + AddSlashes(MyDataReader.GetValue(13).ToString()) + "','" + AddSlashes(MyDataReader.GetValue(14).ToString()) + "','" + MyDataReader.GetValue(15).ToString() + "','" + MyDataReader.GetValue(16).ToString() + "','" + MyDataReader.GetValue(17).ToString() + "','" + MyDataReader.GetValue(18).ToString() + "','" + MyDataReader.GetValue(19).ToString() + "','0','','','','','')";
                        myCommand3.CommandText = CommandText2;
                        myCommand3.ExecuteNonQuery();
                        k3++;
                    }
                    MyDataReader2.Close();
                }
                MyDataReader.Close();
                myConnection2.Close(); //Обязательно закрываем соединение!
                myConnection.Close(); //Обязательно закрываем соединение!
                MessageBox.Show("Обработано объявлений:" + k1.ToString() + " Из них добавлено новых:" + k3.ToString() + " Обновлено старых:" + k2.ToString());
            }
            catch (Exception except)
            {
                //MessageBox.Show(exceptoString());
                MessageBox.Show(except.Message);
            }
        }
        private void toolStripButton12_Click_1(object sender, EventArgs e)
        {
            try
            {
                string CommandText = "select ID_OB,cost,costStr from `ria_rim`.`ob` where KOD_R = 1 and KOL_P>0 and cost <1 and not costStr=''";
                string CommandText2 = "";
                string Connect = "server=" + server + ";Database=ria_rim;User Id=" + dbuser + ";Password=" + dbpass;
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
                myConnection2.Open(); //Устанавливаем соединение с базой данных.
                MySqlDataReader MyDataReader;
                MyDataReader = myCommand.ExecuteReader();
                string id;
                string cost = "";
                string tempCost = "";
                string costStr = "";
                string tempCost2 = "";
                double costD = 0;
                //string torg = "(торг)";
                while (MyDataReader.Read())
                {
                    id = MyDataReader.GetValue(0).ToString();
                    costStr = MyDataReader.GetValue(2).ToString();
                    cost = MyDataReader.GetValue(1).ToString();
                    if (costStr.Contains("т.р."))
                    {
                        tempCost2 = costStr.Replace("1", "");
                        tempCost2 = tempCost2.Replace("2", "");
                        tempCost2 = tempCost2.Replace("3", "");
                        tempCost2 = tempCost2.Replace("4", "");
                        tempCost2 = tempCost2.Replace("5", "");
                        tempCost2 = tempCost2.Replace("6", "");
                        tempCost2 = tempCost2.Replace("7", "");
                        tempCost2 = tempCost2.Replace("8", "");
                        tempCost2 = tempCost2.Replace("9", "");
                        tempCost2 = tempCost2.Replace("0", "");
                        tempCost2 = tempCost2.Replace(" т.р.", "");
                        tempCost2 = tempCost2.TrimStart(',');
                        tempCost = costStr.Replace(" т.р.", "");
                        tempCost = tempCost.Replace("сут", "");
                        tempCost = tempCost.Replace("мес", "");
                        tempCost = tempCost.Replace("/", "");
                        tempCost = tempCost.Replace("+", "");
                        tempCost = tempCost.Replace(".", "");
                        //tempCost = tempCost.Replace(",", "");
                        tempCost = tempCost.Replace(" ", "");
                        tempCost = tempCost.Replace("-", "");
                        tempCost = tempCost.Replace("(торг)", "");
                        //tempCost = tempCost.Replace(",", ".");
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.TrimEnd(',');
                        tempCost = tempCost.TrimEnd(',');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.TrimEnd(',');
                        tempCost = tempCost.TrimEnd(',');
                        tempCost = tempCost.TrimEnd(',');
                        tempCost = tempCost.TrimStart(',');
                        if (double.TryParse(tempCost, out costD))
                        {
                            costD = costD * 1000;
                        }
                        if (costD != 0)
                        {
                            //    cost = cost;
                            //}
                            //else
                            //{
                            cost = costD.ToString();
                        }
                        CommandText2 = "update ria_rim.ob set cost ='" + cost + "', costStr = '" + tempCost2 + "' where ID_OB = '" + id + "'";
                        myCommand2.CommandText = CommandText2;
                        myCommand2.ExecuteNonQuery();
                    }
                    else if (costStr.Contains("млн.р."))
                    {
                        tempCost2 = costStr.Replace("1", "");
                        tempCost2 = tempCost2.Replace("2", "");
                        tempCost2 = tempCost2.Replace("3", "");
                        tempCost2 = tempCost2.Replace("4", "");
                        tempCost2 = tempCost2.Replace("5", "");
                        tempCost2 = tempCost2.Replace("6", "");
                        tempCost2 = tempCost2.Replace("7", "");
                        tempCost2 = tempCost2.Replace("8", "");
                        tempCost2 = tempCost2.Replace("9", "");
                        tempCost2 = tempCost2.Replace("0", "");
                        tempCost2 = tempCost2.Replace(" млн.р.", "");
                        tempCost2 = tempCost2.TrimStart(',');
                        tempCost = costStr.Replace(" т.р.", "");
                        tempCost = tempCost.Replace("сут", "");
                        tempCost = tempCost.Replace("мес", "");
                        tempCost = tempCost.Replace("/", "");
                        tempCost = tempCost.Replace("+", "");
                        tempCost = tempCost.Replace(".", "");
                        //tempCost = tempCost.Replace(",", "");
                        tempCost = tempCost.Replace("(торг)", "");
                        tempCost = tempCost.Replace(" ", "");
                        tempCost = tempCost.Replace("-", "");
                        //tempCost = tempCost.Replace(",", ".");
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.TrimEnd(',');
                        tempCost = tempCost.TrimEnd(',');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.TrimEnd(',');
                        tempCost = tempCost.TrimEnd(',');
                        tempCost = tempCost.TrimEnd(',');
                        tempCost = tempCost.TrimStart(',');
                        if (double.TryParse(tempCost, out costD))
                        {
                            costD = costD * 1000000;
                        }
                        if (costD != 0)
                        {
                            //    cost = cost;
                            //}
                            //else
                            //{
                            cost = costD.ToString();
                        }
                        CommandText2 = "update ria_rim.ob set cost ='" + cost + "', costStr = '" + tempCost2 + "' where ID_OB = '" + id + "'";
                        myCommand2.CommandText = CommandText2;
                        myCommand2.ExecuteNonQuery();
                    }
                    else
                    {
                        tempCost2 = costStr.Replace("1", "");
                        tempCost2 = tempCost2.Replace("2", "");
                        tempCost2 = tempCost2.Replace("3", "");
                        tempCost2 = tempCost2.Replace("4", "");
                        tempCost2 = tempCost2.Replace("5", "");
                        tempCost2 = tempCost2.Replace("6", "");
                        tempCost2 = tempCost2.Replace("7", "");
                        tempCost2 = tempCost2.Replace("8", "");
                        tempCost2 = tempCost2.Replace("9", "");
                        tempCost2 = tempCost2.Replace("0", "");
                        tempCost2 = tempCost2.Replace("р.", "");
                        tempCost2 = tempCost2.TrimStart(',');
                        tempCost = costStr.Replace(" т.р.", "");
                        tempCost = tempCost.Replace("сут", "");
                        tempCost = tempCost.Replace("мес", "");
                        tempCost = tempCost.Replace("/", "");
                        tempCost = tempCost.Replace("+", "");
                        tempCost = tempCost.Replace(".", "");
                        tempCost = tempCost.Replace("(торг)", "");
                        //tempCost = tempCost.Replace(",", "");
                        tempCost = tempCost.Replace(" ", "");
                        tempCost = tempCost.Replace("-", "");
                        //tempCost = tempCost.Replace(",", ".");
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.TrimEnd(',');
                        tempCost = tempCost.TrimEnd(',');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.Trim('а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ы', 'ъ', 'э', 'ю', 'я');
                        tempCost = tempCost.TrimEnd(',');
                        tempCost = tempCost.TrimEnd(',');
                        tempCost = tempCost.TrimEnd(',');
                        tempCost = tempCost.TrimStart(',');
                        if (double.TryParse(tempCost, out costD))
                        {
                            costD = costD * 1;
                        }
                        if (costD != 0)
                        {
                            //    cost = cost;
                            //}
                            //else
                            //{
                            cost = costD.ToString();
                        }
                        CommandText2 = "update ria_rim.ob set cost ='" + cost + "', costStr = '" + tempCost2 + "' where ID_OB = '" + id + "'";
                        myCommand2.CommandText = CommandText2;
                        myCommand2.ExecuteNonQuery();
                    }
                }
                MyDataReader.Close();
                myConnection2.Close(); //Обязательно закрываем соединение!
                myConnection.Close(); //Обязательно закрываем соединение!
            }
            catch (Exception except)
            {
                //MessageBox.Show(exceptoString());
                MessageBox.Show(except.Message + except.Source.ToString());
            }
        }
        private void catalogList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //int count;
            DateTime now = DateTime.Now;
            //cardList.Rows.Add(count, DateTime.Now, cardPhone.Text, cardAddress.Text, cardCost.Text, cardHeader.Text, "");
            //string CommandText = "";// "insert into ria_rim.ob (DATEPOST, K_WORD, STRING_OB, ADRES, TELEPHON, cost, costStr, KOL_P, KOD_R,KOD_PR,KOD_PPR,KOD_PPPR) Values ('" + DateTime.Now.ToString("yyyy-MM-dd") + "', '" + cardHeader.Text + "', '" + cardText.Text + "', '" + cardPhone.Text + "', '" + cardComment.Text + "', '" + cardCost.Text + "', '" + "" + "', '" + cardToShow.Value.ToString() + "', '" + KOD_R1.Text + "', '" + KOD_R2.Text + "', '" + KOD_R3.Text + "', '" + KOD_R4.Text + "')";
            string Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;
            try
            {
                //todo: Сделать добавление информации о рубрике в карточку
                EditControl cont = listOfCards.Controls[selectedCard] as EditControl;
                DataGridViewRow row = catalogList.Rows[catalogList.SelectedCells[0].RowIndex];
                if (row != null)
                {
                    cont.catName.Text = row.Cells[1].Value.ToString();
                    cont.kod_r.Text = row.Cells[2].Value.ToString();
                    cont.kod_pr.Text = row.Cells[3].Value.ToString();
                    cont.kod_ppr.Text = row.Cells[4].Value.ToString();
                    cont.kod_pppr.Text = row.Cells[5].Value.ToString();
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }
        private void cardsView_Click(object sender, EventArgs e)
        {
        }
        private void cardsView_ItemActivate(object sender, EventArgs e)
        {
            //int count;
            //count = cardsView.Controls.Count;
            //for (int i = 0; i < count; i++)
            //{
            //    EditControl cont = cardsView.Controls[i] as EditControl;
            //    if (cont.selected == 1)
            //    {
            //        selectedCard = cont.Name;
            //    }
            //}
        }
        private bool AddNode(TreeNode node)
        {
            //node.
            return false;
        }
        private void catalogList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //int count;
            DateTime now = DateTime.Now;
            //cardList.Rows.Add(count, DateTime.Now, cardPhone.Text, cardAddress.Text, cardCost.Text, cardHeader.Text, "");
            //string CommandText = "";// "insert into ria_rim.ob (DATEPOST, K_WORD, STRING_OB, ADRES, TELEPHON, cost, costStr, KOL_P, KOD_R,KOD_PR,KOD_PPR,KOD_PPPR) Values ('" + DateTime.Now.ToString("yyyy-MM-dd") + "', '" + cardHeader.Text + "', '" + cardText.Text + "', '" + cardPhone.Text + "', '" + cardComment.Text + "', '" + cardCost.Text + "', '" + "" + "', '" + cardToShow.Value.ToString() + "', '" + KOD_R1.Text + "', '" + KOD_R2.Text + "', '" + KOD_R3.Text + "', '" + KOD_R4.Text + "')";
            string Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;
            try
            {            
                EditControl cont = listOfCards.Controls[selectedCard] as EditControl;
                DataGridViewRow row = catalogList.Rows[catalogList.SelectedCells[0].RowIndex];
                if (row != null)
                {
                    cont.catName.Text = row.Cells[1].Value.ToString();
                    cont.kod_r.Text = row.Cells[2].Value.ToString();
                    cont.kod_pr.Text = row.Cells[3].Value.ToString();
                    cont.kod_ppr.Text = row.Cells[4].Value.ToString();
                    cont.kod_pppr.Text = row.Cells[5].Value.ToString();
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }
        private void listOfCards_Click(object sender, EventArgs e)
        {
            //listOfCards.Control
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            
            string CommandText = "select * from ria_rim.ob where ADRES like '%%" + searchstringPhone.Text + "%%'";
            string Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;
            //Переменная Connect - это строка подключения в которой:
            //БАЗА - Имя базы в MySQL
            //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
            //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
            //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL
            try
            {
                AddSearchResult(CommandText, Connect);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ErrorCode.ToString());
            }
        }

        private void AddSearchResult(string CommandText, string Connect)
        {
            cardList.Rows.Clear();
            MySqlConnection myConnection = new MySqlConnection(Connect);
            MySqlCommand myCommand = new MySqlCommand(CommandText, myConnection);
            myConnection.Open(); //Устанавливаем соединение с базой данных.
            myCommand.ExecuteNonQuery();
            MySqlDataReader MyDataReader;
            MyDataReader = myCommand.ExecuteReader();
            while (MyDataReader.Read())
            {
                cardList.Rows.Add(MyDataReader.GetString(0), MyDataReader.GetString(2), MyDataReader.GetString(12), MyDataReader.GetString(13), MyDataReader.GetString(14), MyDataReader.GetString(15), MyDataReader.GetString(16), MyDataReader.GetString(1));
            }
            MyDataReader.Close();
            myConnection.Close(); //Обязательно закрываем соединение!
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            
            string CommandText = "select * from ria_rim.ob where K_WORD like '%%" + searchstringText.Text + "%%'";
            string Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;
            //Переменная Connect - это строка подключения в которой:
            //БАЗА - Имя базы в MySQL
            //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
            //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
            //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL
            try
            {
                AddSearchResult(CommandText, Connect);
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }
        private void cardList_DoubleClick(object sender, EventArgs e)
        {
            EditForm f;
            //MessageBox.Show("0");
            f = new EditForm(cardList.CurrentRow.Cells[0].Value.ToString());
            //MessageBox.Show("01");
            f.Show();
        }
        private void tabPage2_Click(object sender, EventArgs e)
        {
        }
        private void button6_Click(object sender, EventArgs e)
        {
            string CommandText = "select * from ria_rim.ob where TELEPHON like '%%" + secondPhone.Text + "%%'";
            string Connect = "Database=" + dbname + ";Data Source=" + server + ";User Id=" + dbuser + ";Password=" + dbpass + ";Port=" + dbPort;
            //Переменная Connect - это строка подключения в которой:
            //БАЗА - Имя базы в MySQL
            //ХОСТ - Имя или IP-адрес сервера (если локально то можно и localhost)
            //ПОЛЬЗОВАТЕЛЬ - Имя пользователя MySQL
            //ПАРОЛЬ - говорит само за себя - пароль пользователя БД MySQL
            try
            {
                AddSearchResult(CommandText, Connect);
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cardList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            numericUpDown1.Value = Int16.Parse(cardList.SelectedCells[0].OwningRow.Cells[6].Value.ToString());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //int count = cardList.Rows.Count;
            DateTime now = DateTime.Now;

            //EditControl[] cont = this.EditPanel

            string CommandText = "update ria_rim.ob set DATEPOST='" + DateTime.Now.ToString("yyyy-MM-dd") + "', KOL_P='" + numericUpDown1.Value.ToString() + "' where ID_OB = '" + cardList.SelectedCells[0].OwningRow.Cells[0].Value.ToString() + "'";

            //string CommandText = "";
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
                myCommand.ExecuteNonQuery();

                myConnection.Close(); //Обязательно закрываем соединение!
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            new checkForm().Show();
        }
    }
}