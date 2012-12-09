using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace AdvertBase
{
    public partial class SearcResults : Form
    {
        public EditForm f;
        private string dbname, server, dbuser, dbpass, dbPort;

        public SearcResults()
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
        }

        private void cardList_DoubleClick(object sender, EventArgs e)
        {
            //MessageBox.Show("0");
            f = new EditForm(cardList.CurrentRow.Cells[0].Value.ToString());

            //MessageBox.Show("01");
            f.Show();
        }
    }
}