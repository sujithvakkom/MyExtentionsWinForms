namespace MyExtentions.DIalogs
{
    partial class FormMaintance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMaintance));
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBoxPOSConnectionSettings = new System.Windows.Forms.GroupBox();
            this.buttonPOSCheck = new System.Windows.Forms.Button();
            this.buttonGet = new System.Windows.Forms.Button();
            this.textBoxLocation = new System.Windows.Forms.TextBox();
            this.labelLocation = new System.Windows.Forms.Label();
            this.buttonApply = new System.Windows.Forms.Button();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxUser = new System.Windows.Forms.TextBox();
            this.labelUser = new System.Windows.Forms.Label();
            this.textBoxDatabase = new System.Windows.Forms.TextBox();
            this.labelDatabse = new System.Windows.Forms.Label();
            this.textBoxDataSource = new System.Windows.Forms.TextBox();
            this.labelDataSource = new System.Windows.Forms.Label();
            this.groupBoxHQConnectionSettings = new System.Windows.Forms.GroupBox();
            this.buttonHQCheck = new System.Windows.Forms.Button();
            this.buttonHQApply = new System.Windows.Forms.Button();
            this.textBoxHQPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxHQDataSource = new System.Windows.Forms.TextBox();
            this.textBoxHQUser = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxHQDatabase = new System.Windows.Forms.TextBox();
            this.groupBoxWarrentyApplicationSettings = new System.Windows.Forms.GroupBox();
            this.buttonAppliApplicationSettings = new System.Windows.Forms.Button();
            this.comboBoxPrinter = new System.Windows.Forms.ComboBox();
            this.labelPrinter = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxPOSConnectionSettings.SuspendLayout();
            this.groupBoxHQConnectionSettings.SuspendLayout();
            this.groupBoxWarrentyApplicationSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(353, 264);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "&OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // groupBoxPOSConnectionSettings
            // 
            this.groupBoxPOSConnectionSettings.Controls.Add(this.buttonPOSCheck);
            this.groupBoxPOSConnectionSettings.Controls.Add(this.buttonGet);
            this.groupBoxPOSConnectionSettings.Controls.Add(this.textBoxLocation);
            this.groupBoxPOSConnectionSettings.Controls.Add(this.labelLocation);
            this.groupBoxPOSConnectionSettings.Controls.Add(this.buttonApply);
            this.groupBoxPOSConnectionSettings.Controls.Add(this.textBoxPassword);
            this.groupBoxPOSConnectionSettings.Controls.Add(this.labelPassword);
            this.groupBoxPOSConnectionSettings.Controls.Add(this.textBoxUser);
            this.groupBoxPOSConnectionSettings.Controls.Add(this.labelUser);
            this.groupBoxPOSConnectionSettings.Controls.Add(this.textBoxDatabase);
            this.groupBoxPOSConnectionSettings.Controls.Add(this.labelDatabse);
            this.groupBoxPOSConnectionSettings.Controls.Add(this.textBoxDataSource);
            this.groupBoxPOSConnectionSettings.Controls.Add(this.labelDataSource);
            this.groupBoxPOSConnectionSettings.Location = new System.Drawing.Point(12, 12);
            this.groupBoxPOSConnectionSettings.Name = "groupBoxPOSConnectionSettings";
            this.groupBoxPOSConnectionSettings.Size = new System.Drawing.Size(200, 194);
            this.groupBoxPOSConnectionSettings.TabIndex = 1;
            this.groupBoxPOSConnectionSettings.TabStop = false;
            this.groupBoxPOSConnectionSettings.Text = "POS Connection Settings";
            // 
            // buttonPOSCheck
            // 
            this.buttonPOSCheck.Location = new System.Drawing.Point(38, 165);
            this.buttonPOSCheck.Name = "buttonPOSCheck";
            this.buttonPOSCheck.Size = new System.Drawing.Size(75, 23);
            this.buttonPOSCheck.TabIndex = 12;
            this.buttonPOSCheck.Text = "POS Check";
            this.buttonPOSCheck.UseVisualStyleBackColor = true;
            this.buttonPOSCheck.Click += new System.EventHandler(this.buttonPOSCheck_Click);
            // 
            // buttonGet
            // 
            this.buttonGet.Location = new System.Drawing.Point(153, 19);
            this.buttonGet.Name = "buttonGet";
            this.buttonGet.Size = new System.Drawing.Size(41, 23);
            this.buttonGet.TabIndex = 11;
            this.buttonGet.Text = "Get";
            this.buttonGet.UseVisualStyleBackColor = true;
            this.buttonGet.Click += new System.EventHandler(this.buttonGet_Click);
            // 
            // textBoxLocation
            // 
            this.textBoxLocation.Location = new System.Drawing.Point(79, 21);
            this.textBoxLocation.Name = "textBoxLocation";
            this.textBoxLocation.Size = new System.Drawing.Size(68, 20);
            this.textBoxLocation.TabIndex = 10;
            // 
            // labelLocation
            // 
            this.labelLocation.AutoSize = true;
            this.labelLocation.Location = new System.Drawing.Point(25, 24);
            this.labelLocation.Name = "labelLocation";
            this.labelLocation.Size = new System.Drawing.Size(48, 13);
            this.labelLocation.TabIndex = 9;
            this.labelLocation.Text = "Location";
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(119, 164);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 8;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(79, 137);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(115, 20);
            this.textBoxPassword.TabIndex = 7;
            this.textBoxPassword.UseSystemPasswordChar = true;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(20, 140);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(53, 13);
            this.labelPassword.TabIndex = 6;
            this.labelPassword.Text = "Password";
            // 
            // textBoxUser
            // 
            this.textBoxUser.Location = new System.Drawing.Point(79, 108);
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.Size = new System.Drawing.Size(115, 20);
            this.textBoxUser.TabIndex = 5;
            // 
            // labelUser
            // 
            this.labelUser.AutoSize = true;
            this.labelUser.Location = new System.Drawing.Point(44, 111);
            this.labelUser.Name = "labelUser";
            this.labelUser.Size = new System.Drawing.Size(29, 13);
            this.labelUser.TabIndex = 4;
            this.labelUser.Text = "User";
            // 
            // textBoxDatabase
            // 
            this.textBoxDatabase.Location = new System.Drawing.Point(79, 79);
            this.textBoxDatabase.Name = "textBoxDatabase";
            this.textBoxDatabase.Size = new System.Drawing.Size(115, 20);
            this.textBoxDatabase.TabIndex = 3;
            // 
            // labelDatabse
            // 
            this.labelDatabse.AutoSize = true;
            this.labelDatabse.Location = new System.Drawing.Point(20, 82);
            this.labelDatabse.Name = "labelDatabse";
            this.labelDatabse.Size = new System.Drawing.Size(53, 13);
            this.labelDatabse.TabIndex = 2;
            this.labelDatabse.Text = "Database";
            // 
            // textBoxDataSource
            // 
            this.textBoxDataSource.Location = new System.Drawing.Point(79, 50);
            this.textBoxDataSource.Name = "textBoxDataSource";
            this.textBoxDataSource.Size = new System.Drawing.Size(115, 20);
            this.textBoxDataSource.TabIndex = 1;
            // 
            // labelDataSource
            // 
            this.labelDataSource.AutoSize = true;
            this.labelDataSource.Location = new System.Drawing.Point(6, 53);
            this.labelDataSource.Name = "labelDataSource";
            this.labelDataSource.Size = new System.Drawing.Size(67, 13);
            this.labelDataSource.TabIndex = 0;
            this.labelDataSource.Text = "Data Source";
            // 
            // groupBoxHQConnectionSettings
            // 
            this.groupBoxHQConnectionSettings.Controls.Add(this.buttonHQCheck);
            this.groupBoxHQConnectionSettings.Controls.Add(this.buttonHQApply);
            this.groupBoxHQConnectionSettings.Controls.Add(this.textBoxHQPassword);
            this.groupBoxHQConnectionSettings.Controls.Add(this.label1);
            this.groupBoxHQConnectionSettings.Controls.Add(this.label4);
            this.groupBoxHQConnectionSettings.Controls.Add(this.textBoxHQDataSource);
            this.groupBoxHQConnectionSettings.Controls.Add(this.textBoxHQUser);
            this.groupBoxHQConnectionSettings.Controls.Add(this.label2);
            this.groupBoxHQConnectionSettings.Controls.Add(this.label3);
            this.groupBoxHQConnectionSettings.Controls.Add(this.textBoxHQDatabase);
            this.groupBoxHQConnectionSettings.Location = new System.Drawing.Point(235, 12);
            this.groupBoxHQConnectionSettings.Name = "groupBoxHQConnectionSettings";
            this.groupBoxHQConnectionSettings.Size = new System.Drawing.Size(200, 169);
            this.groupBoxHQConnectionSettings.TabIndex = 2;
            this.groupBoxHQConnectionSettings.TabStop = false;
            this.groupBoxHQConnectionSettings.Text = "HQ Connection Settings";
            // 
            // buttonHQCheck
            // 
            this.buttonHQCheck.Location = new System.Drawing.Point(37, 135);
            this.buttonHQCheck.Name = "buttonHQCheck";
            this.buttonHQCheck.Size = new System.Drawing.Size(75, 23);
            this.buttonHQCheck.TabIndex = 9;
            this.buttonHQCheck.Text = "HQ Check";
            this.buttonHQCheck.UseVisualStyleBackColor = true;
            this.buttonHQCheck.Click += new System.EventHandler(this.buttonHQCheck_Click);
            // 
            // buttonHQApply
            // 
            this.buttonHQApply.Location = new System.Drawing.Point(118, 135);
            this.buttonHQApply.Name = "buttonHQApply";
            this.buttonHQApply.Size = new System.Drawing.Size(75, 23);
            this.buttonHQApply.TabIndex = 8;
            this.buttonHQApply.Text = "Apply";
            this.buttonHQApply.UseVisualStyleBackColor = true;
            this.buttonHQApply.Click += new System.EventHandler(this.buttonHQApply_Click);
            // 
            // textBoxHQPassword
            // 
            this.textBoxHQPassword.Location = new System.Drawing.Point(79, 108);
            this.textBoxHQPassword.Name = "textBoxHQPassword";
            this.textBoxHQPassword.Size = new System.Drawing.Size(115, 20);
            this.textBoxHQPassword.TabIndex = 7;
            this.textBoxHQPassword.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Data Source";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Password";
            // 
            // textBoxHQDataSource
            // 
            this.textBoxHQDataSource.Location = new System.Drawing.Point(79, 21);
            this.textBoxHQDataSource.Name = "textBoxHQDataSource";
            this.textBoxHQDataSource.Size = new System.Drawing.Size(115, 20);
            this.textBoxHQDataSource.TabIndex = 1;
            // 
            // textBoxHQUser
            // 
            this.textBoxHQUser.Location = new System.Drawing.Point(79, 79);
            this.textBoxHQUser.Name = "textBoxHQUser";
            this.textBoxHQUser.Size = new System.Drawing.Size(115, 20);
            this.textBoxHQUser.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Database";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "User";
            // 
            // textBoxHQDatabase
            // 
            this.textBoxHQDatabase.Location = new System.Drawing.Point(79, 50);
            this.textBoxHQDatabase.Name = "textBoxHQDatabase";
            this.textBoxHQDatabase.Size = new System.Drawing.Size(115, 20);
            this.textBoxHQDatabase.TabIndex = 3;
            // 
            // groupBoxWarrentyApplicationSettings
            // 
            this.groupBoxWarrentyApplicationSettings.Controls.Add(this.buttonAppliApplicationSettings);
            this.groupBoxWarrentyApplicationSettings.Controls.Add(this.comboBoxPrinter);
            this.groupBoxWarrentyApplicationSettings.Controls.Add(this.labelPrinter);
            this.groupBoxWarrentyApplicationSettings.Location = new System.Drawing.Point(235, 187);
            this.groupBoxWarrentyApplicationSettings.Name = "groupBoxWarrentyApplicationSettings";
            this.groupBoxWarrentyApplicationSettings.Size = new System.Drawing.Size(200, 74);
            this.groupBoxWarrentyApplicationSettings.TabIndex = 3;
            this.groupBoxWarrentyApplicationSettings.TabStop = false;
            this.groupBoxWarrentyApplicationSettings.Text = "Application Settings";
            // 
            // buttonAppliApplicationSettings
            // 
            this.buttonAppliApplicationSettings.Location = new System.Drawing.Point(119, 47);
            this.buttonAppliApplicationSettings.Name = "buttonAppliApplicationSettings";
            this.buttonAppliApplicationSettings.Size = new System.Drawing.Size(75, 23);
            this.buttonAppliApplicationSettings.TabIndex = 2;
            this.buttonAppliApplicationSettings.Text = "Apply";
            this.buttonAppliApplicationSettings.UseVisualStyleBackColor = true;
            this.buttonAppliApplicationSettings.Click += new System.EventHandler(this.buttonAppliApplicationSettings_Click);
            // 
            // comboBoxPrinter
            // 
            this.comboBoxPrinter.FormattingEnabled = true;
            this.comboBoxPrinter.Location = new System.Drawing.Point(47, 20);
            this.comboBoxPrinter.Name = "comboBoxPrinter";
            this.comboBoxPrinter.Size = new System.Drawing.Size(147, 21);
            this.comboBoxPrinter.TabIndex = 1;
            // 
            // labelPrinter
            // 
            this.labelPrinter.AutoSize = true;
            this.labelPrinter.Location = new System.Drawing.Point(6, 23);
            this.labelPrinter.Name = "labelPrinter";
            this.labelPrinter.Size = new System.Drawing.Size(37, 13);
            this.labelPrinter.TabIndex = 0;
            this.labelPrinter.Text = "Printer";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(272, 264);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormMaintance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 294);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBoxWarrentyApplicationSettings);
            this.Controls.Add(this.groupBoxHQConnectionSettings);
            this.Controls.Add(this.groupBoxPOSConnectionSettings);
            this.Controls.Add(this.buttonOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(460, 333);
            this.MinimumSize = new System.Drawing.Size(460, 333);
            this.Name = "FormMaintance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Maintenance";
            this.groupBoxPOSConnectionSettings.ResumeLayout(false);
            this.groupBoxPOSConnectionSettings.PerformLayout();
            this.groupBoxHQConnectionSettings.ResumeLayout(false);
            this.groupBoxHQConnectionSettings.PerformLayout();
            this.groupBoxWarrentyApplicationSettings.ResumeLayout(false);
            this.groupBoxWarrentyApplicationSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.GroupBox groupBoxPOSConnectionSettings;
        private System.Windows.Forms.GroupBox groupBoxHQConnectionSettings;
        private System.Windows.Forms.GroupBox groupBoxWarrentyApplicationSettings;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxUser;
        private System.Windows.Forms.Label labelUser;
        private System.Windows.Forms.TextBox textBoxDatabase;
        private System.Windows.Forms.Label labelDatabse;
        private System.Windows.Forms.TextBox textBoxDataSource;
        private System.Windows.Forms.Label labelDataSource;
        private System.Windows.Forms.Button buttonHQApply;
        private System.Windows.Forms.TextBox textBoxHQPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxHQDataSource;
        private System.Windows.Forms.TextBox textBoxHQUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxHQDatabase;
        private System.Windows.Forms.Button buttonAppliApplicationSettings;
        private System.Windows.Forms.ComboBox comboBoxPrinter;
        private System.Windows.Forms.Label labelPrinter;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxLocation;
        private System.Windows.Forms.Label labelLocation;
        private System.Windows.Forms.Button buttonGet;
        private System.Windows.Forms.Button buttonPOSCheck;
        private System.Windows.Forms.Button buttonHQCheck;
    }
}

