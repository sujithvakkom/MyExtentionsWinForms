using MyExtentions.Controls.FormAuthentificationExtention.IBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyExtentions.DIalogs.FormAuthentificationExtention.IBase
{
    public interface IAuthenticator
    {
         String Login(String UserName, String Password);
         IUser GetUser(String Tocken);
    }
}
