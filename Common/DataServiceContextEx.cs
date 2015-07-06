using System;
using System.Data.Services.Client;
using System.Data.Services.Common;
using System.Xml.Linq;
using Microsoft.Data.Edm;

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
            //WritingEntity += DataModelProxy_WritingEntity;
        }

        void DataModelProxy_WritingEntity(object sender, ReadingWritingEntityEventArgs e)
        {
            RemoveIgnoredProperties(ref e);
        }

        private static void RemoveIgnoredProperties(ref ReadingWritingEntityEventArgs e)
        {
            var attIgnoredProperties = e.Entity.GetType().GetCustomAttributes(typeof(System.Data.Services.IgnorePropertiesAttribute), true);
            if (attIgnoredProperties.Length != 1)
                return;

            var ignoredProperties = (attIgnoredProperties[0] as System.Data.Services.IgnorePropertiesAttribute).PropertyNames;

            foreach (XElement node in e.Data.Elements())
            {
                if (node != null && node.Name.LocalName == "content")
                {
                    foreach (XElement el in node.Elements())
                    {
                        if (el.Name.LocalName == "properties")
                        {
                            foreach (XElement prop in el.Elements())
                            {
                                if (ignoredProperties.Contains(prop.Name.LocalName) == true)
                                    prop.Remove();
                            }
                        }
                    }
                }
            }
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
    }
}
