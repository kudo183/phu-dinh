using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using PhuDinhData.Filter;
using PhuDinhData.Repository;

namespace PhuDinhData.ViewModel
{
    public abstract class BaseViewModel<T> : BindableObject where T : class
    {
        protected PhuDinhEntities _context;

        protected List<T> _origData;

        protected bool _isLoading;

        public ObservableCollection<T> Entity { get; set; }

        public IFilter<T> MainFilter { get; set; }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                if (_message == value)
                {
                    return;
                }

                _message = value;

                RaisePropertyChanged("Message");
            }
        }

        public event EventHandler HeaderFilterChanged;

        private readonly Dictionary<string, object> _defaultValues = new Dictionary<string, object>();
        private readonly Dictionary<string, LambdaExpression> _referenceFilters = new Dictionary<string, LambdaExpression>();

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
                RaisePropertyChanged("CurrentPageIndex");
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
                RaisePropertyChanged("PageSize");
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
                RaisePropertyChanged("ItemCount");
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

        protected void UpdateReferenceData<T1>(out List<T1> data,
            Expression<Func<T1, bool>> filter, Action<T> action)
            where T1 : class
        {
            data = RepositoryLocator<T1>.GetData(_context, filter);
            if (Entity == null)
            {
                return;
            }

            foreach (var item in Entity)
            {
                action(item);
            }
        }

        protected abstract void UpdateAllReferenceData();

        public abstract void UpdateReferenceData(string columnName);

        public void SetDefaultValue(string columnName, object value)
        {
            if (_defaultValues.ContainsKey(columnName))
            {
                _defaultValues[columnName] = value;
                return;
            }

            _defaultValues.Add(columnName, value);
        }

        public object GetDefaultValue(string columnName)
        {
            if (_defaultValues.ContainsKey(columnName))
            {
                return _defaultValues[columnName];
            }

            return null;
        }

        public void SetReferenceFilter<T1>(string columnName, Expression<Func<T1, bool>> filter) where T1 : class
        {
            if (_referenceFilters.ContainsKey(columnName))
            {
                _referenceFilters[columnName] = filter;
                return;
            }

            _referenceFilters.Add(columnName, filter);
        }

        public Expression<Func<T1, bool>> GetReferenceFilter<T1>(string columnName) where T1 : class
        {
            if (_referenceFilters.ContainsKey(columnName))
            {
                return _referenceFilters[columnName] as Expression<Func<T1, bool>>;
            }

            return null;
        }

        public virtual List<Repository<T>.ChangedItemData> Save()
        {
            try
            {
                return RepositoryLocator<T>.Save(_context, Entity.ToList(), _origData);
            }
            catch (Exception)
            {
                PhuDinhCommon.EntityFrameworkUtils.UndoContextChange(_context, EntityState.Modified);
                throw;
            }
        }

        public abstract void Load();

        public abstract void Unload();

        public virtual void RefreshData()
        {
            if (MainFilter.Filter == null)
            {
                return;
            }

            if (_context == null)
            {
                _context = ContextFactory.CreateContext();
            }
            else
            {
                _context.Dispose();
                _context = null;
                _context = ContextFactory.CreateContext();
            }

            ItemCount = RepositoryLocator<T>.GetDataCount(_context, MainFilter.Filter);
            _origData = RepositoryLocator<T>.GetData(_context, MainFilter.Filter, PageSize, CurrentPageIndex, ItemCount);

            Unload();
            Entity.Clear();

            foreach (var item in _origData)
            {
                Entity.Add(item);
            }

            UpdateAllReferenceData();

            Load();
        }

        public virtual void Dispose()
        {
            _context.Dispose();
            _context = null;
        }
    }
}
