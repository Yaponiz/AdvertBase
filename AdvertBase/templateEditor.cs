using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
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
            richTextBox1.Text += " " +listView1.FocusedItem.Text;
        }
    }
}
