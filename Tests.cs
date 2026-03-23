using Microsoft.Playwright.NUnit;

namespace playwright_camp_portfolio;

public class Tests : PageTest
{

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Test1()
    {
        var page = new SearchPage(Page);
        await page.Navigate();
        await page.FillUsername("Dzialam!!!!");

        await page.ClickLogin();

        Assert.IsTrue(Page.Url.Contains("saucedemo"));
    }
}