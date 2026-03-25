namespace Framework.Core.DataModel.Security
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.DataModel.Security.Specialized;
    using Framework.Core.Utils;

    /// <summary>
    /// The credential pool.
    /// </summary>
    public sealed class CredentialPool
    {
        private static readonly ConcurrentDictionary<int, List<IUserInfo>> UserDictionary = new ConcurrentDictionary<int, List<IUserInfo>>();

        private static readonly object Locker = new object();

        /// <summary>
        /// This mapper provides the desired types of users from the UserDictionary,
        /// as it contains different objects which implement IUserInfo and some mapping can be required
        /// </summary>
        private static readonly Dictionary<Type, Func<IEnumerable<IUserInfo>>> UserInfoRetriverMap =
            new Dictionary<Type, Func<IEnumerable<IUserInfo>>>
            {
                [typeof(IUserInfo)] = () => UserDictionary[Thread.CurrentThread.ManagedThreadId],
                [typeof(IUserCredential)] = () => UserDictionary[Thread.CurrentThread.ManagedThreadId],

                [typeof(OnePassUserInfo)] = () => UserDictionary[Thread.CurrentThread.ManagedThreadId].OfType<IUserCredential>().Select(user => user.ToOnePassUserInfo()),

                [typeof(IOnePassUserInfo)] = () => UserDictionary[Thread.CurrentThread.ManagedThreadId].OfType<IOnePassUserInfo>(),
                [typeof(UserCredential)] = () => UserDictionary[Thread.CurrentThread.ManagedThreadId].OfType<UserCredential>(),
                [typeof(UserDbCredential)] = () => UserDictionary[Thread.CurrentThread.ManagedThreadId].OfType<UserDbCredential>(),

                [typeof(WlnUserInfo)] = () => UserDictionary[Thread.CurrentThread.ManagedThreadId].OfType<UserCredential>().Select(u => u.ToWlnUserInfo())
                                                     .Union(UserDictionary[Thread.CurrentThread.ManagedThreadId].OfType<UserDbCredential>().Select(u => u.ToWlnUserInfo()))
            };

        /// <summary>
        /// Disposes of all users in the pool.
        /// </summary>
        public static void DisposeOfUsers()
        {
            lock (Locker)
            {
                if (UserDictionary.ContainsKey(Thread.CurrentThread.ManagedThreadId))
                {
                    foreach (IUserInfo user in UserDictionary[Thread.CurrentThread.ManagedThreadId])
                    {
                        var disposableUser = user as IDisposable;

                        Logger.LogInfo($"Disposing of '{user?.UserName}'...");
                        disposableUser?.Dispose();
                    }
                }
            }

            CredentialPool.RemoveDisposedInstances();
        }

        /// <summary>
        /// Returns all users registered in the credential pool for the current thread
        /// </summary>
        /// <returns> List of users </returns>
        public static IEnumerable<IUserInfo> GetAllRegisteredUsers()
        {
            List<IUserInfo> users;
            UserDictionary.TryGetValue(Thread.CurrentThread.ManagedThreadId, out users);
            return users;
        }

        /// <summary>
        /// Return all registered in credential pool users 
        /// </summary>
        /// <typeparam name="TUserInfo">The type of user context.</typeparam>
        /// <returns>
        /// Collection iterator for the all registered in the credential pool users by required type
        /// </returns>
        public static IEnumerable<TUserInfo> GetAllRegisteredUsers<TUserInfo>() where TUserInfo : class
        {
            Type userInfoType = typeof(TUserInfo);
            CredentialPool.AssertCompatibleAccount(userInfoType);

            IEnumerable<TUserInfo> users = new List<TUserInfo>();

            lock (Locker)
            {
                if (UserDictionary.ContainsKey(Thread.CurrentThread.ManagedThreadId))
                {
                    users = UserInfoRetriverMap[userInfoType]().OfType<TUserInfo>();
                }
            }

            return users;
        }

        /// <summary>
        /// Returns the first user registered in the credential pool for the current thread
        /// </summary>
        /// <param name="predicate">
        /// Function that encapsulate logic for retrieving info about specific user 
        /// </param>
        /// <returns>User info by required type</returns>
        public static IUserInfo GetFirstOrDefaultUser(Func<IUserInfo, bool> predicate = null)
            => UserDictionary[Thread.CurrentThread.ManagedThreadId].FirstOrDefault(predicate ?? (user => true));

        /// <summary>
        /// Returns the first user registered in the credential pool that satisfies the specified condition.
        /// </summary>
        /// <typeparam name="TUserInfo">The type of user context.</typeparam>
        /// <param name="predicate">Function that encapsulate logic for retrieving info about specific user.</param>
        /// <returns>User info.</returns>
        public static TUserInfo GetFirstOrDefaultUser<TUserInfo>(Func<TUserInfo, bool> predicate = null)
            where TUserInfo : class =>
            CredentialPool.GetAllRegisteredUsers<TUserInfo>().FirstOrDefault(predicate ?? (user => true));

        /// <summary>
        /// Register a user in the credential pool
        /// </summary>
        /// <param name="userContext">The user information to register in the pool.</param>
        public static void RegisterUser(IUserInfo userContext)
        {
            CredentialPool.RemoveDisposedInstances();

            lock (Locker)
            {
                if (!string.IsNullOrEmpty(userContext?.UserName))
                {
                    if (!UserDictionary.ContainsKey(Thread.CurrentThread.ManagedThreadId))
                    {
                        UserDictionary.TryAdd(Thread.CurrentThread.ManagedThreadId, new List<IUserInfo>() { userContext });
                    }

                    if (!UserDictionary[Thread.CurrentThread.ManagedThreadId].Any(u => u.UserName == userContext.UserName && u.UniqueKey == userContext.UniqueKey))
                    {
                        UserDictionary[Thread.CurrentThread.ManagedThreadId].Add(userContext);
                    }
                }
            }
        }

        private static void AssertCompatibleAccount(Type type)
        {
            if (!UserInfoRetriverMap.Keys.Contains(type))
            {
                throw new ArgumentOutOfRangeException(
                    $"The specified account type {type.Name} is not supported. You need to update the {nameof(UserInfoRetriverMap)} dictionary in the {nameof(CredentialPool)} class.");
            }
        }

        /// <summary>
        /// Removes all unusable instances of User from the pool.
        /// </summary>
        private static void RemoveDisposedInstances()
        {
            lock (Locker)
            {
                if (UserDictionary.ContainsKey(Thread.CurrentThread.ManagedThreadId))
                {
                    UserDictionary[Thread.CurrentThread.ManagedThreadId].RemoveAll(u => u == null || u.IsDisposed);
                }
            }
        }
    }
}