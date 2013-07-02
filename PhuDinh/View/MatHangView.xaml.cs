using System.Windows.Controls;

namespace PhuDinh.View
{
    /// <summary>
    /// Interaction logic for MatHangView.xaml
    /// </summary>
    public partial class MatHangView : UserControl
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
                _tMatHangView.FilterMatHang = null;
                _tMatHangView.RefreshView();
                return;
            }

            _tMatHangView.FilterMatHang = (p => p.MaLoai == loaihang.Ma);
            _tMatHangView.rLoaiHangDefault = loaihang;
            _tMatHangView.RefreshView();
        }
    }
}
