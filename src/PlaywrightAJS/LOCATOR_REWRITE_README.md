# AJS Locator Rewrite Spike — Developer Guide

**Branch:** `spike/ajs-locator-rewrite`  
**Goal:** Rewrite the AJS `WlaAjsIncludeRelatedFedTests` suite using native Playwright `IPage`/`ILocator` — no shim, no `SyncHelper`, no `Thread.Sleep`.

---

## Why This Spike Exists

The shim approach (in `src/Shim/`) proved that all 800 tests can compile and run on Playwright with zero test file changes. But it limits Playwright to ~40% of its value because:

1. **Wait model is neutered** — the shim caps per-attempt Playwright waits at 2 seconds to keep `WebDriverWait` retry loops working. This causes the wait issues we see in CI.
2. **Trace Viewer is not accessible** — test code can't call `context.Tracing.StartAsync()` through `IWebDriver`.
3. **Shadow DOM requires JS hacks** — the shim can't pierce shadow DOM natively; `saf-checkbox` needs `ExecuteScript` workarounds.
4. **`Clipboard.GetText()` breaks in CI** — Windows Forms, not headless-safe.

This spike rewrites the AJS suite directly against Playwright to measure: **how much faster, how much more stable, and how much code changes**.

---

## Project Structure

```
src/PlaywrightAJS/
├── PlaywrightAJS.csproj                    ← net8.0, Playwright + NUnit
├── Infrastructure/
│   └── PlaywrightAjsBaseTest.cs            ← Base class (PageTest from Playwright.NUnit)
├── Pages/
│   ├── AiJurisdictionalSurveysPagePw.cs    ← Main AJS page object
│   └── Components/
│       ├── JurisdictionsComponentPw.cs     ← Juris selector checkboxes
│       ├── WlaQueryBoxComponentPw.cs       ← Query input + shadow DOM checkbox
│       ├── AjsResultComponentPw.cs         ← Result headings + labels
│       └── AjsToolbarComponentPw.cs        ← Save/Copy/Deliver buttons
└── Tests/
    └── WlaAjsIncludeRelatedFedTestsPw.cs   ← 4 migrated tests
```

---

## Prerequisites

```bash
# 1. Install .NET 8 SDK (if not already installed)
dotnet --version   # should be 8.x

# 2. Restore packages
cd c:\Users\6042165\workspace\WLA_Playwrite
dotnet restore src/PlaywrightAJS/PlaywrightAJS.csproj

# 3. Build the project
dotnet build src/PlaywrightAJS/PlaywrightAJS.csproj

# 4. Install Playwright browsers
pwsh src/PlaywrightAJS/bin/Debug/net8.0/playwright.ps1 install chromium
```

---

## Running the Tests

```bash
# Set credentials (required — do NOT hardcode passwords)
$env:WLA_BASE_URL  = "https://qed.next.westlaw.com"
$env:WLA_TEST_USER = "your-test-user@thomsonreuters.com"
$env:WLA_TEST_PASS = "your-password"
$env:WLA_CLIENT_ID = "Wla Test"

# Run only the first test (START HERE)
dotnet test src/PlaywrightAJS/PlaywrightAJS.csproj `
  --filter "FullyQualifiedName~AjsIncludeRelatedFedJurisSelectorTest"

# Run all AJS tests
dotnet test src/PlaywrightAJS/PlaywrightAJS.csproj `
  --filter "Category=AJS"

# Run headed (see the browser) — useful for debugging
$env:HEADED = "1"
dotnet test src/PlaywrightAJS/PlaywrightAJS.csproj `
  --filter "FullyQualifiedName~AjsIncludeRelatedFedJurisSelectorTest"

# Run with Playwright Inspector (step through actions)
$env:PWDEBUG = "1"
dotnet test src/PlaywrightAJS/PlaywrightAJS.csproj `
  --filter "FullyQualifiedName~AjsIncludeRelatedFedJurisSelectorTest"
```

---

## Step 1: Fix the Sign-On (Required First)

The sign-on in `PlaywrightAjsBaseTest.SignIn()` has placeholder locators. Before any test runs you must:

### Option A — Use Playwright Codegen (recommended)
```bash
# This opens a browser + records your actions as C# code
npx playwright codegen https://qed.next.westlaw.com
```
Sign in manually. Codegen will generate the locators. Copy the sign-on steps into `SignIn()`.

