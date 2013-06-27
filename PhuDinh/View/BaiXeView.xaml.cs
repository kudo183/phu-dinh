using System.Windows.Controls;

namespace PhuDinh.View
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
            var baixe = ((DataGrid)sender).SelectedItem as PhuDinhData.rBaiXe;
            if(baixe == null)
            {
                _rChanhView.FilterChanh = null;
                _rChanhView.RefreshView();
                return;
            }

            _rChanhView.FilterChanh = (p => p.MaBaiXe == baixe.Ma);
            _rChanhView.RefreshView();
        }
    }
}
