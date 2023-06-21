using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.Common
{
    public abstract class SingletonObject<T> where T : class, new()
    {
        protected static object _lock = new object();
        protected static T _instance = null;
        public static bool IsNotNull
        {
            get { return _instance != null; }
        }

        public static T Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new T();
                    }
                    return _instance;
                }
            }
        }

        /// <summary>
        /// this call if want to reset singleton
        /// </summary>
        public static void ResetSingleton()
        {
            _instance = null;
        }

        protected SingletonObject()
        {
            if (_instance == null)
                _instance = this as T;
        }
    }
}
