﻿using System.Windows.Controls;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for ChiPhiView.xaml
    /// </summary>
    public partial class ChiPhiView : UserControl
    {
        public ChiPhiView()
        {
            InitializeComponent();

            Loaded += ChiPhiView_Loaded;
            Unloaded += ChiPhiView_Unloaded;
        }

        void ChiPhiView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _rLoaiChiPhiView.dgLoaiChiPhi.SelectionChanged -= dgLoaiChiPhi_SelectionChanged;
        }

        void ChiPhiView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _rLoaiChiPhiView.dgLoaiChiPhi.SelectionChanged += dgLoaiChiPhi_SelectionChanged;

            _rChiPhiNhanVienGiaoHangView.FilterChiPhi.FilterLoaiChiPhi = (p => false);
            _rLoaiChiPhiView.RefreshView();
        }

        void dgLoaiChiPhi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != e.OriginalSource)
            {
                return;
            }

            var loaiChiPhi = ((DataGrid)sender).SelectedItem as PhuDinhData.rLoaiChiPhi;
            if (loaiChiPhi == null)
            {
                _rChiPhiNhanVienGiaoHangView.FilterChiPhi.FilterLoaiChiPhi = (p => false);
                _rChiPhiNhanVienGiaoHangView.RefreshView();
                return;
            }

            _rChiPhiNhanVienGiaoHangView.FilterChiPhi.FilterLoaiChiPhi = (p => p.MaLoaiChiPhi == loaiChiPhi.Ma);
            _rChiPhiNhanVienGiaoHangView.rLoaiChiPhiDefault = loaiChiPhi;
            _rChiPhiNhanVienGiaoHangView.RefreshView();
        }
    }
}
