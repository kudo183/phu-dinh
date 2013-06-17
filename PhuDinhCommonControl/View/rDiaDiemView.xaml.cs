using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
                PhuDinhData.Repository.rDiaDiemRepository.Save(data.ToList(), FilterDiaDiem);
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

            _rNuocs = PhuDinhData.Repository.rNuocRepository.GetData(context, FilterNuoc);

            var data = PhuDinhData.Repository.rDiaDiemRepository.GetData(context, FilterDiaDiem);

            foreach (var rDiaDiem in data)
            {
                rDiaDiem.Nuoc = _rNuocs.FirstOrDefault(
                    p => p.Ma == rDiaDiem.MaNuoc);

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
