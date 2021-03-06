﻿using System.Threading;

namespace NonBlockingCSharp
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
            return currentValue == Interlocked.CompareExchange<T>(ref destination, newValue, currentValue);
        }
    }
}
