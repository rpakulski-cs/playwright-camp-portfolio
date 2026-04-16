using Microsoft.Playwright;

namespace PlaywrightDemo;

public class LoginPage
{
    private IPage _page;

    private readonly ILocator _lnkLogin;
    private readonly ILocator _txtUserName;
    private readonly ILocator _txtPassword;
    private readonly ILocator _btnSignIn;
    private readonly ILocator _txtHelloAdmin;

    private readonly ILocator _lnkEmployees;

    public LoginPage(IPage page)
    {
        _page = page;
        _lnkLogin = _page.Locator("text=Login");
        _txtUserName = _page.Locator("#UserName");
        _txtPassword = _page.Locator("#Password");
        _btnSignIn = _page.Locator("button", new (){HasTextString = "Sign In"});
        _lnkEmployees = _page.GetByRole(AriaRole.Link, new (){Name = " View Employees"});//("", new (){HasTextString = "Employees"});
        _txtHelloAdmin = _page.Locator("text='Hello admin!'");
    }

    public async Task ClickLogin()
    {
        await _page.RunAndWaitForNavigationAsync(async() =>
        {
            await _lnkLogin.ClickAsync();
        }, new PageRunAndWaitForNavigationOptions
        {
            UrlString = "**/Login"
        });
    }

    public async Task ClickViewEmployees()
    {
        await _lnkEmployees.ClickAsync();
    }

    public async Task Login(string username, string password)
    {
        await _txtUserName.FillAsync(username);
        await _txtPassword.FillAsync(password);
        await _btnSignIn.ClickAsync();
    }

    public async Task<bool> IsHelloAdminExists() 
        => await _txtHelloAdmin.IsVisibleAsync();
}
