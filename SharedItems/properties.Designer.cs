namespace AdvertBase
{
    partial class properties
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.serverName = new System.Windows.Forms.TextBox();
            this.dbName = new System.Windows.Forms.TextBox();
            this.dbuser = new System.Windows.Forms.TextBox();
            this.dbpass = new System.Windows.Forms.TextBox();
            this.dbPort = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(61, 227);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "ОК";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(189, 227);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Отмена";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Сервер";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "База";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(58, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Имя пользователя";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(58, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Пароль";
            // 
            // serverName
            // 
            this.serverName.Location = new System.Drawing.Point(164, 32);
            this.serverName.Name = "serverName";
            this.serverName.Size = new System.Drawing.Size(100, 20);
            this.serverName.TabIndex = 6;
            // 
            // dbName
            // 
            this.dbName.Location = new System.Drawing.Point(164, 79);
            this.dbName.Name = "dbName";
            this.dbName.Size = new System.Drawing.Size(100, 20);
            this.dbName.TabIndex = 7;
            this.dbName.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // dbuser
            // 
            this.dbuser.Location = new System.Drawing.Point(164, 116);
            this.dbuser.Name = "dbuser";
            this.dbuser.Size = new System.Drawing.Size(100, 20);
            this.dbuser.TabIndex = 8;
            // 
            // dbpass
            // 
            this.dbpass.Location = new System.Drawing.Point(164, 156);
            this.dbpass.Name = "dbpass";
            this.dbpass.Size = new System.Drawing.Size(100, 20);
            this.dbpass.TabIndex = 9;
            // 
            // dbPort
            // 
            this.dbPort.Location = new System.Drawing.Point(164, 191);
            this.dbPort.Name = "dbPort";
            this.dbPort.Size = new System.Drawing.Size(100, 20);
            this.dbPort.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(58, 191);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Порт";
            // 
            // properties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 262);
            this.Controls.Add(this.dbPort);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dbpass);
            this.Controls.Add(this.dbuser);
            this.Controls.Add(this.dbName);
            this.Controls.Add(this.serverName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "properties";
            this.Text = "properties";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox serverName;
        public System.Windows.Forms.TextBox dbName;
        public System.Windows.Forms.TextBox dbuser;
        public System.Windows.Forms.TextBox dbpass;
        public System.Windows.Forms.TextBox dbPort;
        private System.Windows.Forms.Label label5;
    }
}