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
    /// Interaction logic for rChanhView.xaml
    /// </summary>
    public partial class rChanhView : BaseView
    {
        public Expression<Func<PhuDinhData.rChanh, bool>> FilterChanh { get; set; }
        public Expression<Func<PhuDinhData.rBaiXe, bool>> FilterBaiXe { get; set; }
        public PhuDinhData.rBaiXe rBaiXeDefault { get; set; }

        private ObservableCollection<PhuDinhData.rChanh> _rChanhs;
        private List<PhuDinhData.rBaiXe> _rBaiXes;
        private PhuDinhData.PhuDinhEntities _context = ContextFactory.CreateContext();

        public rChanhView()
        {
            InitializeComponent();

            FilterChanh = (p => true);
            FilterBaiXe = (p => true);
        }

        #region Override base view method
        public override void CommitEdit()
        {
            dgChanh.CommitEdit();
            base.CommitEdit();
        }

        public override void Save()
        {
            CommitEdit();
            try
            {
                if (FilterChanh == null)
                {
                    return;
                }

                var data = dgChanh.DataContext as ObservableCollection<PhuDinhData.rChanh>;
                PhuDinhData.Repository.rChanhRepository.Save(_context, data.ToList(), FilterChanh);
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
            if (FilterChanh == null)
            {
                dgChanh.DataContext = null;
                return;
            }

            var index = dgChanh.SelectedIndex;

            if (_rChanhs != null)
            {
                _rChanhs.CollectionChanged -= collection_CollectionChanged;
            }

            _context = ContextFactory.CreateContext();
            var rChanhs = PhuDinhData.Repository.rChanhRepository.GetData(_context, FilterChanh);

            _rChanhs = new ObservableCollection<PhuDinhData.rChanh>(rChanhs);
            _rChanhs.CollectionChanged += collection_CollectionChanged;

            UpdateBaiXeReferenceData();

            dgChanh.DataContext = _rChanhs;

            dgChanh.SelectedIndex = index;
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var chanh = e.NewItems[0] as PhuDinhData.rChanh;
                chanh.rBaiXeList = _rBaiXes;

                if (rBaiXeDefault != null)
                {
                    chanh.MaBaiXe = rBaiXeDefault.Ma;
                }
            }
        }

        #endregion

        private void dgChanh_HeaderAddButtonClick(object sender, EventArgs e)
        {
            CommitEdit();

            var view = new rBaiXeView();
            view.RefreshView();
            ChildWindowUtils.ShowChildWindow("Bãi Xe", view);

            UpdateBaiXeReferenceData();
        }

        private void UpdateBaiXeReferenceData()
        {
            _rBaiXes = PhuDinhData.Repository.rBaiXeRepository.GetData(_context, FilterBaiXe);

            if (_rChanhs == null)
            {
                return;
            }

            foreach (var rChanh in _rChanhs)
            {
                rChanh.rBaiXeList = _rBaiXes;
            }
        }
    }
}
