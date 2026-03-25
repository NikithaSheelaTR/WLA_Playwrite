namespace Framework.Common.UI.Products.Shared.DomainObjects
{
    using System;
    using System.Linq;

    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;

    /// <summary>
    /// The base Ui Service class.
    /// </summary>
    public abstract class BaseUiServiceManager
    {
        /// <summary>
        /// The products that support service.
        /// </summary>
        private static CobaltProductId[] SupportedProducts;

        /// <summary>
        /// The environment.
        /// </summary>
        protected readonly EnvironmentInfo environment;

        /// <summary>
        /// The product.
        /// </summary>
        protected readonly CobaltProductInfo product;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseUiServiceManager"/> class.
        /// </summary>
        /// <param name="testExecutionContext">The test execution context.</param>
        /// <param name="product">The product.</param>
        /// <param name="supportedProducts">Supported Products.</param>
        protected BaseUiServiceManager(
            TestExecutionContext testExecutionContext,
            CobaltProductInfo product,
            params CobaltProductId[] supportedProducts)
        {
            SupportedProducts = supportedProducts;

            if (!BaseUiServiceManager.IsAvailableFor(product?.Id))
            {
                throw new ArgumentException(
                    $"Product '{product?.Id.ToString() ?? "NULL"}' is not supported",
                    nameof(product));
            }

            this.product = product;
            this.environment = testExecutionContext.TestEnvironment;
        }

        /// <summary>
        /// Return true if the product supports service
        /// </summary>
        /// <param name="productId">The product id.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool IsAvailableFor(CobaltProductId? productId)
            => productId != null && SupportedProducts.Contains(productId.Value);
    }
}