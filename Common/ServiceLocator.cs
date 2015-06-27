using System;
using System.Collections.Generic;

namespace Common
{
    public class ServiceLocator
    {
        private static volatile ServiceLocator instance;
        private static readonly object syncRoot = new Object();

        /// <summary>
        /// Need call Initialize static method before use
        /// </summary>
        public static ServiceLocator Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ServiceLocator();
                    }
                }

                return instance;
            }
        }

        private Dictionary<Type, Type> _typeMapping = null;

        public void Initialize(Dictionary<Type, Type> typeMapping)
        {
            _typeMapping = typeMapping;
        }

        public T GetInstance<T>() where T : class
        {
            Type type = typeof(T);
            if (_typeMapping.ContainsKey(type) == false)
                return null;

            return Activator.CreateInstance(_typeMapping[type]) as T;
        }
    }
}
