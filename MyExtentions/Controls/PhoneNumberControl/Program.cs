using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PhoneNumberControl
{
     static class Program
    {
         [STAThread]
         static void Main(String[] args)
         {

             Application.EnableVisualStyles();
             Application.SetCompatibleTextRenderingDefault(false);
             //new LSExtendedWarrenty.AppBase.WarrentyItem().GetFormatedRtf(null);
             Application.Run(new Test());
         }
    }
}
