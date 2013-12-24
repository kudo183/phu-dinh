using System;
using System.Windows.Controls.Primitives;
using PhuDinhData.ViewModel;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;
using PhuDinhCommon;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rKhachHangView.xaml
    /// </summary>
    public partial class rKhachHangView : BaseView<PhuDinhData.rKhachHang>
    {
        public rKhachHangView()
        {
            InitializeComponent();

            dg = dgKhachHang;
            _viewModel = new KhachHangViewModel();
            DataContext = _viewModel;
        }

        private void dgKhachHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var header = (sender as DataGridColumnHeader).Content as IHeaderFilterModel;
            
            var view = new rDiaDiemView();
            
            ChildWindowUtils.ShowChildWindow(Constant.ViewName_DiaDiem, view);

            _viewModel.UpdateReferenceData(header.Name);
        }
    }
}
