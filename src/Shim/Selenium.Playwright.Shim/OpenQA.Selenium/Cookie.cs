using System;

namespace OpenQA.Selenium
{
    public class Cookie
    {
        public Cookie(string name, string value)
            : this(name, value, null, null, null) { }

        public Cookie(string name, string value, string path)
            : this(name, value, null, path, null) { }

        public Cookie(string name, string value, string path, DateTime? expiry)
            : this(name, value, null, path, expiry) { }

        public Cookie(string name, string value, string domain, string path, DateTime? expiry)
        {
            this.Name = name;
            this.Value = value;
            this.Domain = domain ?? string.Empty;
            this.Path = path ?? "/";
            this.Expiry = expiry;
        }

        public string Name { get; }
        public string Value { get; }
        public string Domain { get; }
        public string Path { get; }
        public DateTime? Expiry { get; }
        public bool Secure { get; set; }
        public bool IsHttpOnly { get; set; }
        public string SameSite { get; set; }

        public override string ToString() => $"{Name}={Value}";

        public override bool Equals(object obj)
        {
            if (obj is Cookie other)
                return this.Name == other.Name && this.Value == other.Value && this.Domain == other.Domain;
            return false;
        }

        public override int GetHashCode() => (Name + Value).GetHashCode();
    }
}
