using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MyExtentions.Controls.FormAuthentificationExtention.IBase;

namespace MyExtentions.Controls.FormAuthentificationExtention
{
    public partial class Authorize : Form
    {
        public Authorize()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            IAuthenticator x = new DefaultAuthenticator();
            this.Tocken =
            x.Login(
                textBoxUserName.Text, textBoxPassword.Text);
            if (Tocken != null)
            {
                this.DialogResult = DialogResult.Yes;
            }
            else this.DialogResult = DialogResult.No;
        }

        public string Tocken { get; set; }
    }
}
