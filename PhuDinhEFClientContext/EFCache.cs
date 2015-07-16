using System;
using System.Collections.Generic;
using System.Linq;

namespace PhuDinhEFClientContext
{
    internal class EFCache
    {
        private readonly Dictionary<string, Tuple<DateTime?, object>> _cache = new Dictionary<string, Tuple<DateTime?, object>>();
        public List<T1> GetData<T1>(IQueryable<T1> dbQuery, string key, System.DateTime? lastUpdate) where T1 : Common.BindableObject
        {
            List<T1> result = null;

            if (_cache.ContainsKey(key) == false)
            {
                result = dbQuery.ToList();
                _cache.Add(key, new Tuple<DateTime?, object>(lastUpdate, result));
                return result;
            }

            if (lastUpdate.HasValue == false)
            {
                return _cache[key].Item2 as List<T1>;
            }

            if (_cache[key].Item1.HasValue == false)
            {
                result = dbQuery.ToList();
                _cache[key] = new Tuple<DateTime?, object>(lastUpdate, result);
                return result;
            }

            if (lastUpdate.Value <= _cache[key].Item1)
            {
                return _cache[key].Item2 as List<T1>;
            }

            result = dbQuery.ToList();
            _cache[key] = new Tuple<DateTime?, object>(lastUpdate, result);
            return result;
        }

    }
}
