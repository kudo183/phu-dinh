using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rPhuongTienView.xaml
    /// </summary>
    public partial class rPhuongTienView : BaseView
    {
        public Expression<Func<PhuDinhData.rPhuongTien, bool>> FilterPhuongTien { get; set; }
        
        private readonly PhuDinhData.PhuDinhEntities _context = new PhuDinhData.PhuDinhEntities();

        public rPhuongTienView()
        {
            InitializeComponent();

            FilterPhuongTien = (p => true);
        }

        #region Override base view method
        public override void Save()
        {
            try
            {
                var data = this.dgPhuongTien.DataContext as List<PhuDinhData.rPhuongTien>;
                PhuDinhData.Repository.rPhuongTienRepository.Save(_context, data, FilterPhuongTien);
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
            this.dgPhuongTien.DataContext = PhuDinhData.Repository.rPhuongTienRepository.GetData(_context, FilterPhuongTien);
        }
        #endregion
    }
}
