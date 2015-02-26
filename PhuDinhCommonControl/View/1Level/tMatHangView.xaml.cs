using System;
using System.Windows.Controls.Primitives;
using PhuDinhData.ViewModel;
using PhuDinhCommon;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class tMatHangView : BaseView<PhuDinhData.tMatHang>
    {
        public tMatHangView()
        {
            InitializeComponent();

            dg = dgMatHang;

            _viewModel = new MatHangViewModel();
            DataContext = _viewModel;
            dg.Columns[1].Header = (_viewModel as MatHangViewModel).Header_LoaiHang;
        }

        private void dgMatHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var header = (sender as DataGridColumnHeader).Content as IHeaderFilterModel;

            var view = new rLoaiHangView();

            ChildWindowUtils.ShowChildWindow(Constant.ViewName_LoaiHang, view);

            _viewModel.UpdateReferenceData(header.Name);
        }
    }
}
