using System.Windows.Controls;

namespace PhuDinh.View
{
    /// <summary>
    /// Interaction logic for DonHangView.xaml
    /// </summary>
    public partial class DonHangView : UserControl
    {
        public DonHangView()
        {
            InitializeComponent();
            _tDonHangView.RefreshView();
            
            _tDonHangView.dgDonHang.SelectionChanged += dgDonHang_SelectionChanged;
        }

        void dgDonHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                return;
            }

            var DonHang = e.AddedItems[0] as PhuDinhData.tDonHang;
            if(DonHang == null)
            {
                return;
            }

            _tChiTietDonHangView.FilterChiTietDonHang = (p => p.MaDonHang == DonHang.Ma);

            _tChiTietDonHangView.RefreshView();
        }
    }
}
