using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using PhuDinhData;

namespace TestWpfApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
         
            string s = "metadata=res://*/PhuDinh.csdl|res://*/PhuDinh.ssdl|res://*/PhuDinh.msl;provider=System.Data.SqlClient;provider connection string=\"Data Source=sqlhuy.hopto.org;Initial Catalog=PhuDinh;User ID=phudinh;Password=phudinh183\"";
            var context = new PhuDinhEntities(s);
            context.Database.Log = Console.WriteLine;
            Pager.Source = context.tChiTietDonHangs.OrderBy(p => p.Ma);            
        }
    }
}
