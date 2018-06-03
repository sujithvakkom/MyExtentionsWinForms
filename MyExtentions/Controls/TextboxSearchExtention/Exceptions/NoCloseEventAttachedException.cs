using System;
using System.Collections.Generic;
using System.Text;

namespace MyExtentions.Controls.TextboxSearchExtention.Exceptions
{
    class NoCloseEventAttachedException : Exception
    {

        public NoCloseEventAttachedException(string Message)
            : base(Message)
        {
        }
    }
}
