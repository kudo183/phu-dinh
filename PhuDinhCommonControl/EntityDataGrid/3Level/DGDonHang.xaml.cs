﻿using CustomControl;

namespace PhuDinhCommonControl.EntityDataGrid
{
    /// <summary>
    /// Interaction logic for DGDonHang.xaml
    /// </summary>
    public partial class DGDonHang : DataGridExt
    {
        public DGDonHang()
        {
            InitializeComponent();

            SkippedColumnIndex.Add(1);
        }
    }
}
