﻿using PhuDinhData.ViewModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rKhoHangView.xaml
    /// </summary>
    public partial class rKhoHangView : BaseView<PhuDinhDataEntity.rKhoHang>
    {
        public rKhoHangView()
        {
            InitializeComponent();

            dg = dgKhoHang;

            _viewModel = new KhoHangViewModel();
            DataContext = _viewModel;
        }
    }
}
