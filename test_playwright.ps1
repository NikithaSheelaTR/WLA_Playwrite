# Quick Playwright test - does Chrome launch and navigate?
$code = @"
using System;
using System.Threading.Tasks;
using Microsoft.Playwright;

class Test {
    static async Task Main() {
        Console.WriteLine("Creating Playwright...");
        var pw = await Playwright.CreateAsync();
        Console.WriteLine("Launching Chrome (channel=chrome)...");
        var browser = await pw.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { 
            Headless = false, 
            Channel = "chrome" 
        });
        Console.WriteLine("Creating context...");
        var ctx = await browser.NewContextAsync(new BrowserNewContextOptions {
            IgnoreHTTPSErrors = true
        });
        var page = await ctx.NewPageAsync();
        Console.WriteLine("Navigating to https://www.google.com ...");
        await page.GotoAsync("https://www.google.com", new PageGotoOptions {
            WaitUntil = WaitUntilState.DOMContentLoaded,
            Timeout = 30000
        });
        Console.WriteLine("SUCCESS! Page title: " + await page.TitleAsync());
        Console.WriteLine("URL: " + page.Url);
        await Task.Delay(3000);
        await ctx.CloseAsync();
        await browser.CloseAsync();
        pw.Dispose();
        Console.WriteLine("Done.");
    }
}
"@

# Use the published Playwright DLL
$publishDir = "C:\Github\WLA_Playwrite\src\Shim\publish"
$env:PLAYWRIGHT_BROWSERS_PATH = "C:\Users\6120715\AppData\Local\ms-playwright"

# Create temp project
$tmpDir = "$env:TEMP\pw_test_$(Get-Random)"
New-Item -ItemType Directory -Path $tmpDir -Force | Out-Null
Set-Content "$tmpDir\Program.cs" $code
@"
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Microsoft.Playwright" Version="1.40.0" />
  </ItemGroup>
</Project>
"@ | Set-Content "$tmpDir\test.csproj"

cd $tmpDir
dotnet run 2>&1
