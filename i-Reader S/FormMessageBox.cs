using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace i_Reader_S
{
    public partial class FormMessageBox : Form
    {
        public FormMessageBox()
        {
            InitializeComponent();
        }

        private void FormMessageBox_Load(object sender, EventArgs e)
        {
            var str = ReaderS.messstr.Split('|');
            label2.Text = str[str.Count() - 1];
            if (str.Length >1 )
            {
                label1.Text = str[0];
                label2.Text = str[1];
            }
            else if(str.Length==1)
            {
                label1.Text = "";
                label2.Text = str[0];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
