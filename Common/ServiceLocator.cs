using System;
using System.Collections.Generic;

namespace Common
{
    public class ServiceLocator
    {
        private static volatile ServiceLocator instance;
        private static readonly object syncRoot = new Object();

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
            foreach (var type in typeMapping)
            {
                if (type.Key.IsGenericType)
                {
                    if (type.Key.IsGenericTypeDefinition != type.Value.IsGenericTypeDefinition)
                        throw new ArgumentException("ServiceLocator.Initialize: key type and value type must be the same GenericTypeDefinition or not");

                    else if (type.Key.IsGenericTypeDefinition == false && type.Value.IsGenericTypeDefinition == false)
                    {
                        if (type.Key.IsAssignableFrom(type.Value) == false)
                            throw new ArgumentException(string.Format(
                                "ServiceLocator.Initialize: key type {0} must assignable from value type {1}", type.Key, type.Value));
                    }

                    else if (type.Key.IsGenericTypeDefinition == true && type.Value.IsGenericTypeDefinition == true)
                    {
                        if (IsAssignableToGenericType(type.Value, type.Key) == false)
                            throw new ArgumentException(string.Format(
                                "ServiceLocator.Initialize: key type {0} must assignable from value type {1}", type.Key, type.Value));
                    }
                }
                else
                {
                    if (type.Key.IsAssignableFrom(type.Value) == false)
                        throw new ArgumentException("ServiceLocator.Initialize: key type must assignable from value type");
                }
            }

            _typeMapping = typeMapping;
        }

        public T GetInstance<T>() where T : class
        {
            if (_typeMapping == null)
                throw new InvalidOperationException("ServiceLocator.GetInstance: Need call Initialize method before use.");

            Type type = typeof(T);
            if (_typeMapping.ContainsKey(type) == false)
            {
                if (type.IsGenericType)
                {
                    var genericTypeDefinition = type.GetGenericTypeDefinition();
                    if (_typeMapping.ContainsKey(genericTypeDefinition) == false)
                        throw new ArgumentException(string.Format("ServiceLocator.GetInstance: {0} type not found.", type));

                    var genericArguments = type.GetGenericArguments();
                    var constructed = _typeMapping[genericTypeDefinition].MakeGenericType(genericArguments);
                    return Activator.CreateInstance(constructed) as T;
                }
            }

            return Activator.CreateInstance(_typeMapping[type]) as T;
        }

        private bool IsAssignableToGenericType(Type givenType, Type genericType)
        {
            var interfaceTypes = givenType.GetInterfaces();

            foreach (var it in interfaceTypes)
            {
                if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
                    return true;
            }

            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
                return true;

            Type baseType = givenType.BaseType;
            if (baseType == null) return false;

            return IsAssignableToGenericType(baseType, genericType);
        }
    }
}
