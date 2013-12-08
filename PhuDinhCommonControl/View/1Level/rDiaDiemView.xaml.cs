using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rDiaDiemView.xaml
    /// </summary>
    public partial class rDiaDiemView : BaseView
    {
        public Filter_rDiaDiem FilterDiaDiem { get; set; }
        public Expression<Func<PhuDinhData.rNuoc, bool>> FilterNuoc { get; set; }

        private ObservableCollection<PhuDinhData.rDiaDiem> _rDiaDiems;
        private List<PhuDinhData.rNuoc> _rNuocs;
        private PhuDinhData.PhuDinhEntities _context = ContextFactory.CreateContext();

        private List<PhuDinhData.rDiaDiem> _origData;

        private string _filterNuoc = string.Empty;

        public rDiaDiemView()
        {
            InitializeComponent();

            FilterDiaDiem = new Filter_rDiaDiem();
            FilterNuoc = (p => true);

            Loaded += rDiaDiemView_Loaded;
            Unloaded += rDiaDiemView_Unloaded;
        }

        void rDiaDiemView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DataGridColumnHeaderTextFilter.DiaDiem_Nuoc.Text = _filterNuoc;
            DataGridColumnHeaderTextFilter.DiaDiem_Nuoc.PropertyChanged += DiaDiem_Nuoc_PropertyChanged;
        }

        void rDiaDiemView_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _filterNuoc = DataGridColumnHeaderTextFilter.DiaDiem_Nuoc.Text;
            DataGridColumnHeaderTextFilter.DiaDiem_Nuoc.PropertyChanged -= DiaDiem_Nuoc_PropertyChanged;
        }

        void DiaDiem_Nuoc_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(DataGridColumnHeaderTextFilter.DiaDiem_Nuoc.Text) == false)
            {
                FilterDiaDiem.FilterNuoc = (p => p.rNuoc.TenNuoc.Contains(DataGridColumnHeaderTextFilter.DiaDiem_Nuoc.Text));
            }
            else
            {
                FilterDiaDiem.FilterNuoc = (p => true);
            }

            RefreshView();
        }

        #region Override base view method
        public override void CommitEdit()
        {
            dgDiaDiem.CommitEdit();
            base.CommitEdit();
        }

        public override void Save()
        {
            CommitEdit();
            try
            {
                if (FilterDiaDiem == null)
                {
                    return;
                }

                var data = dgDiaDiem.DataContext as ObservableCollection<PhuDinhData.rDiaDiem>;
                PhuDinhData.Repository.rDiaDiemRepository.Save(_context, data.ToList(), _origData);
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

            _context = ContextFactory.CreateContext();
            _origData = PhuDinhData.Repository.rDiaDiemRepository.GetData(_context, FilterDiaDiem.FilterDiaDiem);

            _rDiaDiems = new ObservableCollection<PhuDinhData.rDiaDiem>(_origData);
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
            CommitEdit();

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
