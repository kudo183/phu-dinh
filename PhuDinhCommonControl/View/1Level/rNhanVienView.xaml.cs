using System;
using System.Windows.Controls.Primitives;
using PhuDinhData.ViewModel;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;
using PhuDinhCommon;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class rNhanVienView : BaseView<PhuDinhData.rNhanVien>
    {
        public rNhanVienView()
        {
            InitializeComponent();

            dg = dgNhanVien;
            _viewModel = new NhanVienViewModel();
            DataContext = _viewModel;
            dg.Columns[1].Header = (_viewModel as NhanVienViewModel).Header_PhuongTien;
        }

        private void dgNhanVien_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var header = (sender as DataGridColumnHeader).Content as IHeaderFilterModel;

            var view = new rPhuongTienView();

            ChildWindowUtils.ShowChildWindow(Constant.ViewName_PhuongTien, view);

            _viewModel.UpdateReferenceData(header.Name);
        }
    }
}
