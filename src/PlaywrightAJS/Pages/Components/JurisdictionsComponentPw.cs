namespace WestlawAdvantage.Playwright.AJS.Pages.Components;

using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

/// <summary>
/// Native Playwright component for the Jurisdictions selector on the AJS page.
///
/// SHIM VERSION EQUIVALENT: AiJurisdictionalSurveysJurisdictionsComponent
///
/// API MAPPING:
///   Shim SelectJurisdiction(codes)         → SelectJurisdiction(codes)  — clear all, then check each
///   Shim SelectJurisdiction(false, codes)  → AddJurisdiction(codes)     — check each, no clear
///   Shim SelectJurisdiction(false, code)   → DeselectJurisdiction(code) — uncheck (when code is checked)
///
///   Shim WaitForJurisdictionDisabled(code) → WaitForJurisdictionDisabled(code)
///   Shim WaitForJurisdictionEnabled(code)  → WaitForJurisdictionEnabled(code)
///   Shim WaitForJurisdictionSelected(code) → WaitForJurisdictionSelected(code)
///   Shim WaitForSelectedCountToContain(t)  → WaitForSelectedCountToContain(t)
///
/// LOCATOR SOURCE:
///   saf-checkbox[current-value='{code}'] input#control
///   Confirmed: shim uses JS shadowRoot.querySelector('input[id=control]') on saf-checkbox[current-value='{code}']
///   Playwright CSS pierces open shadow roots with descendant combinator — equivalent, no JS needed.
/// </summary>
public class JurisdictionsComponentPw
{
    private readonly IPage _page;

    public JurisdictionsComponentPw(IPage page) => _page = page;

    // ── Locators ─────────────────────────────────────────────────────────────

    /// <summary>
    /// Inner input[id=control] inside the saf-checkbox shadow DOM.
    /// Shim: shadowRoot.querySelector('input[id=control]') on saf-checkbox[current-value='{code}']
    /// Confirmed locator — Playwright CSS shadow piercing equivalent.
    /// </summary>
    private ILocator JurisCheckboxInput(string jurisCode) =>
        _page.Locator($"saf-checkbox[current-value='{jurisCode}'] input#control");

    /// <summary>
    /// The "X selected" count label.
    /// Shim XPath: .//span[contains(@class,'jurisdictionCardSelectedCount')]
    /// </summary>
    public ILocator SelectedCountLabel =>
        _page.Locator("span[class*='jurisdictionCardSelectedCount']");

    /// <summary>
    /// "Clear selections" button — resets all jurisdiction checkboxes.
    /// Shim XPath: .//saf-button[contains(text(),'Clear selections')]
    /// </summary>
    private ILocator ClearSelectionsButton =>
        _page.Locator("saf-button:has-text('Clear selections')");

    // ── Actions ───────────────────────────────────────────────────────────────

    /// <summary>
    /// Clear all selected jurisdictions by clicking "Clear selections".
    /// Shim: called internally at start of SelectJurisdiction(params) when clearAllSelected=true.
    /// Safe to call when nothing is selected.
    /// </summary>
    public async Task ClearAllJurisdictions()
    {
        if (await ClearSelectionsButton.IsVisibleAsync())
            await ClearSelectionsButton.ClickAsync();
    }

    /// <summary>
    /// Clear all, then check each jurisdiction.
    /// Shim equivalent: SelectJurisdiction(params string[] codes) — clearAllSelected=true (default).
    /// Use this when you want exactly these jurisdictions selected, clearing any prior state.
    /// </summary>
    public async Task SelectJurisdiction(params string[] jurisCodes)
    {
        await ClearAllJurisdictions();
        foreach (var code in jurisCodes)
        {
            var input = JurisCheckboxInput(code);
            await input.CheckAsync();
        }
    }

