using System.Windows.Controls;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for MatHangView.xaml
    /// </summary>
    public partial class MatHangView : BaseView
    {
        public MatHangView()
        {
            InitializeComponent();
            _rLoaiHangView.RefreshView();

            _rLoaiHangView.dgLoaiHang.SelectionChanged += dgLoaiHang_SelectionChanged;
        }

        void dgLoaiHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != e.OriginalSource)
            {
                return;
            }

            var loaihang = ((DataGrid)sender).SelectedItem as PhuDinhData.rLoaiHang;
            if (loaihang == null)
            {
                _tMatHangView.ViewModel.MainFilter.SetFilter(PhuDinhData.Filter.Filter_tMatHang.MaLoaiHang, null, true);
                _tMatHangView.RefreshView();
                return;
            }

            _tMatHangView.ViewModel.MainFilter.SetFilter(PhuDinhData.Filter.Filter_tMatHang.MaLoaiHang, loaihang.Ma);
            _tMatHangView.ViewModel.rLoaiHangDefault = loaihang;
            _tMatHangView.RefreshView();
        }
    }
}
