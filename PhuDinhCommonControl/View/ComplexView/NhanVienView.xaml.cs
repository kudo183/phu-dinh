﻿using System.Windows.Controls;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for NhanVienView.xaml
    /// </summary>
    public partial class NhanVienView : BaseView
    {
        public NhanVienView()
        {
            InitializeComponent();
            _rPhuongTienView.RefreshView();

            _rPhuongTienView.dgPhuongTien.SelectionChanged += dgPhuongTien_SelectionChanged;
        }

        void dgPhuongTien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != e.OriginalSource)
            {
                return;
            }

            var phuongTien = ((DataGrid)sender).SelectedItem as PhuDinhData.rPhuongTien;
            if (phuongTien == null)
            {
                _rNhanVienView.FilterNhanVien = null;
                _rNhanVienView.RefreshView();
                return;
            }

            _rNhanVienView.FilterNhanVien.FilterPhuongTien = (p => p.MaPhuongTien == phuongTien.Ma);
            _rNhanVienView.rPhuongTienDefault = phuongTien;
            _rNhanVienView.RefreshView();
        }
    }
}
