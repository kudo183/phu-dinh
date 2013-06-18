using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Windows;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tChiTietChuyenHangDonHangView.xaml
    /// </summary>
    public partial class tChiTietChuyenHangDonHangView : BaseView
    {
        public Expression<Func<PhuDinhData.tChiTietChuyenHangDonHang, bool>> FilterChiTietChuyenHangDonHang { get; set; }
        public Expression<Func<PhuDinhData.tChuyenHangDonHang, bool>> FilterChuyenHangDonHang { get; set; }
        public Expression<Func<PhuDinhData.tMatHang, bool>> FilterMatHang { get; set; }

        private readonly PhuDinhData.PhuDinhEntities _context = new PhuDinhData.PhuDinhEntities();

        public tChiTietChuyenHangDonHangView()
        {
            InitializeComponent();
            FilterChiTietChuyenHangDonHang = (p => true);
            FilterChuyenHangDonHang = (p => true);
            FilterMatHang = (p => true);
        }

        #region Override base view method
        public override void Save()
        {
            try
            {
                var data = this.dgChiTietChuyenHangDonHang.DataContext as List<PhuDinhData.tChiTietChuyenHangDonHang>;
                PhuDinhData.Repository.tChiTietChuyenHangDonHangRepository.Save(_context, data, FilterChiTietChuyenHangDonHang);
                RefreshView();
            }
            catch (Exception ex)
            {
                PhuDinhCommon.EntityFrameworkUtils.UndoContextChange(_context, EntityState.Modified);
            }
        }

        public override void Cancel()
        {
            RefreshView();
        }

        public override void RefreshView()
        {
            PhuDinhData.tChiTietChuyenHangDonHang.tChuyenHangDonHangs =
                PhuDinhData.Repository.tChuyenHangDonHangRepository.GetData(_context, FilterChuyenHangDonHang);
            PhuDinhData.tChiTietChuyenHangDonHang.tMatHangs =
                PhuDinhData.Repository.tMatHangRepository.GetData(_context, FilterMatHang);

            var data = PhuDinhData.Repository.tChiTietChuyenHangDonHangRepository.GetData(_context, FilterChiTietChuyenHangDonHang);

            foreach (var tChiTietChuyenHangDonHang in data)
            {
            }

            this.dgChiTietChuyenHangDonHang.DataContext = data;

            this.dgChiTietChuyenHangDonHang.UpdateLayout();
        }
        #endregion
    }
}
