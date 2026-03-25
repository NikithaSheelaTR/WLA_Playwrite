namespace Framework.Core.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// 
    /// </summary>
    public class ReflectionUtils
    {

        /// <summary>
        /// Returns the class of the specified object. This is primarily used for generic classes where .getClass() doesn't work.
        /// </summary>
        /// <param name="obj">The object you want the class of.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>The class of the specified object.</returns>
        public static Type GetGenericClass<T>(T obj)
        {
            return obj.GetType();
        }

        /// <summary>
        /// Returns the class of the objects in the specified array. This is primarily used for generic classes where .getClass() doesn't work.
        /// If the array is empty, this will return null.
        /// </summary>
        /// <param name="objectArray">The object array that you want the class of.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>The class of the elements in the specified object array.</returns>
        public static Type GetGenericClass<T>(T[] objectArray)
        {
            if (objectArray.Length > 0)
            {
                return objectArray[0].GetType();
            }
            return null;
        }

        /// <summary>
        /// Returns the class of the objects in the specified Collection. This is primarily used for generic classes where .getClass() doesn't work.
        /// If the List is empty, this will return null.
        /// </summary>
        /// <param name="objectCollection">The object Collection that you want the class of.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>The class of the elements in the specified object Collection.</returns>
        public static Type GetGenericClass<T>(Collection<T> objectCollection)
        {
            if (objectCollection.Count > 0)
            {
                return (objectCollection.GetEnumerator().Current.GetType());
            }
            return null;
        }

        /// <summary>
        /// Returns a list of classes that extend from the specified class in the specified package.
        /// </summary>
        /// <param name="clazz">The class that you want the subclasses of.</param>
        /// <param name="packageString">The package that should be searched in.</param>
        /// <returns>A list of classes that extend from the specified class in the specified package.</returns>
        public static List<Type> GetDeclaredSubClasses(Type clazz, string packageString = "")
        {
            List<Type> subclasses = new List<Type>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                try
                {
                    var types = assembly.GetTypes();

                    foreach (var type in types)
                    {
                        if (type.GetInterfaces().Contains(clazz))
                        {
                            subclasses.Add(type);
                        }
                    }
                }
                catch (Exception)
                {
                    //Console.Error.WriteLine("Unable to get type from assembly: " + assembly.FullName);
                    //Console.Error.WriteLine(e.InnerException);

                }
            }
            return subclasses;
        }

        /// <summary>
        /// Uses reflection to return a list of Field objects that are inherited by the specified class.
        /// </summary>
        /// <param name="clazz">The class that you want to inherited Fields for.</param>
        /// <returns> A List of Fields that are inherited by the specified class.</returns>
        public static List<FieldInfo> GetInheritedFields(Type clazz)
        {
            List<FieldInfo> fields = new List<FieldInfo>();
            for (Type c = clazz; c != null; c = c.BaseType)
            {
                fields.AddRange(new List<FieldInfo>(c.GetFields()));
            }
            return fields;
        }

        /// <summary>
        /// Uses reflection to return the Field object corresponding to the specified name within the specified class.
        /// </summary>
        /// <param name="clazz">The class that you're getting the field for.</param>
        /// <param name="fieldName">The name of the field you want.</param>
        /// <returns>The desired Field object corresponding to the specified name and class.</returns>
        /// <exception cref="Exception">Thrown if no Field with the specified name exists in the specified class or its superclasses.</exception>
        public static FieldInfo GetField(Type clazz, string fieldName)
        {
            try
            {
                return clazz.GetField(fieldName);
            }
            catch (Exception e)
            {
                Type superClass = clazz.BaseType;
                if (superClass == null)
                {
                    throw e;
                }
                else
                {
                    return ReflectionUtils.GetField(superClass, fieldName);
                }
            }
        }

        /// <summary>
        /// Executes the specified method, within the specified class given the specified method parameters.
        /// </summary>
        /// <param name="returnClass">The class of the object being returned.</param>
        /// <param name="methodName">The name of the method you want to call.</param>
        /// <param name="targetClass">The Class object containing the method you want to call.</param>
        /// <param name="parameters">The parameters for the class you want to call.</param>
        /// <typeparam name="R"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Thrown if no valid method is found given the specified values.</exception>
        public static R Invoke<R>(Type returnClass, string methodName, Type targetClass, params Object[] parameters)
        {
            try
            {
                var classMethods = targetClass.GetMethods();
                foreach (var method in classMethods)
                {
                    if (!method.GetBaseDefinition().Name.Equals(methodName))
                    {
                        continue;
                    }

                    ParameterInfo[] parameterInfos = method.GetParameters();
                    if (parameterInfos.Length == parameters.Length)
                    {
                        bool matches = true;
                        for (int i = 0; i < parameterInfos.Length; i++)
                        {
                            if (!parameterInfos[i].ParameterType.IsInstanceOfType(parameters[i]) && parameters[i]!=null)
                            {
                                matches = false;
                                break;
                            }
                        }
                        if (matches)
                        {
                            return (R) method.Invoke(targetClass, parameters);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Exception occurred
                Console.Out.WriteLine(ex);
            }

            // If no matching method is found and the class has a super class, check for the method there
            if (targetClass.BaseType != null)
            {
                return ReflectionUtils.Invoke<R>(returnClass, methodName, targetClass.BaseType, parameters);
            }

            throw new ArgumentException("Could not invoke the " + methodName + " method in the " +
                                        targetClass.ToString() + " class with the specified parameters.");
        }

        /// <summary>
        /// Checks to see if the specified class is a primitive class or a primitive wrapper class.
        /// </summary>
        /// <param name="clazz">The class being checked.</param>
        /// <returns>True if the specified class is a primitive class or a primitive wrapper class and flase if not.</returns>
        public static bool IsPrimitiveClass(Type clazz)
        {
            // First check if the class is primitive
            if (clazz.IsPrimitive)
            {
                return true;
            }

            // If it's not, check for the primitive wrapper classes
            var wrappers = new HashSet<Type>
            {
                typeof (Boolean),
                typeof (Char),
                typeof (Byte),
                typeof (Int64),
                typeof (Int16),
                typeof (Int32),
                typeof (Double),
                typeof (void)
            };
            return wrappers.Contains(clazz);
        }

        /// <summary>
        /// Returns a List of classes that implement the specified interface class within the specified package.
        /// </summary>
        /// <param name="interfaceClass">The class of the interface that the desired classes should implement.</param>
        /// <param name="packagePath">The path of the package where the desired classes should be.</param>
        /// <returns>A List of classes that implement the specified interface class within the specified package.</returns>
        public static List<Type> GetClassesFromInterface(Type interfaceClass, string packagePath = null)
        {

            List<Type> classes = new List<Type>();
            foreach (var currentClass in ReflectionUtils.GetDeclaredSubClasses(interfaceClass))
            {
                List<Type> interfaces = new List<Type>(currentClass.GetInterfaces());
                if ((interfaceClass == null) || interfaces.Contains(interfaceClass))
                {
                    classes.Add(currentClass);
                }
            }
            classes.Add(interfaceClass);
            return classes;
        }

    }
}
