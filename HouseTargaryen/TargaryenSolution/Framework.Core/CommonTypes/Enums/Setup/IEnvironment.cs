namespace Framework.Core.CommonTypes.Enums.Setup
{
    /// <summary>
    /// Environment Interface
    /// </summary>
    public interface IEnvironment
    {
        /// <summary>
        /// Returns the name of the EnvironmentUnderTest.
        /// </summary>
        /// <returns>The name of the EnvironmentUnderTest.</returns>
        string GetName();
    }
}
