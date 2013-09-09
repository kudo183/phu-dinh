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
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class tMatHangView : BaseView
    {
        public Expression<Func<PhuDinhData.tMatHang, bool>> FilterMatHang { get; set; }
        public Expression<Func<PhuDinhData.rLoaiHang, bool>> FilterLoaiHang { get; set; }
        public PhuDinhData.rLoaiHang rLoaiHangDefault { get; set; }

        private ObservableCollection<PhuDinhData.tMatHang> _tMatHangs;
        private List<PhuDinhData.rLoaiHang> _rLoaiHangs;
        private PhuDinhData.PhuDinhEntities _context = new PhuDinhData.PhuDinhEntities();

        public tMatHangView()
        {
            InitializeComponent();

            FilterMatHang = (p => true);
            FilterLoaiHang = (p => true);
        }

        #region Override base view method
        public override void Save()
        {
            try
            {
                if (FilterMatHang == null)
                {
                    return;
                }

                var data = dgMatHang.DataContext as ObservableCollection<PhuDinhData.tMatHang>;
                PhuDinhData.Repository.tMatHangRepository.Save(_context, data.ToList(), FilterMatHang);
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
            if (FilterMatHang == null)
            {
                dgMatHang.DataContext = null;
                return;
            }

            var index = dgMatHang.SelectedIndex;

            if (_tMatHangs != null)
            {
                _tMatHangs.CollectionChanged -= collection_CollectionChanged;
            }

            _context = new PhuDinhData.PhuDinhEntities();
            var tMatHangs = PhuDinhData.Repository.tMatHangRepository.GetData(_context, FilterMatHang);

            _tMatHangs = new ObservableCollection<PhuDinhData.tMatHang>(tMatHangs);
            _tMatHangs.CollectionChanged += collection_CollectionChanged;

            UpdateLoaiHangReferenceData();

            dgMatHang.DataContext = _tMatHangs;

            dgMatHang.SelectedIndex = index;
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tMatHang = e.NewItems[0] as PhuDinhData.tMatHang;
                tMatHang.rLoaiHangList = _rLoaiHangs;

                if (rLoaiHangDefault != null)
                {
                    tMatHang.MaLoai = rLoaiHangDefault.Ma;
                }
            }
        }

        #endregion

        private void dgMatHang_HeaderAddButtonClick(object sender, EventArgs e)
        {
            dgMatHang.CommitEdit();

            var view = new rLoaiHangView();
            view.RefreshView();
            ChildWindowUtils.ShowChildWindow("Loại Hàng", view);

            UpdateLoaiHangReferenceData();
        }

        private void UpdateLoaiHangReferenceData()
        {
            _rLoaiHangs = PhuDinhData.Repository.rLoaiHangRepository.GetData(_context, FilterLoaiHang);

            if (_tMatHangs == null)
            {
                return;
            }

            foreach (var tMatHang in _tMatHangs)
            {
                tMatHang.rLoaiHangList = _rLoaiHangs;
            }

        }
    }
}
