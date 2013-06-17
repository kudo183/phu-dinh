using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;

namespace PhuDinhCommonControl
{
    /// <summary>
    /// Interaction logic for rChanhView.xaml
    /// </summary>
    public partial class rChanhView : BaseView
    {
        public Expression<Func<PhuDinhData.rChanh, bool>> FilterChanh { get; set; }
        public Expression<Func<PhuDinhData.rBaiXe, bool>> FilterBaiXe { get; set; }

        private List<PhuDinhData.rBaiXe> _rBaiXes;
        public rChanhView()
        {
            InitializeComponent();

            FilterChanh = (p => true);
            FilterBaiXe = (p => true);
        }

        #region Override base view method
        public override void Save()
        {
            try
            {
                var data = this.dgChanh.DataContext as ObservableCollection<PhuDinhData.rChanh>;
                PhuDinhData.Repository.rChanhRepository.Save(data.ToList(), FilterChanh);
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

            _rBaiXes = PhuDinhData.Repository.rBaiXeRepository.GetData(context, FilterBaiXe);

            var data = PhuDinhData.Repository.rChanhRepository.GetData(context, FilterChanh);

            foreach (var rChanh in data)
            {
                rChanh.BaiXe = _rBaiXes.FirstOrDefault(
                    p => p.Ma == rChanh.MaBaiXe);

                rChanh.rBaiXeList = _rBaiXes;
            }

            var collection = new ObservableCollection<PhuDinhData.rChanh>(data);
            collection.CollectionChanged += collection_CollectionChanged;
            this.dgChanh.DataContext = collection;

            this.dgChanh.UpdateLayout();
        }

        void collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var chanh = e.NewItems[0] as PhuDinhData.rChanh;
                chanh.rBaiXeList = _rBaiXes;
            }
        }

        #endregion        
    }
}
