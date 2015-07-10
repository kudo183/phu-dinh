using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Data.Services.Common;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Data.Edm;
using Microsoft.Data.OData;

namespace Common
{
    public class DataServiceContextEx : DataServiceContext
    {
        /// <summary>Constructor taking the service root in parameter.</summary>
        /// <param name="serviceRoot"></param>
        /// <param name="edmModel"></param>
        public DataServiceContextEx(Uri serviceRoot, IEdmModel edmModel)
            : base(serviceRoot, DataServiceProtocolVersion.V3)
        {
            Format.UseJson(edmModel);

            Configurations.RequestPipeline.OnEntryEnding((a =>
            {
                var properties = a.Entry.Properties as List<ODataProperty>;
                var attIgnoredProperties = a.Entity.GetType().GetCustomAttributes(typeof(System.Data.Services.IgnorePropertiesAttribute), true);
                var ignoredProperties = (attIgnoredProperties[0] as System.Data.Services.IgnorePropertiesAttribute).PropertyNames;
                properties.RemoveAll(p => ignoredProperties.Contains(p.Name));
                a.Entry.Properties = properties;
            }));
        }

        public DataServiceQuery<T> Set<T>() where T : Common.BindableObject
        {
            return CreateQuery<T>(typeof(T).Name + "s");
        }

        public void Add<T>(T entity) where T : Common.BindableObject
        {
            AddObject(typeof(T).Name + "s", entity);
        }

        public void Delete<T>(T entity) where T : Common.BindableObject
        {
            DeleteObject(entity);
        }

        public void Update<T>(T entity) where T : Common.BindableObject
        {
            UpdateObject(entity);
        }

        public void ReloadEntity<T>(T entity, string entityKeyPropertyName) where T : Common.BindableObject
        {
            var pe = Expression.Parameter(typeof(T), "p");
            var left = Expression.Property(pe, entityKeyPropertyName);
            var right = Expression.Constant(entity.GetKey());
            var predicateBody = Expression.Equal(left, right);

            var expression = Expression.Lambda<Func<T, bool>>(predicateBody, new[] { pe });
            var temp = MergeOption;
            MergeOption = MergeOption.OverwriteChanges;
            Set<T>().Where(expression).First();
            MergeOption = temp;
        }
    }
}
