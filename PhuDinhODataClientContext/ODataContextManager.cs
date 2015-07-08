﻿using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Linq.Expressions;
using Common;
using PhuDinhCommon;

namespace PhuDinhODataClientContext
{
    public sealed class ODataContextManager : IClientContextManager
    {
        private DataServiceContextEx _context;

        public ODataContextManager()
        {
            CreateContext();
        }

        public void CreateContext()
        {
            _context = new DataServiceContextEx(new Uri("http://luoithepvinhphat.com:8888/odata")
                , PhuDinhDataEntity.PhuDinhDataEntityEDM.GetPhuDinhDataEntityEDM());

            _context.ReceivingResponse += _context_ReceivingResponse;
        }

        void _context_ReceivingResponse(object sender, ReceivingResponseEventArgs e)
        {
            var r = e.ResponseMessage as HttpWebResponseMessage;

            Logger.LogDebug(r.Response.ResponseUri + "   " + r.Response.ContentLength.ToString());
        }

        public List<T> GetData<T>(Expression<Func<T, bool>> filter) where T : Common.BindableObject
        {
            var result = new List<T>();
            var query = _context.Set<T>().Where(filter) as DataServiceQuery<T>;
            query = Repository.RepositoryLocator<T>.GetDataQuery(query) as DataServiceQuery<T>;//add order, expand
            var respone = query.Execute() as QueryOperationResponse<T>;

            do
            {
                foreach (var entity in respone)
                {
                    result.Add(entity);
                }

                if (result.Count >= 1000)
                    break;

                DataServiceQueryContinuation<T> token = respone.GetContinuation();
                if (token == null)
                    break;

                respone = _context.Execute(token);

            } while (true);

            return result;
        }

        public List<T> GetData<T>(Expression<Func<T, bool>> filter, int pageSize, int currentPageIndex, int itemCount) where T : Common.BindableObject
        {
            var query = _context.Set<T>().Where(filter);//add filter
            query = Repository.RepositoryLocator<T>.GetDataQuery(query);//add order, expand
            query = query.Skip(pageSize * (currentPageIndex - 1)).Take(pageSize);//add paging

            var collection = new DataServiceCollection<T>(query);//this line enable change tracking and execute the query, not need to call query.ToList()

            return collection.ToList();
        }

        public void UndoChange()
        {
        }

        public void Dispose()
        {
        }

        public void Save<T>(List<T> data, List<T> originalData) where T : Common.BindableObject
        {
            RemoveItem(_context, data, originalData);
            AddNewItem(_context, data);
            _context.SaveChanges();
        }

        public int GetDataCount<T>(Expression<Func<T, bool>> filter) where T : Common.BindableObject
        {
            var query = _context.Set<T>().IncludeTotalCount().Where(filter).Skip(0).Take(1) as DataServiceQuery;//skip(0).Take(1) because just need get TotalCount
            var response = query.Execute() as QueryOperationResponse<T>;

            return (int)response.TotalCount;
        }

        public void ReloadEntity<T>(T entity) where T : Common.BindableObject
        {
            _context.LoadProperty(entity, "");
        }

        private static List<T> AddNewItem<T>(DataServiceContextEx context, List<T> gridDataSource) where T : Common.BindableObject
        {
            var result = new List<T>();

            foreach (var item in gridDataSource)
            {
                if (item.IsNewItem())
                {
                    result.Add(item);
                    context.Add(item);
                }
            }

            return result;
        }

        private static List<T> RemoveItem<T>(DataServiceContextEx context, List<T> gridDataSource, List<T> origData) where T : Common.BindableObject
        {
            var result = new List<T>();

            foreach (var item in origData)
            {
                var entity = gridDataSource.FirstOrDefault(p => item.IsEqual(p));
                if (entity == null)
                {
                    result.Add(item);
                    context.Delete(item);
                }
            }

            return result;
        }
    }
}
