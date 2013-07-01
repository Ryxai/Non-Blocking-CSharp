using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NonBlockingCSharp.AtomicBool
{
    [Serializable]
    public class AtomicBool
    {
        private static object TrueObject = true;
        private static object FalseObject = false;

        private object item;

        public AtomicBool(bool value) 
        {
            Interlocked.Exchange(ref item, value);
        }

        /// <summary>
        /// Sets to new value
        /// </summary>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public bool GetAndSet(bool newValue) 
        {
            object temp = item;
            Interlocked.Exchange(ref item, newValue);
            return (bool)temp;
        }

        public bool CompareAndSet(bool expectedValue, bool updatedValue) 
        {
            object expectedValueObject = expectedValue;
            object updatedValueObject = updatedValue;
            return (Interlocked.CompareExchange(ref item, updatedValueObject, expectedValueObject) == expectedValueObject);
        }

        public static bool operator |(AtomicBool atomicBool, bool b)
        {
            return ((bool)atomicBool.item) || b;
        }

        public static bool operator &(AtomicBool atomicBool, bool b)
        {
            return ((bool)atomicBool.item) && b;
        }

        public static bool operator !(AtomicBool b)
        {
            return !(bool)b.item;
        }

        public static implicit operator AtomicBool(bool b)
        {
            return new AtomicBool(b);
        }

        public static implicit operator bool(AtomicBool b)
        {
            return (bool)b.item;
        } 
    }
}
