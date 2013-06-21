using System.Windows.Controls;

namespace PhuDinh.View
{
    /// <summary>
    /// Interaction logic for KhachHangView.xaml
    /// </summary>
    public partial class KhachHangView : UserControl
    {
        public KhachHangView()
        {
            InitializeComponent();
            _rDiaDiemView.RefreshView();

            _rDiaDiemView.dgDiaDiem.SelectionChanged += dgDiaDiem_SelectionChanged;
            _rKhachHangView.dgKhachHang.SelectionChanged += dgKhachHang_SelectionChanged;
        }

        void dgDiaDiem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                return;
            }

            var diaDiem = e.AddedItems[0] as PhuDinhData.rDiaDiem;
            if (diaDiem == null)
            {
                return;
            }

            _rKhachHangView.FilterKhachHang =
                (p => p.MaDiaDiem == diaDiem.Ma);

            _rKhachHangView.RefreshView();
        }

        void dgKhachHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                return;
            }

            var khachHang = e.AddedItems[0] as PhuDinhData.rKhachHang;
            if (khachHang == null)
            {
                return;
            }

            _tDonHangView.FilterDonHang = (p => p.MaKhachHang == khachHang.Ma);

            _tDonHangView.RefreshView();
        }
    }
}
