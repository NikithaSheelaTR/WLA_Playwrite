namespace Framework.Core.Url
{
    /// <summary>
    /// UrlType "enum" equivalent.
    /// 
    /// "Enum" equivalent with all of the possible types of URLs that can be used in a test.
    /// </summary>
    public class UrlType : IUrlType
    {
        /// <summary>
        /// 
        /// </summary>
        protected string Name;

        /// <summary>
        /// 
        /// </summary>
        public static UrlType DEFAULT = new UrlType("DEFAULT");

        /// <summary>
        /// 
        /// </summary>
        public static UrlType HOST = new UrlType("HOST");

        /// <summary>
        /// 
        /// </summary>
        public static UrlType ROUTING_PAGE = new UrlType("ROUTING_PAGE");

        /// <summary>
        /// 
        /// </summary>
        public static UrlType LOGIN_PAGE = new UrlType("LOGIN_PAGE");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public UrlType(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return this.Name;
        }
    }
}
