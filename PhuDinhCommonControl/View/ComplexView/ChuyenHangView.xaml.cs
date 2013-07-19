using System.Windows.Controls;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for ChuyenHangView.xaml
    /// </summary>
    public partial class ChuyenHangView : BaseView
    {
        public ChuyenHangView()
        {
            InitializeComponent();

            Loaded += ChuyenHangView_Loaded;
            Unloaded += ChuyenHangView_Unloaded;
        }

        void ChuyenHangView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _tChuyenHangView.dgChuyenHang.SelectionChanged += dgChuyenHang_SelectionChanged;
            _tChuyenHangDonHangView.dgChuyenHangDonHang.SelectionChanged += dgChuyenHangDonHang_SelectionChanged;

            _tChiTietChuyenHangDonHangView.AfterSave += _tChiTietChuyenHangDonHangView_AfterSave;

            _tChuyenHangView.RefreshView();
        }

        void ChuyenHangView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _tChuyenHangView.dgChuyenHang.SelectionChanged -= dgChuyenHang_SelectionChanged;
            _tChuyenHangDonHangView.dgChuyenHangDonHang.SelectionChanged -= dgChuyenHangDonHang_SelectionChanged;

            _tChiTietChuyenHangDonHangView.AfterSave -= _tChiTietChuyenHangDonHangView_AfterSave;
        }

        void _tChiTietChuyenHangDonHangView_AfterSave(object sender, System.EventArgs e)
        {
            _tChuyenHangView.RefreshView();
        }

        void dgChuyenHangDonHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != e.OriginalSource)
            {
                return;
            }

            var chuyenHangDonHang = ((DataGrid)sender).SelectedItem as PhuDinhData.tChuyenHangDonHang;
            if (chuyenHangDonHang == null)
            {
                _tChiTietChuyenHangDonHangView.FilterChiTietChuyenHangDonHang = null;
                _tChiTietChuyenHangDonHangView.RefreshView();
                return;
            }

            _tChiTietChuyenHangDonHangView.FilterChiTietDonHang =
                (p => p.Xong == false && p.MaDonHang == chuyenHangDonHang.MaDonHang);
            _tChiTietChuyenHangDonHangView.FilterChiTietChuyenHangDonHang =
                (p => p.MaChuyenHangDonHang == chuyenHangDonHang.Ma);
            _tChiTietChuyenHangDonHangView.tChuyenHangDonHangDefault = chuyenHangDonHang;
            _tChiTietChuyenHangDonHangView.RefreshView();
        }

        void dgChuyenHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != e.OriginalSource)
            {
                return;
            }

            _tChiTietChuyenHangDonHangView.FilterChiTietChuyenHangDonHang = null;
            _tChiTietChuyenHangDonHangView.RefreshView();

            var grid = ((DataGrid)sender);
            if (grid.SelectedIndex == -1)
            {
                return;
            }

            var chuyenHang = grid.SelectedItem as PhuDinhData.tChuyenHang;
            if (chuyenHang == null)
            {
                _tChuyenHangDonHangView.FilterChuyenHangDonHang = null;
                _tChuyenHangDonHangView.RefreshView();
                return;
            }

            _tChuyenHangDonHangView.FilterDonHang = (p => p.Xong == false);
            _tChuyenHangDonHangView.FilterChuyenHangDonHang = (p => p.MaChuyenHang == chuyenHang.Ma);
            _tChuyenHangDonHangView.tChuyenHangDefault = chuyenHang;
            _tChuyenHangDonHangView.RefreshView();
        }
    }
}
