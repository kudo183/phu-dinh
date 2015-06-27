using System.Windows.Controls;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for KhachHangChanhView.xaml
    /// </summary>
    public partial class KhachHangChanhView : UserControl
    {
        public KhachHangChanhView()
        {
            InitializeComponent();

            Loaded += KhachHangChanhView_Loaded;
            Unloaded += KhachHangChanhView_Unloaded;
        }

        void KhachHangChanhView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _rKhachHangView.dgKhachHang.SelectionChanged += dgKhachHang_SelectionChanged;

            _rKhachHangChanhView.SetMainFilter(
                    PhuDinhData.Filter.Filter_rKhachHangChanh.MaKhachHang, null, true);
        }

        void KhachHangChanhView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _rKhachHangView.dgKhachHang.SelectionChanged -= dgKhachHang_SelectionChanged;
        }

        void dgKhachHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != e.OriginalSource)
            {
                return;
            }

            var rKhachHang = ((DataGrid)sender).SelectedItem as PhuDinhDataEntity.rKhachHang;
            if (rKhachHang == null)
            {
                _rKhachHangChanhView.SetMainFilter(
                    PhuDinhData.Filter.Filter_rKhachHangChanh.MaKhachHang, null, true);

                _rKhachHangChanhView.RefreshView();
                return;
            }

            _rKhachHangChanhView.SetMainFilter(
                PhuDinhData.Filter.Filter_rKhachHangChanh.MaKhachHang, rKhachHang.Ma);

            _rKhachHangChanhView.RefreshView();
        }
    }
}
