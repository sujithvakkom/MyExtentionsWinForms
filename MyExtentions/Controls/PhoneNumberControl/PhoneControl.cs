using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PhoneNumberControl
{
    public partial class PhoneControl : UserControl
    {
        public bool IsControlValid
        {
            get
            {
                this.ValidationMessage = "";
                bool valid = false;
                foreach (DataRow country in Countries.Rows)
                {
                    if (this.comboBoxCountry.Text == country[Resource.COUNTRY].ToString())
                    {
                        if (this.textBoxPhoneNumber.Text.Length >= Int32.Parse(Resource.MIN_PHONE_LENGTH))
                            return true;
                        else
                        {
                            ValidationMessage="Selected Phone Number is Not Valid";
                            return false;
                        }
                    }
                }
                ValidationMessage="Selected Country is Not Valid";
                return valid;
            }
        }

        public String Country
        {
            get
            {
                if (!IsControlValid) throw new InvalidPhoneNumber("Phone Number is Invalid");
                return this.comboBoxCountry.Text.Trim();
            }
        }

        public String PhoneNumber
        {
            get
            {
                if (!IsControlValid) throw new InvalidPhoneNumber("Phone Number is Invalid");
                return this.textBoxPhoneCode.Text.Trim() + this.textBoxPhoneNumber.Text.Trim();
            }
        }
        
        DataTable Countries { get; set; }
        
        public PhoneControl()
        {
            InitData();
            InitializeComponent();
            comboBoxCountry.DataSource = Countries;
            comboBoxCountry.DisplayMember = Resource.COUNTRY;
            comboBoxCountry.SelectedIndex = -1;
            textBoxPhoneCode.Text = "";
        }

        private void InitData()
        {
            Countries = new DataTable();
            Countries.Columns.Add(Resource.COUNTRY, typeof(string));
            Countries.Columns.Add(Resource.AREA_CODE, typeof(string));
            Countries.Columns.Add(Resource.PHONE_CODE, typeof(string));
            string countryData = Resource.CountryDetails;
            string[] countryDataLines = countryData.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var country in countryDataLines)
            {
                Countries.Rows.Add(country.Split(new[] { '\t' }));
            }
        }

        private void comboBoxCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.textBoxPhoneCode.Text = Countries.Rows[((ComboBox)sender).SelectedIndex][Resource.PHONE_CODE].ToString();
            }
            catch (IndexOutOfRangeException) { }
        }

        private void textBoxPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        public void ClearUI()
        {
            this.comboBoxCountry.SelectedIndex = -1;
            this.textBoxPhoneCode.Text = "";
            this.textBoxPhoneNumber.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this,this.comboBoxCountry.SelectedIndex.ToString(),
                this.comboBoxCountry.Text);
        }

        private void PhoneControl_TabIndexChanged(object sender, EventArgs e)
        {
            this.comboBoxCountry.TabIndex = this.TabIndex + this.comboBoxCountry.TabIndex;
            this.textBoxPhoneNumber.TabIndex = this.TabIndex + this.textBoxPhoneNumber.TabIndex;
        }

        public string ValidationMessage { get; set; }
    }
}
