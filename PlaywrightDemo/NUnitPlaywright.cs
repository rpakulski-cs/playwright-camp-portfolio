using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightDemo;

public class NUnitPlaywright : PageTest
{
    [SetUp]
    public async Task Setup()
    {
        await Page.GotoAsync("http://www.eaapp.somee.com");
    }

    [Test]
    public async Task Test2()
    {
        //Page.SetDefaultTimeout(1000);
        var loginBtn= Page.Locator("text=Login");
        await loginBtn.ClickAsync();
        await Page.FillAsync("#UserName", "admin");
        await Page.FillAsync("#Password", "password");

       // await Page.GetByRole(AriaRole.Button, new(){Name = "Sign In"}).ClickAsync();

        var signInBtn= Page.Locator("button", new (){HasTextString = "Sign In"});
        await signInBtn.ClickAsync();

        await Expect(Page.Locator("text='Hello admin!'")).ToBeVisibleAsync();

        await Page.ScreenshotAsync(new(){Path = "eaap.jpg"});
    }
}