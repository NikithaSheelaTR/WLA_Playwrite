namespace Framework.Core.Utils.Enums
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class EnumUtils
    {
        /// <summary>
        /// Returns an enum value with the specified enum ID, implementing the specified interface, in the specified package.
        /// The enum ID is the value returned by the name() function.
        /// If enumInterfaceClass is null, it will search all enums regardless of what they implement.
        /// If packagePath is null, it will use the default package (com.trgr.quality).
        /// </summary>
        /// <param name="enumClass">The class of the interface that the desired enum implements.</param>
        /// <param name="packagePath">The path of the package where the desired enum would be.</param>
        /// <param name="enumId">The ID of the enum you want.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>An enum that matches the specified enum ID, implementing the specified interface, in the specified package.</returns>
        /// <exception cref="ArgumentException">Thrown if no enum is found matching the specified enum ID, implementing the specified interface, in the specified package.</exception>
        public static T GetEnumFromId<T>(Type enumClass, string packagePath, string enumId)
        {
            if (enumClass.IsEnum)
            {
                return (T)Enum.Parse(enumClass, enumId);
            }
            else
            {
                foreach (Type currentClass in ReflectionUtils.GetClassesFromInterface(enumClass, packagePath))
                {
                    if (currentClass.IsEnum)
                    {
                        try
                        {
                            Type concreteEnumClass = currentClass;
                            return (T)Enum.Parse(concreteEnumClass, enumId);
                        }
                        catch (Exception)
                        {
                            // Enum value was not found
                        }
                    }
                }
                throw new ArgumentException("No enum could be found corresponding to the enum ID: \'" + enumId + "\'.");
            }
        }


        /// <summary>
        /// Returns an enum value with the specified String value, implementing the specified interface, in the specified package.
        /// The String value is the value returned by the toString() function.
        /// If enumInterfaceClass is null, it will search all enums regardless of what they implement.
        /// If packagePath is null, it will use the default package (com.trgr.quality).
        /// </summary>
        /// <param name="enumClass">The class of the interface that the desired enum implements.</param>
        /// <param name="packagePath">The path of the package where the desired enum would be.</param>
        /// <param name="enumString">The String value of the enum you want.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>An enum that matches the specified String value, implementing the specified interface, in the specified package.</returns>
        /// <exception cref="ArgumentException">Thrown if no enum is found matching the specified String value, implementing the specified interface, in the specified package.</exception>
        public static T GetEnumFromString<T>(Type enumClass, string packagePath, string enumString)
        {
            if (enumClass.IsEnum)
            {

                foreach (var value in enumClass.GetEnumNames())
                {
                    string valueString = value.ToString();
                    if (valueString.Equals(enumString))
                    {
                        return (T)Convert.ChangeType(valueString, enumClass);
                    }
                }
                throw new ArgumentException("No enum could be found corresponding to the string value: \'" + enumString + "\'.");
            }
            else
            {
                foreach (Type currentClass in ReflectionUtils.GetClassesFromInterface(enumClass, packagePath))
                {
                    if (currentClass.IsEnum)
                    {
                        Type concreteEnumClass = currentClass;
                        foreach (var value in concreteEnumClass.GetEnumNames())
                        {
                            string valueString = value;
                            if (valueString.Equals(enumString))
                            {
                                return (T)Convert.ChangeType(valueString, concreteEnumClass);
                            }
                        }
                    }
                    else
                    {
                        foreach (var field in currentClass.GetFields())
                        {
                            if (field.Name.Equals(enumString))
                            {
                                return (T)field.GetValue(null);
                            }
                            else if (field.GetValue(null).ToString().Equals(enumString))
                            {
                                return (T)field.GetValue(null);
                            }


                        }
                    }
                }
                throw new ArgumentException("No enum could be found corresponding to the string value: \'" + enumString + "\'.");
            }
        }
    }
}
