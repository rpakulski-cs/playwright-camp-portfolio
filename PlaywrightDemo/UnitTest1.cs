using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightDemo;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Test1()
    {
        //Plawrgitht
        using var playwright = await Playwright.CreateAsync();
        //Browser
        await using var browser = await playwright.Chromium.LaunchAsync(
            new(){ Headless = false, SlowMo = 1000 });
        //Page
        var page = await browser.NewPageAsync();
        await page.GotoAsync("http://www.eaapp.somee.com");
        await page.ClickAsync("text=Login");

        await page.ScreenshotAsync(new(){Path = "eaap.jpg"});

    }
}