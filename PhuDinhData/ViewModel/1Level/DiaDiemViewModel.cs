using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using PhuDinhData.Filter;
using PhuDinhData.Repository;
using PhuDinhData.ViewModel.DataGridColumnHeaderFilterModel;
using System;

namespace PhuDinhData.ViewModel
{
    public class DiaDiemViewModel : BaseViewModel<rDiaDiem>
    {
        private List<rNuoc> _rNuocs;
        private string _filterNuoc = string.Empty;

        public PhuDinhEntities Context { get { return _context; } }

        public List<rNuoc> rNuoc { get { return _rNuocs; } }

        public Expression<Func<rNuoc, bool>> FilterNuoc { get; set; }

        public rNuoc rNuocDefault { get; set; }

        public Filter_rDiaDiem MainFilter { get; set; }

        public static HeaderTextFilter Header_Nuoc = new HeaderTextFilter("Nước");

        public DiaDiemViewModel()
        {
            Entity = new ObservableCollection<rDiaDiem>();

            FilterNuoc = (p => true);
            MainFilter = new Filter_rDiaDiem();
        }

        public void Load()
        {
            Entity.CollectionChanged += Entity_CollectionChanged;

            DiaDiemViewModel.Header_Nuoc.Text = _filterNuoc;
            DiaDiemViewModel.Header_Nuoc.PropertyChanged += Nuoc_PropertyChanged;
        }

        public void Unload()
        {
            Entity.CollectionChanged -= Entity_CollectionChanged;

            _filterNuoc = DiaDiemViewModel.Header_Nuoc.Text;
            DiaDiemViewModel.Header_Nuoc.PropertyChanged -= Nuoc_PropertyChanged;
        }

        void Nuoc_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            MainFilter.SetFilterTenNuoc(DiaDiemViewModel.Header_Nuoc.Text);

            OnHeaderFilterChanged();
        }

        void Entity_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var diadiem = e.NewItems[0] as rDiaDiem;
                diadiem.rNuocList = rNuoc;

                if (rNuocDefault != null)
                {
                    diadiem.MaNuoc = rNuocDefault.Ma;
                }
            }
        }

        public override void RefreshData()
        {
            if (MainFilter.FilterDiaDiem == null)
            {
                return;
            }

            _context = ContextFactory.CreateContext();
            _origData = rDiaDiemRepository.GetData(_context, MainFilter.FilterDiaDiem);

            ItemCount = rDiaDiemRepository.GetDataCount(_context, MainFilter.FilterDiaDiem);
            _origData = rDiaDiemRepository.GetData(_context, MainFilter.FilterDiaDiem, PageSize, CurrentPageIndex, ItemCount);

            Unload();
            Entity.Clear();

            foreach (var rDiaDiem in _origData)
            {
                Entity.Add(rDiaDiem);
            }

            UpdateNuocReferenceData();

            Load();
        }

        public void UpdateNuocReferenceData()
        {
            _rNuocs = rNuocRepository.GetData(_context, FilterNuoc);

            if (Entity == null)
            {
                return;
            }

            foreach (var rDiaDiem in Entity)
            {
                rDiaDiem.rNuocList = _rNuocs;
            }
        }

        public List<Repository<rDiaDiem>.ChangedItemData> Save()
        {
            try
            {
                return rDiaDiemRepository.Save(_context, Entity.ToList(), _origData);
            }
            catch (Exception)
            {
                PhuDinhCommon.EntityFrameworkUtils.UndoContextChange(_context, EntityState.Modified);
                throw;
            }
        }
    }
}
