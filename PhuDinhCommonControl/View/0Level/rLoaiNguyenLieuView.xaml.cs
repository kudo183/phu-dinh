﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        private List<PhuDinhData.rLoaiNguyenLieu> _origData;

        public rLoaiNguyenLieuView()
        {
            InitializeComponent();

            FilterLoaiNguyenLieu = (p => true);
        }

        #region Override base view method
        public override void CommitEdit()
        {
            dgLoaiNguyenLieu.CommitEdit();
            base.CommitEdit();
        }

        public override void Save()
        {
            CommitEdit();
            try
            {
                var data = dgLoaiNguyenLieu.DataContext as List<PhuDinhData.rLoaiNguyenLieu>;
                PhuDinhData.Repository.rLoaiNguyenLieuRepository.Save(_context, data, _origData);
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
            _origData = PhuDinhData.Repository.rLoaiNguyenLieuRepository.GetData(_context, FilterLoaiNguyenLieu);
            dgLoaiNguyenLieu.DataContext = _origData;

            dgLoaiNguyenLieu.SelectedIndex = index;
        }
        #endregion
    }
}
