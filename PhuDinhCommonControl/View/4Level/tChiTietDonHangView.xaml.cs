﻿using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using PhuDinhData.ViewModel;
using PhuDinhCommon;
using CustomControl.DataGridColumnHeaderFilterModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tChiTietDonHangView.xaml
    /// </summary>
    public partial class tChiTietDonHangView : BaseView<PhuDinhDataEntity.tChiTietDonHang>
    {
        public tChiTietDonHangView()
        {
            InitializeComponent();

            dg = dgChiTietDonHang;

            _viewModel = new ChiTietDonHangViewModel();
            DataContext = _viewModel;

            dg.Columns[2].Header = (_viewModel as ChiTietDonHangViewModel).Header_MatHang;
        }

        private void dgChiTietDonHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var header = (sender as DataGridColumnHeader).Content as IHeaderFilterModel;

            UserControl view = null;

            switch (header.Name)
            {
                case Constant.ColumnName_MatHang:
                    view = new tMatHangView();
                    ChildWindowUtils.ShowChildWindow(Constant.ViewName_MatHang, view);

                    _viewModel.UpdateReferenceData(header.Name);
                    break;
            }
        }
    }
}
