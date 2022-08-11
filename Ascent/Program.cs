using System;
using System.Linq;
using System.Windows.Forms;

namespace Ascent
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    if (db.Users.FirstOrDefault(c => c.logins == Helper.GetMd5Hash("admin")) == null)
                    {
                        db.Users.Add(new Users()
                        {
                            logins = Helper.GetMd5Hash("admin"),
                            Passwords = Helper.GetMd5Hash("admin"),
                           
                        });
                        db.SaveChanges();
                    }
                }

                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Auth());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
