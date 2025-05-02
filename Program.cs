using System;
using System.Windows.Forms;
using HR.Core;
using HR.UI.Forms;
using DevExpress.LookAndFeel;
using DevExpress.UserSkins;

namespace HR
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Initialize DevExpress skins and themes
            BonusSkins.Register();
            UserLookAndFeel.Default.SetSkinStyle("Office 2019 Colorful");

            try
            {
                // Initialize the connection to the database
                if (!ConnectionManager.Initialize())
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                        "Failed to connect to the database. Please check your connection settings and try again.",
                        "Database Connection Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                // Check if system is configured
                bool isConfigured = ConnectionManager.IsSystemConfigured();

                // Initialize session manager
                SessionManager.Initialize();

                // Start the appropriate form
                if (!isConfigured)
                {
                    // System needs initial setup
                    Application.Run(new SetupWizard());
                }
                else
                {
                    // System is configured, show splash screen and login
                    Application.Run(new SplashScreen());
                }
            }
            catch (Exception ex)
            {
                LogManager.LogException(ex);
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "An unexpected error occurred: " + ex.Message,
                    "Application Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
