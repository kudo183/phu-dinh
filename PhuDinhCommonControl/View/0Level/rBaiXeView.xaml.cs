﻿using PhuDinhData.ViewModel;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rBaiXeView.xaml
    /// </summary>
    public partial class rBaiXeView : BaseView<PhuDinhDataEntity.rBaiXe>
    {
        public rBaiXeView()
        {
            InitializeComponent();

            dg = dgBaiXe;

            _viewModel = new BaiXeViewModel();
            DataContext = _viewModel;
        }
    }
}
