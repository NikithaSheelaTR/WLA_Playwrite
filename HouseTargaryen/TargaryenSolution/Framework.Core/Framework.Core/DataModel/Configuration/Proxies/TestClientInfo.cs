namespace Framework.Core.DataModel.Configuration.Proxies
{
    using System;

    using Framework.Core.DataModel.Configuration.Constants;

    /// <summary>
    /// Represents a test client used as a test mediator.
    /// </summary>
    public sealed class TestClientInfo : CobaltEntityInfo<TestClientId, TestClientType>
    {
        /// <summary>
        /// Gets a test client's alias.
        /// </summary>
        public string Alias { get; internal set; }

        /// <summary>
        /// Gets the generic family of the test client.
        /// </summary>
        public TestClientFamily Family { get; internal set; }

        /// <summary>
        /// Gets the path to executable.
        /// </summary>
        public string PathToExecutable { get; internal set; }

        /// <summary>
        /// Uri for remote browser
        /// </summary>
        public Uri RemoteDriverUri { get; internal set; }

        /// <summary>
        /// A string representation of the current object.
        /// </summary>
        /// <returns>Returns a string representation of the current object.</returns>
        public override string ToString() =>
            string.Format(
                "{0}, Alias={1}, Family={2}, Executable='{3}', RemoteDriverUri='{4}'",
                base.ToString(),
                this.Alias,
                this.Family,
                this.PathToExecutable,
                this.RemoteDriverUri?.AbsolutePath ?? "None");
    }
}