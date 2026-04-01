namespace WestlawAdvantage.Playwright.AJS.Pages.Components;

using Microsoft.Playwright;

/// <summary>
/// Native Playwright component for the Jurisdictions selector on the AJS page.
///
/// SHIM VERSION EQUIVALENT:
///   surveysPage.Jurisdictions.SelectJurisdiction(JurisCA)
///   surveysPage.Jurisdictions.IsJurisdictionSelectionDisabled(JurisIncludeRelatedFed)
///   surveysPage.Jurisdictions.IsJurisdictionSelected(JurisIncludeRelatedFed)
///   surveysPage.Jurisdictions.SelectedCountLabel.Text
///
/// HOW THE SHIM HANDLES THIS vs NATIVE PLAYWRIGHT:
///
///   SHIM: IWebElement.Enabled → shim calls Locator.IsEnabledAsync()
///         But IsEnabled checks the element itself, not whether it's "grayed out"
///         due to a disabled state set by a parent or CSS — this can be inaccurate.
///
///   NATIVE: IsDisabledAsync() — Playwright checks the actual disabled attribute
///           AND computed accessibility state. More reliable.
///
///   SHIM: IWebElement.Selected → shim calls EvaluateAsync("el => !!(el.selected || el.checked)")
///         This works but goes through 2 translation layers.
///
///   NATIVE: IsCheckedAsync() — direct Playwright API, cleaner.
///
/// TODO: The jurisdiction checkboxes use data values like "CA-CS", "ALLFEDS", "Select All".
///       Inspect the DOM to find the exact attribute that holds these values.
///       Common patterns in TR apps:
///         - input[data-value='CA-CS']
///         - input[aria-label='CA-CS']  
///         - label:has-text('CA-CS') input
///         - [data-automation='juris-CA-CS']
///       Update JurisCheckbox() locator once you know the actual pattern.
/// </summary>
public class JurisdictionsComponentPw
{
    private readonly IPage _page;

    public JurisdictionsComponentPw(IPage page) => _page = page;

    // ── Locators ─────────────────────────────────────────────────────────────

    /// <summary>
    /// Locator for a specific jurisdiction checkbox by its code (e.g., "CA-CS", "ALLFEDS").
    ///
    /// TODO: Inspect the DOM and update this locator.
    /// The jurisdiction code is passed as used in the original tests:
    ///   JurisCA = "CA-CS", JurisIncludeRelatedFed = "ALLFEDS", JurisSelectAll = "Select All"
    ///
    /// Try these in Playwright Inspector (Page.Pause()) one by one until one works:
    ///   input[data-value='{code}']
    ///   input[aria-label='{code}']
    ///   [data-automation='juris-{code}']
    ///   label:has-text('{code}') input[type='checkbox']
    /// </summary>
    private ILocator JurisCheckbox(string jurisCode) =>
        _page.Locator(
            $"input[data-value='{jurisCode}'], " +
            $"input[aria-label='{jurisCode}'], " +
            $"[data-automation='juris-{jurisCode}'] input, " +
            $"label:has-text('{jurisCode}') input[type='checkbox']"
        ).First;

    /// <summary>
    /// The "X selected" count label.
    /// Replaces: surveysPage.Jurisdictions.SelectedCountLabel.Text.Contains("0 selected")
    ///
    /// SHIM: .Text → EvaluateAsync("el => el.innerText") → string comparison
    /// NATIVE: TextContentAsync() → direct, no translation
    ///
    /// TODO: Verify locator for the count label.
    /// </summary>
    public ILocator SelectedCountLabel =>
        _page.Locator("[data-automation='selected-count'], [class*='selectedCount'], [class*='jurisCount']");

    // ── Actions ───────────────────────────────────────────────────────────────

    /// <summary>
    /// Select one or more jurisdictions by checking their checkboxes.
    ///
    /// SHIM VERSION:
    ///   surveysPage.Jurisdictions.SelectJurisdiction(JurisCA, JurisMN, JurisIncludeRelatedFed)
    ///   ← calls IWebElement.Click() for each, which goes through the shim
    ///   ← each click has 2s per-attempt Playwright wait
    ///
    /// NATIVE PLAYWRIGHT:
    ///   CheckAsync() — Playwright auto-waits for the checkbox to be actionable,
    ///   verifies it's actually checked after, retries if needed. Reliable.
    /// </summary>
    public async Task SelectJurisdiction(params string[] jurisCodes)
    {
        foreach (var code in jurisCodes)
        {
            var checkbox = JurisCheckbox(code);
            // Playwright auto-waits for the element to be visible + enabled
            if (!await checkbox.IsCheckedAsync())
                await checkbox.CheckAsync();
        }
    }

    /// <summary>
    /// Deselect one or more jurisdictions.
    ///
    /// SHIM VERSION:
    ///   surveysPage.Jurisdictions.SelectJurisdiction(false, JurisCA)
    ///   ← the "false" parameter means "deselect" in the original API
    ///
    /// NATIVE PLAYWRIGHT:
    ///   UncheckAsync() — explicit, no boolean flag ambiguity.
    /// </summary>
    public async Task DeselectJurisdiction(params string[] jurisCodes)
    {
        foreach (var code in jurisCodes)
        {
            var checkbox = JurisCheckbox(code);
            if (await checkbox.IsCheckedAsync())
                await checkbox.UncheckAsync();
        }
    }

    /// <summary>
    /// Returns true if the jurisdiction checkbox is disabled (not interactable).
    ///
    /// SHIM VERSION:
    ///   surveysPage.Jurisdictions.IsJurisdictionSelectionDisabled(JurisIncludeRelatedFed)
    ///   ← IWebElement.Enabled → shim → Locator.IsEnabledAsync()
    ///
    /// NATIVE PLAYWRIGHT:
    ///   IsDisabledAsync() — checks disabled attribute + ARIA disabled state.
    ///   More accurate than negating IsEnabledAsync() in some edge cases.
    /// </summary>
    public async Task<bool> IsJurisdictionSelectionDisabled(string jurisCode) =>
        await JurisCheckbox(jurisCode).IsDisabledAsync();

    /// <summary>
    /// Returns true if the jurisdiction checkbox is checked/selected.
    ///
    /// SHIM VERSION:
    ///   surveysPage.Jurisdictions.IsJurisdictionSelected(JurisIncludeRelatedFed)
    ///   ← IWebElement.Selected → shim → EvaluateAsync("el => !!(el.selected || el.checked)")
    ///
    /// NATIVE PLAYWRIGHT:
    ///   IsCheckedAsync() — direct API, no JavaScript evaluation needed.
    /// </summary>
    public async Task<bool> IsJurisdictionSelected(string jurisCode) =>
        await JurisCheckbox(jurisCode).IsCheckedAsync();

    /// <summary>
    /// Get the current selected count text (e.g., "2 selected").
    /// Replaces: surveysPage.Jurisdictions.SelectedCountLabel.Text
    /// </summary>
    public async Task<string> GetSelectedCountText() =>
        await SelectedCountLabel.TextContentAsync() ?? string.Empty;
}