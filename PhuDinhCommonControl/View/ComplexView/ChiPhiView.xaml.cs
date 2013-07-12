using System.Windows.Controls;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for ChiPhiView.xaml
    /// </summary>
    public partial class ChiPhiView : BaseView
    {
        public ChiPhiView()
        {
            InitializeComponent();

            Loaded += ChiPhiView_Loaded;
            Unloaded += ChiPhiView_Unloaded;
        }

        void ChiPhiView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _rLoaiChiPhiView.dgLoaiChiPhi.SelectionChanged -= dgLoaiChiPhi_SelectionChanged;
        }

        void ChiPhiView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _rLoaiChiPhiView.dgLoaiChiPhi.SelectionChanged += dgLoaiChiPhi_SelectionChanged;

            _rLoaiChiPhiView.RefreshView();
        }

        void dgLoaiChiPhi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != e.OriginalSource)
            {
                return;
            }

            var loaiChiPhi = ((DataGrid)sender).SelectedItem as PhuDinhData.rLoaiChiPhi;
            if (loaiChiPhi == null)
            {
                _rChiPhiNhanVienGiaoHangView.FilterChiPhiNhanVienGiaoHang = null;
                _rChiPhiNhanVienGiaoHangView.RefreshView();
                return;
            }

            _rChiPhiNhanVienGiaoHangView.FilterChiPhiNhanVienGiaoHang = (p => p.MaLoaiChiPhi == loaiChiPhi.Ma);
            _rChiPhiNhanVienGiaoHangView.rLoaiChiPhiDefault = loaiChiPhi;
            _rChiPhiNhanVienGiaoHangView.RefreshView();
        }
    }
}
