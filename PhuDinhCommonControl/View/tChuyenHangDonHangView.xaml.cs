using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tChuyenHangDonHangView.xaml
    /// </summary>
    public partial class tChuyenHangDonHangView : BaseView
    {
        public Expression<Func<PhuDinhData.tChuyenHangDonHang, bool>> FilterChuyenHangDonHang { get; set; }
        public Expression<Func<PhuDinhData.tChuyenHang, bool>> FilterChuyenHang { get; set; }
        public Expression<Func<PhuDinhData.tDonHang, bool>> FilterDonHang { get; set; }

        private readonly PhuDinhData.PhuDinhEntities _context = new PhuDinhData.PhuDinhEntities();

        public tChuyenHangDonHangView()
        {
            InitializeComponent();

            FilterChuyenHangDonHang = (p => true);
            FilterChuyenHang = (p => true);
            FilterDonHang = (p => true);
        }

        #region Override base view method
        public override void Save()
        {
            try
            {
                var data = this.dgChuyenHangDonHang.DataContext as List<PhuDinhData.tChuyenHangDonHang>;
                PhuDinhData.Repository.tChuyenHangDonHangRepository.Save(_context, data, FilterChuyenHangDonHang);
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
            PhuDinhData.tChuyenHangDonHang.tChuyenHangs = PhuDinhData.Repository.tChuyenHangRepository.GetData(_context, FilterChuyenHang);
            PhuDinhData.tChuyenHangDonHang.tDonHangs = PhuDinhData.Repository.tDonHangRepository.GetData(_context, FilterDonHang);

            var data = PhuDinhData.Repository.tChuyenHangDonHangRepository.GetData(_context, FilterChuyenHangDonHang);

            foreach (var tChuyenHangDonHang in data)
            {
            }

            this.dgChuyenHangDonHang.DataContext = data;

            this.dgChuyenHangDonHang.UpdateLayout();
        }
        #endregion
    }
}
