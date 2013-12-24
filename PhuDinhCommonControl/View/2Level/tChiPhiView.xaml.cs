using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using PhuDinhData.ViewModel;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;
using PhuDinhCommon;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tChiPhiView.xaml
    /// </summary>
    public partial class tChiPhiView : BaseView<PhuDinhData.tChiPhi>
    {
        public tChiPhiView()
        {
            InitializeComponent();

            dg = dgChiPhi;

            _viewModel = new ChiPhiViewModel();
            DataContext = _viewModel;
        }

        private void dgChiPhi_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var header = (sender as DataGridColumnHeader).Content as IHeaderFilterModel;

            UserControl view = null;

            switch (header.Name)
            {
                case Constant.ColumnName_NhanVien:
                    view = new rNhanVienView();
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_NhanVien, view);

                    _viewModel.UpdateReferenceData(header.Name);
                    break;
                case Constant.ColumnName_LoaiChiPhi:
                    view = new rLoaiChiPhiView();
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_LoaiChiPhi, view);

                    _viewModel.UpdateReferenceData(header.Name);
                    break;
            }
        }
    }
}
