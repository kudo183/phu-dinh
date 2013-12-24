using System;
using System.Windows.Controls.Primitives;
using PhuDinhData.ViewModel;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;
using PhuDinhCommon;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rNguyenLieuView.xaml
    /// </summary>
    public partial class rNguyenLieuView : BaseView<PhuDinhData.rNguyenLieu>
    {
        public rNguyenLieuView()
        {
            InitializeComponent();

            dg = dgNguyenLieu;

            _viewModel = new NguyenLieuModel();
            DataContext = _viewModel;
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
