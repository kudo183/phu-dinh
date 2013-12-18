using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PhuDinhData.ViewModel
{
    public class BaseViewModel<T> : INotifyPropertyChanged
    {
        protected PhuDinhEntities _context;

        protected List<T> _origData;

        protected bool _isLoading;

        public ObservableCollection<T> Entity { get; set; }

        public event EventHandler HeaderFilterChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        private int _currentPageIndex = 1;
        public int CurrentPageIndex
        {
            get { return _currentPageIndex; }
            set
            {
                if (_currentPageIndex == value)
                {
                    return;
                }

                _currentPageIndex = value;

                RefreshData();
                OnPropertyChanged("CurrentPageIndex");
            }
        }

        private int _pageSize = 30;
        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                if (_pageSize == value)
                {
                    return;
                }

                _pageSize = value;
                OnPropertyChanged("PageSize");
            }
        }

        private int _itemCount;
        public int ItemCount
        {
            get { return _itemCount; }
            protected set
            {
                if (_itemCount == value)
                {
                    return;
                }

                _itemCount = value;
                OnPropertyChanged("ItemCount");
            }
        }

        protected virtual void OnHeaderFilterChanged()
        {
            if (_isLoading == true)
            {
                return;
            }

            EventHandler handler = HeaderFilterChanged;
            if (handler != null) handler(this, null);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void RefreshData()
        {
            
        }
    }
}
