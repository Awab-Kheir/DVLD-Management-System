using DVLD.Global_Classes;
using DVLD.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmMain());
            /*********/
            //  مشان الوان الفورمز تتفير II
            Application.Idle += (sender, e) =>
            {
                ThemeManager.ApplyThemeToOpenForms();
            };
            /*********/


            Application.Run(new frmLogin());
        }
    }
}
