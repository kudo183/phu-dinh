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

        private ObservableCollection<PhuDinhData.rDiaDiem> _rDiaDiems;
        private List<PhuDinhData.rNuoc> _rNuocs;
        private PhuDinhData.PhuDinhEntities _context = new PhuDinhData.PhuDinhEntities();

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
                if (FilterDiaDiem == null)
                {
                    return;
                }

                var data = dgDiaDiem.DataContext as ObservableCollection<PhuDinhData.rDiaDiem>;
                PhuDinhData.Repository.rDiaDiemRepository.Save(_context, data.ToList(), FilterDiaDiem);
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
            if (FilterDiaDiem == null)
            {
                dgDiaDiem.DataContext = null;
                return;
            }

            var index = dgDiaDiem.SelectedIndex;

            if (_rDiaDiems != null)
            {
                _rDiaDiems.CollectionChanged -= collection_CollectionChanged;
            }

            _context = new PhuDinhData.PhuDinhEntities();
            var rDiaDiems = PhuDinhData.Repository.rDiaDiemRepository.GetData(_context, FilterDiaDiem);

            _rDiaDiems = new ObservableCollection<PhuDinhData.rDiaDiem>(rDiaDiems);
            _rDiaDiems.CollectionChanged += collection_CollectionChanged;

            UpdateNuocReferenceData();

            dgDiaDiem.DataContext = _rDiaDiems;

            dgDiaDiem.SelectedIndex = index;
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

        private void dgDiaDiem_HeaderAddButtonClick(object sender, EventArgs e)
        {
            dgDiaDiem.CommitEdit();

            var view = new rNuocView();
            view.RefreshView();            
            ChildWindowUtils.ShowChildWindow("Nước", view);

            UpdateNuocReferenceData();
        }

        private void UpdateNuocReferenceData()
        {
            _rNuocs = PhuDinhData.Repository.rNuocRepository.GetData(_context, FilterNuoc);

            if (_rDiaDiems == null)
            {
                return;
            }

            foreach (var rDiaDiem in _rDiaDiems)
            {
                rDiaDiem.rNuocList = _rNuocs;
            }
        }
    }
}
