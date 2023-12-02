using Microsoft.AspNetCore.Identity;

namespace OSsemes.Areas.Identity.Data
{
    public static class RolesData
    {
        private static readonly string[] Roles = new string[] { "Admin", "Reception" };     //pridavane role

        public static async Task SeedRoles(IServiceProvider serviceProvider) {     
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())     //taktika na pridanie roli do db
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                foreach (var role in Roles)         //pridavanie jednotlivych roli
                {
                    if (!await roleManager.RoleExistsAsync(role))       //ak neexistuje rola
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));  //tak prida rolu
                    }
                }
            }
        }
    }
}
