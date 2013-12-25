using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Controls.Primitives;
using PhuDinhCommon;
using PhuDinhData.ViewModel;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tChuyenHangView.xaml
    /// </summary>
    public partial class tChuyenHangView : BaseView<PhuDinhData.tChuyenHang>
    {
        public tChuyenHangView()
        {
            InitializeComponent();

            dg = dgChuyenHang;

            _viewModel = new ChuyenHangViewModel();
            DataContext = _viewModel;
        }

        private void dgChuyenHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var header = (sender as DataGridColumnHeader).Content as IHeaderFilterModel;

            var view = new rNhanVienView();
            
            ChildWindowUtils.ShowChildWindow(Constant.ViewName_NhanVien, view);

            _viewModel.UpdateReferenceData(header.Name);
        }
    }
}
