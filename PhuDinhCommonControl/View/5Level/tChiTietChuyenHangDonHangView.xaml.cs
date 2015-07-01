using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using PhuDinhCommon;
using PhuDinhData.ViewModel;
using CustomControl.DataGridColumnHeaderFilterModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tChiTietChuyenHangDonHangView.xaml
    /// </summary>
    public partial class tChiTietChuyenHangDonHangView : BaseView<PhuDinhDataEntity.tChiTietChuyenHangDonHang>
    {
        public tChiTietChuyenHangDonHangView()
        {
            InitializeComponent();

            dg = dgChiTietChuyenHangDonHang;

            _viewModel = new ChiTietChuyenHangDonHangViewModel();
            DataContext = _viewModel;

            dg.Columns[2].Header = (_viewModel as ChiTietChuyenHangDonHangViewModel).Header_ChiTietDonHang;

            SetReferenceFilter<PhuDinhDataEntity.tChiTietDonHang>(
                Constant.ColumnName_ChiTietDonHang, (p => p.Xong == false));
        }

        private void dgChiTietChuyenHangDonHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var header = (sender as DataGridColumnHeader).Content as IHeaderFilterModel;

            UserControl view = null;

            switch (header.Name)
            {
                case Constant.ColumnName_ChiTietDonHang:
                    view = new tChiTietDonHangView();
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_ChiTietDonHang, view);

                    _viewModel.UpdateReferenceData(header.Name);
                    break;
            }
        }
    }
}
