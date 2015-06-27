using System;
using System.Windows.Controls.Primitives;
using PhuDinhData.ViewModel;
using PhuDinhCommon;
using CustomControl.DataGridColumnHeaderFilterModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rNguyenLieuView.xaml
    /// </summary>
    public partial class rNguyenLieuView : BaseView<PhuDinhDataEntity.rNguyenLieu>
    {
        public rNguyenLieuView()
        {
            InitializeComponent();

            dg = dgNguyenLieu;

            _viewModel = new NguyenLieuViewModel();
            DataContext = _viewModel;
            dg.Columns[1].Header = (_viewModel as NguyenLieuViewModel).Header_LoaiNguyenLieu;
        }

        private void dgNguyenLieu_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var header = (sender as DataGridColumnHeader).Content as IHeaderFilterModel;

            var view = new rLoaiNguyenLieuView();
            
            ChildWindowUtils.ShowChildWindow(Constant.ViewName_LoaiNguyenLieu, view);

            _viewModel.UpdateReferenceData(header.Name);
        }
    }
}
