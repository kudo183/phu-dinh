using System.Windows.Controls;

namespace PhuDinh.View
{
    /// <summary>
    /// Interaction logic for DonHangView.xaml
    /// </summary>
    public partial class DonHangView : UserControl
    {
        public DonHangView()
        {
            InitializeComponent();
            _tDonHangView.RefreshView();
            
            _tDonHangView.dgDonHang.SelectionChanged += dgDonHang_SelectionChanged;
        }

        void dgDonHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            var donHang = ((DataGrid)sender).SelectedItem as PhuDinhData.tDonHang;
            if(donHang == null)
            {
                _tChiTietDonHangView.FilterChiTietDonHang = null;
                _tChiTietDonHangView.RefreshView();
                return;
            }

            _tChiTietDonHangView.FilterChiTietDonHang = (p => p.MaDonHang == donHang.Ma);
            _tChiTietDonHangView.tDonHangDefault = donHang;
            _tChiTietDonHangView.RefreshView();
        }
    }
}
