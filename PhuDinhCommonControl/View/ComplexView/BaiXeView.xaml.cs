using System.Windows.Controls;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for BaiXeView.xaml
    /// </summary>
    public partial class BaiXeView : BaseView
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
                _rChanhView.ViewModel.MainFilter.SetFilter(PhuDinhData.Filter.Filter_rChanh.MaBaiXe, null, true);
                _rChanhView.RefreshView();
                return;
            }

            _rChanhView.ViewModel.MainFilter.SetFilter(PhuDinhData.Filter.Filter_rChanh.MaBaiXe, baixe.Ma);
            _rChanhView.ViewModel.rBaiXeDefault = baixe;
            _rChanhView.RefreshView();
        }
    }
}
