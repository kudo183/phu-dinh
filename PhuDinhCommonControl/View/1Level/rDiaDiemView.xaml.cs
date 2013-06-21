using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rDiaDiemView.xaml
    /// </summary>
    public partial class rDiaDiemView : BaseView
    {
        public Expression<Func<PhuDinhData.rDiaDiem, bool>> FilterDiaDiem { get; set; }
        public Expression<Func<PhuDinhData.rNuoc, bool>> FilterNuoc { get; set; }

        private List<PhuDinhData.rNuoc> _rNuocs;
        private readonly PhuDinhData.PhuDinhEntities _context = new PhuDinhData.PhuDinhEntities();

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
                var data = this.dgDiaDiem.DataContext as ObservableCollection<PhuDinhData.rDiaDiem>;
                PhuDinhData.Repository.rDiaDiemRepository.Save(_context, data.ToList(), FilterDiaDiem);
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
            _rNuocs = PhuDinhData.Repository.rNuocRepository.GetData(_context, FilterNuoc);

            var data = PhuDinhData.Repository.rDiaDiemRepository.GetData(_context, FilterDiaDiem);

            foreach (var rDiaDiem in data)
            {
                rDiaDiem.rNuocList = _rNuocs;
            }

            var collection = new ObservableCollection<PhuDinhData.rDiaDiem>(data);
            collection.CollectionChanged += collection_CollectionChanged;
            this.dgDiaDiem.DataContext = collection;

            this.dgDiaDiem.UpdateLayout();
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var diaDiem = e.NewItems[0] as PhuDinhData.rDiaDiem;
                diaDiem.rNuocList = _rNuocs;
            }
        }

        #endregion
    }
}
