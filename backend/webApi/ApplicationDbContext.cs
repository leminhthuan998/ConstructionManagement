﻿using ConstructionApp.Entity;
using ConstructionApp.Entity.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionApp
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Vehicle>();

            #region UserRole Entity
            builder.Entity<User>(b =>
            {
                b.HasMany<UserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
            });

            builder.Entity<Role>(b =>
            {
                b.HasKey(r => r.Id);
                b.HasIndex(r => r.NormalizedName).IsUnique();
                b.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

                b.Property(u => u.Name).HasMaxLength(256);
                b.Property(u => u.NormalizedName).HasMaxLength(256);

                b.HasMany<UserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();
                b.HasMany<RoleClaim>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();
            });

            builder.Entity<RoleClaim>(b =>
            {
                b.HasKey(rc => rc.Id);
            });

            builder.Entity<UserRole>(b =>
            {
                b.HasKey(r => new { r.UserId, r.RoleId });
            });

            builder.Entity<UserClaim>(b =>
            {
                b.HasKey(rc => rc.Id);
            });

            builder.Entity<UserToken>(b =>
            {
                b.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
            });

            builder.Entity<UserLogin>(b =>
            {
                b.HasKey(t => new { t.UserId, t.LoginProvider, t.ProviderKey });
            });

            builder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.UserId });

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
            #endregion

        }
    }
}
