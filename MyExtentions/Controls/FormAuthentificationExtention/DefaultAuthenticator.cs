using System;
using System.Collections.Generic;
using System.Text;
using MyExtentions.Controls.FormAuthentificationExtention.IBase;

namespace MyExtentions.Controls.FormAuthentificationExtention
{
    class DefaultAuthenticator : IAuthenticator
    {
        const string SEPERATOR ="?:" ;
        #region IAuthenticator Members

        public string Login(string UserName, string Password)
        {
            String password = "ADMIN" + DateTime.Now.ToString("ddMMyyyy");
            if (UserName == "ADMIN" && Password == password)
            {
                return UserName + SEPERATOR + Password;
            }
            else return null;
        }

        public IUser GetUser(string Tocken)
        {
            String[] x= Tocken.Split(new String[]{SEPERATOR},StringSplitOptions.None);
            return new DefaultUser() { UserName = x[0], Password = x[1] };
        }

        #endregion
    }
}
