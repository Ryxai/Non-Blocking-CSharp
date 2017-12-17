using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NonBlockingCSharp
{
    public abstract class AtomicUpdater<T, V>
        where T : class
        where V : class
    {
        public abstract T CompareAndSet(T obj, V expect, V update);

        /// <summary>
        /// Atomically sets the field of the given object managed by this 
        /// updater to the given value and returns the old value.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="expect"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public abstract T GetAndSet(T obj, V update);

        public static AtomicUpdater<T, V> NewUpdater(Type type, string name)
        {
            Contract.Requires<ArgumentNullException>(type != null);
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(name));

            var field = type.GetField(name);
            var property = type.GetProperty(name);

            if (field != null)
            {
                if (field.FieldType != typeof(V))
                {
                    throw new ArgumentException("The field  " + name + " is not an integer");
                }
                if (field.IsInitOnly)
                {
                    throw new ArgumentException("The field  " + name + " is not writeable");
                }
                return new AtomicFieldUpdater<T, V>(field);
            }
            else if (property != null)
            {
                if (property.PropertyType != typeof(V))
                {
                    throw new ArgumentException("The property  " + name + " is not of type");
                }
                if (!property.CanWrite)
                {
                    throw new ArgumentException("The property  " + name + " is not writeable");
                }
                return new AtomicPropertyUpdater<T, V>(property);
            }
            else
            {
                throw new ArgumentException("This type has no property of field named " + name);
            }
        }

        /// <summary>
        /// Get all the fields of a class
        /// </summary>
        /// <param name="type">Type object of that class</param>
        /// <returns></returns>
        public static IEnumerable<FieldInfo> GetAllFields(this Type type)
        {
            if (type == null)
            {
                return Enumerable.Empty<FieldInfo>();
            }

            BindingFlags flags = BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Static |
            BindingFlags.Instance |
            BindingFlags.DeclaredOnly;
            return type.GetFields(flags).Union(GetAllFields(type.BaseType));
        }

        /// <summary>
        /// Get all the fields of a class
        /// </summary>
        /// <param name="type">Type object of that class</param>
        /// <returns></returns>
        private static FieldInfo GetField(this Type type, string fieldName)
        {
            Contract.Requires<ArgumentNullException>(type != null);
            Contract.Requires<ArgumentException>(string.IsNullOrWhiteSpace(fieldName));

            BindingFlags flags = BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Static |
            BindingFlags.Instance |
            BindingFlags.DeclaredOnly;

            return type.GetFields(flags)
                       .Union(GetAllFields(type.BaseType))
                       .Where(field => (field.Name == fieldName))
                       .SingleOrDefault();
        }

        /// <summary>
        /// Get all properties of a class
        /// </summary>
        /// <param name="type">Type object of that class</param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetAllProperties(this Type type)
        {
            if (type == null)
            {
                return Enumerable.Empty<PropertyInfo>();
            }
            BindingFlags flags = BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Static |
            BindingFlags.Instance |
            BindingFlags.DeclaredOnly;

            return type.GetProperties(flags).Union(GetAllProperties(type.BaseType));
        }

        /// <summary>
        /// Get all properties of a class
        /// </summary>
        /// <param name="type">Type object of that class</param>
        /// <returns></returns>
        private static PropertyInfo GetPropery(this Type type, string propertyName)
        {
            Contract.Requires<ArgumentNullException>(type != null);
            Contract.Requires<ArgumentException>(string.IsNullOrWhiteSpace(propertyName));

            BindingFlags flags = BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Static |
            BindingFlags.Instance |
            BindingFlags.DeclaredOnly;

            return type.GetProperties(flags)
                       .Union(GetAllProperties(type.BaseType))
                       .Where(property => (property.Name == propertyName))
                       .SingleOrDefault();
        }


        private sealed class AtomicFieldUpdater<T, V>
            : AtomicUpdater<T, V>
        {
            private FieldInfo field;

            public AtomicFieldUpdater(FieldInfo field)
            {
                Contract.Requires<ArgumentNullException>(field != null);

                this.field = field;
            }
            /*
            public T AddAndGet(T obj)
            {

            }
            */
            public T CompareAndSet(T obj, V expect, V update)
            {
                throw new NotImplementedException();
                //V currentValue;
                //do
                //{
                //    currentValue = (V)field.GetValue(obj);
                //} while()

            }

            //public T DecrementAndGet(T obj)
            //{

            //}

            //public T GetAndAdd(T obj)
            //{

            //}

            //public T GetAndDecrement(T obj)
            //{

            //}

            //public T GetAndIncrement(T obj)
            //{

            //}

            public T GetAndSet(T obj, V update)
            {
                throw new NotImplementedException();
            }

            //public int IncrementAndGet(T obj);
        }


        private sealed class AtomicPropertyUpdater<T, V>
            : AtomicUpdater<T, V>
        {
            protected PropertyInfo property;

            public AtomicPropertyUpdater(PropertyInfo property)
            {
                Contract.Requires<ArgumentNullException>(property != null);

                this.property = property;
            }

            public T CompareAndSet(T obj, V expect, V update)
            {
                throw new NotImplementedException();
 
            }

            public T GetAndSet(T obj, V update)
            {
                throw new NotImplementedException();
 
            }
        }
    }
}
