using PhuDinhCommon;
using System.Configuration;
using System.Globalization;
using System.Security.Cryptography;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PhuDinhXBAP
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            LogManager.Log(event_type.et_Internal, severity_type.st_info, new string('*', 20));
            LogManager.Log(event_type.et_Internal, severity_type.st_info, "Start main page");

            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("vi-VN");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProcessLogin();
        }

        private void txtPassword_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                ProcessLogin();
            }
        }

        private void ProcessLogin()
        {
            var hasher = new SHA256Managed();
            var unhashedPassword = System.Text.Encoding.Unicode.GetBytes(txtPassword.Password);
            var hashed = hasher.ComputeHash(unhashedPassword);
            var base64 = System.Convert.ToBase64String(hashed);

            if (string.Equals(base64, ConfigurationManager.AppSettings["LoginHash"]))
            {
                mainContent.Visibility = Visibility.Visible;
                loginSP.Visibility = Visibility.Collapsed;
            }
        }
    }
}
