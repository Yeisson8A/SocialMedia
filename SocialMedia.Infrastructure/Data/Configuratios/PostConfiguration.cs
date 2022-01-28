using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastructure.Data.Configuratios
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            //Nombre de la tabla de base de datos
            builder.ToTable("Publicacion");
            //Clave primaria de la tabla
            builder.HasKey(e => e.PostId);
            //Columna Id Publicacion
            builder.Property(e => e.PostId)
                .HasColumnName("IdPublicacion");
            //Columna Id Usuario
            builder.Property(e => e.UserId)
                .HasColumnName("IdUsuario");
            //Columna Fecha
            builder.Property(e => e.Date)
                .HasColumnName("Fecha")
                .HasColumnType("datetime");
            //Columna Descripción
            builder.Property(e => e.Description)
                .HasColumnName("Descripcion")
                .IsRequired()
                .HasMaxLength(1000)
                .IsUnicode(false);
            //Columna Imagen
            builder.Property(e => e.Image)
                .HasColumnName("Imagen")
                .HasMaxLength(500)
                .IsUnicode(false);
            //Relación foranea Publicacion-Usuario
            builder.HasOne(d => d.User)
                .WithMany(p => p.Posts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Publicacion_Usuario");
        }
    }
}
