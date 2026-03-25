namespace Framework.Common.UI.Products.Shared.Models.Annotations
{
    using Framework.Core.DataModel.Security;
    using Framework.Core.DataModel.Security.Proxies;

    /// <summary>
    /// Highlight Item Object Model
    /// </summary>
    public class HighlightItemModel
    {
        private string userInfo;

        /// <summary>
        /// User info namely First and Last name
        /// </summary>
        public string UserInfo
        {
            get
            {
                if (this.userInfo == null)
                {
                    var wlnUserInfo = CredentialPool.GetFirstOrDefaultUser<WlnUserInfo>();
                    return $"{wlnUserInfo.FirstName} {wlnUserInfo.LastName}";
                }

                return this.userInfo;
            }

            set
            {
                this.userInfo = value;
            }
        }

        /// <summary>
        /// Highlighted text
        /// </summary>
        public string HighlightedText { get; set; }
    }
}