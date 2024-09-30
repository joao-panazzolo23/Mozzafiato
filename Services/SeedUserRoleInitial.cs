using Microsoft.AspNetCore.Identity;

namespace Mozzafiato.Services
{
    public class SeedUserRoleInitial : InterSeedRoleInitial
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInitial(UserManager<IdentityUser> userManager,
               RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedRoles()
        {
            if (!_roleManager.RoleExistsAsync("Member").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Member";
                role.NormalizedName = "MEMBER";
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }
            if (!_roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }
        }

        public void SeedUsers()
        {
            if (_userManager.FindByEmailAsync("admininastro@localhost").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "admininastro";
                user.Email = "admininastro@localhost";
                user.NormalizedEmail = "ADMININASTRO@LOCALHOST";
                user.EmailConfirmed = false;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(user, "senha123").Result;
                if(result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Admin").Wait();
                }

            }
        }
    }
}
