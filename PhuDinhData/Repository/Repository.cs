﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PhuDinhData.Repository
{
    public static class Repository<T> where T : class
    {
        public static IQueryable<T> GetData(PhuDinhEntities context, Expression<Func<T, bool>> filter)
        {
            return context.Set<T>().Where(filter);
        }

        public static List<T> PagingData(IQueryable<T> data, int pageSize, int currentPageIndex, int itemCount)
        {
            if (pageSize > itemCount)
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

        public static List<ChangedItemData> Save(PhuDinhEntities context, List<T> data, List<T> origData, Func<T, bool> CheckNewItemFunc, Func<T, T, bool> CompareFunc)
        {
            var removedItems = RemoveItem(context, data, origData, CompareFunc);
            var addedItems = AddNewItem(context, data, CheckNewItemFunc);
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

        private static List<T> AddNewItem(PhuDinhEntities context, List<T> gridDataSource, Func<T, bool> CheckNewItemFunc)
        {
            var result = new List<T>();

            foreach (var item in gridDataSource)
            {
                if (CheckNewItemFunc(item))
                {
                    result.Add(item);
                    context.Set<T>().Add(item);
                }
            }

            return result;
        }

        private static List<T> RemoveItem(PhuDinhEntities context, List<T> gridDataSource, List<T> origData, Func<T, T, bool> CompareFunc)
        {
            var result = new List<T>();

            foreach (var item in origData)
            {
                var entity = gridDataSource.FirstOrDefault(p => CompareFunc(item, p));
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
