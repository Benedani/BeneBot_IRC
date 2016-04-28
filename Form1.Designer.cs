namespace IRCbot
{
    partial class Form1
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.connbutton = new System.Windows.Forms.Button();
            this.console_tbox = new System.Windows.Forms.TextBox();
            this.sendbutton = new System.Windows.Forms.Button();
            this.sendmsg_box = new System.Windows.Forms.TextBox();
            this.userlist_tbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "Server";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(13, 39);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 1;
            this.textBox2.Text = "Channel";
            // 
            // connbutton
            // 
            this.connbutton.Location = new System.Drawing.Point(13, 66);
            this.connbutton.Name = "connbutton";
            this.connbutton.Size = new System.Drawing.Size(100, 23);
            this.connbutton.TabIndex = 2;
            this.connbutton.Text = "Connect";
            this.connbutton.UseVisualStyleBackColor = true;
            this.connbutton.Click += new System.EventHandler(this.onClickConnect);
            // 
            // console_tbox
            // 
            this.console_tbox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.console_tbox.Location = new System.Drawing.Point(163, 13);
            this.console_tbox.Multiline = true;
            this.console_tbox.Name = "console_tbox";
            this.console_tbox.ReadOnly = true;
            this.console_tbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.console_tbox.Size = new System.Drawing.Size(329, 292);
            this.console_tbox.TabIndex = 3;
            // 
            // sendbutton
            // 
            this.sendbutton.Location = new System.Drawing.Point(451, 311);
            this.sendbutton.Name = "sendbutton";
            this.sendbutton.Size = new System.Drawing.Size(41, 23);
            this.sendbutton.TabIndex = 4;
            this.sendbutton.Text = "Send";
            this.sendbutton.UseVisualStyleBackColor = true;
            this.sendbutton.Click += new System.EventHandler(this.onClickSend);
            // 
            // sendmsg_box
            // 
            this.sendmsg_box.Location = new System.Drawing.Point(163, 313);
            this.sendmsg_box.Name = "sendmsg_box";
            this.sendmsg_box.Size = new System.Drawing.Size(282, 20);
            this.sendmsg_box.TabIndex = 5;
            this.sendmsg_box.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onEnterSend);
            // 
            // userlist_tbox
            // 
            this.userlist_tbox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.userlist_tbox.Location = new System.Drawing.Point(13, 118);
            this.userlist_tbox.Multiline = true;
            this.userlist_tbox.Name = "userlist_tbox";
            this.userlist_tbox.ReadOnly = true;
            this.userlist_tbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.userlist_tbox.Size = new System.Drawing.Size(122, 215);
            this.userlist_tbox.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Users";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 352);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.userlist_tbox);
            this.Controls.Add(this.sendmsg_box);
            this.Controls.Add(this.sendbutton);
            this.Controls.Add(this.console_tbox);
            this.Controls.Add(this.connbutton);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "BeneBot (IRC)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button connbutton;
        public System.Windows.Forms.TextBox console_tbox;
        private System.Windows.Forms.Button sendbutton;
        private System.Windows.Forms.TextBox sendmsg_box;
        public System.Windows.Forms.TextBox userlist_tbox;
        private System.Windows.Forms.Label label1;
    }
}