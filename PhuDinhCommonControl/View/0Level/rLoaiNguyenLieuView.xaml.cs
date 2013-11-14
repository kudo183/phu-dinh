using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rLoaiNguyenLieuView.xaml
    /// </summary>
    public partial class rLoaiNguyenLieuView : BaseView
    {
        public Expression<Func<PhuDinhData.rLoaiNguyenLieu, bool>> FilterLoaiNguyenLieu { get; set; }

        private PhuDinhData.PhuDinhEntities _context = ContextFactory.CreateContext();

        public rLoaiNguyenLieuView()
        {
            InitializeComponent();

            FilterLoaiNguyenLieu = (p => true);
        }
        
        #region Override base view method
        public override void Save()
        {
            try
            {
                var data = dgLoaiNguyenLieu.DataContext as List<PhuDinhData.rLoaiNguyenLieu>;
                PhuDinhData.Repository.rLoaiNguyenLieuRepository.Save(_context, data, FilterLoaiNguyenLieu);
            }
            catch (Exception ex)
            {
                PhuDinhCommon.EntityFrameworkUtils.UndoContextChange(_context, EntityState.Modified);
            }

            base.Save();
        }

        public override void Cancel()
        {
            RefreshView();

            base.Cancel();
        }

        public override void RefreshView()
        {
            var index = dgLoaiNguyenLieu.SelectedIndex;

            _context = ContextFactory.CreateContext();
            dgLoaiNguyenLieu.DataContext = PhuDinhData.Repository.rLoaiNguyenLieuRepository.GetData(_context, FilterLoaiNguyenLieu);

            dgLoaiNguyenLieu.SelectedIndex = index;
        }
        #endregion
    }
}
