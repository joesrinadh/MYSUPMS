using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;

namespace SUPMS.Infrastructure.Utilities
{
    /// <summary>
    /// Cache Manager Class
    /// </summary>
    public sealed class CacheManager
    {
        private static readonly object lockObj = new object();

        /// <summary>
        /// Private Constructor to prevent instantiation
        /// </summary>
        private CacheManager()
        {

        }

        /// <summary>
        /// Static Property to implement Singleton
        /// </summary>
        public static CacheManager Instance
        {
            get
            {
                lock (lockObj)
                {
                    return Nested.instance;
                }
            }
        }

        /// <summary>
        /// Method to add key and data to the Cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public void Add(string key, object data)
        {
            lock (lockObj)
            {
                if (!(IsExists(key)))
                    HttpContext.Current.Cache.Insert(key.ToUpper(), data);
                else
                    HttpContext.Current.Cache[key] = data;
            }
        }

        /// <summary>
        /// Method to remove key from the Cache
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            lock (lockObj)
            {
                if (IsExists(key.ToUpper()))
                    HttpContext.Current.Cache.Remove(key.ToUpper());
            }
        }

        /// <summary>
        /// Method to check if key exists in the Cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Boolean IsExists(string key)
        {
            if (HttpContext.Current.Cache[key.ToUpper()] != null)
                return true;
            return false;
        }



        /// <summary>
        /// Method to get data as object from Cache based on a key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object Get(string key)
        {
            if (IsExists(key.ToUpper()))
                return HttpContext.Current.Cache[key.ToUpper()];
            return null;
        }


        /// <summary>
        /// Method to get data as object from Cache based on a key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetResource(string key)
        {
            if (IsExists("RESOURCEKEY"))
            {
                Dictionary<string, string> objResourcekey =  GetAllResourcekeys();
                if (objResourcekey.ContainsKey(key.ToUpper()))
                {
                    return objResourcekey[key.ToUpper()];
                }
            }
            return null;
        }

        public void AddResourceList(Dictionary<string, string> objlist)
        {
            this.Add("RESOURCEKEY", objlist);
        }

        /// <summary>
        /// Removes items by pattern
        /// </summary>
        /// <param name="pattern">pattern</param>
        public void RemoveResourceByPattern(string pattern)
        {
            IDictionary items = GetAllResourcekeys();
            if (items == null)
                return;

            var enumerator = items.GetEnumerator();
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = new List<String>();
            while (enumerator.MoveNext())
            {
                if (regex.IsMatch(enumerator.Key.ToString().ToUpper()))
                {
                    keysToRemove.Add(enumerator.Key.ToString().ToUpper());
                }
            }
        }

        private Dictionary<string, string> GetAllResourcekeys()
        {
            Dictionary<string, string> dic = (Dictionary<string, string>)HttpContext.Current.Cache["RESOURCEKEY"];
            return dic;
        }



        /// <summary>
        /// Method to return the number of keys in the Cache
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            if (HttpContext.Current.Cache != null)
                return HttpContext.Current.Cache.Count;
            return -1;
        }



        /// <summary>
        /// 
        /// </summary>
        private class Nested
        {
            // Explicit static constructor to inform the C# compiler not to mark type as beforefieldinit

            static Nested()
            {

            }

            internal static readonly CacheManager instance = new CacheManager();
        }
    }
}
