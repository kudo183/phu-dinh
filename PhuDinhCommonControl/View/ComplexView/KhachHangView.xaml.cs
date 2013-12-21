using System.Windows.Controls;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for KhachHangView.xaml
    /// </summary>
    public partial class KhachHangView : BaseView
    {
        public KhachHangView()
        {
            InitializeComponent();

            _rDiaDiemView.dgDiaDiem.SelectionChanged += dgDiaDiem_SelectionChanged;
            _rKhachHangView.dgKhachHang.SelectionChanged += dgKhachHang_SelectionChanged;

            _tDonHangView.ViewModel.MainFilter.SetFilter(PhuDinhData.Filter.Filter_tDonHang.MaKhachHang, null, true);
        }

        void dgDiaDiem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != e.OriginalSource)
            {
                return;
            }

            _tDonHangView.ViewModel.MainFilter.SetFilter(PhuDinhData.Filter.Filter_tDonHang.MaKhachHang, null, true);
            _tDonHangView.RefreshView();

            var diaDiem = ((DataGrid)sender).SelectedItem as PhuDinhData.rDiaDiem;
            if (diaDiem == null)
            {
                _rKhachHangView.FilterKhachHang = null;
                _rKhachHangView.RefreshView();
                return;
            }

            _rKhachHangView.FilterKhachHang = (p => p.MaDiaDiem == diaDiem.Ma);
            _rKhachHangView.rDiaDiemDefault = diaDiem;
            _rKhachHangView.RefreshView();
        }

        void dgKhachHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != e.OriginalSource)
            {
                return;
            }

            var khachHang = ((DataGrid)sender).SelectedItem as PhuDinhData.rKhachHang;
            if (khachHang == null)
            {
                _tDonHangView.ViewModel.MainFilter.SetFilter(PhuDinhData.Filter.Filter_tDonHang.MaKhachHang, null, true);
                _tDonHangView.RefreshView();
                return;
            }

            _tDonHangView.ViewModel.MainFilter.SetFilter(PhuDinhData.Filter.Filter_tDonHang.MaKhachHang, khachHang.Ma);
            _tDonHangView.ViewModel.rKhachHangDefault = khachHang;
            _tDonHangView.RefreshView();
        }
    }
}
