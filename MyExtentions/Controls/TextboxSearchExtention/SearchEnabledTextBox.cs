using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using MyExtentions.Controls.TextboxSearchExtention.TextboxSearchExtention;
using MyExtentions.Controls.TextboxSearchExtention.IBase;

namespace MyExtentions.Controls.TextboxSearchExtention
{
    public partial class SearchEnabledTextBox : TextBox
    {
        #region Components
        public SearchBox SearchBox
        {
            get;
            set;
        }
        internal IDataSource _DataSource;
        internal IDataSource DataSource
        {
            get { return this._DataSource; }
            set
            {
                this._DataSource = value;
            }
        }

        private SelectItem _SelectedItem;

        internal SelectItem SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                _SelectedItem = value;
                this.Text = _SelectedItem.DisplayString();
            }
        }

        #endregion
        public SearchEnabledTextBox()
            : base()
        {
            InitSearchEnabledTextBox();
        }

        private void InitSearchEnabledTextBox()
        {
            this.KeyPress += new KeyPressEventHandler(SearchEnabledTextBox_KeyPress);
        }

        void SearchEnabledTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            this.SearchBox = new SearchBox(this);
            this.SearchBox.DataSource = DataSource;
            this.SearchBox.Size = new Size(200, 300);
            this.SearchBox.Leave += new System.EventHandler(SearchBox_Leave);
            this.SearchBox.Close += new SearchBox.CloseEventHandeler(SearchBox_Close);
            Parent.Controls.Add(SearchBox);
            this.SearchBox.BringToFront();
            this.SearchBox.Focus();
            this.SearchBox.SearchString = ((TextBox)sender).Text + (char.IsControl(e.KeyChar) ? null : (object)e.KeyChar);
            this.SearchBox.ItemSelect += new SearchBox.ItemSelectEventHandeler(SearchBox_ItemSelect);
        }

        void SearchBox_Close(System.EventArgs e)
        {
            this.SearchBox.Dispose();
            Parent.Controls.Remove(this.SearchBox);
        }

        void SearchBox_ItemSelect(ItemSelecteEventArg e)
        {
            this.SelectedItem = e.SelectedItem;
            this.SearchBox.Dispose();
            Parent.Controls.Remove(this.SearchBox);
        }

        void SearchBox_Leave(object sender, System.EventArgs e)
        {
            Parent.Controls.Remove(this.SearchBox);
            this.SearchBox.Dispose();
        }
    }
}
