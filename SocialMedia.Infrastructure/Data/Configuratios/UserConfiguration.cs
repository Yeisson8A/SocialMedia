using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastructure.Data.Configuratios
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //Nombre de la tabla en la base de datos
            builder.ToTable("Usuario");
            //Clave primaria de la tabla
            builder.HasKey(e => e.Id);
            //Columna Id Usuario
            builder.Property(e => e.Id)
                .HasColumnName("IdUsuario");
            //Columna Nombres
            builder.Property(e => e.FirstName)
                .HasColumnName("Nombres")
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            //Columna Apellidos
            builder.Property(e => e.LastName)
                .HasColumnName("Apellidos")
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            //Columna Email
            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);
            //Columna Fecha Nacimiento
            builder.Property(e => e.DateBirth)
                .HasColumnName("FechaNacimiento")
                .HasColumnType("date");
            //Columna Telefono
            builder.Property(e => e.Telephone)
                .HasColumnName("Telefono")
                .HasMaxLength(10)
                .IsUnicode(false);
            //Columna Activo
            builder.Property(e => e.IsActive)
                .HasColumnName("Activo");
        }
    }
}
