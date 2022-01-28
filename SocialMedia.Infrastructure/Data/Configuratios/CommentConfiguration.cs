using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastructure.Data.Configuratios
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            //Nombre de la tabla en la base de datos
            builder.ToTable("Comentario");
            //Clave primaria de la tabla
            builder.HasKey(e => e.CommentId);
            //Columna Id Comentario
            builder.Property(e => e.CommentId)
                .HasColumnName("IdComentario")
                .ValueGeneratedNever();
            //Columna Id Publicación
            builder.Property(e => e.PostId)
                .HasColumnName("IdPublicacion");
            //Columna Id Usuario
            builder.Property(e => e.UserId)
                .HasColumnName("IdUsuario");
            //Columna Descripción
            builder.Property(e => e.Description)
                .HasColumnName("Descripcion")
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);
            //Columna Fecha
            builder.Property(e => e.Date)
                .HasColumnName("Fecha")
                .HasColumnType("datetime");
            //Columna Activo
            builder.Property(e => e.IsActive)
                .HasColumnName("Activo");
            //Relación foranea Comentario-Publicación
            builder.HasOne(d => d.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comentario_Publicacion");
            //Relación foranea Comentario-Usuario
            builder.HasOne(d => d.User)
                .WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comentario_Usuario");
        }
    }
}
