using System;

//using System.Linq;
using System.Windows.Forms;

namespace AdvertBase
{
    public partial class templateEditor : Form
    {
        public templateEditor()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            richTextBox1.Text += " " + listView1.FocusedItem.Text;
        }
    }
}