using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NonBlockingCSharp.Utilities
{
    public static class AtomicOperationsUtilities
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="anotherObject"></param>
        /// <returns></returns>
        public static bool Compare<T>(ref T obj, T anotherObject)
            where T : class
        {
            return (Interlocked.CompareExchange(ref obj, anotherObject, anotherObject) == anotherObject);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="destination"></param>
        /// <param name="currentValue"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public static bool  CompareAndSwap<T>(ref T destination, T currentValue, T newValue)
            where T : class
        {
            if (currentValue == Interlocked.CompareExchange<T>(ref destination, newValue, currentValue))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
