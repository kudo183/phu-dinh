using System.Windows.Controls;

namespace PhuDinh.View
{
    /// <summary>
    /// Interaction logic for ChuyenHangView.xaml
    /// </summary>
    public partial class ChuyenHangView : UserControl
    {
        public ChuyenHangView()
        {
            InitializeComponent();
            _tChuyenHangView.RefreshView();
            
            _tChuyenHangView.dgChuyenHang.SelectionChanged += dgChuyenHang_SelectionChanged;
            _tChuyenHangDonHangView.dgChuyenHangDonHang.SelectionChanged += dgChuyenHangDonHang_SelectionChanged;
        }

        void dgChuyenHangDonHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var chuyenHangDonHang = ((DataGrid)sender).SelectedItem as PhuDinhData.tChuyenHangDonHang;
            if (chuyenHangDonHang == null)
            {
                _tChiTietChuyenHangDonHangView.FilterChiTietChuyenHangDonHang = null;
                _tChiTietChuyenHangDonHangView.RefreshView();
                return;
            }

            _tChiTietChuyenHangDonHangView.FilterChiTietChuyenHangDonHang =
                (p => p.MaChuyenHangDonHang == chuyenHangDonHang.Ma);

            _tChiTietChuyenHangDonHangView.RefreshView();
        }

        void dgChuyenHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _tChiTietChuyenHangDonHangView.FilterChiTietChuyenHangDonHang = null;
            _tChiTietChuyenHangDonHangView.RefreshView();

            var chuyenHang = ((DataGrid)sender).SelectedItem as PhuDinhData.tChuyenHang;
            if(chuyenHang == null)
            {
                _tChuyenHangDonHangView.FilterChuyenHangDonHang = null;
                _tChuyenHangDonHangView.RefreshView();
                return;
            }

            _tChuyenHangDonHangView.FilterChuyenHangDonHang = (p => p.MaChuyenHang == chuyenHang.Ma);
            _tChuyenHangDonHangView.RefreshView();
        }
    }
}
