using DoctorToothieApp.Persistence.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DoctorToothieApp.Persistence.Extensions;

public static class ModelBuilderExtensionSeeder
{
    public static void SeedRole(this ApplicationDbContext context)
    {
        if (!context.Roles.Any())
        {
            context.Roles.AddRange(
                new IdentityRole
                {
                    Id = "1E2B8D51-DA03-4920-B675-E0504ED8E7FF",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "19B13701-0451-412B-B80E-6FD559437F53"
                },
                new IdentityRole
                {
                    Id = "9F753729-3A0F-4AC0-8130-11E1133A8DF6",
                    Name = "Doctor",
                    NormalizedName = "DOCTOR",
                    ConcurrencyStamp = "1CA2811D-872D-49AD-8F4B-4364B7D23FBC"
                },
                new IdentityRole
                {
                    Id = "B3CCBED4-1866-4323-A06B-ED7D3BBDB3C4",
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "27D16A8F-8C32-4F46-BFEE-2CB8A3AA8B10"
                }
            );
            context.SaveChanges();
        }
    }

    public static void SeedAdminUser(this ApplicationDbContext context)
    {
        var hasher = new PasswordHasher<AppUser>();

        if (!context.Users.Any(u => u.UserName == "admin@admin.com"))
        {
            var admin = new AppUser
            {
                Id = "A9E65B22-97D2-4B63-8C3F-1154D6FA5422",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                FirstName = "System",
                LastName = "Admin",
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            admin.PasswordHash = hasher.HashPassword(admin, "123");

            context.Users.Add(admin);
            context.SaveChanges();

            context.UserRoles.Add(new IdentityUserRole<string>
            {
                UserId = admin.Id,
                RoleId = "1E2B8D51-DA03-4920-B675-E0504ED8E7FF" // Admin role
            });

            context.SaveChanges();
        }
    }


}
