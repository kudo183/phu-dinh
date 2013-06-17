using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

        private List<PhuDinhData.rLoaiHang> _rLoaiHangs;

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
                var data = this.dgMatHang.DataContext as ObservableCollection<PhuDinhData.tMatHang>;
                PhuDinhData.Repository.tMatHangRepository.Save(data.ToList(), FilterMatHang);
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
            _rLoaiHangs = PhuDinhData.Repository.rLoaiHangRepository.GetData(context, FilterLoaiHang);
            var data = PhuDinhData.Repository.tMatHangRepository.GetData(context, FilterMatHang);

            foreach (var tMatHang in data)
            {
                tMatHang.rLoaiHangList = _rLoaiHangs;
            }

            var collection = new ObservableCollection<PhuDinhData.tMatHang>(data);
            collection.CollectionChanged += collection_CollectionChanged;
            this.dgMatHang.DataContext = collection;
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var matHang = e.NewItems[0] as PhuDinhData.tMatHang;
                matHang.rLoaiHangList = _rLoaiHangs;
            }
        }

        #endregion
    }
}
