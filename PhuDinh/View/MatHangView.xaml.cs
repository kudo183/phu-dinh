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
            if (e.AddedItems.Count == 0)
            {
                return;
            }

            var loaihang = e.AddedItems[0] as PhuDinhData.rLoaiHang;
            if (loaihang == null)
            {
                return;
            }

            _tMatHangView.FilterMatHang = (p => p.MaLoai == loaihang.Ma);

            _tMatHangView.RefreshView();
        }
    }
}
