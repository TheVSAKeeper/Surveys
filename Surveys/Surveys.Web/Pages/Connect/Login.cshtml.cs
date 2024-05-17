using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Surveys.Infrastructure;
using Surveys.Web.Application.Services;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Surveys.Web.Pages.Connect;

[AllowAnonymous]
public class LoginModel(
    IAccountService accountService,
    SignInManager<ApplicationUser> signInManager,
    UserManager<ApplicationUser> userManager)
    : PageModel
{
    private const string Username = nameof(Input.UserName);
    [BindProperty(SupportsGet = true)] public string ReturnUrl { get; set; } = null!;

    [BindProperty] public LoginViewModel? Input { get; set; }

    public void OnGet() => Input = new LoginViewModel
    {
        ReturnUrl = ReturnUrl
    };

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid == false)
            return Page();

        if (Input != null)
        {
            ApplicationUser? user = await userManager.FindByNameAsync(Input.UserName);

            if (user == null)
            {
                ModelState.AddModelError(Username, "Пользователь не найден");
                return Page();
            }

            SignInResult signInResult = await signInManager.PasswordSignInAsync(user, Input.Password, true, false);

            if (signInResult.Succeeded)
            {
                ClaimsPrincipal principal = await accountService.GetPrincipalByIdAsync(user.Id.ToString());
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                if (Url.IsLocalUrl(ReturnUrl))
                    return Redirect(ReturnUrl);

                return RedirectToPage("/swagger");
            }
        }

        ModelState.AddModelError(Username, "Пользователь не найден");
        return Page();
    }
}