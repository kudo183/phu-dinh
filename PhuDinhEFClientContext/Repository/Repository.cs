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
            return PagingData(RepositoryLocator<T>.GetDataQuery(GetDataQueryWithFilter(context, filter)), pageSize, currentPageIndex, itemCount);
        }

        public static int GetDataCount(PhuDinhEntities context, Expression<Func<T, bool>> filter)
        {
            return GetDataQueryWithFilter(context, filter).Count();
        }

        public static IQueryable<T> GetDataQuery(PhuDinhEntities context, Expression<Func<T, bool>> filter)
        {
            return RepositoryLocator<T>.GetDataQuery(GetDataQueryWithFilter(context, filter));
        }

        private static IQueryable<T> GetDataQueryWithFilter(PhuDinhEntities context, Expression<Func<T, bool>> filter)
        {
            var q = filter != null ? context.Set<T>().Where(filter) : context.Set<T>();
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

        public static List<ChangedItemData> Save(PhuDinhEntities context, List<T> data, List<T> origData)
        {
            var removedItems = RemoveItem(context, data, origData);
            var addedItems = AddNewItem(context, data);
            var changed = context.ChangeTracker.Entries<T>().Where(p => p.State == EntityState.Modified).ToList();

            var changedItems = new List<ChangedItemData>();
            changedItems.AddRange(changed.Select(item => new ChangedItemData
            {
                State = EntityState.Modified,
                OriginalValues = item.OriginalValues.Clone().ToObject() as T,
                CurrentValues = item.CurrentValues.Clone().ToObject() as T
            }));

            changedItems.AddRange(removedItems.Select(item => new ChangedItemData
            {
                State = EntityState.Deleted,
                OriginalValues = null,
                CurrentValues = item
            }));

            context.SaveChanges();

            changedItems.AddRange(addedItems.Select(item => new ChangedItemData
            {
                State = EntityState.Added,
                OriginalValues = null,
                CurrentValues = item
            }));

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

        private static List<T> AddNewItem(PhuDinhEntities context, List<T> gridDataSource)
        {
            var result = new List<T>();

            foreach (var item in gridDataSource)
            {
                if (item.IsNewItem())
                {
                    result.Add(item);
                    context.Set<T>().Add(item);
                }
            }

            return result;
        }

        private static List<T> RemoveItem(PhuDinhEntities context, List<T> gridDataSource, List<T> origData)
        {
            var result = new List<T>();

            foreach (var item in origData)
            {
                var entity = gridDataSource.FirstOrDefault(p => item.IsEqual(p));
                if (entity == null)
                {
                    result.Add(item);
                    context.Set<T>().Remove(item);
                }
            }

            return result;
        }
    }
}
