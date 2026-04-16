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

    //[Test]
    public async Task Test1()
    {
        //Plawrgitht
        using var playwright = await Playwright.CreateAsync();
        //Browser
        await using var browser = await playwright.Chromium.LaunchAsync(
            new(){ Headless = false, SlowMo = 1 });
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

    //[Test]
    public async Task TestWithPOM()
    {
        //Plawrgitht
        using var playwright = await Playwright.CreateAsync();
        //Browser
        await using var browser = await playwright.Chromium.LaunchAsync(
            new(){ Headless = false, SlowMo = 100 });
        //Page
        var page = await browser.NewPageAsync();
        await page.GotoAsync("http://www.eaapp.somee.com");
        
        LoginPage loginPage = new(page);
        await loginPage.ClickLogin();
        await loginPage.Login("admin", "password");
        

        var isExist = await loginPage.IsHelloAdminExists();
        Assert.IsTrue(isExist);

        await loginPage.ClickViewEmployees();

    }
    
    [Test]
    public async Task TestNetwork()
    {
        //Plawrgitht
        using var playwright = await Playwright.CreateAsync();
        //Browser
        await using var browser = await playwright.Chromium.LaunchAsync(
            new(){ Headless = true, SlowMo = 1 });
        //Page
        var page = await browser.NewPageAsync();
        await page.GotoAsync("http://www.eaapp.somee.com");
        
        LoginPage loginPage = new(page);
        await loginPage.ClickLogin();

        await loginPage.Login("admin", "password");
        
        var isExist = await loginPage.IsHelloAdminExists();
        Assert.IsTrue(isExist);


        //var waitResponse = page.WaitForResponseAsync("**/Employee");
        //await loginPage.ClickViewEmployees();
        //var getResponse = await waitResponse;
        
        var response = await page.RunAndWaitForResponseAsync(async () =>
        {
            await loginPage.ClickViewEmployees();
        }, x => x.Url.Contains("/Employee"));


        //Console.WriteLine(getResponse.ResponseAsync());

    }
}