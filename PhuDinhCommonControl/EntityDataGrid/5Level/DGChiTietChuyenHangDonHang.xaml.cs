﻿using CustomControl;

namespace PhuDinhCommonControl.EntityDataGrid
{
    /// <summary>
    /// Interaction logic for DGChiTietChuyenHangDonHang.xaml
    /// </summary>
    public partial class DGChiTietChuyenHangDonHang : DataGridExt
    {
        public DGChiTietChuyenHangDonHang()
        {
            InitializeComponent();

            SkippedColumnIndex.Add(1);
        }
    }
}
