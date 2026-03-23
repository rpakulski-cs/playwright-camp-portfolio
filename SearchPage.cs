using Microsoft.Playwright;

namespace playwright_camp_portfolio;

public class SearchPage
{
    private readonly IPage _page;

    private ILocator _username => _page.Locator("[data-test='username']");
    private ILocator _login => _page.Locator("[data-test='login-button']");

    public SearchPage(IPage page)
    {
        _page = page;
    }

    public async Task Navigate()
    {
        await _page.GotoAsync("https://www.saucedemo.com");
    }

    public async Task ClickLogin()
    {
        await _login.ClickAsync();
    }

    public async Task FillUsername(string username)
    {
        await _username.FillAsync(username);
    }


}
