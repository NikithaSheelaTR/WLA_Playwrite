namespace Framework.Common.Api.DataModel.Configuration
{
    using Framework.Core.CommonTypes.Configuration;

    /// <summary>
    /// The api test execution context.
    /// </summary>
    public class ApiTestExecutionContext : TestExecutionContext
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ApiTestExecutionContext"/>
        /// </summary>
        /// <param name="settings">
        /// The test settings
        /// </param>
        public ApiTestExecutionContext(TestSettings settings)
            : base(settings)
        {
        }

        /// <summary>
        /// The force routing
        /// </summary>
        public bool ForceRouting { get; set; } = false;
    }
}