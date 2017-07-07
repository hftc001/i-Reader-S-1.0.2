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
            if (ReaderS.MessageType[0] == "OkCancel")
            {
                button2.Visible = true;
                button1.Text = "取消";
            }
            else
            {
                button2.Visible = false;
                button1.Text = "确定";
            }
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
            if (label1.Text == "休眠警告")
            {
                timer1.Start();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ReaderS.MessageType[0] == "OkCancel")
            { ReaderS.MessageType[2] = "Cancel"; }
            this.Close();
        }

        private void FormMessageBox_FormClosed(object sender, FormClosedEventArgs e)
        {
            ReaderS.MessageType[0] = "";
            this.Dispose();
            GC.Collect();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ReaderS.MessageType[0] == "OkCancel")
            { ReaderS.MessageType[2] = "Ok"; }
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var minute = int.Parse(label2.Text.ToString().Substring(2, 1));
            minute = minute - 1;
            if (minute == 0)
            {
                this.Close();
            }
            else
            {
                label2.Text = "还有" + minute.ToString() + "分钟进入休眠";
            }
        }
    }
}
