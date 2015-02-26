using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using PhuDinhData.ViewModel;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;
using PhuDinhCommon;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rMatHangNguyenLieuView.xaml
    /// </summary>
    public partial class rMatHangNguyenLieuView : BaseView<PhuDinhData.rMatHangNguyenLieu>
    {
        public rMatHangNguyenLieuView()
        {
            InitializeComponent();

            dg = dgMatHangNguyenLieu;

            _viewModel = new MatHangNguyenLieuViewModel();
            DataContext = _viewModel;

            dg.Columns[1].Header = (_viewModel as MatHangNguyenLieuViewModel).Header_MatHang;
            dg.Columns[2].Header = (_viewModel as MatHangNguyenLieuViewModel).Header_NguyenLieu;
        }

        private void dgMatHangNguyenLieu_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var header = (sender as DataGridColumnHeader).Content as IHeaderFilterModel;

            UserControl view = null;

            switch (header.Name)
            {
                case Constant.ColumnName_NguyenLieu:
                    view = new rNguyenLieuView();
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_NguyenLieu, view);

                    _viewModel.UpdateReferenceData(header.Name);
                    break;
                case Constant.ColumnName_MatHang:
                    view = new tMatHangView();
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_MatHang, view);

                    _viewModel.UpdateReferenceData(header.Name);
                    break;
            }
        }
    }
}
