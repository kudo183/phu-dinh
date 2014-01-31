using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using PhuDinhData.ViewModel;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;
using PhuDinhCommon;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tChiTietNhapHangView.xaml
    /// </summary>
    public partial class tChiTietNhapHangView : BaseView<PhuDinhData.tChiTietNhapHang>
    {
        public tChiTietNhapHangView()
        {
            InitializeComponent();

            dg = dgChiTietNhapHang;

            _viewModel = new ChiTietNhapHangViewModel();
            DataContext = _viewModel;
        }

        private void dgChiTietNhapHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var header = (sender as DataGridColumnHeader).Content as IHeaderFilterModel;

            UserControl view = null;

            switch (header.Name)
            {
                case Constant.ColumnName_NhapHang:
                    view = new tDonHangView();
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_NhapHang, view);

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
