using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        ApplicationUser user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        if (user is null)
            return false;

        return await _userManager.IsInRoleAsync(user, role);
    }
}

