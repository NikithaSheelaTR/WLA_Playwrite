namespace WestlawPrecision.Tests.LitigationAnalytics.DeliveryTests
    { 
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.ProfileTabs;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.LitigationAnalytics;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.LitigationAnalytics;
    using Framework.Common.UI.Utils;

    public class BaseDeliveryTest : BaseAnalyticsTest
    {
        public BaseDeliveryTest() { }

        protected void DownloadCustomizedReport(LitigationAnalyticsDownloadDialog downloadDialog, string reportName)
        {
            downloadDialog.LitigationAnalyticsTheBasicsTab.WhatToDeliver.SelectOption(WhatToDeliver.CustomizedReport);
            var theBasicsTab = downloadDialog.LitigationAnalyticsTheBasicsTab;
            theBasicsTab.WhatToDeliver.CustomizedReportsOptions.UnselectAllCheckboxes();
            theBasicsTab.WhatToDeliver.CustomizedReportsOptions.SelectAllIncludeSectionOption();
            theBasicsTab.ItemsToInclude.UnselectAllCheckboxes();
            theBasicsTab.ItemsToInclude.SelectAllCheckboxes();
            theBasicsTab.FormatDropdown.SelectOption(DeliveryFormat.Pdf);
            downloadDialog.ClickDownloadButton<LitigationAnalyticsReadyForDeliveryDialog>().DownloadButton.Click<ExperienceProfileTabComponent>();
            FileUtils.WaitForFileDownload(FolderToSave, reportName);
        }

        protected void DownloadCurrentViewReport(LitigationAnalyticsDownloadDialog downloadDialog, string reportName)
        {
            var theBasicsTab = downloadDialog.LitigationAnalyticsTheBasicsTab;
            downloadDialog.LayoutAndLimitsTab.CoverPageCheckBox.Set(true);
            downloadDialog.LitigationAnalyticsTheBasicsTab.WhatToDeliver.SelectOption(WhatToDeliver.CurrentView);
            theBasicsTab.ItemsToInclude.UnselectAllCheckboxes();
            theBasicsTab.ItemsToInclude.SelectAllCheckboxes();
            downloadDialog.LitigationAnalyticsTheBasicsTab.FormatDropdown.SelectOption(DeliveryFormat.Pdf);
            downloadDialog.ClickDownloadButton<LitigationAnalyticsReadyForDeliveryDialog>().DownloadButton.Click<ExperienceProfileTabComponent>();
            FileUtils.WaitForFileDownload(FolderToSave, reportName);
        }

        protected void DownloadCurrentViewReportWithSettings(LitigationAnalyticsDownloadDialog downloadDialog, string reportName, ItemsToInclude optionToInclude)
        {
            var theBasicsTab = downloadDialog.LitigationAnalyticsTheBasicsTab;
            downloadDialog.LitigationAnalyticsTheBasicsTab.WhatToDeliver.SelectOption(WhatToDeliver.CurrentView);
            theBasicsTab.ItemsToInclude.UnselectAllCheckboxes();
            downloadDialog.LitigationAnalyticsTheBasicsTab.ItemsToInclude.SetIncludeSectionOption(optionToInclude);
            downloadDialog.LitigationAnalyticsTheBasicsTab.FormatDropdown.SelectOption(DeliveryFormat.Pdf);
            downloadDialog.ClickDownloadButton<LitigationAnalyticsReadyForDeliveryDialog>().DownloadButton.Click<ExperienceProfileTabComponent>();
            FileUtils.WaitForFileDownload(FolderToSave, reportName);
        }

        protected void DownloadCustomizedReportSelectedLabel(LitigationAnalyticsDownloadDialog downloadDialog, string reportName)
        {
            downloadDialog.LayoutAndLimitsTab.CoverPageCheckBox.Set(true);
            downloadDialog.LitigationAnalyticsTheBasicsTab.WhatToDeliver.SelectOption(WhatToDeliver.CustomizedReport);
            var theBasicsTab = downloadDialog.LitigationAnalyticsTheBasicsTab;
            theBasicsTab.WhatToDeliver.CustomizedReportsOptions.UnselectAllCheckboxes();
            theBasicsTab.WhatToDeliver.CustomizedReportsOptions.SelectCustomizedReportTabName("Overview");
            theBasicsTab.WhatToDeliver.CustomizedReportsOptions.SelectCustomizedReportTabName("Experience");
            theBasicsTab.ItemsToInclude.UnselectAllCheckboxes();
            theBasicsTab.ItemsToInclude.SelectAllCheckboxes();
            theBasicsTab.FormatDropdown.SelectOption(DeliveryFormat.Pdf);
            downloadDialog.ClickDownloadButton<LitigationAnalyticsReadyForDeliveryDialog>().DownloadButton.Click<ExperienceProfileTabComponent>();
            FileUtils.WaitForFileDownload(FolderToSave, reportName);
        }
    }
}