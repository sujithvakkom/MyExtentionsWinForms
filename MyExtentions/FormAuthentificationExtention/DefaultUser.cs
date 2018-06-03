using System;
using System.Collections.Generic;
using System.Text;
using MyExtentions.Controls.FormAuthentificationExtention.IBase;

namespace MyExtentions.DIalogs.FormAuthentificationExtention
{
    public class DefaultUser:IUser
    {
        public String UserName { get; set; }
        public String Password { get; set; }
    }
}
