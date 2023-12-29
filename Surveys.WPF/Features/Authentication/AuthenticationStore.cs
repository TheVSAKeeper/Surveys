using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Surveys.Infrastructure;
using Surveys.WPF.Properties;

namespace Surveys.WPF.Features.Authentication;

public class AuthenticationStore(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
{
    public ApplicationUser? User { get; private set; }

    public bool IsLoggedIn => User != null;

    public bool IsInRole(string roleName)
    {
        return User != null && Task.Run(async () => await userManager.IsInRoleAsync(User, roleName)).Result;
    }

    public async Task Initialize()
    {
        string userIdJson = Settings.Default.User;

        if (string.IsNullOrEmpty(userIdJson))
            return;

        Guid? userId = JsonSerializer.Deserialize<Guid>(userIdJson);

        if (userId == null)
            return;

        User = await userManager.FindByIdAsync(userId.ToString()!);

        SaveAuthenticationState();
    }

    public async Task<SignInResult> SignInAsync(string userName, string password)
    {
        ApplicationUser? user = await userManager.FindByNameAsync(userName);

        if (user == null)
            return SignInResult.Failed;

        bool result = await userManager.CheckPasswordAsync(user, password);

        if (result == false)
            return SignInResult.Failed;

        User = user;
        SaveAuthenticationState();

        return SignInResult.Success;
    }

    public void SignOut()
    {
        User = null;
        ClearAuthenticationState();
    }

    private void SaveAuthenticationState()
    {
        if (User == null)
            return;

        string userIdJson = JsonSerializer.Serialize(User.Id);
        Settings.Default.User = userIdJson;
        Settings.Default.Save();
    }

    private void ClearAuthenticationState()
    {
        Settings.Default.User = null;
        Settings.Default.Save();
    }

    public async Task<IdentityResult> CreateUserAsync(string username, string password, string roleName)
    {
        ApplicationRole? role = await roleManager.FindByNameAsync(roleName);

        if (role == null)
            return IdentityResult.Failed();

        ApplicationUser user = new()
        {
            Id = Guid.NewGuid(),
            UserName = username,
            NormalizedUserName = username.ToUpper(),
            FirstName = string.Empty,
            LastName = string.Empty,
            Patronymic = string.Empty,
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString("D"),
            Roles = new List<ApplicationRole>
            {
                role
            }
        };

        IdentityResult createResult = await userManager.CreateAsync(user, password);

        return createResult;
    }

    public async Task UpdateUserAsync(Guid id)
    {
        User = await userManager.FindByIdAsync(id.ToString());
    }
}