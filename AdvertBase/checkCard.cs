using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace AdvertBase
{
    public partial class checkCard : UserControl
    {
        private int indexMain;
        public checkCard()
        {
            InitializeComponent();

        }

        public checkCard(string text, int index)
        {
            InitializeComponent();
            label1.Text = text;
            indexMain = index;
        }

        private void checkCard_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void checkCard_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                new EditForm(indexMain.ToString()).Show();
            }
        }
    }
}
