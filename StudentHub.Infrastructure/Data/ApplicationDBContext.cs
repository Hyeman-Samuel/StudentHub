using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentHub.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentHub.Infrastructure
{
   public class ApplicationDBContext:IdentityDbContext<Domain.Identity.ApplicationUser>
    {
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

            builder.Entity<Solution>()
                .HasOne(x => x.Question)
                .WithMany(x => x.Solutions)
                .OnDelete(DeleteBehavior.ClientCascade);
            builder.Entity<Comment>()
                .HasOne(x => x.Question)
                .WithMany(x => x.Comments)
                .OnDelete(DeleteBehavior.ClientCascade);
            builder.Entity<Comment>()
                .HasMany(x => x.Replies)
                .WithOne(x => x.CommentRepliedTo)
                .HasForeignKey(x => x.CommentRepliedToId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }

    }
}
