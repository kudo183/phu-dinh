using System.Collections.ObjectModel;
using System.Linq;
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

            _tChuyenHangView.AfterSave += _tChuyenHangView_AfterSave;
            _tChuyenHangDonHangView.AfterSave += _tChuyenHangDonHangView_AfterSave;
            _tChiTietChuyenHangDonHangView.AfterSave += _tChiTietChuyenHangDonHangView_AfterSave;

            _tChuyenHangView.RefreshView();
        }

        void ChuyenHangView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _tChuyenHangView.dgChuyenHang.SelectionChanged -= dgChuyenHang_SelectionChanged;
            _tChuyenHangDonHangView.dgChuyenHangDonHang.SelectionChanged -= dgChuyenHangDonHang_SelectionChanged;

            _tChuyenHangView.AfterSave -= _tChuyenHangView_AfterSave;
            _tChuyenHangDonHangView.AfterSave -= _tChuyenHangDonHangView_AfterSave;
            _tChiTietChuyenHangDonHangView.AfterSave -= _tChiTietChuyenHangDonHangView_AfterSave;
        }

        void _tChiTietChuyenHangDonHangView_AfterSave(object sender, System.EventArgs e)
        {
            _tChuyenHangView.RefreshView();
        }

        void _tChuyenHangDonHangView_AfterSave(object sender, System.EventArgs e)
        {
            var chuyenHangDonHang =
                _tChuyenHangDonHangView.dgChuyenHangDonHang.SelectedItem
                as PhuDinhData.tChuyenHangDonHang;

            RefreshChiTietChuyenHangDonHang(chuyenHangDonHang);

            if (chuyenHangDonHang != null && chuyenHangDonHang.tChiTietChuyenHangDonHangs.Count > 0)
            {
                return;
            }

            var context =
                _tChiTietChuyenHangDonHangView.dgChiTietChuyenHangDonHang.DataContext
                as ObservableCollection<PhuDinhData.tChiTietChuyenHangDonHang>;

            if (chuyenHangDonHang == null)
                return;

            foreach (var tChiTietDonHang in chuyenHangDonHang.tDonHang.tChiTietDonHangs.Where(p => p.Xong == false))
            {
                var ct = new PhuDinhData.tChiTietChuyenHangDonHang
                             {
                                 MaChiTietDonHang = tChiTietDonHang.Ma,
                                 tChuyenHangDonHang = chuyenHangDonHang,
                                 tChiTietDonHang = tChiTietDonHang,
                                 SoLuong = tChiTietDonHang.SoLuong - tChiTietDonHang.tChiTietChuyenHangDonHangs.Sum(p => p.SoLuong)
                             };

                context.Add(ct);
            }
        }

        void _tChuyenHangView_AfterSave(object sender, System.EventArgs e)
        {
            var chuyenHang = _tChuyenHangView.dgChuyenHang.SelectedItem as PhuDinhData.tChuyenHang;
            RefreshChuyenHangDonHangView(chuyenHang);
        }

        void dgChuyenHangDonHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != e.OriginalSource)
            {
                return;
            }

            var chuyenHangDonHang = ((DataGrid)sender).SelectedItem as PhuDinhData.tChuyenHangDonHang;
            RefreshChiTietChuyenHangDonHang(chuyenHangDonHang);
        }

        void dgChuyenHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != e.OriginalSource)
            {
                return;
            }

            var grid = ((DataGrid)sender);
            if (grid.SelectedIndex == -1)
            {
                return;
            }

            var chuyenHang = grid.SelectedItem as PhuDinhData.tChuyenHang;
            RefreshChuyenHangDonHangView(chuyenHang);
        }

        private void RefreshChuyenHangDonHangView(PhuDinhData.tChuyenHang chuyenHang)
        {
            _tChiTietChuyenHangDonHangView.FilterChiTietChuyenHangDonHang = null;
            _tChiTietChuyenHangDonHangView.RefreshView();

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

        private void RefreshChiTietChuyenHangDonHang(PhuDinhData.tChuyenHangDonHang chuyenHangDonHang)
        {
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
    }
}
