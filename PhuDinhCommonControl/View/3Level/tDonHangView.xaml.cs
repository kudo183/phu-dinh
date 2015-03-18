using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using PhuDinhCommon;
using PhuDinhData.ViewModel;
using CustomControl.DataGridColumnHeaderFilterModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tDonHangView.xaml
    /// </summary>
    public partial class tDonHangView : BaseView<PhuDinhData.tDonHang>
    {
        public tDonHangView()
        {
            LogManager.Log(event_type.et_Internal, severity_type.st_debug, string.Format("{0} {1}", "tDonHangView_Contructor", "Enter"));

            InitializeComponent();
            
            dg = dgDonHang;

            _viewModel = new DonHangViewModel();
            DataContext = _viewModel;
            dg.Columns[1].Header = (_viewModel as DonHangViewModel).Header_Ngay;
            dg.Columns[2].Header = (_viewModel as DonHangViewModel).Header_KhachHang;
            dg.Columns[3].Header = (_viewModel as DonHangViewModel).Header_KhoHang;
            dg.Columns[4].Header = (_viewModel as DonHangViewModel).Header_Chanh;
            
            _viewModel.SetDefaultValue(Constant.ColumnName_MaKhoHang, 1);

            LogManager.Log(event_type.et_Internal, severity_type.st_debug, string.Format("{0} {1}", "tDonHangView_Contructor", "Exit"));
        }

        private void dgDonHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
            LogManager.Log(event_type.et_Internal, severity_type.st_debug, string.Format("{0} {1}", "dgDonHang_HeaderAddButtonClick", "Enter"));

            CommitEdit();
            var header = (sender as DataGridColumnHeader).Content as IHeaderFilterModel;
            LogManager.Log(event_type.et_Internal, severity_type.st_info, string.Format("{0} {1}", "Header Add Button Click", header.Name));

            UserControl view = null;

            switch (header.Name)
            {
                case Constant.ColumnName_KhachHang:
                    view = new rKhachHangView();
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_KhachHang, view);

                    _viewModel.UpdateReferenceData(header.Name);
                    break;
                case Constant.ColumnName_Chanh:
                    view = new rKhachHangChanhView();
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_KhachHangChanh, view);

                    _viewModel.UpdateReferenceData(header.Name);
                    break;
                case Constant.ColumnName_KhoHang:
                    view = new rKhoHangView();
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_KhoHang, view);

                    _viewModel.UpdateReferenceData(header.Name);
                    break;
            }

            LogManager.Log(event_type.et_Internal, severity_type.st_debug, string.Format("{0} {1}", "dgDonHang_HeaderAddButtonClick", "Exit"));
        }
    }
}
