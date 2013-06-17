using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rDiaDiemView.xaml
    /// </summary>
    public partial class rDiaDiemView : BaseView
    {
        public Expression<Func<PhuDinhData.rDiaDiem, bool>> FilterDiaDiem { get; set; }
        public Expression<Func<PhuDinhData.rNuoc, bool>> FilterNuoc { get; set; }

        public rDiaDiemView()
        {
            InitializeComponent();

            FilterDiaDiem = (p => true);
            FilterNuoc = (p => true);
        }

        #region Override base view method
        public override void Save()
        {
            try
            {
                var data = this.dgDiaDiem.DataContext as List<PhuDinhData.rDiaDiem>;
                PhuDinhData.Repository.rDiaDiemRepository.Save(data, FilterDiaDiem);
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
            var context = new PhuDinhData.PhuDinhEntities();

            var rNuocs = PhuDinhData.Repository.rNuocRepository.GetData(context, FilterNuoc);

            var data = PhuDinhData.Repository.rDiaDiemRepository.GetData(context, FilterDiaDiem);

            foreach (var rDiaDiem in data)
            {
                rDiaDiem.Nuoc = rNuocs.FirstOrDefault(
                    p => p.Ma == rDiaDiem.MaNuoc);

                rDiaDiem.rNuocList = rNuocs;
            }

            this.dgDiaDiem.DataContext = data;

            this.dgDiaDiem.UpdateLayout();
        }
        #endregion        
    }
}
