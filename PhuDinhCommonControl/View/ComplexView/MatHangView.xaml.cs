using System.Windows.Controls;
using PhuDinhCommon;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for MatHangView.xaml
    /// </summary>
    public partial class MatHangView : UserControl
    {
        public MatHangView()
        {
            InitializeComponent();

            Loaded += MatHangView_Loaded;
            Unloaded += MatHangView_Unloaded;
        }

        void MatHangView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _rLoaiHangView.dgLoaiHang.SelectionChanged += dgLoaiHang_SelectionChanged;

            _tMatHangView.SetMainFilter(PhuDinhData.Filter.Filter_tMatHang.MaLoaiHang, null, true);
        }

        void MatHangView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _rLoaiHangView.dgLoaiHang.SelectionChanged -= dgLoaiHang_SelectionChanged;
        }

        void dgLoaiHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != e.OriginalSource)
            {
                return;
            }

            var loaihang = ((DataGrid)sender).SelectedItem as PhuDinhDataEntity.rLoaiHang;
            if (loaihang == null || loaihang.Ma == 0)
            {
                _tMatHangView.SetMainFilter(PhuDinhData.Filter.Filter_tMatHang.MaLoaiHang, null, true);
                _tMatHangView.RefreshView();
                return;
            }

            _tMatHangView.SetMainFilter(PhuDinhData.Filter.Filter_tMatHang.MaLoaiHang, loaihang.Ma);
            _tMatHangView.SetDefaultValue(Constant.ColumnName_MaLoaiHang, loaihang.Ma);
            _tMatHangView.RefreshView();
        }
    }
}
