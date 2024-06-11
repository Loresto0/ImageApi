using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Context;

public partial class ItbaseContext : DbContext
{
    public ItbaseContext()
    {
    }

    public ItbaseContext(DbContextOptions<ItbaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Image> Images { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Ваши данные");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("images_pk");

            entity.ToTable("images");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Identifier).HasColumnName("identifier");
            entity.Property(e => e.Imagebase64).HasColumnName("imagebase64");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
