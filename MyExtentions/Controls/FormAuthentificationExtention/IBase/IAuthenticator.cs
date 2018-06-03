using System;
using System.Collections.Generic;
using System.Text;

namespace MyExtentions.Controls.FormAuthentificationExtention.IBase
{
    public interface IAuthenticator
    {
         String Login(String UserName, String Password);
         IUser GetUser(String Tocken);
    }
}
