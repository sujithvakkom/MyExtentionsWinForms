using System;
using System.Collections.Generic;
using System.Text;
using MyExtentions.Controls.TextboxSearchExtention.IBase;

namespace MyExtentions.Controls.TextboxSearchExtention.TextboxSearchExtention
{
    public class SelectItem : ISelectItem
    {
        public object ItemId { get; set; }
        public object ItemName { get; set; }
        public KeyValuePair<String, Object> Properties { get; set; }

        #region ISelectItem Members

        public string DisplayString()
        {
            return ItemName.ToString();
        }

        #endregion
    }
}
