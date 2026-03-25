using System.Collections.Generic;

namespace OpenQA.Selenium.Chrome
{
    public class ChromeOptions
    {
        private readonly List<string> _arguments = new List<string>();
        private readonly Dictionary<string, object> _userProfilePreferences = new Dictionary<string, object>();
        private readonly Dictionary<string, object> _additionalCapabilities = new Dictionary<string, object>();

        public string BinaryLocation { get; set; }

        public List<string> Arguments => _arguments;

        public void AddArgument(string argument)
        {
            _arguments.Add(argument);
        }

        public void AddArguments(params string[] arguments)
        {
            _arguments.AddRange(arguments);
        }

        public void AddArguments(IEnumerable<string> arguments)
        {
            _arguments.AddRange(arguments);
        }

        public void AddUserProfilePreference(string preferenceName, object preferenceValue)
        {
            _userProfilePreferences[preferenceName] = preferenceValue;
        }

        public void AddAdditionalCapability(string capabilityName, object capabilityValue)
        {
            _additionalCapabilities[capabilityName] = capabilityValue;
        }

        public void AddExcludedArgument(string argument) { }

        internal Dictionary<string, object> UserProfilePreferences => _userProfilePreferences;
        internal Dictionary<string, object> AdditionalCapabilities => _additionalCapabilities;
    }
}
