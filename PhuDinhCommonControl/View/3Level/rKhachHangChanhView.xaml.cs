using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using PhuDinhCommon;
using PhuDinhData.ViewModel;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rKhachHangChanhView.xaml
    /// </summary>
    public partial class rKhachHangChanhView : BaseView<PhuDinhData.rKhachHangChanh>
    {
        public rKhachHangChanhView()
        {
            InitializeComponent();

            dg = dgKhachHangChanh;
            _viewModel = new KhachHangChanhViewModel();
            DataContext = _viewModel;
        }

        private void dgKhachHangChanh_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var header = (sender as DataGridColumnHeader).Content as IHeaderFilterModel;

            UserControl view = null;

            switch (header.Name)
            {
                case Constant.ViewName_KhachHang:
                    view = new rKhachHangView();
                    ChildWindowUtils.ShowChildWindow(header.Name, view);

                    _viewModel.UpdateReferenceData(header.Name);
                    break;
                case Constant.ViewName_Chanh:
                    view = new rChanhView();
                    ChildWindowUtils.ShowChildWindow(header.Name, view);

                    _viewModel.UpdateReferenceData(header.Name);
                    break;
            }
        }
    }
}
