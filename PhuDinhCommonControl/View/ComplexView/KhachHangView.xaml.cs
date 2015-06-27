using System.Windows.Controls;
using PhuDinhCommon;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for KhachHangView.xaml
    /// </summary>
    public partial class KhachHangView : UserControl
    {
        public KhachHangView()
        {
            InitializeComponent();

            Loaded += KhachHangView_Loaded;
            Unloaded += KhachHangView_Unloaded;
        }

        void KhachHangView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _rDiaDiemView.dgDiaDiem.SelectionChanged += dgDiaDiem_SelectionChanged;
            _rKhachHangView.dgKhachHang.SelectionChanged += dgKhachHang_SelectionChanged;

            _rKhachHangView.SetMainFilter(PhuDinhData.Filter.Filter_rKhachHang.MaDiaDiem, null, true);
            _tDonHangView.SetMainFilter(PhuDinhData.Filter.Filter_tDonHang.MaKhachHang, null, true);
        }

        void KhachHangView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _rDiaDiemView.dgDiaDiem.SelectionChanged -= dgDiaDiem_SelectionChanged;
            _rKhachHangView.dgKhachHang.SelectionChanged -= dgKhachHang_SelectionChanged;
        }

        void dgDiaDiem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != e.OriginalSource)
            {
                return;
            }

            _tDonHangView.SetMainFilter(PhuDinhData.Filter.Filter_tDonHang.MaKhachHang, null, true);
            _tDonHangView.RefreshView();

            var diaDiem = ((DataGrid)sender).SelectedItem as PhuDinhDataEntity.rDiaDiem;
            if (diaDiem == null)
            {
                _rKhachHangView.SetMainFilter(PhuDinhData.Filter.Filter_rKhachHang.MaDiaDiem, null, true);
                _rKhachHangView.RefreshView();
                return;
            }

            _rKhachHangView.SetMainFilter(PhuDinhData.Filter.Filter_rKhachHang.MaDiaDiem, diaDiem.Ma);
            _rKhachHangView.SetDefaultValue(Constant.ColumnName_MaDiaDiem, diaDiem.Ma);
            _rKhachHangView.RefreshView();
        }

        void dgKhachHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != e.OriginalSource)
            {
                return;
            }

            var khachHang = ((DataGrid)sender).SelectedItem as PhuDinhDataEntity.rKhachHang;
            if (khachHang == null)
            {
                _tDonHangView.SetMainFilter(PhuDinhData.Filter.Filter_tDonHang.MaKhachHang, null, true);
                _tDonHangView.RefreshView();
                return;
            }

            _tDonHangView.SetMainFilter(PhuDinhData.Filter.Filter_tDonHang.MaKhachHang, khachHang.Ma);
            _tDonHangView.SetDefaultValue(Constant.ColumnName_MaKhachHang, khachHang.Ma);
            _tDonHangView.RefreshView();
        }
    }
}
