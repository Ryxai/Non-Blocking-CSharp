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
        private bool item;

        public AtomicBool(bool value) 
        {
            item = value;
        }

        /// <summary>
        /// Sets to new value
        /// </summary>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public bool GetAndSet(bool newValue) 
        {
            bool temp = item;
            item = newValue;
            return temp;
        }

        public static bool operator |(AtomicBool atomicBool, bool b)
        {
            return atomicBool.item || b;
        }

        public static bool operator &(AtomicBool atomicBool, bool b)
        {
            return atomicBool.item && b;
        }

        public static bool operator !(AtomicBool b)
        {
            return !b.item;
        }

        public static implicit operator bool(AtomicBool b)
        {
            return b.item;
        } 
    }
}
