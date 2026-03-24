using Microsoft.Playwright;

namespace playwright_camp_portfolio;

public class LoginPage
{
    private readonly IPage _page;

    private ILocator _username => _page.Locator("[data-test='username']");
    private ILocator _password => _page.Locator("[data-test='password']");
    private ILocator _loginBtn => _page.GetByRole(AriaRole.Button, new() {Name = "Login"});

    public LoginPage(IPage page)
    {
        _page = page;
    }

    public async Task Navigate()
    {
        await _page.GotoAsync("https://www.saucedemo.com");
    }

    public async Task Login(string username, string password)
    {
        await FillCredentials(username, password);
        await ClickLogin();
    }

    private async Task ClickLogin() => await _loginBtn.ClickAsync();

    private async Task FillCredentials(string username, string password)
    {
        await _username.FillAsync(username);
        await _password.FillAsync(password);
    }


}
