using System.Collections.Generic;

namespace Common
{
    public static class MyContainer<T> where T : new()
    {
        private static readonly Dictionary<string, T> _container = new Dictionary<string, T>();

        public static T GetInstance(string instanceName)
        {
            if (_container.ContainsKey(instanceName) == false)
                _container.Add(instanceName, new T());

            return _container[instanceName];
        }
    }
}
