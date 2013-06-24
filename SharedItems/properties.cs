using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace AdvertBase
{
    public partial class properties : Form
    {
        public properties()
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
                        serverName.Text = settings.GetAttribute("servername");
                        dbName.Text = settings.GetAttribute("dbname");
                        dbuser.Text = settings.GetAttribute("dbuser");
                        dbpass.Text = settings.GetAttribute("dbpass");
                        dbPort.Text = settings.GetAttribute("dbport");
                    }
                }
            }
            f.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
    }
}