using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rLoaiHangView.xaml
    /// </summary>
    public partial class rLoaiHangView : BaseView
    {
        public Expression<Func<PhuDinhData.rLoaiHang, bool>> FilterLoaiHang { get; set; }

        private readonly PhuDinhData.PhuDinhEntities _context = new PhuDinhData.PhuDinhEntities();

        public rLoaiHangView()
        {
            InitializeComponent();

            FilterLoaiHang = (p => true);
        }
        
        #region Override base view method
        public override void Save()
        {
            try
            {
                var data = this.dgLoaiHang.DataContext as List<PhuDinhData.rLoaiHang>;
                PhuDinhData.Repository.rLoaiHangRepository.Save(_context, data, FilterLoaiHang);
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
            this.dgLoaiHang.DataContext = PhuDinhData.Repository.rLoaiHangRepository.GetData(_context, FilterLoaiHang);
        }
        #endregion
    }
}
