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
            if(baixe == null)
            {
                _rChanhView.FilterChanh = null;
                _rChanhView.RefreshView();
                return;
            }

            _rChanhView.FilterChanh.FilterBaiXe = (p => p.MaBaiXe == baixe.Ma);
            _rChanhView.rBaiXeDefault = baixe;
            _rChanhView.RefreshView();
        }
    }
}