### Option B — Use Page.Pause()
Add `await Page.Pause();` at the start of `SignIn()` and step through manually in the Inspector.

### What to look for
The sign-on goes through CIAM (OnePass). The typical flow:
1. `input[name='username']` or similar — enter username
2. `button[type='submit']` — click Continue
3. `input[type='password']` — enter password  
4. `button[type='submit']` — click Sign In
5. Client ID page (may not appear) — `input` for client ID → Continue
6. WLA home page loads

---

## Step 2: Fix the AJS Navigation Locator

In `PlaywrightAjsBaseTest.NavigateToLandingPage()`, the feature card locator needs to be verified:

```csharp
// Current (educated guess):
var featureCard = Page.Locator(
    "[data-analytics*='aijs'], [data-automation*='aijs'], " +
    "a:has-text('AI Jurisdictional Surveys'), " +
    "button:has-text('AI Jurisdictional Surveys')"
).First;
```

**How to find the right locator:**
1. Sign in to WLA manually
2. Right-click the "AI Jurisdictional Surveys" card → Inspect
3. Look for `data-automation`, `data-analytics`, `href`, or unique class names
4. Update the locator in the base test

---

## Step 3: Fix Jurisdiction Checkbox Locators

This is the most important locator to get right. In `JurisdictionsComponentPw.JurisCheckbox()`:

```csharp
// Current (try these in order):
$"input[data-value='{jurisCode}'], " +
$"input[aria-label='{jurisCode}'], " +
$"[data-automation='juris-{jurisCode}'] input, " +
$"label:has-text('{jurisCode}') input[type='checkbox']"
```

**How to find the right locator:**
1. Navigate to the AJS page in WLA
2. Open DevTools (F12) → Console
3. Run: `document.querySelectorAll('input[type="checkbox"]')` — see what comes back
4. Look for the attribute that holds the jurisdiction code ("CA-CS", "ALLFEDS")
5. Add `await Page.Pause();` in the test to open Inspector, then click a checkbox to see its selector

**Useful locator patterns for TR/WLA web components:**
```
// By data attribute
input[data-jurisdiction='CA-CS']
[data-value='CA-CS'] input

// By aria
input[aria-label='California State Cases']

// Inside a custom element
wl-checkbox[data-code='CA-CS'] input
saf-checkbox[data-value='CA-CS'] >> input

// By text of nearby label
label:has-text('California') input[type='checkbox']
```

---

## Step 4: Fix the Page Description Locator

In `AiJurisdictionalSurveysPagePw.WaitForPageReady()`, the page description locator confirms the AJS landing page has loaded:

```csharp
// Current:
_page.Locator("[data-automation='page-description'], .page-description, [class*='pageDescription']");
```

Inspect the landing page to find what element appears immediately when the page is ready. It might be the query input box, a heading, or a description paragraph.

---

## Shim vs Native Playwright — Pattern Reference

| Pattern | Shim (old) | Native Playwright (new) |
|---|---|---|
| Find element | `driver.FindElement(By.Id("x"))` | `page.Locator("#x")` |
| Click | `element.Click()` | `await locator.ClickAsync()` |
| Type text | `element.SendKeys("text")` | `await locator.FillAsync("text")` |
| Check checkbox | `element.Click()` | `await locator.CheckAsync()` |
| Is checked | `element.Selected` | `await locator.IsCheckedAsync()` |
| Is disabled | `!element.Enabled` | `await locator.IsDisabledAsync()` |
| Get text | `element.Text` | `await locator.TextContentAsync()` |
| Is visible | `element.Displayed` | `await locator.IsVisibleAsync()` |
| Wait for visible | `WebDriverWait.Until(ElementIsVisible)` | `await locator.WaitForAsync(Visible)` |
| Wait for hidden | `WaitUntil(() => !el.Displayed)` | `await locator.WaitForAsync(Hidden)` |
| Shadow DOM | `ExecuteScript("shadowRoot.querySelector...")` | `page.Locator("host >> inner")` |
| Multiple elements text | Loop + `.Text` per element | `await locator.AllTextContentsAsync()` |
| New tab | `BrowserPool.CreateTab()` | `await context.NewPageAsync()` |
| Page title | `BrowserPool.CurrentBrowser.Title` | `await page.TitleAsync()` |
| Download file | `FileUtil.WaitForFileDownload()` | `await page.RunAndWaitForDownloadAsync()` |
| Clipboard | `Clipboard.GetText()` | `await page.EvaluateAsync("navigator.clipboard.readText()")` |
| Sleep | `Thread.Sleep(2000)` | ❌ Remove — use WaitForAsync instead |
| Poll until | `SafeMethodExecutor.WaitUntil(...)` | `await locator.WaitForAsync(state)` |

