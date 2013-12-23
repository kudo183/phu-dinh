using System;
using PhuDinhData.ViewModel;
using PhuDinhCommon;

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
        }

        private void dgMatHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var view = new rLoaiHangView();
            view.RefreshView();
            ChildWindowUtils.ShowChildWindow(Constant.ViewName_LoaiHang, view);

            _viewModel.UpdateReferenceData(Constant.ColumnName_LoaiHang);
        }
    }
}
