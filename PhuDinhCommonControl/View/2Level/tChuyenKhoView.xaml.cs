﻿using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using PhuDinhCommon;
using PhuDinhData.ViewModel;
using CustomControl.DataGridColumnHeaderFilterModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tChuyenKhoView.xaml
    /// </summary>
    public partial class tChuyenKhoView : BaseView<PhuDinhDataEntity.tChuyenKho>
    {
        public tChuyenKhoView()
        {
            InitializeComponent();

            dg = dgChuyenKho;

            _viewModel = new ChuyenKhoViewModel();
            DataContext = _viewModel;

            dg.Columns[1].Header = (_viewModel as ChuyenKhoViewModel).Header_Ngay;
            dg.Columns[2].Header = (_viewModel as ChuyenKhoViewModel).Header_NhanVien;
            dg.Columns[3].Header = (_viewModel as ChuyenKhoViewModel).Header_KhoHangXuat;
            dg.Columns[4].Header = (_viewModel as ChuyenKhoViewModel).Header_KhoHangNhap;
        }

        private void dgChuyenKho_HeaderAddButtonClick(object sender, EventArgs e)
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
                case Constant.ColumnName_KhoHangXuat:
                    view = new rKhoHangView();
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_KhoHang, view);

                    _viewModel.UpdateReferenceData(header.Name);
                    break;
                case Constant.ColumnName_KhoHangNhap:
                    view = new rKhoHangView();
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_KhoHang, view);

                    _viewModel.UpdateReferenceData(header.Name);
                    break;
            }
        }
    }
}
