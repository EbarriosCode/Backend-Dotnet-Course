using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AdminProjectsDemo.DataContext.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var _context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {                               
                if (_context.Roles.Any(x => x.Name.Equals("Invitado")) && _context.Roles.Any(x => x.Name.Equals("Administrador")))
                    return;

                _context.Roles.AddRange(
                    new IdentityRole { Name = "Invitado", NormalizedName = "Invitado" },
                    new IdentityRole { Name = "Administrador", NormalizedName = "Administrador" }
                 );

                _context.SaveChanges();
            }
        }
    }
}