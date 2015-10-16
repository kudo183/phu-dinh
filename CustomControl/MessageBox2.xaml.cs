using System.Windows;

namespace CustomControl
{
    /// <summary>
    /// Interaction logic for MessageBox2.xaml
    /// </summary>
    public partial class MessageBox2 : Window
    {
        public MessageBox2()
        {
            InitializeComponent();
        }

        public static void Show(
            System.Collections.Generic.List<MessageBox2Data> message,
            double width = 300,
            double height = 200)
        {
            MessageBox2.Show("", message, width, height);
        }

        public static void Show(string title,
            System.Collections.Generic.List<MessageBox2Data> message,
            double width = 300,
            double height = 200)
        {
            var messageBox = new MessageBox2
            {
                Title = title,
                DataContext = message,
                Width = width,
                Height = height,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            messageBox.ShowDialog();
        }

        public class MessageBox2Data
        {
            public string Title { get; set; }
            public string Content { get; set; }
        }
    }
}
