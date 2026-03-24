using Microsoft.Playwright.NUnit;

namespace playwright_camp_portfolio;

public class Tests : PageTest
{

    [SetUp]
    public void Setup()
    {
    }

    [TestCase("standard_user", "secret_sauce")]
    public async Task ShouldLoginSuccesfully(string username, string passowrd)
    {
        var page = new LoginPage(Page);
        await page.Navigate();

        await page.Login(username, passowrd);

        await Expect(Page).ToHaveURLAsync("https://www.saucedemo.com/inventory.html");
    }
}