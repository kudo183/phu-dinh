using System;
using System.Windows.Controls.Primitives;
using PhuDinhCommon;
using PhuDinhData.ViewModel;
using System.Windows.Controls;
using CustomControl.DataGridColumnHeaderFilterModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tChuyenHangDonHangView.xaml
    /// </summary>
    public partial class tChuyenHangDonHangView : BaseView<PhuDinhData.tChuyenHangDonHang>
    {
        public tChuyenHangDonHangView()
        {
            InitializeComponent();

            dg = dgChuyenHangDonHang;

            _viewModel = new ChuyenHangDonHangViewModel();
            DataContext = _viewModel;

            dg.Columns[2].Header = (_viewModel as ChuyenHangDonHangViewModel).Header_DonHang;
        }

        private void dgChuyenHangDonHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var header = (sender as DataGridColumnHeader).Content as IHeaderFilterModel;

            UserControl view = null;

            switch (header.Name)
            {
                case Constant.ColumnName_DonHang:
                    view = new tDonHangView();
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_DonHang, view);

                    _viewModel.UpdateReferenceData(header.Name);
                    break;
            }
        }
    }
}
