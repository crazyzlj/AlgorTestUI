namespace TestDemo
{
    partial class ConnectMapServiceFrm
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
            this.label1 = new System.Windows.Forms.Label();
            this.radio_internet = new System.Windows.Forms.RadioButton();
            this.radio_local = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.t_url = new System.Windows.Forms.TextBox();
            this.t_hostName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.t_userName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.t_userPwd = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.b_ok = new System.Windows.Forms.Button();
            this.b_exit = new System.Windows.Forms.Button();
            this.list_services = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择ArcGisServer连接类型:";
            // 
            // radio_internet
            // 
            this.radio_internet.AutoSize = true;
            this.radio_internet.Checked = true;
            this.radio_internet.Location = new System.Drawing.Point(23, 46);
            this.radio_internet.Name = "radio_internet";
            this.radio_internet.Size = new System.Drawing.Size(71, 16);
            this.radio_internet.TabIndex = 1;
            this.radio_internet.TabStop = true;
            this.radio_internet.Text = "Internet";
            this.radio_internet.UseVisualStyleBackColor = true;
            this.radio_internet.CheckedChanged += new System.EventHandler(this.radio_internet_CheckedChanged);
            // 
            // radio_local
            // 
            this.radio_local.AutoSize = true;
            this.radio_local.Location = new System.Drawing.Point(23, 95);
            this.radio_local.Name = "radio_local";
            this.radio_local.Size = new System.Drawing.Size(47, 16);
            this.radio_local.TabIndex = 2;
            this.radio_local.Text = "本地";
            this.radio_local.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(59, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "服务器URL:";
            // 
            // t_url
            // 
            this.t_url.Location = new System.Drawing.Point(128, 67);
            this.t_url.Name = "t_url";
            this.t_url.Size = new System.Drawing.Size(252, 21);
            this.t_url.TabIndex = 4;
            this.t_url.Validated += new System.EventHandler(this.t_url_Validated);
            this.t_url.KeyDown += new System.Windows.Forms.KeyEventHandler(this.t_url_KeyDown);
            // 
            // t_hostName
            // 
            this.t_hostName.Enabled = false;
            this.t_hostName.Location = new System.Drawing.Point(128, 119);
            this.t_hostName.Name = "t_hostName";
            this.t_hostName.Size = new System.Drawing.Size(252, 21);
            this.t_hostName.TabIndex = 6;
            this.t_hostName.Validated += new System.EventHandler(this.t_hostName_Validated);
            this.t_hostName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.t_hostName_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(65, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "主机名称:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(126, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(239, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "http://www.myserver.com/arcgis/services";
            // 
            // t_userName
            // 
            this.t_userName.Location = new System.Drawing.Point(126, 20);
            this.t_userName.Name = "t_userName";
            this.t_userName.Size = new System.Drawing.Size(158, 21);
            this.t_userName.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(63, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "用户名:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.t_userPwd);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.t_userName);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(23, 158);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(357, 86);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "身份验证(可选):";
            // 
            // t_userPwd
            // 
            this.t_userPwd.Location = new System.Drawing.Point(126, 47);
            this.t_userPwd.Name = "t_userPwd";
            this.t_userPwd.Size = new System.Drawing.Size(158, 21);
            this.t_userPwd.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(75, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "密码:";
            // 
            // b_ok
            // 
            this.b_ok.Location = new System.Drawing.Point(88, 371);
            this.b_ok.Name = "b_ok";
            this.b_ok.Size = new System.Drawing.Size(75, 23);
            this.b_ok.TabIndex = 11;
            this.b_ok.Text = "确定";
            this.b_ok.UseVisualStyleBackColor = true;
            this.b_ok.Click += new System.EventHandler(this.b_ok_Click);
            // 
            // b_exit
            // 
            this.b_exit.Location = new System.Drawing.Point(232, 371);
            this.b_exit.Name = "b_exit";
            this.b_exit.Size = new System.Drawing.Size(75, 23);
            this.b_exit.TabIndex = 12;
            this.b_exit.Text = "关闭";
            this.b_exit.UseVisualStyleBackColor = true;
            this.b_exit.Click += new System.EventHandler(this.b_exit_Click);
            // 
            // list_services
            // 
            this.list_services.FormattingEnabled = true;
            this.list_services.ItemHeight = 12;
            this.list_services.Location = new System.Drawing.Point(23, 269);
            this.list_services.Name = "list_services";
            this.list_services.Size = new System.Drawing.Size(357, 88);
            this.list_services.TabIndex = 13;
            // 
            // ConnectMapServiceFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 413);
            this.Controls.Add(this.list_services);
            this.Controls.Add(this.b_exit);
            this.Controls.Add(this.b_ok);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.t_hostName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.t_url);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.radio_local);
            this.Controls.Add(this.radio_internet);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConnectMapServiceFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "连接地图服务";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radio_internet;
        private System.Windows.Forms.RadioButton radio_local;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox t_url;
        private System.Windows.Forms.TextBox t_hostName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox t_userName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox t_userPwd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button b_ok;
        private System.Windows.Forms.Button b_exit;
        private System.Windows.Forms.ListBox list_services;
    }
}