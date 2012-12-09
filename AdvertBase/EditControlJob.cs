using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AdvertBase
{
    public partial class EditControlJob : UserControl
    {
        public int level;
        public int userID;
        public int selected = 0;
        private string dbname, server, dbuser, dbpass, dbPort;

        public EditControlJob(string id)
        {
            InitializeComponent();
            string CommandText = "select * from ria_rim.ob where ID_OB='" + id + "'";
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

                //SearcResults f = new SearcResults();
                while (MyDataReader.Read())
                {
                    //KOD_R-KOD_PPPR
                    richTextBox4.Text = MyDataReader.GetString(4);
                    richTextBox3.Text = MyDataReader.GetString(5);
                    richTextBox2.Text = MyDataReader.GetString(6);
                    richTextBox1.Text = MyDataReader.GetString(7);
                    //KOD_R-KOD_PPPR

                    richTextBox5.Text = MyDataReader.GetString(12);
                    richTextBox11.Text = MyDataReader.GetString(13);
                    richTextBox10.Text = MyDataReader.GetString(15);
                    richTextBox9.Text = MyDataReader.GetString(14);
                    richTextBox8.Text = MyDataReader.GetString(20);
                    numericUpDown1.Value = MyDataReader.GetDecimal(16);

                    // richTextBox9.Text = MyDataReader.GetString(14);
                }

                //f.Show();
                MyDataReader.Close();
                myConnection.Close(); //Обязательно закрываем соединение!
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }

        public EditControlJob()
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

        private void label3_Click(object sender, EventArgs e)
        {
        }
    }
}