    /// <summary>
    /// Check each jurisdiction WITHOUT clearing first.
    /// Shim equivalent: SelectJurisdiction(false, params string[] codes) — when used to ADD to selection.
    /// Use this to add jurisdictions to an existing selection.
    /// </summary>
    public async Task AddJurisdiction(params string[] jurisCodes)
    {
        foreach (var code in jurisCodes)
        {
            var input = JurisCheckboxInput(code);
            if (!await input.IsCheckedAsync())
                await input.CheckAsync();
        }
    }

    /// <summary>
    /// Uncheck each jurisdiction.
    /// Shim equivalent: SelectJurisdiction(false, params string[] codes) — when used to REMOVE from selection.
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

    // ── Queries ───────────────────────────────────────────────────────────────

    /// <summary>
    /// Returns true if the jurisdiction checkbox is disabled (aria-disabled or disabled attr).
    /// Shim: JS getAttribute('aria-disabled') on shadow DOM input.
    /// </summary>
    public async Task<bool> IsJurisdictionSelectionDisabled(string jurisCode) =>
        await JurisCheckboxInput(jurisCode).IsDisabledAsync();

    /// <summary>
    /// Returns true if the jurisdiction checkbox is checked.
    /// Shim: JS getAttribute('aria-checked') on shadow DOM input.
    /// </summary>
    public async Task<bool> IsJurisdictionSelected(string jurisCode) =>
        await JurisCheckboxInput(jurisCode).IsCheckedAsync();

    /// <summary>
    /// Get the current selected count text (e.g., "2 selected").
    /// Shim: SelectedCountLabel.Text
    /// </summary>
    public async Task<string> GetSelectedCountText() =>
        await SelectedCountLabel.TextContentAsync() ?? string.Empty;

    // ── Explicit waits (mirrors shim's SafeMethodExecutor.WaitUntil polling) ──

    /// <summary>
    /// Wait for a jurisdiction checkbox to become disabled.
    /// Shim: WaitForJurisdictionDisabled(code, timeoutFromSec=10)
    ///   → SafeMethodExecutor.WaitUntil(() => IsJurisdictionSelectionDisabled(code))
    /// </summary>
    public async Task WaitForJurisdictionDisabled(string jurisCode, int timeoutMs = 10000) =>
        await Expect(JurisCheckboxInput(jurisCode)).ToBeDisabledAsync(
            new LocatorAssertionsToBeDisabledOptions { Timeout = timeoutMs });

    /// <summary>
    /// Wait for a jurisdiction checkbox to become enabled (not disabled).
    /// Shim: WaitForJurisdictionEnabled(code, timeoutFromSec=10)
    ///   → SafeMethodExecutor.WaitUntil(() => !IsJurisdictionSelectionDisabled(code))
    /// </summary>
    public async Task WaitForJurisdictionEnabled(string jurisCode, int timeoutMs = 10000) =>
        await Expect(JurisCheckboxInput(jurisCode)).ToBeEnabledAsync(
            new LocatorAssertionsToBeEnabledOptions { Timeout = timeoutMs });

    /// <summary>
    /// Wait for a jurisdiction checkbox to become checked/selected.
    /// Shim: WaitForJurisdictionSelected(code, timeoutFromSec=10)
    ///   → SafeMethodExecutor.WaitUntil(() => IsJurisdictionSelected(code))
    /// </summary>
    public async Task WaitForJurisdictionSelected(string jurisCode, int timeoutMs = 10000) =>
        await Expect(JurisCheckboxInput(jurisCode)).ToBeCheckedAsync(
            new LocatorAssertionsToBeCheckedOptions { Timeout = timeoutMs });

    /// <summary>
    /// Wait for the selected count label to contain the expected text.
    /// Shim: WaitForSelectedCountToContain(text, timeoutFromSec=10)
    ///   → SafeMethodExecutor.WaitUntil(() => SelectedCountLabel.Text.Contains(text))
    /// </summary>
    public async Task WaitForSelectedCountToContain(string expectedText, int timeoutMs = 10000) =>
        await Expect(SelectedCountLabel).ToContainTextAsync(
            expectedText, new LocatorAssertionsToContainTextOptions { Timeout = timeoutMs });
}
