namespace Framework.Common.UI.Products.Shared.Models.Annotations
{
    using Framework.Core.DataModel.Security;
    using Framework.Core.DataModel.Security.Proxies;

    /// <summary>
    /// Note Item Object Model
    /// </summary>
    public class NoteItemModel
    {
        private string firstName;

        /// <summary>
        /// First name
        /// </summary>
        public string FirstName
        {
            get
            {
                if (this.firstName == null)
                {
                    return CredentialPool.GetFirstOrDefaultUser<WlnUserInfo>().FirstName;
                }

                return this.firstName;
            }

            set
            {
                this.firstName = value;
            }
        }

        /// <summary>
        /// Note text
        /// </summary>
        public string NoteText { get; set; }
    }
}