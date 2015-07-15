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

        protected Expression<Func<T, bool>> FilterNullable(object value, bool setFalse, string propertyPath)
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
                    result = FilterExpression(value, propertyPath);
                }
            }

            return result;
        }

        protected Expression<Func<T, bool>> FilterExpression(object obj, string propertyPath)
        {
            var pe = Expression.Parameter(typeof(T), "p");

            var left = propertyPath.Split('.').Aggregate<string, Expression>(pe, Expression.Property);
            var right = Expression.Constant(obj);
            var predicateBody = Expression.Equal(left, right);

            var result = Expression.Lambda<Func<T, bool>>(predicateBody, new[] { pe });
            return result;
        }

        protected Expression<Func<T, bool>> FilterText(string text, bool setFalse, string propertyPath)
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
                    var pe = Expression.Parameter(typeof(T), "p");

                    var left = propertyPath.Split('.').Aggregate<string, Expression>(pe, Expression.Property);

                    switch (text[0])
                    {
                        case '$':
                            {
                                text = text.Substring(1, text.Length - 1);
                                var right = Expression.Constant(text);
                                var predicateBody = Expression.Equal(left, right);

                                result = Expression.Lambda<Func<T, bool>>(predicateBody, new[] { pe });
                            }
                            break;
                        case '#':
                            {
                                text = text.Substring(1, text.Length - 1);
                                var right = Expression.Constant(text);

                                var predicateBody = Expression.Call(left, "Contains", null, new Expression[] { right });

                                result = Expression.Lambda<Func<T, bool>>(Expression.Not(predicateBody), new[] { pe });
                            }
                            break;
                        default:
                            {
                                var right = Expression.Constant(text);

                                var predicateBody = Expression.Call(left, "Contains", null, new Expression[] { right });

                                result = Expression.Lambda<Func<T, bool>>(predicateBody, new[] { pe });
                            }
                            break;
                    }
                }
            }
            return result;
        }

        protected void UpdateMainFilter()
        {
            Filter = null;

            foreach (var filter in _filters)
            {
                if (filter.Value.Body.NodeType == ExpressionType.Constant && filter.Value.Body.ToString() == "True")
                {
                    continue;
                }
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
