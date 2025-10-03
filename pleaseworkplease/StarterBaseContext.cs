using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace pleaseworkplease;

public partial class StarterBaseContext : DbContext
{
    public StarterBaseContext()
    {
    }

    public StarterBaseContext(DbContextOptions<StarterBaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Novel> Novels { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LiteratureArchive");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Novel>(entity =>
        {
            entity.HasOne(d => d.GenreNavigation).WithMany(p => p.Novels)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Novels_Genre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