---

## Test Implementation Order

### ✅ Test 1: `AjsIncludeRelatedFedJurisSelectorTest` — START HERE
- No survey generation needed
- Pure UI checkbox state verification
- Fastest to get passing
- Forces you to nail the juris checkbox locators

### 🔲 Test 2: `AjsIncludeRelatedFedFolderingTest`
- Needs survey generation (implement `WaitForSurveyComplete`)
- Needs `PrepareTestFolder` implementation
- Needs save dialog + folder navigation page objects
- Implement after Test 1 is stable

### 🔲 Test 3: `AjsIncludeRelatedFedCopyLinkTest`
- Needs sign-out + sign-back-in helpers
- Clipboard read via `navigator.clipboard.readText()` — needs clipboard permission granted
- New tab navigation via `Context.NewPageAsync()`
- Implement after Test 2

### 🔲 Test 4: `AjsIncludeRelatedFedDeliveryTest`
- Needs full download dialog implementation
- Use `Page.RunAndWaitForDownloadAsync()` to capture the file
- Implement last

---

## Debugging Tips

### See what's happening in the browser
```csharp
// Add to any test or page object method to pause and open Inspector:
await Page.Pause();
```

### Record locators with Codegen
```bash
npx playwright codegen https://qed.next.westlaw.com
```

### Enable trace recording
Add to `PlaywrightAjsBaseTest`:
```csharp
[SetUp]
public async Task StartTrace()
{
    await Context.Tracing.StartAsync(new TracingStartOptions
    {
        Screenshots = true,
        Snapshots = true,
        Sources = true
    });
}

[TearDown]
public async Task StopTrace()
{
    await Context.Tracing.StopAsync(new TracingStopOptions
    {
        Path = $"traces/{TestContext.CurrentContext.Test.Name}.zip"
    });
}
```

Then open the trace:
```bash
npx playwright show-trace traces/AjsIncludeRelatedFedJurisSelectorTest.zip
```

### Print the current page HTML for debugging
```csharp
var html = await Page.ContentAsync();
Console.WriteLine(html.Substring(0, 2000)); // first 2KB
```

---

## What to Measure and Report Back

Once Test 1 (`AjsIncludeRelatedFedJurisSelectorTest`) is passing, record:

| Metric | Shim version | Native Playwright |
|---|---|---|
| Test execution time | ___ seconds | ___ seconds |
| Wait-related failures in 10 runs | ___/10 | ___/10 |
| Lines of test code changed | 0 (it's a new file) | ~30 lines from original |
| Locator lines changed | 0 | ~10 per component |
| Time to discover/fix locators | N/A | ___ hours |

---

## Files Changed Summary

| File | Status | Purpose |
|---|---|---|
| `PlaywrightAJS.csproj` | 🆕 New | Project file, Playwright NUnit package |
| `Infrastructure/PlaywrightAjsBaseTest.cs` | 🆕 New | Base test (replaces WlaBaseTest + WlaAjsBaseTest) |
| `Pages/AiJurisdictionalSurveysPagePw.cs` | 🆕 New | Main page object (replaces AiJurisdictionalSurveysPage) |
| `Pages/Components/JurisdictionsComponentPw.cs` | 🆕 New | Juris selector (native, no JS executor) |
| `Pages/Components/WlaQueryBoxComponentPw.cs` | 🆕 New | Query box (shadow DOM via pierce, not JS) |
| `Pages/Components/AjsResultComponentPw.cs` | 🆕 New | Result headings (XPath preserved + CSS alternatives) |
| `Pages/Components/AjsToolbarComponentPw.cs` | 🆕 New | Toolbar buttons (stub for delivery/folder) |
| `Tests/WlaAjsIncludeRelatedFedTestsPw.cs` | 🆕 New | 4 tests (1 fully ready, 3 with TODO stubs) |

**Original files are NOT modified.** This is a parallel implementation for comparison.

---

*Branch: `spike/ajs-locator-rewrite` | Repo: github.com/NikithaSheelaTR/WLA_Playwrite*