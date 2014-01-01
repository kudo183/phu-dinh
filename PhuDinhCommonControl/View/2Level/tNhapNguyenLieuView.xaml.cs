﻿using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using PhuDinhCommon;
using PhuDinhData.ViewModel;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tNhapNguyenLieuView.xaml
    /// </summary>
    public partial class tNhapNguyenLieuView : BaseView<PhuDinhData.tNhapNguyenLieu>
    {
        public tNhapNguyenLieuView()
        {
            InitializeComponent();

            dg = dgNhapNguyenLieu;

            _viewModel = new NhapNguyenLieuViewModel();
            DataContext = _viewModel;
        }

        private void dgNhapNguyenLieu_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var header = (sender as DataGridColumnHeader).Content as IHeaderFilterModel;

            UserControl view = null;

            switch (header.Name)
            {
                case Constant.ColumnName_NhanCungCap:
                    view = new rNhaCungCapView();
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_NhaCungCap, view);

                    _viewModel.UpdateReferenceData(header.Name);
                    break;
                case Constant.ColumnName_NguyenLieu:
                    view = new rNguyenLieuView();
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_NguyenLieu, view);

                    _viewModel.UpdateReferenceData(header.Name);
                    break;
            }
        }
    }
}
