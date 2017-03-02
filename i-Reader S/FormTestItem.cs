using System;
using System.Drawing;
using System.Windows.Forms;

namespace i_Reader_S
{
    public partial class FormTestItem : Form
    {
        public static string Returnstr;

        public FormTestItem()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (ReaderS.TestItemText.Substring(0, 2) == "QC")
            {
                var dttestitem1 = SqlData.SelectQcTestItem();
                for (int i = 0; i < dttestitem1.Rows.Count; i++)
                {
                    Button btns = (Button)(Controls.Find("button" + i, true)[0]);
                    btns.BackgroundImage = Properties.Resources.button_Black1;
                    btns.ForeColor = Color.White;
                }
                Button btn1 = (Button)sender;
                btn1.BackgroundImage = Properties.Resources.button_Black2;
                btn1.ForeColor = Color.FromArgb(64, 64, 64);
                Returnstr = btn1.Text;
                return;
            }
            for (int i = 0; i < Controls.Count - 1; i++)
            {
                Button btns = (Button)(Controls.Find("button" + i, true)[0]);
                btns.BackgroundImage = Properties.Resources.button_Black1;
                btns.ForeColor = Color.White;
            }
            Button btn = (Button)sender;
            btn.BackgroundImage = Properties.Resources.button_Black2;
            btn.ForeColor = Color.FromArgb(64, 64, 64);
            Returnstr = btn.Text;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (Returnstr == "All") Returnstr = "";
            Dispose();
        }

        private void FormTestItem_Load(object sender, EventArgs e)
        {
            if (ReaderS.TestItemText.Substring(0, 2) == "QC")
            {
                string[] detail1 = ReaderS.TestItemText.Split('|');
                Button[] btns1 = new Button[detail1.Length - 2];
                Size = new Size(1024, 82 * ((detail1.Length - 2) / 5 + 1) + 4);
                for (int i = 0; i < btns1.Length; i++)
                {
                    btns1[i] = new Button
                    {
                        BackColor = Color.Transparent,
                        BackgroundImage = Properties.Resources.button_Black1,
                        BackgroundImageLayout = ImageLayout.Stretch
                    };
                    btns1[i].FlatAppearance.BorderSize = 0;
                    btns1[i].FlatStyle = FlatStyle.Flat;
                    btns1[i].Text = detail1[i + 1];
                    btns1[i].Font = new Font("微软雅黑", btns1[i].Text.Length<9?21.75F:15F, FontStyle.Regular, GraphicsUnit.Point, 134);
                    btns1[i].ForeColor = Color.White;
                    btns1[i].Location = new Point(4 + 164 * (i % 5), 82 * (i / 5) + 4);
                    btns1[i].Name = "button" + i;
                    btns1[i].Size = new Size(160, 78);

                    if (btns1[i].Text == detail1[0].Substring(2))
                    {
                        btns1[i].BackgroundImage = Properties.Resources.button_Black2;
                        btns1[i].ForeColor = Color.FromArgb(64, 64, 64);
                        Returnstr = detail1[0].Substring(2);
                       
                    }
                    Controls.Add(btns1[i]);
                    btns1[i].Click += button_Click;
                }
                Location = new Point(0, 768 - Size.Height);
                return;
            }

            string[] detail = ReaderS.TestItemText.Split('|');
            var productId = ReaderS.TestItemText.Substring(ReaderS.TestItemText.IndexOf("|", StringComparison.Ordinal) + 1);
            productId = productId.Replace("|", ",");
            productId = productId.Substring(0,Math.Max(productId.Length - 1,0));
            var dttestitem = SqlData.SelectTestItem(productId);
            Button[] btns = new Button[dttestitem.Rows.Count];
            Size = new Size(1024, 82 * ((dttestitem.Rows.Count - 1) / 5 + 1) + 4);
            for (int i = 0; i < dttestitem.Rows.Count; i++)
            {
                /*
                */
                btns[i] = new Button
                {
                    BackColor = Color.Transparent,
                    BackgroundImage = Properties.Resources.button_Black1,
                    BackgroundImageLayout = ImageLayout.Stretch
                };
                btns[i].FlatAppearance.BorderSize = 0;
                btns[i].FlatStyle = FlatStyle.Flat;
                btns[i].Text = dttestitem.Rows[i][0].ToString();
                btns[i].Font = new Font("微软雅黑", btns[i].Text.Length<9?21.75F:15F, FontStyle.Regular, GraphicsUnit.Point, 134);
                btns[i].ForeColor = Color.White;
                btns[i].Location = new Point(4 + 164 * (i % 5), 82 * (i / 5) + 4);
                btns[i].Name = "button" + i;
                btns[i].Size = new Size(160, 78);
                
                if (btns[i].Text == detail[0])
                {
                    btns[i].BackgroundImage = Properties.Resources.button_Black2;
                    btns[i].ForeColor = Color.FromArgb(64, 64, 64);
                }
                Controls.Add(btns[i]);

                if (ReaderS.TestItemText.IndexOf("|" + dttestitem.Rows[i][1] + "|", StringComparison.Ordinal) + ReaderS.TestItemText.IndexOf("SearchResult", StringComparison.Ordinal) == -2)
                {
                    btns[i].Enabled = false;
                    //FontStyle.Strikeout | FontStyle.Underline
                    btns[i].Font = new Font("微软雅黑", 21.75F, FontStyle.Strikeout, GraphicsUnit.Point, 134);
                }
                btns[i].Click += button_Click;
            }
            if (ReaderS.TestItemText.IndexOf("SearchResult", StringComparison.Ordinal) > -1)
            {
                Size = new Size(1024, 82 * ((dttestitem.Rows.Count) / 5 + 1) + 4);
                Button btn = new Button
                {
                    BackColor = Color.Transparent,
                    BackgroundImage = Properties.Resources.button_Black1,
                    BackgroundImageLayout = ImageLayout.Stretch
                };
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatStyle = FlatStyle.Flat;
                btn.Font = new Font("微软雅黑", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
                btn.ForeColor = Color.White;
                btn.Location = new Point(4 + 164 * (dttestitem.Rows.Count % 5), 82 * (dttestitem.Rows.Count / 5) + 4);
                btn.Name = "button" + dttestitem.Rows.Count;
                btn.Size = new Size(160, 78);
                btn.Text = @"All";
                if (btn.Text == detail[0])
                {
                    btn.BackgroundImage = Properties.Resources.button_Black2;
                    btn.ForeColor = Color.FromArgb(64, 64, 64);
                }
                Controls.Add(btn);
                btn.Click += button_Click;
            }
            Location = new Point(0, 768 - Size.Height);
        }
    }
}