using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentHub.Core;
using StudentHub.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentHub.Infrastructure.Data.Mapping
{
    class UserMap : IEntityTypeConfiguration<ApplicationUser>
    {
        public PasswordHasher<ApplicationUser> Hasher { get; set; } = new PasswordHasher<ApplicationUser>();
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            // builder.ToTable("AspNetUsers");
            //builder.Property(h => h.FirstName).HasMaxLength(100).IsUnicode(false);

            SeedUsers(builder);
        }

        private void SeedUsers(EntityTypeBuilder<ApplicationUser> builder)
        {
            var systemUser = new ApplicationUser
            {
                //CreationTime = DateTime.UtcNow,
                Email = CoreConstants.SystemUserEmail,
                //FirstName = "Sytem",
                //LastName = "Admin",
                Id = CoreConstants.SystemUserId,
                NormalizedEmail = CoreConstants.SystemUserEmail.ToUpper(),
                NormalizedUserName = CoreConstants.SystemUserEmail.ToUpper(),
                UserName = CoreConstants.SystemUserEmail,
                PasswordHash = Hasher.HashPassword(null, "P@ssw0rd"),
                SecurityStamp = Guid.NewGuid().ToString()
            };






            //builder.HasData(systemUser);
        }
    }
}
