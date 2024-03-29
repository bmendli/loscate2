﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Loscate.Site.DbContext
{
    public partial class LoscateDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public LoscateDbContext()
        {
        }

        public LoscateDbContext(DbContextOptions<LoscateDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ChatMessage> ChatMessages { get; set; }
        public virtual DbSet<Dialog> Dialogs { get; set; }
        public virtual DbSet<FirebaseUser> FirebaseUsers { get; set; }
        public virtual DbSet<Pin> Pins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=31.31.196.234;Initial Catalog=u1410979_loscate;Persist Security Info=True;User ID=u1410979_loscate;Password=MrNimbus123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("u1410979_loscate")
                .HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<ChatMessage>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("ChatMessage_pk")
                    .IsClustered(false);

                entity.ToTable("ChatMessage");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.HasOne(d => d.Dialog)
                    .WithMany(p => p.ChatMessages)
                    .HasForeignKey(d => d.DialogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ChatMessage_Dialog_Id_fk");

                entity.HasOne(d => d.SendUser)
                    .WithMany(p => p.ChatMessages)
                    .HasForeignKey(d => d.SendUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ChatMessage_FirebaseUser_Id_fk");
            });

            modelBuilder.Entity<Dialog>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Dialog_pk")
                    .IsClustered(false);

                entity.ToTable("Dialog");

                entity.HasOne(d => d.UserId1Navigation)
                    .WithMany(p => p.DialogUserId1Navigations)
                    .HasForeignKey(d => d.UserId1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Dialog_FirebaseUser_Id_fk");

                entity.HasOne(d => d.UserId2Navigation)
                    .WithMany(p => p.DialogUserId2Navigations)
                    .HasForeignKey(d => d.UserId2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Dialog_FirebaseUser_Id_fk_2");
            });

            modelBuilder.Entity<FirebaseUser>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("FirebaseUser_pk")
                    .IsClustered(false);

                entity.ToTable("FirebaseUser");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PhotoBase64).IsUnicode(false);

                entity.Property(e => e.PictureUrl)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.Uid)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("UID");
            });

            modelBuilder.Entity<Pin>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Pin_pk")
                    .IsClustered(false);

                entity.ToTable("Pin");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AddDate)
                    .HasColumnType("datetime")
                    .HasColumnName("addDate");

                entity.Property(e => e.FullName)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("fullName");

                entity.Property(e => e.Latitude).HasColumnName("latitude");

                entity.Property(e => e.Longitude).HasColumnName("longitude");

                entity.Property(e => e.Photo)
                    .IsUnicode(false)
                    .HasColumnName("photo");

                entity.Property(e => e.ShortName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("shortName");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Pins)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Pin_FirebaseUser_Id_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
