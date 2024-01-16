using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IntroAsp.Models;

public partial class PubContext : DbContext
{
    public PubContext()
    {
    }

    public PubContext(DbContextOptions<PubContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Beer> Beers { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:PUB");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Beer>(entity =>
        {
            entity.HasKey(e => e.IdBeer).HasName("PK__Beer__F381132EBDF3E88B");

            entity.ToTable("Beer");

            entity.Property(e => e.IdBeer)
                .ValueGeneratedNever()
                .HasColumnName("ID_Beer");
            entity.Property(e => e.ID_Brand).HasColumnName("ID_Brand");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.ID_BrandNavigation).WithMany(p => p.Beers)
                .HasForeignKey(d => d.ID_Brand)
                .HasConstraintName("FK__Beer__ID_Brand__3B75D760");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.ID_Brand).HasName("PK__Brand__290988DCEEB41F3D");

            entity.ToTable("Brand");

            entity.Property(e => e.ID_Brand)
                .ValueGeneratedNever()
                .HasColumnName("ID_Brand");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
