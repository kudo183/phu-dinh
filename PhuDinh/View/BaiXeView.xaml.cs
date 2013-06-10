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
            if (e.AddedItems.Count == 0)
            {
                return;
            }

            var baixe = e.AddedItems[0] as PhuDinhData.rBaiXe;
            if(baixe == null)
            {
                return;
            }

            _rChanhView.FilterChanh = (p => p.MaBaiXe == baixe.Ma);

            _rChanhView.RefreshView();
        }
    }
}
