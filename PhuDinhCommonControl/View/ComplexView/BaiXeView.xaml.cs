using System.Windows.Controls;
using PhuDinhCommon;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for BaiXeView.xaml
    /// </summary>
    public partial class BaiXeView : UserControl
    {
        public BaiXeView()
        {
            InitializeComponent();

            Loaded += BaiXeView_Loaded;
            Unloaded += BaiXeView_Unloaded;
        }

        void BaiXeView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _rBaiXeView.dgBaiXe.SelectionChanged += dgBaiXe_SelectionChanged;

            _rChanhView.SetMainFilter(PhuDinhData.Filter.Filter_rChanh.MaBaiXe, null, true);
        }

        void BaiXeView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _rBaiXeView.dgBaiXe.SelectionChanged -= dgBaiXe_SelectionChanged;
        }

        void dgBaiXe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != e.OriginalSource)
            {
                return;
            }

            var baixe = ((DataGrid)sender).SelectedItem as PhuDinhDataEntity.rBaiXe;
            if (baixe == null || baixe.Ma == 0)
            {
                _rChanhView.SetMainFilter(PhuDinhData.Filter.Filter_rChanh.MaBaiXe, null, true);
                _rChanhView.RefreshView();
                return;
            }

            _rChanhView.SetMainFilter(PhuDinhData.Filter.Filter_rChanh.MaBaiXe, baixe.Ma);
            _rChanhView.SetDefaultValue(Constant.ColumnName_MaBaiXe, baixe.Ma);
            _rChanhView.RefreshView();
        }
    }
}
