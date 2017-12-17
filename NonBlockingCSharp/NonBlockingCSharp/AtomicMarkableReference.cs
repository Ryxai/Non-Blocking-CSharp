using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace NonBlockingCSharp
{
    /// <summary>
    /// This class is modeled after Java's AtomicMarkableReference class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AtomicMarkableReference<T> 
        where T : class
    {
        private T item;
        private AtomicBool mark;

        public T Reference 
        {
            get { return item; }
            set { Interlocked.Exchange(ref item, value); }
        }

        public bool Marked 
        {
            get { return (bool)mark; }
            set { Interlocked.Exchange(ref mark, value);  }
        }

        public AtomicMarkableReference(T initialRef, bool initialMark = false)
        {
            Interlocked.Exchange(ref item, initialRef);
            Interlocked.Exchange(ref mark, initialMark);
        }

        public void Exchange(T newReference, bool newMark)
        {
            Interlocked.Exchange(ref item, newReference);
            Interlocked.Exchange(ref mark, newMark);
        }

        public bool CompareAndExchange(T expectedReference, T newReference,
                                    bool expectedMark, bool newMark)
        {
            return AtomicOperationsUtilities.CompareAndSwap(ref item, expectedReference, newReference) &&
                AtomicOperationsUtilities.CompareAndSwap(ref mark, expectedMark, newMark);
        }
        
        /// <summary>
        /// Attempts to set mark to the new mark if reference is equal to holding object
        /// </summary>
        /// <param name="expectedReference">Expecting reference when the mark is being applied</param>
        /// <param name="newMark">New boolean value to be set</param>
        /// <returns>True if new mark setting is successful</returns>
        public bool AttemptMark(T expectedReference, bool newMark) 
        {
            if (AtomicOperationsUtilities.Compare(ref item, expectedReference))
            {
                Interlocked.Exchange(ref mark, newMark);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
