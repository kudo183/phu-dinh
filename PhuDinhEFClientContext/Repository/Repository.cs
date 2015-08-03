using PhuDinhDataEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Common;

namespace PhuDinhEFClientContext.Repository
{
    public static class Repository<T> where T : BindableObject
    {
        public static List<T> GetData(PhuDinhEntities context,
            Expression<Func<T, bool>> filter,
            int pageSize, int currentPageIndex, int itemCount)
        {
            return PagingData(RepositoryLocator<T>.GetDataQuery(GetDataQueryWithFilterNoTracking(context, filter)), pageSize, currentPageIndex, itemCount);
        }

        public static int GetDataCount(PhuDinhEntities context, Expression<Func<T, bool>> filter)
        {
            return GetDataQueryWithFilterNoTracking(context, filter).Count();
        }

        public static IQueryable<T> GetDataQuery(PhuDinhEntities context, Expression<Func<T, bool>> filter)
        {
            return RepositoryLocator<T>.GetDataQuery(GetDataQueryWithFilterNoTracking(context, filter));
        }

        private static IQueryable<T> GetDataQueryWithFilterNoTracking(PhuDinhEntities context, Expression<Func<T, bool>> filter)
        {
            var q = filter != null ? context.Set<T>().AsNoTracking().Where(filter) : context.Set<T>().AsNoTracking();
            return q;
        }

        private static List<T> PagingData(IQueryable<T> data, int pageSize, int currentPageIndex, int itemCount)
        {
            if (itemCount == 0)
            {
                return new List<T>();
            }

            if (pageSize > itemCount || pageSize == 0)
            {
                return data.ToList();
            }

            var skippedItem = pageSize * (currentPageIndex - 1);

            var takeItem = itemCount - skippedItem;
            if (takeItem > pageSize)
            {
                takeItem = pageSize;
            }

            if (takeItem <= 0)
            {
                return new List<T>();
            }

            return data.Skip(skippedItem).Take(takeItem).ToList();
        }

        /// <summary>
        /// ***This method will null all virtual properties of data and origData. Need to reload data from db after call this method.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="data"></param>
        /// <param name="origData"></param>
        /// <returns></returns>
        public static List<ChangedItemData> Save(PhuDinhEntities context, List<T> data, List<T> origData)
        {
            var virtualProperties = typeof(T).GetProperties().Where(p => p.IsVirtual()).ToList();
            foreach (var property in virtualProperties)
            {
                if (property.CanRead == false || property.CanWrite == false)
                    continue;

                foreach (var item in data)
                {
                    property.SetValue(item, null);
                }
                foreach (var item in origData)
                {
                    property.SetValue(item, null);
                }
            }

            var removedItems = RemoveItem(context, data, origData);
            var addedOrchangedItems = AddNewOrUpdateItem(context, data, origData);

            context.SaveChanges();

            PhuDinhCommon.EntityFrameworkUtils.DetachAllUnchangedEntity(context);

            var changedItems = new List<ChangedItemData>();
            changedItems.AddRange(addedOrchangedItems);
            changedItems.AddRange(removedItems);

            return changedItems;
        }

        public static string BuildLogString(IEnumerable<ChangedItemData> changedItems)
        {
            var result = new StringBuilder();

            foreach (var item in changedItems)
            {
                if (item.State == EntityState.Modified)
                {
                    result.AppendLine(item.State.ToString());
                    result.AppendLine(item.OriginalValues.ToString());
                    result.AppendLine(item.CurrentValues.ToString());
                    result.AppendLine();
                }
                else
                {
                    result.AppendLine(item.State.ToString());
                    result.AppendLine(item.CurrentValues.ToString());
                    result.AppendLine();
                }
            }

            return result.ToString();
        }

        public class ChangedItemData
        {
            public EntityState State { get; set; }
            public T OriginalValues { get; set; }
            public T CurrentValues { get; set; }
        }

        private static List<ChangedItemData> AddNewOrUpdateItem(PhuDinhEntities context, List<T> gridDataSource, List<T> origData)
        {
            var result = new List<ChangedItemData>();

            foreach (var item in gridDataSource)
            {
                if (item.IsNewItem())
                {
                    context.Set<T>().Attach(item);
                    context.Entry(item).State = EntityState.Added;

                    result.Add(new ChangedItemData()
                    {
                        State = EntityState.Added,
                        CurrentValues = item
                    });
                }
                else
                {
                    context.Set<T>().Attach(item);
                    var r = context.Entry(item);
                    r.OriginalValues.SetValues(origData.Find(p => p.GetKey() == item.GetKey()));

                    if (r.State == EntityState.Modified)
                    {
                        result.Add(new ChangedItemData()
                        {
                            State = EntityState.Modified,
                            CurrentValues = r.CurrentValues.ToObject() as T,
                            OriginalValues = r.OriginalValues.ToObject() as T
                        });
                    }
                }
            }

            return result;
        }

        private static List<ChangedItemData> RemoveItem(PhuDinhEntities context, List<T> gridDataSource, List<T> origData)
        {
            var result = new List<ChangedItemData>();

            foreach (var item in origData)
            {
                var entity = gridDataSource.FirstOrDefault(p => item.IsEqual(p));
                if (entity == null)
                {
                    context.Set<T>().Attach(item);
                    context.Entry(item).State = EntityState.Deleted;

                    result.Add(new ChangedItemData()
                    {
                        State = EntityState.Deleted,
                        CurrentValues = item
                    });
                }
            }

            return result;
        }
    }
}
