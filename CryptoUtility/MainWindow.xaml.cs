using System.Security.Cryptography;
using System.Windows;
using PhuDinhCommon;

namespace CryptoUtility
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            txtOutput.Text = Crypto.Encrypt(txtInputText.Text, txtInputPassword.Text);
        }

        private void btnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            txtOutput.Text = Crypto.Decrypt(txtInputText.Text, txtInputPassword.Text);
        }

        private void btnHash_Click(object sender, RoutedEventArgs e)
        {
            var hasher = new SHA256Managed();
            var unhashedPassword = System.Text.Encoding.Unicode.GetBytes(txtInputText.Text);
            var hashed = hasher.ComputeHash(unhashedPassword);
            txtOutput.Text = System.Convert.ToBase64String(hashed);
        }
    }
}
