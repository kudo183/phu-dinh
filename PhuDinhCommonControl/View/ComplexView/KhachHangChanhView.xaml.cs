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
                _rKhachHangChanhView.ViewModel.MainFilter.
                SetFilter(PhuDinhData.Filter.Filter_rKhachHangChanh.MaKhachHang, null, true);

                _rKhachHangChanhView.RefreshView();
                return;
            }

            _rKhachHangChanhView.ViewModel.MainFilter.
                SetFilter(PhuDinhData.Filter.Filter_rKhachHangChanh.MaKhachHang, rKhachHang.Ma);

            _rKhachHangChanhView.RefreshView();
        }
    }
}
