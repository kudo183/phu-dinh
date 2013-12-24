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
            _rBaiXeView.RefreshView();

            _rBaiXeView.dgBaiXe.SelectionChanged += dgBaiXe_SelectionChanged;
        }

        void dgBaiXe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != e.OriginalSource)
            {
                return;
            }

            var baixe = ((DataGrid)sender).SelectedItem as PhuDinhData.rBaiXe;
            if (baixe == null)
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
