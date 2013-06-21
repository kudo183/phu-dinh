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
            if (e.AddedItems.Count == 0)
            {
                return;
            }

            var phuongTien = e.AddedItems[0] as PhuDinhData.rPhuongTien;
            if (phuongTien == null)
            {
                return;
            }

            _rNhanVienGiaoHangView.FilterNhanVienGiaoHang = (p => p.MaPhuongTien == phuongTien.Ma);

            _rNhanVienGiaoHangView.RefreshView();
        }
    }
}
