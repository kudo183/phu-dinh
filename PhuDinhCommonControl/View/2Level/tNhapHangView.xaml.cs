using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using PhuDinhCommon;
using PhuDinhData.ViewModel;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tNhapHangView.xaml
    /// </summary>
    public partial class tNhapHangView : BaseView<PhuDinhData.tNhapHang>
    {
        public tNhapHangView()
        {
            InitializeComponent();

            dg = dgNhapHang;

            _viewModel = new NhapHangViewModel();
            DataContext = _viewModel;

            dg.Columns[1].Header = (_viewModel as NhapHangViewModel).Header_Ngay;
        }

        private void dgNhapHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var header = (sender as DataGridColumnHeader).Content as IHeaderFilterModel;

            UserControl view = null;

            switch (header.Name)
            {
                case Constant.ColumnName_NhanCungCap:
                    view = new rNhaCungCapView();
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_NhaCungCap, view);

                    _viewModel.UpdateReferenceData(header.Name);
                    break;
                case Constant.ColumnName_NhanVien:
                    view = new rNhanVienView();
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_NhanVien, view);

                    _viewModel.UpdateReferenceData(header.Name);
                    break;
                case Constant.ColumnName_KhoHang:
                    view = new rKhoHangView();
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_KhoHang, view);

                    _viewModel.UpdateReferenceData(header.Name);
                    break;
            }
        }
    }
}
