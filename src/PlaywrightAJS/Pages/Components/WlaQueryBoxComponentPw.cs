namespace WestlawAdvantage.Playwright.AJS.Pages.Components;

using Microsoft.Playwright;

/// <summary>
/// Native Playwright component for the AJS query box (question input + options).
///
/// SHIM VERSION EQUIVALENT: WlaAjsQueryBoxComponent (Framework.Common.UI)
///
/// THE SHADOW DOM PROBLEM — and why Native Playwright is better here:
///
///   The Include Cases checkbox uses a custom web component &lt;saf-checkbox&gt;
///   which renders its actual &lt;input&gt; inside a Shadow DOM.
///
///   SHIM VERSION had to use JavaScript executor to pierce the shadow root:
///     private const string IncludeCasesCheckedScript =
///         "return arguments[0].shadowRoot.querySelector('input[id=control]').getAttribute('aria-checked');";
///     private const string IncludeCasesClickScript =
///         "arguments[0].shadowRoot.querySelector('input[id=control]').click();";
///
///     IWebElement includeCasesElement = DriverExtensions.GetElement(IncludeCasesCheckboxLocator);
///     DriverExtensions.ExecuteScript(IncludeCasesCheckedScript, includeCasesElement);
///
///   This is fragile because:
///     1. It bypasses Playwright's actionability checks
///     2. The result comes back as a JsonElement/object and needs ToString() parsing
///     3. If the shadow DOM structure changes, the script breaks silently
///
///   NATIVE PLAYWRIGHT uses pierce selectors (>>) which automatically
///   cross shadow DOM boundaries — no JavaScript needed:
///     _page.Locator("saf-checkbox >> input#control")
///
/// TODO: Verify the shadow DOM locator by running:
///   await Page.Locator("saf-checkbox").First.EvaluateAsync("el => el.shadowRoot?.innerHTML");
///   This shows you what's inside the shadow root so you can write the right locator.
/// </summary>
public class WlaQueryBoxComponentPw
{
    private readonly IPage _page;

    public WlaQueryBoxComponentPw(IPage page) => _page = page;

    // ── Locators ─────────────────────────────────────────────────────────────

    /// <summary>
    /// The question/query text input.
    /// Replaces: (inherited from parent component — EnterQuestion method)
    /// TODO: Verify this locator.
    /// </summary>
    // Shim: By.XPath("//saf-text-area[@id='fiftyStateQuestionInput']")
    // Playwright pierces the shadow DOM — textarea lives inside saf-text-area's shadow root.
    private ILocator QueryInput =>
        _page.Locator("saf-text-area#fiftyStateQuestionInput textarea, saf-text-area#fiftyStateQuestionInput input").First;

    /// <summary>
    /// The "Include Cases" checkbox — inside a shadow DOM (&lt;saf-checkbox&gt;).
    ///
    /// SHIM VERSION:
    ///   By.XPath(".//saf-checkbox[contains(@class,'checkbox-')]") → JS executor to click inside shadow root
    ///
    /// NATIVE PLAYWRIGHT:
    ///   Use the pierce operator (>>) to cross the shadow DOM boundary.
    ///   Playwright handles this natively — zero JavaScript required.
    ///
    ///   Pattern: "saf-checkbox.checkbox- >> input#control"
    ///   The >> operator tells Playwright to look inside the shadow root.
    ///
    /// TODO: Verify by running Page.Pause() and inspecting the saf-checkbox element.
    ///       If the input id is different, update "input#control" to match.
    ///       Alternative: try "saf-checkbox >> input[type='checkbox']"
    /// </summary>
    // Shim: By.XPath(".//saf-checkbox[contains(@class,'checkbox-')]") + JS click inside shadow root
    // Playwright pierces shadow DOM natively — no JS executor needed.
    private ILocator IncludeCasesCheckbox =>
        _page.Locator("saf-checkbox[class*='checkbox-'] input").First;

    // ── Actions ───────────────────────────────────────────────────────────────

    /// <summary>
    /// Type a question into the query box.
    /// Replaces: surveysPage.WlaQueryBox.EnterQuestion(SurveyQuestion)
    ///
    /// FillAsync clears any existing text and types the new value.
    /// No need to call Clear() first like Selenium required.
    /// </summary>
    public async Task EnterQuestion(string question)
    {
        await QueryInput.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = 10000
        });
        await QueryInput.FillAsync(question);
    }

    /// <summary>
    /// Select the "Include Cases" checkbox if not already selected.
    ///
    /// SHIM VERSION:
    ///   if (!IsIncludeCasesSelected())
    ///   {
    ///       IWebElement includeCasesElement = DriverExtensions.GetElement(IncludeCasesCheckboxLocator);
    ///       DriverExtensions.ExecuteScript(IncludeCasesClickScript, includeCasesElement);
    ///   }
    ///   ← Two separate steps: get element, then execute JS to click inside shadow root
    ///   ← JS click bypasses actionability checks
    ///
    /// NATIVE PLAYWRIGHT:
    ///   CheckAsync() — pierces shadow DOM, checks actionability, then checks the box.
    ///   Single line. More reliable. No JavaScript.
    /// </summary>
    public async Task SelectIncludeCases()
    {
        if (!await IsIncludeCasesSelected())
            await IncludeCasesCheckbox.CheckAsync();
    }

    /// <summary>
    /// Returns true if the "Include Cases" checkbox is checked.
    ///
    /// SHIM VERSION:
    ///   var result = DriverExtensions.ExecuteScript(IncludeCasesCheckedScript, includeCasesElement);
    ///   return result != null &amp;&amp; result.ToString().Contains("true");
    ///   ← Gets aria-checked attribute via JS, parses the string result
    ///   ← Returns a JsonElement from Playwright that needs .ToString() — fragile
    ///
    /// NATIVE PLAYWRIGHT:
    ///   IsCheckedAsync() — returns a proper bool. No string parsing.
    /// </summary>
    public async Task<bool> IsIncludeCasesSelected() =>
        await IncludeCasesCheckbox.IsCheckedAsync();
}