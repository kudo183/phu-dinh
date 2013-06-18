using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for tChiTietDonHangView.xaml
    /// </summary>
    public partial class tChiTietDonHangView : BaseView
    {
        public Expression<Func<PhuDinhData.tChiTietDonHang, bool>> FilterChiTietDonHang { get; set; }
        public Expression<Func<PhuDinhData.tDonHang, bool>> FilterDonHang { get; set; }
        public Expression<Func<PhuDinhData.tMatHang, bool>> FilterMatHang { get; set; }

        private List<PhuDinhData.tDonHang> _tDonHangs;
        private List<PhuDinhData.tMatHang> _tMatHangs;
        private readonly PhuDinhData.PhuDinhEntities _context = new PhuDinhData.PhuDinhEntities();

        public tChiTietDonHangView()
        {
            InitializeComponent();

            FilterChiTietDonHang = (p => true);
            FilterDonHang = (p => true);
            FilterMatHang = (p => true);
        }

        #region Override base view method
        public override void Save()
        {
            try
            {
                var data = this.dgChiTietDonHang.DataContext as List<PhuDinhData.tChiTietDonHang>;
                PhuDinhData.Repository.tChiTietDonHangRepository.Save(_context, data, FilterChiTietDonHang);
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
            _tDonHangs = PhuDinhData.Repository.tDonHangRepository.GetData(_context, FilterDonHang);
            _tMatHangs = PhuDinhData.Repository.tMatHangRepository.GetData(_context, FilterMatHang);

            var data = PhuDinhData.Repository.tChiTietDonHangRepository.GetData(_context, FilterChiTietDonHang);

            foreach (var tChiTietDonHang in data)
            {
                tChiTietDonHang.tDonHangList = _tDonHangs;
                tChiTietDonHang.tMatHangList = _tMatHangs;
            }

            this.dgChiTietDonHang.DataContext = data;

            this.dgChiTietDonHang.UpdateLayout();
        }
        #endregion
    }
}
