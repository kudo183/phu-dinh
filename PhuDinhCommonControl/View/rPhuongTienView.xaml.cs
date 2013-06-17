using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rPhuongTienView.xaml
    /// </summary>
    public partial class rPhuongTienView : BaseView
    {
        public Expression<Func<PhuDinhData.rPhuongTien, bool>> FilterPhuongTien { get; set; }

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
                PhuDinhData.Repository.rPhuongTienRepository.Save(data, FilterPhuongTien);
                RefreshView();
            }
            catch (Exception ex)
            {
            }
        }

        public override void Cancel()
        {
            RefreshView();
        }

        public override void RefreshView()
        {
            this.dgPhuongTien.DataContext = PhuDinhData.Repository.rPhuongTienRepository.GetData(FilterPhuongTien);
        }
        #endregion
    }
}
