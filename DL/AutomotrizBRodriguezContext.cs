using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DL
{
    public partial class AutomotrizBRodriguezContext : DbContext
    {
        public AutomotrizBRodriguezContext()
        {
        }

        public AutomotrizBRodriguezContext(DbContextOptions<AutomotrizBRodriguezContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Agencium> Agencia { get; set; } = null!;
        public virtual DbSet<Automovil> Automovils { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.; Database= AutomotrizBRodriguez; TrustServerCertificate=True; User ID=sa; Password=pass@word1;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agencium>(entity =>
            {
                entity.HasKey(e => e.IdAgencia)
                    .HasName("PK__Agencia__00863C7D004214AA");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Automovil>(entity =>
            {
                entity.HasKey(e => e.IdAutomovil)
                    .HasName("PK__Automovi__3D90B2E31F86B1A4");

                entity.ToTable("Automovil");

                entity.Property(e => e.Color)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.FechaLanzamiento).HasColumnType("datetime");

                entity.Property(e => e.Generacion)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Marca)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Modelo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdAgenciaNavigation)
                    .WithMany(p => p.Automovils)
                    .HasForeignKey(d => d.IdAgencia)
                    .HasConstraintName("FK__Automovil__IdAge__1273C1CD");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
