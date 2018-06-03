using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MyExtentions.Controls.TextboxSearchExtention.TextboxSearchExtention;
using MyExtentions.Controls.TextboxSearchExtention.Exceptions;
using MyExtentions.Controls.TextboxSearchExtention.IBase;

namespace MyExtentions.Controls.TextboxSearchExtention
{
    public partial class SearchBox : UserControl
    {
        #region Compnents
        public string SearchString
        {
            set
            {
                this.itemFilterTextBox.Text = value;
                this.itemFilterTextBox.Select(this.itemFilterTextBox.Text.Length, 0);
            }
        }
        public IDataSource DataSource
        {
            get;
            set;
        }
        public delegate void ItemSelectEventHandeler(ItemSelecteEventArg e);
        public event ItemSelectEventHandeler ItemSelect;
        public delegate void CloseEventHandeler(EventArgs e);
        public event CloseEventHandeler Close;
        #endregion
        public SearchBox(Control Parent)
        {
            this.Parent = Parent;
            InitializeComponent();
            this.Location = this.SetupLocation();
            this.GotFocus += new EventHandler(SearchBox_GotFocus);
            this.itemFilterTextBox.GotFocus += new EventHandler(itemFilterTextBox_GotFocus);
            this.itemFilterTextBox.TextChanged += new EventHandler(itemFilterTextBox_TextChanged);
            this.itemListDataGridView.CellDoubleClick += new DataGridViewCellEventHandler(itemListDataGridView_CellDoubleClick);
        }

        void itemListDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectItemIndex = e.RowIndex;
            DataTable item = (DataTable)this.itemListDataGridView.DataSource;
            string Discription = item.Rows[selectItemIndex][1].ToString();
            string ID = item.Rows[selectItemIndex][0].ToString();
            SelectItem SelectedItem = new SelectItem() { ItemId = ID, ItemName = Discription };
            SelectedItem.Properties = DataSource.SelectProperties(ID);
            if (ItemSelect != null) ItemSelect(new ItemSelecteEventArg() { SelectedItem = SelectedItem });
        }

        void itemFilterTextBox_TextChanged(object sender, EventArgs e)
        {
            var Control = (TextBox)sender;
            if (DataSource != null)
            {
                DataTable table = DataSource.Filter(Control.Text);
                this.itemListDataGridView.DataSource = table;
            }
            else
            {
                throw new EmptyDataSourceException("No Data Source Set.");
            }
        }

        void itemFilterTextBox_GotFocus(object sender, EventArgs e)
        {
            var Control = (TextBox)sender;
            Control.Select(0, Control.Text.Length);
        }

        void SearchBox_GotFocus(object sender, EventArgs e)
        {
            this.itemFilterTextBox.Focus();
        }

        private Point SetupLocation()
        {
            Point ParentLocation = Parent.Location;
            Point SearchBoxLocation = Parent.Location;
            int ParentHeight = Parent.Height;
            SearchBoxLocation.Offset(0, ParentHeight);
            return SearchBoxLocation;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            if (Close != null) Close(new EventArgs());
            else throw new NoCloseEventAttachedException("No Close Event Attached.");
        }
    }
}
