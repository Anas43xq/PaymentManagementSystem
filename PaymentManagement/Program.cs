using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaymentManagement
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

            // Show login form first
            using (var login = new Forms.Auth.frmLogin())
            {
                var result = login.ShowDialog();
                if (result != System.Windows.Forms.DialogResult.OK)
                    return; // exit if not authenticated
            }

            Application.Run(new MainForm());
        }
    }
}
