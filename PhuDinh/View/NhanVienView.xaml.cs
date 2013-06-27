using System.Windows.Controls;

namespace PhuDinh.View
{
    /// <summary>
    /// Interaction logic for NhanVienView.xaml
    /// </summary>
    public partial class NhanVienView : UserControl
    {
        public NhanVienView()
        {
            InitializeComponent();
            _rPhuongTienView.RefreshView();

            _rPhuongTienView.dgPhuongTien.SelectionChanged += dgPhuongTien_SelectionChanged;
        }

        void dgPhuongTien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var phuongTien = ((DataGrid)sender).SelectedItem as PhuDinhData.rPhuongTien;
            if (phuongTien == null)
            {
                _rNhanVienGiaoHangView.FilterNhanVienGiaoHang = null;
                _rNhanVienGiaoHangView.RefreshView();
                return;
            }

            _rNhanVienGiaoHangView.FilterNhanVienGiaoHang = (p => p.MaPhuongTien == phuongTien.Ma);
            _rNhanVienGiaoHangView.RefreshView();
        }
    }
}
