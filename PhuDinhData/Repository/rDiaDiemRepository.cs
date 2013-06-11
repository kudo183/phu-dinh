using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rDiaDiemRepository
    {
        private static void RemoveOrUpdateItem(PhuDinhEntities context, List<rDiaDiem> gridDataSource, Expression<Func<rDiaDiem, bool>> filter)
        {
            foreach (var item in GetData(context, filter))
            {
                var entity = gridDataSource.FirstOrDefault(p => p.Ma == item.Ma);
                //remove deleted item
                if (entity == null)
                {
                    context.rDiaDiems.Remove(item);
                }
                //update exist item
                else
                {
                    item.MaNuoc = entity.MaNuoc;
                    item.Tinh = entity.Tinh;
                }
            }
        }

        private static void AddNewItem(PhuDinhEntities context, List<rDiaDiem> gridDataSource)
        {
            foreach (var item in gridDataSource)
            {
                if (item.Ma == 0)
                {
                    context.rDiaDiems.Add(item);
                }
            }
        }

        public static List<rDiaDiem> GetData(Expression<Func<rDiaDiem, bool>> filter)
        {
            var context = new PhuDinhEntities();
            return GetData(context, filter);
        }

        public static List<rDiaDiem> GetData(PhuDinhEntities context, Expression<Func<rDiaDiem, bool>> filter)
        {
            return context.rDiaDiems.Where(filter).ToList();
        }

        public static void Save(List<rDiaDiem> data, Expression<Func<rDiaDiem, bool>> filter)
        {
            var context = new PhuDinhEntities();
            Save(context, data, filter);
        }

        public static void Save(PhuDinhEntities context, List<rDiaDiem> data, Expression<Func<rDiaDiem, bool>> filter)
        {
            RemoveOrUpdateItem(context, data, filter);
            AddNewItem(context, data);
            context.SaveChanges();
        }
    }
}
