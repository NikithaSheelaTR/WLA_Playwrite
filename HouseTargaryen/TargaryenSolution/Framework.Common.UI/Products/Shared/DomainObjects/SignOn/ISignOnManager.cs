namespace Framework.Common.UI.Products.Shared.DomainObjects.SignOn
{
    using Framework.Common.UI.Interfaces;
    using Framework.Core.DataModel.Security.Proxies;

    /// <summary>
    /// The interface to implement by Web UI sign-on managers.
    /// </summary>
    public interface ISignOnManager
    {
        /// <summary>
        /// Signs off of the product.
        /// </summary>
        /// <returns>The <see cref="ICreatablePageObject"/>.</returns>
        ICreatablePageObject SignOff();

        /// <summary>
        /// Signs on to the product using the specified context.
        /// </summary>
        /// <param name="signOnContext">The sign-on context that includes account information and other details.</param>
        /// <typeparam name="TPage">The type of a page to return.</typeparam>
        /// <typeparam name="TSignOnContext">The type of the sign-on context (<see cref="ISignOnContext{T}"/>).</typeparam>
        /// <returns>The page to return.</returns>
        TPage SignOn<TPage, TSignOnContext>(TSignOnContext signOnContext) where TPage : ICreatablePageObject
                                                                          where TSignOnContext :
                                                                          ISignOnContext<IUserInfo>;

        /// <summary>
        /// Signs on to the product using the specified context.
        /// </summary>
        /// <param name="signOnContext">The sign-on context that includes account information and other details.</param>
        /// <typeparam name="TSignOnContext">The type of the sign-on context (<see cref="ISignOnContext{T}"/>).</typeparam>
        /// <returns>
        /// The <see cref="ICreatablePageObject"/>.
        /// </returns>
        ICreatablePageObject SignOn<TSignOnContext>(TSignOnContext signOnContext)
            where TSignOnContext : ISignOnContext<IUserInfo>;
    }
}