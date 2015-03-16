using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using PhuDinhCommon;
using PhuDinhData.ViewModel;
using CustomControl.DataGridColumnHeaderFilterModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tNhapNguyenLieuView.xaml
    /// </summary>
    public partial class tNhapNguyenLieuView : BaseView<PhuDinhData.tNhapNguyenLieu>
    {
        public tNhapNguyenLieuView()
        {
            InitializeComponent();

            dg = dgNhapNguyenLieu;

            _viewModel = new NhapNguyenLieuViewModel();
            DataContext = _viewModel;

            dg.Columns[1].Header = (_viewModel as NhapNguyenLieuViewModel).Header_Ngay;
            dg.Columns[2].Header = (_viewModel as NhapNguyenLieuViewModel).Header_NhaCungCap;
            dg.Columns[3].Header = (_viewModel as NhapNguyenLieuViewModel).Header_NguyenLieu;
        }

        private void dgNhapNguyenLieu_HeaderAddButtonClick(object sender, EventArgs e)
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
                case Constant.ColumnName_NguyenLieu:
                    view = new rNguyenLieuView();
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_NguyenLieu, view);

                    _viewModel.UpdateReferenceData(header.Name);
                    break;
            }
        }
    }
}
