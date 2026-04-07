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


        await page.FillAsync("#UserName", "admin");
        await page.FillAsync("#Password", "password");
        await page.GetByRole(AriaRole.Button, new(){Name = "Sign In"}).ClickAsync();

        await page.ScreenshotAsync(new(){Path = "eaap.jpg"});

        var isExist = await page.Locator("text='Hello admin!'").IsVisibleAsync();
        Assert.IsTrue(isExist);

    }
}