using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for ChuyenHangView.xaml
    /// </summary>
    public partial class ChuyenHangView : UserControl
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
            _tChiTietChuyenHangDonHangView.ViewModel.MainFilter.SetFilter(
                PhuDinhData.Filter.Filter_tChiTietChuyenHangDonHang.MaChuyenHangDonHang, null, true);

            _tChiTietChuyenHangDonHangView.RefreshView();

            if (chuyenHang == null)
            {
                _tChuyenHangDonHangView.ViewModel.MainFilter
                    .SetFilter(PhuDinhData.Filter.Filter_tChuyenHangDonHang.MaChuyenHang, null,true);

                _tChuyenHangDonHangView.RefreshView();
                return;
            }

            _tChuyenHangDonHangView.ViewModel.RFilter_DonHang = (p => p.Xong == false);

            _tChuyenHangDonHangView.ViewModel.MainFilter.
                SetFilter(PhuDinhData.Filter.Filter_tChuyenHangDonHang.MaChuyenHang, chuyenHang.Ma);

            _tChuyenHangDonHangView.ViewModel.tChuyenHangDefault = chuyenHang;
            _tChuyenHangDonHangView.RefreshView();
        }

        private void RefreshChiTietChuyenHangDonHang(PhuDinhData.tChuyenHangDonHang chuyenHangDonHang)
        {
            if (chuyenHangDonHang == null)
            {
                _tChiTietChuyenHangDonHangView.ViewModel.MainFilter.SetFilter(
                    PhuDinhData.Filter.Filter_tChiTietChuyenHangDonHang.MaChuyenHangDonHang, null, true);

                _tChiTietChuyenHangDonHangView.RefreshView();

                return;
            }

            _tChiTietChuyenHangDonHangView.ViewModel.RFilter_ChiTietDonHang =
                (p => p.Xong == false && p.MaDonHang == chuyenHangDonHang.MaDonHang);

            _tChiTietChuyenHangDonHangView.ViewModel.MainFilter.SetFilter(
                PhuDinhData.Filter.Filter_tChiTietChuyenHangDonHang.MaChuyenHangDonHang, chuyenHangDonHang.Ma);
            
            _tChiTietChuyenHangDonHangView.ViewModel.tChuyenHangDonHangDefault = chuyenHangDonHang;
            _tChiTietChuyenHangDonHangView.RefreshView();
        }
    }
}
