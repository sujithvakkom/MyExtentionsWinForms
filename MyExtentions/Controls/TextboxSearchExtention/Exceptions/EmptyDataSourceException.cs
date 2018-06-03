using System;
using System.Collections.Generic;
using System.Text;

namespace MyExtentions.Controls.TextboxSearchExtention.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    internal class EmptyDataSourceException:Exception
    {
        public EmptyDataSourceException(string Message)
            : base(Message)
        {
        }
    }
}
