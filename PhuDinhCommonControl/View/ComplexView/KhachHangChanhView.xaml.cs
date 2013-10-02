using System.Windows.Controls;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for KhachHangChanhView.xaml
    /// </summary>
    public partial class KhachHangChanhView : BaseView
    {
        public KhachHangChanhView()
        {
            InitializeComponent();
            _rKhachHangView.RefreshView();

            _rKhachHangView.dgKhachHang.SelectionChanged += dgKhachHang_SelectionChanged;
        }

        void dgKhachHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != e.OriginalSource)
            {
                return;
            }

            var rKhachHang = ((DataGrid)sender).SelectedItem as PhuDinhData.rKhachHang;
            if (rKhachHang == null)
            {
                _rKhachHangChanhView.FilterKhachHangChanh = null;
                _rKhachHangChanhView.RefreshView();
                return;
            }

            _rKhachHangChanhView.FilterKhachHangChanh = (p => p.MaKhachHang == rKhachHang.Ma);
            _rKhachHangChanhView.RefreshView();
        }
    }
}
