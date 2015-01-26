using System;
using System.Windows.Controls.Primitives;
using PhuDinhCommon;
using PhuDinhData.ViewModel;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tChuyenHangView.xaml
    /// </summary>
    public partial class tChuyenHangView : BaseView<PhuDinhData.tChuyenHang>
    {
        public tChuyenHangView()
        {
            InitializeComponent();

            dg = dgChuyenHang;

            _viewModel = new ChuyenHangViewModel();
            DataContext = _viewModel;

            dg.Columns[1].Header = (_viewModel as ChuyenHangViewModel).Header_Ngay;
        }

        private void dgChuyenHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var header = (sender as DataGridColumnHeader).Content as IHeaderFilterModel;

            var view = new rNhanVienView();
            
            ChildWindowUtils.ShowChildWindow(Constant.ViewName_NhanVien, view);

            _viewModel.UpdateReferenceData(header.Name);
        }
    }
}
