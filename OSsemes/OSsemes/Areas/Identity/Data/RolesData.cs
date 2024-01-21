using Microsoft.AspNetCore.Identity;

namespace OSsemes.Areas.Identity.Data
{
    public static class RolesData
    {
        private static readonly string[] Roles = new string[] { "Admin", "Reception", "Guest" };     //pridavane role

        public static async Task SeedRoles(IServiceProvider serviceProvider) {     
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())     //taktika na pridanie roli do db (pomohol som si z internetu tutoriály/AI)
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
