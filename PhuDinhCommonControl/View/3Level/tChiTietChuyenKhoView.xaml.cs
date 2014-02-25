﻿using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using PhuDinhData.ViewModel;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;
using PhuDinhCommon;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tChiTietChuyenKhoView.xaml
    /// </summary>
    public partial class tChiTietChuyenKhoView : BaseView<PhuDinhData.tChiTietChuyenKho>
    {
        public tChiTietChuyenKhoView()
        {
            InitializeComponent();

            dg = dgChiTietChuyenKho;

            _viewModel = new ChiTietChuyenKhoViewModel();
            DataContext = _viewModel;
        }

        private void dgChiTietChuyenKho_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var header = (sender as DataGridColumnHeader).Content as IHeaderFilterModel;

            UserControl view = null;

            switch (header.Name)
            {
                case Constant.ColumnName_ChuyenKho:
                    view = new tDonHangView();
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_ChuyenKho, view);

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