using System;
using System.Reflection;

namespace PhuDinhCommon
{
    public class Singleton<T>
    {
        delegate T Instantiator();
        delegate void Seter(T value);

        static volatile Instantiator instantiator = new Instantiator(Instantiate_SubstituteInstantiator_Get);
        static Seter seter = new Seter(SetValue);
        static T t;

        /// <summary>
        /// For cases where constructor has couple params and need for manual initialization.
        /// </summary>
        /// <param name="value">Instance</param>
        static void SetValue(T value)
        {
            if (t == null)
            {
                lock (instantiator)
                {
                    if (t == null)
                    {
                        t = value;
                        instantiator = new Instantiator(ReturnValue);
                        seter = new Seter(SeterUnavailable);
                    }
                }
            }
        }

        /// <summary>
        /// Instantiates object on first use.
        /// </summary>
        /// <returns></returns>
        static T Instantiate_SubstituteInstantiator_Get()
        {
            ConstructorInfo constructor = null;

            // Binding flags exclude public constructors.
            constructor = typeof(T).GetConstructor(BindingFlags.Instance |
                          BindingFlags.NonPublic | BindingFlags.Public, null, new Type[0], null);

            if (constructor == null || constructor.IsAssembly)
            {
                // Also exclude internal constructors.
                throw new Exception(string.Format("A private or " +
                      "protected constructor is missing for '{0}'.", typeof(T).Name));
            }

            SetValue(
                (T)constructor.Invoke(null)
                );

            return t;
        }

        static T ReturnValue()
        {
            return t;
        }

        static void SeterUnavailable(T value)
        {
            //Debug.Assert(false, "The singleton is already instantiated.");
            t = default(T);
            SetValue(value);
        }

        public static T Instance
        {
            get
            { return instantiator(); }
            set
            { seter(value); }
        }

        /// <summary>
        /// Instantiates the singleton
        /// </summary>
        public static void Touch()
        {
            instantiator();
        }
    }
}
