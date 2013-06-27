using System.Windows.Controls;

namespace PhuDinh.View
{
    /// <summary>
    /// Interaction logic for ChiPhiView.xaml
    /// </summary>
    public partial class ChiPhiView : UserControl
    {
        public ChiPhiView()
        {
            InitializeComponent();
            _rLoaiChiPhiView.RefreshView();

            _rLoaiChiPhiView.dgLoaiChiPhi.SelectionChanged += dgLoaiChiPhi_SelectionChanged;
        }

        void dgLoaiChiPhi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var loaiChiPhi = ((DataGrid)sender).SelectedItem as PhuDinhData.rLoaiChiPhi;
            if (loaiChiPhi == null)
            {
                _rChiPhiNhanVienGiaoHangView.FilterChiPhiNhanVienGiaoHang = null;
                _rChiPhiNhanVienGiaoHangView.RefreshView();
                return;
            }

            _rChiPhiNhanVienGiaoHangView.FilterChiPhiNhanVienGiaoHang = (p => p.MaLoaiChiPhi == loaiChiPhi.Ma);
            _rChiPhiNhanVienGiaoHangView.RefreshView();
        }
    }
}
