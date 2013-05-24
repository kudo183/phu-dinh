using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestWpfApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<TestCombobox> lst;
        public MainWindow()
        {
           
            InitializeComponent();
            lst = new List<TestCombobox>() { new TestCombobox() };
            TestCombobox.ComboboxSource = new List<Item>() { new Item() { Text1 = "text11", Text2 = "itext21" }, new Item() { Text1 = "text12", Text2 = "text22" } };

            this.DataContext = lst;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public class TestCombobox
    {
        public DateTime Ngay { get; set; }
        public string SelectedValue { get; set; }
        public static List<Item> ComboboxSource { get; set; }
    }

    public class Item
    {
        public string Text1 { get; set; }
        public string Text2 { get; set; }

        public override string ToString()
        {
            return string.Format("{0} : {1}", Text1, Text2);
        }
    }
}
