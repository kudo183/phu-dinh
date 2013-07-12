using System.Windows;
using System.Windows.Controls;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for DonHangView.xaml
    /// </summary>
    public partial class DonHangView : BaseView
    {
        public DonHangView()
        {
            InitializeComponent();

            Loaded += DonHangView_Loaded;
            Unloaded += DonHangView_Unloaded;
        }

        void DonHangView_Loaded(object sender, RoutedEventArgs e)
        {
            _tDonHangView.dgDonHang.SelectionChanged += dgDonHang_SelectionChanged;
            _tChiTietDonHangView.AfterSave += _tChiTietDonHangView_AfterSave;

            _tDonHangView.RefreshView();
        }

        void DonHangView_Unloaded(object sender, RoutedEventArgs e)
        {
            _tDonHangView.dgDonHang.SelectionChanged -= dgDonHang_SelectionChanged;
            _tChiTietDonHangView.AfterSave -= _tChiTietDonHangView_AfterSave;
        }

        void _tChiTietDonHangView_AfterSave(object sender, System.EventArgs e)
        {
            _tDonHangView.RefreshView();
        }

        void dgDonHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != e.OriginalSource)
            {
                return;
            }

            var donHang = ((DataGrid)sender).SelectedItem as PhuDinhData.tDonHang;
            if (donHang == null)
            {
                _tChiTietDonHangView.FilterChiTietDonHang = null;
                _tChiTietDonHangView.RefreshView();
                return;
            }

            _tChiTietDonHangView.FilterChiTietDonHang = (p => p.MaDonHang == donHang.Ma);
            _tChiTietDonHangView.tDonHangDefault = donHang;
            _tChiTietDonHangView.RefreshView();
        }
    }
}
