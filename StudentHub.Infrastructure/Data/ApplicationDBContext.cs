using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentHub.Domain;
using StudentHub.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentHub.Infrastructure
{
   public partial class ApplicationDBContext:IdentityDbContext<Domain.Identity.ApplicationUser>
    {
        public PasswordHasher<ApplicationUser> Hasher { get; set; } = new PasswordHasher<ApplicationUser>();
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<StudentHub.Domain.Question> Question { get; set; }
        public DbSet<StudentHub.Domain.Comment> Comment { get; set; }
        public DbSet<StudentHub.Domain.Solution> Solution { get; set; }
        public DbSet<StudentHub.Domain.Image> Image { get; set; }
        public DbSet<StudentHub.Domain.Reaction> Reaction {get; set;}       
        public DbSet<StudentHub.Domain.Tag> Tag { get; set; } 
        public DbSet<StudentHub.Domain.Reply> Reply { get; set; }
        public DbSet<StudentHub.Domain.Join.QuestionTag> QuestionTag { get; set; }
        public DbSet<StudentHub.Domain.Identity.RefreshToken> RefreshToken { get; set; }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedUser(builder);
            
        }


        private void SeedUser(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = Data.Roles.Admin,
                NormalizedName = Data.Roles.Admin,
                Id = Constants.SystemRoleId,
                ConcurrencyStamp = Guid.NewGuid().ToString()
            });


            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Email = Constants.SystemUserEmail,
                Id = Constants.SystemUserId,
                NormalizedEmail = Constants.SystemUserEmail.ToUpper(),
                NormalizedUserName = Constants.SystemUserEmail.ToUpper(),
                UserName = Constants.SystemUserEmail,
                PasswordHash = Hasher.HashPassword(null, Constants.SystemUserPassword),
                SecurityStamp = Guid.NewGuid().ToString()
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = Constants.SystemRoleId,
                UserId = Constants.SystemUserId
            });
        }

    }
}
