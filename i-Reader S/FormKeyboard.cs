using System;
using System.Windows.Forms;

namespace i_Reader_S
{
    public partial class KeyboardforIrd : Form
    {
        public static string Returnstr;

        private string _changeType = "";

        public KeyboardforIrd()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            ReaderS.CounterText = "Cancel";
            Close();
        }

        private void buttonCancel2_Click(object sender, EventArgs e)
        {
            ReaderS.CounterText = "Cancel";
            Close();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void buttonDate_MouseDown(object sender, MouseEventArgs e)
        {
            var btn = (Button)sender;
            _changeType = btn.Name;
            timer1_Tick(null, null);
            timer1.Start();
        }

        private void buttonDate_MouseUP(object sender, MouseEventArgs e)
        {
            timer1.Stop();
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
                textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
        }

        private void buttonDot_Click(object sender, EventArgs e)
        {
            if (buttonDot.Text == @".")
                textBox1.Text += @".";
            else if (buttonDot.Text == "/")
                textBox1.Text += "/";
            else if (buttonDot.Text == "$")
                textBox1.Text += "$";
            else
            {
                textBox1.Text = @"Del|";
                buttonOK_Click(null, null);
            }
        }

        private void buttonNo_MouseDown(object sender, MouseEventArgs e)
        {
            var btn = (Button)sender;
            var str = btn.Text.Replace(" ", "");
            textBox1.Text += str.Substring(0, 1);
            var detail = ReaderS.CounterText.Split('|');
            if (detail[0].Substring(0, 3) == "All"| detail[0].Substring(0, 3) == "CMD")
                timer2.Start();
        }

        private void buttonNo_MouseUp(object sender, MouseEventArgs e)
        {
            timer2.Stop();
        }

        private void buttonNo0_Click(object sender, EventArgs e)
        {
            textBox1.Text += @"0";
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            ReaderS.CounterText = textBox1.Text;
            Dispose();
        }

        private void buttonOK2_Click(object sender, EventArgs e)
        {
            ReaderS.CounterText = textBox2.Text;
            Close();
        }        
        private void KeyboardforIrd_Load(object sender, EventArgs e)
        {
            Returnstr = "";
            var detail = ReaderS.CounterText.Split('|');
            textBox3.Visible = detail[0] == "password";
            if (detail[0] == "Date" | detail[0] == "Time")
            {
                Size = panel1.Size;
                panel1.Visible = true;
                textBox2.Text = detail[1];
                return;
            }
            textBox3.PasswordChar = detail[0] == "password" ? '*' : '\0';

            if (detail[0] == "Del") buttonDot.Text = @"删除样本";
            else if (detail[0] == "Int") buttonDot.Enabled = false;
            else if (detail[0] == "AllnoDot") buttonDot.Enabled = false;
            else if (detail[0] == "CMD") buttonDot.Text = "$";
            textBox1.Text = detail[1];
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (textBox2.Text.Length == 8)
            {
                DateTime dt = DateTime.Parse("2015-01-01 " + textBox2.Text);
                switch (_changeType)
                {
                    case "buttonLast1":
                        textBox2.Text = dt.AddHours(-1).ToString("HH:mm:ss");
                        break;

                    case "buttonLast2":
                        textBox2.Text = dt.AddMinutes(-1).ToString("HH:mm:ss");
                        break;

                    case "buttonLast3":
                        textBox2.Text = dt.AddSeconds(-1).ToString("HH:mm:ss");
                        break;

                    case "buttonNext1":
                        textBox2.Text = dt.AddHours(+1).ToString("HH:mm:ss");
                        break;

                    case "buttonNext2":
                        textBox2.Text = dt.AddMinutes(+1).ToString("HH:mm:ss");
                        break;

                    case "buttonNext3":
                        textBox2.Text = dt.AddSeconds(+1).ToString("HH:mm:ss");
                        break;
                }
            }
            else
            {
                DateTime dt = DateTime.Parse(textBox2.Text);
                switch (_changeType)
                {
                    case "buttonLast1":
                        textBox2.Text = dt.AddYears(-1).ToString("yyyy/MM/dd");
                        break;

                    case "buttonLast2":
                        textBox2.Text = dt.AddMonths(-1).ToString("yyyy/MM/dd");
                        break;

                    case "buttonLast3":
                        textBox2.Text = dt.AddDays(-1).ToString("yyyy/MM/dd");
                        break;

                    case "buttonNext1":
                        textBox2.Text = dt.AddYears(+1).ToString("yyyy/MM/dd");
                        break;

                    case "buttonNext2":
                        textBox2.Text = dt.AddMonths(+1).ToString("yyyy/MM/dd");
                        break;

                    case "buttonNext3":
                        textBox2.Text = dt.AddDays(+1).ToString("yyyy/MM/dd");
                        break;
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            var strlist = "1ABCabc2DEFdef3GHIghi4JKLjkl5MNOmno6PQRpqr7STUstu8VWXvwx9YZyz-";
            var str = textBox1.Text.Substring(textBox1.Text.Length - 1);
            var index = strlist.IndexOf(str, StringComparison.Ordinal);
            if (str == "-")
                textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1) + @"9";
            else
                textBox1.Text = index % 7 == 6
                   ? textBox1.Text.Substring(0, textBox1.Text.Length - 1) + strlist.Substring(index - 6, 1)
                   : textBox1.Text.Substring(0, textBox1.Text.Length - 1) + strlist.Substring(index + 1, 1);
        }

        private void KeyboardforIrd_FormClosing(object sender, FormClosingEventArgs e)
        {
            Returnstr = ReaderS.CounterText;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.textBox1.Font = new System.Drawing.Font("微软雅黑", 37 - Math.Max(0, (textBox1.Text.Length - 12) * 2));
            textBox3.Text = textBox1.Text;
        }
    }
}