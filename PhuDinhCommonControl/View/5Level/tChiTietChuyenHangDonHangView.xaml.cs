using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using PhuDinhCommon;
using PhuDinhData.ViewModel;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tChiTietChuyenHangDonHangView.xaml
    /// </summary>
    public partial class tChiTietChuyenHangDonHangView : BaseView<PhuDinhData.tChiTietChuyenHangDonHang>
    {
        public tChiTietChuyenHangDonHangView()
        {
            InitializeComponent();

            dg = dgChiTietChuyenHangDonHang;

            _viewModel=new ChiTietChuyenHangDonHangViewModel();
            DataContext = _viewModel;
        }

        private void dgChiTietChuyenHangDonHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var header = (sender as DataGridColumnHeader).Content as IHeaderFilterModel;

            UserControl view = null;

            switch (header.Name)
            {
                case Constant.ColumnName_ChuyenHangDonHang:
                    view = new tChuyenHangDonHangView();                    
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_ChuyenHangDonHang, view);

                    _viewModel.UpdateReferenceData(header.Name);
                    break;
                case Constant.ColumnName_ChiTietDonHang:
                    view = new tMatHangView();                    
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_ChiTietDonHang, view);

                    _viewModel.UpdateReferenceData(header.Name);
                    break;
            }
        }
    }
}
