namespace WestlawAdvantage.Playwright.AJS.Pages.Components;

using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

/// <summary>
/// Native Playwright component for the AJS result toolbar.
/// Replaces: surveysPage.Toolbar.SaveToFolderButton, surveysPage.Toolbar.ResearchCopyLinkButton,
///           surveysPage.Toolbar.DeliveryDropdown
///
/// LOCATOR SOURCE (confirmed from shim AiJurisdictionalSurveysToolbarComponent + EdgeToolbarElementsInfo.json):
///   CopyLink:      button[contains(@class, 'linkBuilder-button icon_link co_tbButton')]
///   SaveToFolder:  //div[contains(@class,'co_saveTo')]//*[contains(@class,'co_dropdownTitle')]
///   Delivery:      //*[@id='deliveryLink1'] | //li[@id='co_deliveryWidget']//button
/// </summary>
public class AjsToolbarComponentPw
{
    private readonly IPage _page;

    public AjsToolbarComponentPw(IPage page) => _page = page;

    // ── Toolbar button locators ───────────────────────────────────────────────

    /// <summary>
    /// Save to Folder dropdown button.
    /// Shim XPath: //div[@class='co_saveTo']/*[contains(@class,'co_dropdownTitle')]
    /// </summary>
    private ILocator SaveToFolderButton =>
        _page.Locator("xpath=//div[contains(@class,'co_saveTo')]//*[contains(@class,'co_dropdownTitle')]");

    /// <summary>
    /// Copy Link (Research) button.
    /// Shim XPath: .//button[contains(@class, 'linkBuilder-button icon_link co_tbButton')]
    /// </summary>
    private ILocator CopyLinkButton =>
        _page.Locator("xpath=//button[contains(@class,'linkBuilder-button') and contains(@class,'icon_link') and contains(@class,'co_tbButton')]");

    /// <summary>
    /// Delivery dropdown button.
    /// Shim XPath: //*[@id='deliveryLink1'] | //li[@id='co_deliveryWidget']//button
    /// </summary>
    private ILocator DeliveryButton =>
        _page.Locator("#deliveryLink1, #deliveryWidgetButton1, #co_deliveryWidget button").First;

    // ── Actions ───────────────────────────────────────────────────────────────

    /// <summary>
    /// Click Save to Folder and wait for the dialog.
    /// Replaces: surveysPage.Toolbar.SaveToFolderButton.Click&lt;SaveToFolderDialog&gt;()
    ///
    /// Dialog root confirmed shim XPath:
    ///   //div[@id='coid_lightboxOverlay' and not(contains(@class,'co_hideState'))]
    ///   //div[contains(@class,'co_folderAction')]
    /// </summary>
    public async Task<ILocator> ClickSaveToFolder()
    {
        await SaveToFolderButton.ClickAsync();
        var dialog = _page.Locator(
            "xpath=//div[@id='coid_lightboxOverlay' and not(contains(@class,'co_hideState'))]" +
            "//div[contains(@class,'co_folderAction')]");
        await dialog.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = 10000
        });
        return dialog;
    }

    /// <summary>
    /// Click the Copy Link button.
    /// Replaces: surveysPage.Toolbar.ResearchCopyLinkButton.Click()
    /// The button is initially disabled while the survey result renders — wait for enabled first.
    /// </summary>
    public async Task ClickCopyLink()
    {
        await Expect(CopyLinkButton).ToBeEnabledAsync(new LocatorAssertionsToBeEnabledOptions { Timeout = 30000 });
        await CopyLinkButton.ClickAsync();
    }

    /// <summary>
    /// Click the delivery dropdown and select the Download option.
    /// Replaces: surveysPage.Toolbar.DeliveryDropdown.SelectOption&lt;DownloadDialog&gt;(DeliveryMethod.Download)
    ///
    /// Returns the dialog locator. Full DownloadDialog page object to be added later.
    /// </summary>
    public async Task<ILocator> SelectDeliveryDownload()
    {
        await DeliveryButton.ClickAsync();

        // Click the "Download" option in the dropdown
        var downloadOption = _page.Locator(
            "[data-automation='delivery-download'], " +
            "[role='menuitem']:has-text('Download'), " +
            "li:has-text('Download')"
        ).First;
        await downloadOption.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
        await downloadOption.ClickAsync();

        // Wait for the download dialog to appear
        var dialog = _page.Locator("[role='dialog'], [data-automation='download-dialog']");
        await dialog.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
        return dialog;
    }
}