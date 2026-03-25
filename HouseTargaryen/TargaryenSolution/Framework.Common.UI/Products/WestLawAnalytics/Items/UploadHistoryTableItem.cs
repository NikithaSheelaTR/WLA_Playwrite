namespace Framework.Common.UI.Products.WestLawAnalytics.Items
{
    using System;

    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Upload History Table Item
    /// </summary>
    public class UploadHistoryTableItem : BaseItem
    {
        private const string ContainerLctMask = "//table[@id='wa_clientMattersHistoryTable']//tr[{0}]";

        private static readonly By FileNameLocator = By.XPath(".//td[1]");
        private static readonly By UploaderLocator = By.XPath(".//td[2]");
        private static readonly By DateUploadedLocator = By.XPath(".//td[3]");
        private static readonly By StatusLocator = By.XPath(".//td[4]");
        private static readonly By DescriptionLocator = By.XPath(".//td[5]");
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="itemIndex"></param>
        public UploadHistoryTableItem(int itemIndex)
            : base(DriverExtensions.GetElement(By.XPath(string.Format(ContainerLctMask, itemIndex))))
        {
        }

        /// <summary>
        /// File Name
        /// </summary>
        public string FileName => DriverExtensions.GetElement(this.Container, FileNameLocator).GetText();

        /// <summary>
        /// Uploader
        /// </summary>
        public string Uploader => DriverExtensions.GetElement(this.Container, UploaderLocator).GetText();

        /// <summary>
        /// Date Uploaded
        /// </summary>
        public DateTime DateUploaded
        {
            get
            {
                var dateUploader = DriverExtensions.GetElement(this.Container, DateUploadedLocator).GetText();
                DateTime dateTime;
                if (string.IsNullOrEmpty(dateUploader))
                {
                    dateTime = default(DateTime);
                }
                else
                {
                    dateUploader = dateUploader.Substring(0, 24);
                    dateTime = DateTime.Parse(dateUploader);
                }
                return dateTime;
            }
        }

        /// <summary>
        /// Status
        /// </summary>
        public string Status => DriverExtensions.GetElement(this.Container, StatusLocator).GetText();

        /// <summary>
        /// Description
        /// </summary>
        public string Description => DriverExtensions.GetElement(this.Container, DescriptionLocator).GetText();    
    }
}
