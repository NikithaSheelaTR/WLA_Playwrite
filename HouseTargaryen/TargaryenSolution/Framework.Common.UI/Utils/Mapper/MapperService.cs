namespace Framework.Common.UI.Utils.Mapper
{
    using System;
    using System.Reflection;

    using AutoMapper;

    /// <summary>
    /// The Auto Mapper Wrapper Service
    /// </summary>
    public class MapperService
    {
        /// <summary>
        /// The instance.
        /// </summary>
        private static IMapper instance;

        /// <summary>
        /// Creating Mapper Instance using all profiles from currect assembly
        /// </summary>
        private static IMapper Instance =>
            instance = instance ?? new MapperConfiguration(cfg => cfg.AddProfiles(Assembly.GetExecutingAssembly()))
                           .CreateMapper();

        /// <summary>
        /// Execute a mapping from the source object to a new destination object.
        /// The source type is inferred from the source object.
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TDestination Map<TDestination>(object source) => Instance.Map<TDestination>(source);

        /// <summary>
        /// Execute a mapping from the source object to a new destination object with supplied mapping options.
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="opts"></param>
        /// <returns></returns>
        public static TDestination Map<TDestination>(object source, Action<IMappingOperationOptions> opts) =>
            Instance.Map<TDestination>(source, opts);

        /// <summary>
        /// Execute a mapping from the source object to a new destination object.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TDestination Map<TSource, TDestination>(TSource source) =>
            Instance.Map<TSource, TDestination>(source);

        /// <summary>
        /// Execute a mapping from the source object to a new destination object with supplied mapping options.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="opts"></param>
        /// <returns></returns>
        public static TDestination Map<TSource, TDestination>(
            TSource source,
            Action<IMappingOperationOptions<TSource, TDestination>> opts) =>
            Instance.Map(source, opts);

        /// <summary>
        /// Execute a mapping from the source object to the existing destination object.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination) =>
            Instance.Map(source, destination);

        /// <summary>
        /// Execute a mapping from the source object to the existing destination object with supplied mapping options.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="opts"></param>
        /// <returns></returns>
        public static TDestination Map<TSource, TDestination>(
            TSource source,
            TDestination destination,
            Action<IMappingOperationOptions<TSource, TDestination>> opts) =>
            Instance.Map(source, destination, opts);

        /// <summary>
        ///  Execute a mapping from the source object to a new destination object with explicit <see cref="System.Type" /> objects
        /// </summary>
        /// <param name="source"></param>
        /// <param name="sourceType"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public static object Map(object source, Type sourceType, Type destinationType) =>
            Instance.Map(source, sourceType, destinationType);

        /// <summary>
        /// Execute a mapping from the source object to a new destination object with explicit <see cref="System.Type" /> objects and supplied mapping options.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="sourceType"></param>
        /// <param name="destinationType"></param>
        /// <param name="opts"></param>
        /// <returns></returns>
        public static object Map(
            object source,
            Type sourceType,
            Type destinationType,
            Action<IMappingOperationOptions> opts) =>
            Instance.Map(source, sourceType, destinationType, opts);

        /// <summary>
        /// Execute a mapping from the source object to existing destination object with explicit <see cref="System.Type" /> objects
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="sourceType"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public static object Map(object source, object destination, Type sourceType, Type destinationType) =>
            Instance.Map(source, destination, sourceType, destinationType);

        /// <summary>
        /// Execute a mapping from the source object to existing destination object with supplied mapping options and explicit <see cref="System.Type" /> objects
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="sourceType"></param>
        /// <param name="destinationType"></param>
        /// <param name="opts"></param>
        /// <returns></returns>
        public static object Map(
            object source,
            object destination,
            Type sourceType,
            Type destinationType,
            Action<IMappingOperationOptions> opts) =>
            Instance.Map(source, destination, sourceType, destinationType, opts);
    }
}