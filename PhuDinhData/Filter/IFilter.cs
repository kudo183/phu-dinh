using System;
using System.Linq.Expressions;

namespace PhuDinhData.Filter
{
    public interface IFilter<T>
    {
        Expression<Func<T, bool>> Filter { get; }
        bool IsClearAllData { get; }
        void SetFilter(string key, object value, bool setFalse = false);
    }
}
