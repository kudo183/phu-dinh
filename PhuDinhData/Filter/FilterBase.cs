using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PhuDinhCommon;

namespace PhuDinhData.Filter
{
    public abstract class FilterBase<T> : IFilter<T>
    {
        public Expression<Func<T, bool>> Filter { get; protected set; }

        public bool IsClearAllData
        {
            get
            {
                return _filters.Any(filter =>
                    filter.Value.Body.NodeType == ExpressionType.Constant
                    && filter.Value.Body.ToString() == "False");
            }
        }

        protected readonly Dictionary<string, Expression<Func<T, bool>>> _filters
            = new Dictionary<string, Expression<Func<T, bool>>>();

        protected readonly Dictionary<string, object> _filtersValue = new Dictionary<string, object>();

        protected Expression<Func<T, bool>> FilterNullable(object value, bool setFalse, Expression<Func<T, bool>> filter)
        {
            Expression<Func<T, bool>> result;

            if (setFalse == true)
            {
                result = (p => false);
            }
            else
            {
                if (value == null)
                {
                    result = (p => true);
                }
                else
                {
                    result = filter;
                }
            }

            return result;
        }

        protected Expression<Func<T, bool>> FilterText(string text, bool setFalse, Expression<Func<T, bool>> filter)
        {
            Expression<Func<T, bool>> result;

            if (setFalse == true)
            {
                result = (p => false);
            }
            else
            {
                if (string.IsNullOrEmpty(text))
                {
                    result = (p => true);
                }
                else
                {
                    result = filter;
                }
            }
            return result;
        }

        protected void UpdateMainFilter()
        {
            Filter = null;

            foreach (var filter in _filters)
            {
                Filter = Filter != null ? filter.Value.And(Filter) : filter.Value;
            }
        }

        public abstract void SetFilter(string key, object value, bool setFalse = false);

        public virtual object GetFilterValue(string key)
        {
            return _filtersValue.ContainsKey(key) ? _filtersValue[key] : null;
        }
    }
}
