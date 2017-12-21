using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Interop.ExternalCC.HandlersHelper.Contracts;
using Interop.ExternalCC.CrossCutting;

namespace Interop.ExternalCC.HandlersHelper.HelperMethods
{
    public class CacheHelper : ICacheHelper
    {
        /// <summary>
        /// Insert value into the cache using
        /// appropriate name/value pairs
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="o">Item to be cached</param>
        /// <param name="key">Name of item</param>
        public void Add<T>(T o, string key)
        {
            // NOTE: Apply expiration parameters as you see fit.
            // I typically pull from configuration file.

            // In this example, I want an absolute
            // timeout so changes will always be reflected
            // at that time. Hence, the NoSlidingExpiration.
            HttpRuntime.Cache.Insert(
                key,
                o,
                null,
                DateTime.Now.AddDays(AppSettings.Get<int>("CashAddDays")),
                System.Web.Caching.Cache.NoSlidingExpiration);
        }

        /// <summary>
        /// Remove item from cache
        /// </summary>
        /// <param name="key">Name of cached item</param>
        public void Clear(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }

        public void ClearAll()
        {
            var enumerator = HttpRuntime.Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                HttpRuntime.Cache.Remove(enumerator.Key.ToString());
            }
        }

        /// <summary>
        /// Check for item in cache
        /// </summary>
        /// <param name="key">Name of cached item</param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            return HttpRuntime.Cache[key] != null;
        }

        /// <summary>
        /// Retrieve cached item
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="key">Name of cached item</param>
        /// <param name="value">Cached value. Default(T) if item doesn't exist.</param>
        /// <returns>Cached item as type</returns>
        public T Get<T>(string key)
        {
           return (T)HttpRuntime.Cache[key];
        }

        public int GetAllCount()
        {
            return HttpRuntime.Cache.Count;
        }
    }

}
