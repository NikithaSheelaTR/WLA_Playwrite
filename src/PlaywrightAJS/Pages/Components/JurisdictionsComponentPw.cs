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
    /// Outer saf-checkbox element — used for disabled state checks.
    /// The shim used: By.CssSelector("saf-checkbox[current-value='{code}']")
    /// IsDisabledAsync() is checked on the outer element because the disabled
    /// attribute is applied to the custom element wrapper, not the inner input.
    /// </summary>
    private ILocator JurisCheckboxElement(string jurisCode) =>
        _page.Locator($"saf-checkbox[current-value='{jurisCode}']");

    /// <summary>
    /// Inner input inside the saf-checkbox shadow DOM — used for check/uncheck/isChecked.
    /// Playwright auto-pierces open shadow roots with CSS descendant combinators.
    /// The shim reached this via JS: shadowRoot.querySelector('input[id=control]').click()
    /// </summary>
    private ILocator JurisCheckboxInput(string jurisCode) =>
        _page.Locator($"saf-checkbox[current-value='{jurisCode}'] input");

    /// <summary>
    /// The "X selected" count label.
    /// Shim: By.XPath(".//span[contains(@class,'jurisdictionCardSelectedCount')]")
    /// </summary>
    public ILocator SelectedCountLabel =>
        _page.Locator("span[class*='jurisdictionCardSelectedCount']");

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
            var input = JurisCheckboxInput(code);
            if (!await input.IsCheckedAsync())
                await input.CheckAsync();
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
            var input = JurisCheckboxInput(code);
            if (await input.IsCheckedAsync())
                await input.UncheckAsync();
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
        await JurisCheckboxElement(jurisCode).IsDisabledAsync();

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
        await JurisCheckboxInput(jurisCode).IsCheckedAsync();

    /// <summary>
    /// Get the current selected count text (e.g., "2 selected").
    /// Replaces: surveysPage.Jurisdictions.SelectedCountLabel.Text
    /// </summary>
    public async Task<string> GetSelectedCountText() =>
        await SelectedCountLabel.TextContentAsync() ?? string.Empty;
}