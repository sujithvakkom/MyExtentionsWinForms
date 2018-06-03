using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneNumberControl
{
    class InvalidPhoneNumber : Exception
    {
        public InvalidPhoneNumber(string Message)
            : base(Message)
        {

        }
    }
}
