using System;
using System.Windows.Controls.Primitives;
using PhuDinhData.ViewModel;
using PhuDinhCommon;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rChanhView.xaml
    /// </summary>
    public partial class rChanhView : BaseView<PhuDinhData.rChanh>
    {
        public rChanhView()
        {
            InitializeComponent();

            dg = dgChanh;

            _viewModel = new ChanhViewModel();
            DataContext = _viewModel;
            dg.Columns[1].Header = (_viewModel as ChanhViewModel).Header_BaiXe;
        }

        private void dgChanh_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var header = (sender as DataGridColumnHeader).Content as IHeaderFilterModel;
            
            var view = new rBaiXeView();
            
            ChildWindowUtils.ShowChildWindow(Constant.ViewName_BaiXe, view);

            _viewModel.UpdateReferenceData(header.Name);
        }
    }
}
