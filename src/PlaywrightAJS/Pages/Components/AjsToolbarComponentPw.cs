namespace WestlawAdvantage.Playwright.AJS.Pages.Components;

using Microsoft.Playwright;

/// <summary>
/// Native Playwright component for the AJS result toolbar.
/// Replaces: surveysPage.Toolbar.SaveToFolderButton, surveysPage.Toolbar.ResearchCopyLinkButton,
///           surveysPage.Toolbar.DeliveryDropdown
///
/// TODO: Verify all locators by inspecting the toolbar DOM after a survey completes.
/// The toolbar typically appears above the survey result area.
/// </summary>
public class AjsToolbarComponentPw
{
    private readonly IPage _page;

    public AjsToolbarComponentPw(IPage page) => _page = page;

    // ── Toolbar button locators ───────────────────────────────────────────────

    private ILocator SaveToFolderButton =>
        _page.Locator(
            "[data-automation='save-to-folder'], " +
            "button[aria-label*='folder' i], " +
            "button[title*='folder' i], " +
            "[class*='saveToFolder']"
        ).First;

    private ILocator CopyLinkButton =>
        _page.Locator(
            "[data-automation='copy-link'], " +
            "button[aria-label*='copy link' i], " +
            "[class*='copyLink']:not([class*='success'])"
        ).First;

    private ILocator DeliveryButton =>
        _page.Locator(
            "[data-automation='delivery-button'], " +
            "button[aria-label*='deliver' i], " +
            "[class*='deliveryDropdown'] button"
        ).First;

    // ── Actions ───────────────────────────────────────────────────────────────

    /// <summary>
    /// Click Save to Folder and wait for the dialog.
    /// Replaces: surveysPage.Toolbar.SaveToFolderButton.Click&lt;SaveToFolderDialog&gt;()
    /// 
    /// Returns the ILocator for the dialog so the test can interact with it.
    /// Full dialog page object (SaveToFolderDialogPw) can be added later.
    /// For the spike, return the dialog root locator.
    /// </summary>
    public async Task<ILocator> ClickSaveToFolder()
    {
        await SaveToFolderButton.ClickAsync();
        var dialog = _page.Locator("[role='dialog'], [data-automation='save-to-folder-dialog']");
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
    /// </summary>
    public async Task ClickCopyLink()
    {
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