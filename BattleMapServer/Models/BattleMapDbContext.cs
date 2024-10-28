using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BattleMapServer.Models;

public partial class BattleMapDbContext : DbContext
{
    public BattleMapDbContext()
    {
    }

    public BattleMapDbContext(DbContextOptions<BattleMapDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Character> Characters { get; set; }

    public virtual DbSet<Friend> Friends { get; set; }

    public virtual DbSet<Monster> Monsters { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB;Initial Catalog=BattleMapDB;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Character>(entity =>
        {
            entity.HasKey(e => e.CharaterId).HasName("PK__Characte__220023A1F8306B7D");

            entity.HasOne(d => d.User).WithMany(p => p.Characters).HasConstraintName("FK__Character__UserI__2D27B809");
        });

        modelBuilder.Entity<Friend>(entity =>
        {
            entity.HasOne(d => d.User).WithMany().HasConstraintName("FK__friends__UserId__2F10007B");
        });

        modelBuilder.Entity<Monster>(entity =>
        {
            entity.HasKey(e => e.MonsterId).HasName("PK__Monsters__DC1540267D4DC3EE");

            entity.HasOne(d => d.User).WithMany(p => p.Monsters).HasConstraintName("FK__Monsters__UserId__29572725");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CD4AE6B47");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
