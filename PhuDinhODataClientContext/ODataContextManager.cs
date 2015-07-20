using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Linq.Expressions;
using Common;
using PhuDinhCommon;

namespace PhuDinhODataClientContext
{
    public sealed class ODataContextManager<T> : IClientContextManager<T> where T : Common.BindableObject
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

        public List<T1> GetData<T1>(Expression<Func<T1, bool>> filter) where T1 : Common.BindableObject
        {
            var result = new List<T1>();
            var query = _context.Set<T1>().Where(filter) as DataServiceQuery<T1>;
            query = Repository.RepositoryLocator<T1>.GetDataQuery(query) as DataServiceQuery<T1>;//add order, expand
            var respone = query.Execute() as QueryOperationResponse<T1>;

            do
            {
                foreach (var entity in respone)
                {
                    result.Add(entity);
                }

                if (result.Count >= 1000)
                    break;

                DataServiceQueryContinuation<T1> token = respone.GetContinuation();
                if (token == null)
                    break;

                respone = _context.Execute(token);

            } while (true);

            return result;
        }

        public List<T> GetData(Expression<Func<T, bool>> filter, int pageSize, int currentPageIndex, int itemCount)
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

        public void Save(List<T> data, List<T> originalData)
        {
            RemoveItem(_context, data, originalData);
            AddNewItem(_context, data);
            _context.SaveChanges();
        }

        public int GetDataCount(Expression<Func<T, bool>> filter)
        {
            var query = _context.Set<T>().IncludeTotalCount().Where(filter).Skip(0).Take(1) as DataServiceQuery;//skip(0).Take(1) because just need get TotalCount
            var response = query.Execute() as QueryOperationResponse<T>;

            return (int)response.TotalCount;
        }

        public void ReloadEntity(T entity)
        {
            _context.ReloadEntity(entity, "Ma");
        }

        public List<T1> LoadEntityWithRelated<T1>(Expression<Func<T1, bool>> filter, List<string> related) where T1 : BindableObject
        {
            throw new NotImplementedException();
        }

        private static List<T> AddNewItem(DataServiceContextEx context, List<T> gridDataSource)
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

        private static List<T> RemoveItem(DataServiceContextEx context, List<T> gridDataSource, List<T> origData)
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
