using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SocialMedia.Core.Entities;
using SocialMedia.Infrastructure.Data.Configuratios;

namespace SocialMedia.Infrastructure.Data
{
    public partial class SocialMediaContext : DbContext
    {
        public SocialMediaContext()
        {
        }

        public SocialMediaContext(DbContextOptions<SocialMediaContext> options) : base(options)
        {
        }

        //Entidades
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Entidad comentario
            modelBuilder.ApplyConfiguration(new CommentConfiguration());

            //Entidad Publicación
            modelBuilder.ApplyConfiguration(new PostConfiguration());

            //Entidad Usuario
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
