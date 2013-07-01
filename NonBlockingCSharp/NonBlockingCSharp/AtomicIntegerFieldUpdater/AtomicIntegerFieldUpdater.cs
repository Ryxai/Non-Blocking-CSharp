using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NonBlockingCSharp.AtomicIntegerUpdater
{
    public abstract class AtomicIntegerUpdater<T>
    {
        protected PropertyInfo property;
        protected FieldInfo field;
        

        public abstract int AddAndGet();

        public abstract int CompareAndSet();


        public static AtomicIntegerUpdater<T> 	NewUpdater(Type type, string name)
        {
            Contract.Requires<ArgumentNullException>(type != null);
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(name));
        
            var field = type.GetField(name);
            var property = type.GetProperty(name);
            MemberInfo member;

            if(field != null)
            {
                member = field;
            }
            else if(property != null)
            {
                member = property;
            }
            else
            {
                throw new ArgumentException("This type has no property of field named " + name);
            }

            if(member.CanWrite())
            {

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

     
        private sealed class AtomicIntegerFieldUpdater<T>
            : AtomicIntegerUpdater<T>
        {
            public AtomicIntegerFieldUpdater (FieldInfo field)
            {
                Contract.Requires<ArgumentNullException>(field != null);
            
            }    
        }
    
        
        private sealed class AtomicIntegerPropertyUpdater<T>
            : AtomicIntegerUpdater<
        {
            public AtomicIntegerPropertyUpdater (PropertyInfo property)
            {
                Contract.Requires<ArgumentNullException>(property != null);

            }
        }
    }
}
