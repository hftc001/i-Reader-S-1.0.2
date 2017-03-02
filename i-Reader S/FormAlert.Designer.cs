namespace i_Reader_S
{
    partial class FormAlert
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAlert));
            this.btnMessageClose = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMessage = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnMessageClose
            // 
            this.btnMessageClose.BackColor = System.Drawing.Color.Transparent;
            this.btnMessageClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMessageClose.BackgroundImage")));
            this.btnMessageClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnMessageClose.FlatAppearance.BorderSize = 0;
            this.btnMessageClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMessageClose.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMessageClose.ForeColor = System.Drawing.Color.White;
            this.btnMessageClose.Location = new System.Drawing.Point(242, 3);
            this.btnMessageClose.Name = "btnMessageClose";
            this.btnMessageClose.Size = new System.Drawing.Size(57, 38);
            this.btnMessageClose.TabIndex = 62;
            this.btnMessageClose.UseVisualStyleBackColor = false;
            this.btnMessageClose.Click += new System.EventHandler(this.btnMessageClose_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblMessage);
            this.panel1.Location = new System.Drawing.Point(2, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(235, 106);
            this.panel1.TabIndex = 63;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoEllipsis = true;
            this.lblMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(127)))), ((int)(((byte)(186)))));
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMessage.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMessage.ForeColor = System.Drawing.Color.White;
            this.lblMessage.Location = new System.Drawing.Point(0, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(235, 106);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "样本1001取片失败";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormAlert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(127)))), ((int)(((byte)(186)))));
            this.ClientSize = new System.Drawing.Size(300, 114);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnMessageClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormAlert";
            this.Text = "FormTest";
            this.Load += new System.EventHandler(this.FormTest_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnMessageClose;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMessage;
    }
}