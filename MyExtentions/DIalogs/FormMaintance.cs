using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Data.SqlClient;
using MyExtentions.Controls.FormAuthentificationExtention;

namespace MyExtentions.DIalogs
{
    public partial class FormMaintance : Form
    {
        public FormMaintance()
        {
            InitializeComponent();
            var printers = PrinterSettings.InstalledPrinters;
            foreach (var x in printers)
            {
                this.comboBoxPrinter.Items.Add(x.ToString());
            }
            this.textBoxDataSource.Text = SettingsProvider.GetDataSource();
            this.textBoxDatabase.Text = SettingsProvider.GetDatabase();
            this.textBoxUser.Text = SettingsProvider.GetUserName();
            this.textBoxPassword.Text = SettingsProvider.GetPassword();
            this.comboBoxPrinter.Text = SettingsProvider.GetDefaultPrinter();

            /*
            this.textBoxHQDataSource.Text = SettingsProvider.GetHQDataSource();
            this.textBoxHQDatabase.Text = SettingsProvider.GetHQDatabase();
            this.textBoxHQUser.Text = SettingsProvider.GetHQUserName();
            this.textBoxHQPassword.Text = SettingsProvider.GetHQPassword();
            */
            this.textBoxHQDataSource.Text = SettingsProvider.GetHQDataSource().Length == 0 ? "GSLSR" : SettingsProvider.GetHQDataSource();
            this.textBoxHQDatabase.Text = SettingsProvider.GetHQDatabase().Length == 0 ? "Sitemanager" : SettingsProvider.GetHQDatabase();
            this.textBoxHQUser.Text = SettingsProvider.GetHQUserName().Length == 0 ?"sa":SettingsProvider.GetHQUserName();
            this.textBoxHQPassword.Text = SettingsProvider.GetHQPassword().Length == 0?"gstoreshq100":SettingsProvider.GetHQPassword();

            if (SettingsProvider.GetHQDataSource().Length == 0)
            {
                SettingsProvider.SetHQDataSource("GSLSR");
                SettingsProvider.SetHQDatabase("Sitemanager");
                SettingsProvider.SetHQUserName("sa");
                SettingsProvider.SetHQPassword("gstoreshq100");
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
            //new FormExtendedWarrenty().Show();
        }

        private void buttonAppliApplicationSettings_Click(object sender, EventArgs e)
        {
            if (this.Authorize())
                SettingsProvider.SetDefaultPrinter(this.comboBoxPrinter.Text);
            else
                MessageBox.Show("User Name or password Wrong", "Cannot Save");
        }

        public bool Authorize()
        {
            var frm = new Authorize();
            frm.Parent = this.Parent;
            if (frm.ShowDialog(this.Parent) == DialogResult.Yes) return true;
            else return false;
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {

            if (this.Authorize())
            {
                SettingsProvider.SetDataSource(this.textBoxDataSource.Text.Trim());
                SettingsProvider.SetDatabase(this.textBoxDatabase.Text.Trim());
                SettingsProvider.SetUserName(this.textBoxUser.Text.Trim());
                SettingsProvider.SetPassword(this.textBoxPassword.Text.Trim());
                /*
                try
                {

                    DialogResult result = MessageBox.Show(this, "Do you want to Update Database?", "Update Database", MessageBoxButtons.YesNo);

                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        List<AppUpdator.XX_EXTENDED_SETTINGS> ExtendedWarrentySettings =
                            AppUpdator.DataProiver.GetWarrentySettings(
                            SettingsProvider.GetHQDataSource(),
                            SettingsProvider.GetHQDatabase(),
                            SettingsProvider.GetHQUserName(),
                            SettingsProvider.GetHQPassword(), null
                            );

                        AppUpdator.LocationSettings SettingsLocationSettings = new AppUpdator.LocationSettings()
                        {
                            ServerAddress = SettingsProvider.GetDataSource(),
                            Database = SettingsProvider.GetDatabase(),
                            User = SettingsProvider.GetUserName(),
                            Password = SettingsProvider.GetPassword()
                        };
                        SettingsLocationSettings.Init();
                        SettingsLocationSettings.UpdateWarrentySettings(ExtendedWarrentySettings);
                        AppUpdator.SettingServerSettings SettingsServerSettings = new AppUpdator.SettingServerSettings()
                        {
                            ServerAddress = SettingsProvider.GetHQDataSource(),
                            Database = SettingsProvider.GetHQDatabase(),
                            User = SettingsProvider.GetHQUserName(),
                            Password = SettingsProvider.GetHQPassword()
                        };
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Error Updating.");
                }
                this.Cursor = Cursors.Default;
            */
                
            }
            else
                MessageBox.Show("User Name or password Wrong", "Cannot Save");
        }

        private void buttonHQApply_Click(object sender, EventArgs e)
        {


            if (this.Authorize())
            {
                SettingsProvider.SetHQDataSource(this.textBoxHQDataSource.Text.Trim());
                SettingsProvider.SetHQDatabase(this.textBoxHQDatabase.Text.Trim());
                SettingsProvider.SetHQUserName(this.textBoxHQUser.Text.Trim());
                SettingsProvider.SetHQPassword(this.textBoxHQPassword.Text.Trim());
            }
            else
                MessageBox.Show("User Name or password Wrong", "Cannot Save");
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void buttonGet_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonPOSCheck_Click(object sender, EventArgs e)
        {
            String Error =null;
            if (FormMaintance.ConnectionCheck(this.textBoxDataSource.Text,
                this.textBoxDatabase.Text,
                this.textBoxUser.Text,
                this.textBoxPassword.Text,ref Error))
            {
                MessageBox.Show(this, "Connection Success", "Success",MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show(this, "Connection Failed\n"+Error, "Failed",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private static bool ConnectionCheck(string DataSource, string Database, string User, string Password,ref string  Error)
        {
            bool success = false;

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = DataSource;
            builder.InitialCatalog = Database;
            builder.UserID = User;
            builder.Password = Password;

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                try
                {
                    connection.Open();
                    success = true;
                }
                catch (Exception ex)
                {
                    Error = ex.Message;
                }
                finally
                {
                    connection.Close();
                }
            }

            return success;
        }

        private void buttonHQCheck_Click(object sender, EventArgs e)
        {

            String Error = null;
            if (FormMaintance.ConnectionCheck(this.textBoxHQDataSource.Text,
                this.textBoxHQDatabase.Text,
                this.textBoxHQUser.Text,
                this.textBoxHQPassword.Text, ref Error))
            {
                MessageBox.Show(this, "Connection Success", "Success", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show(this, "Connection Failed\n" + Error, "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
