using System.Windows.Controls;
using PhuDinhCommon;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for ChiPhiView.xaml
    /// </summary>
    public partial class ChiPhiView : UserControl
    {
        public ChiPhiView()
        {
            InitializeComponent();

            Loaded += ChiPhiView_Loaded;
            Unloaded += ChiPhiView_Unloaded;
        }

        void ChiPhiView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _rLoaiChiPhiView.dgLoaiChiPhi.SelectionChanged += dgLoaiChiPhi_SelectionChanged;

            _rChiPhiNhanVienGiaoHangView.SetMainFilter(
                PhuDinhData.Filter.Filter_tChiPhi.MaLoaiChiPhi, null, true);
        }

        void ChiPhiView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _rLoaiChiPhiView.dgLoaiChiPhi.SelectionChanged -= dgLoaiChiPhi_SelectionChanged;
        }

        void dgLoaiChiPhi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != e.OriginalSource)
            {
                return;
            }

            var loaiChiPhi = ((DataGrid)sender).SelectedItem as PhuDinhDataEntity.rLoaiChiPhi;
            if (loaiChiPhi == null || loaiChiPhi.Ma == 0)
            {
                _rChiPhiNhanVienGiaoHangView.SetMainFilter(
                PhuDinhData.Filter.Filter_tChiPhi.MaLoaiChiPhi, null, true);

                _rChiPhiNhanVienGiaoHangView.RefreshView();
                return;
            }

            _rChiPhiNhanVienGiaoHangView.SetMainFilter(
                PhuDinhData.Filter.Filter_tChiPhi.MaLoaiChiPhi, loaiChiPhi.Ma);

            _rChiPhiNhanVienGiaoHangView.SetDefaultValue(Constant.ColumnName_MaLoaiChiPhi, loaiChiPhi.Ma);
            _rChiPhiNhanVienGiaoHangView.RefreshView();
        }
    }
}
