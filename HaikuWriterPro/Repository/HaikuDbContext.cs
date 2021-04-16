using Microsoft.EntityFrameworkCore;
using System;

using Models;

namespace Repository
{
    public class HaikuDbContext : DbContext
    {
        public HaikuDbContext(DbContextOptions<HaikuDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<HaikuLine> HaikuLines { get; set; }
        public DbSet<Thread> Threads { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserFav> UserFavs { get; set; }
        public DbSet<Haiku> Haikus { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ManyToManyRelationshipConfiguration(modelBuilder);
        }

        // Method to setup many to many relationship for ef
        private void ManyToManyRelationshipConfiguration(ModelBuilder modelBuilder)
        {
            //Model config for many to many relation for userfav
            modelBuilder.Entity<UserFav>()
                .HasKey(uf => new { uf.Username, uf.HaikuId });

            modelBuilder.Entity<UserFav>()
                .HasOne(uf => uf.User)
                .WithMany(u => u.UserFavs)
                .HasForeignKey(uf => uf.Username);

            modelBuilder.Entity<UserFav>()
                .HasOne(uf => uf.Haiku)
                .WithMany(h => h.UserFavs)
                .HasForeignKey(uf => uf.HaikuId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.HaikuLines)
                .WithOne(hl => hl.User)
                .HasForeignKey(hl => hl.Username);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Threads)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.Username);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Messages)
                .WithOne(m => m.User)
                .HasForeignKey(m => m.Username);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Haikus)
                .WithOne(h => h.User)
                .HasForeignKey(h => h.Username);

        }


    }
}