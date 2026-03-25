

namespace Framework.Common.UI.Products.WestlawEdge.Components.Document
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.StatutesCompare;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    /// <summary>
    /// Documents compare chunk component
    /// </summary>
    public class CompareDocumentsChunkComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class = 'co_chunkPagination']");

        private static readonly By ChunkCurrentStatusLocator = By.XPath("//input[@class = 'co_compareCurrentChunkStatus']");

        private static readonly By ChunkCountStatusLocator = By.XPath("//*[@class = 'co_compareChunkCountStatus']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Returns dedicated chunk page
        /// </summary>
        /// <returns> The <see cref="string"/>. String with counts. </returns>
        public CompareVersionsDialog GoToChunk(int chunkNumber)
        {
            var chunkStatus = DriverExtensions.GetElement(ChunkCurrentStatusLocator);
            chunkStatus.SetTextField(chunkNumber.ToString());
            DriverExtensions.PressKey(Keys.Enter);
            return new CompareVersionsDialog();
        }

        /// <summary>
        /// Returns total chunks count
        /// </summary>
        /// <returns> The <see cref="int"/>. String with counts. </returns>
        public int GetChunksCount() => int.Parse(DriverExtensions.GetText(ChunkCountStatusLocator));
    }
}
