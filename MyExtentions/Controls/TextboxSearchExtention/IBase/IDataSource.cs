using System;
using System.Collections.Generic;
using System.Text;

namespace MyExtentions.Controls.TextboxSearchExtention.IBase
{
    public interface IDataSource
    {
        System.Data.DataTable Filter(string p);
        KeyValuePair<String, Object> SelectProperties(Object ID);
    }
}
