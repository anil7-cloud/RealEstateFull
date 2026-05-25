using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using RealEstateApp.Web.Models;

namespace RealEstateApp.Web.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(
        DbContextOptions<AppDbContext> options
    ) : base(options)
    {
    }

    // TABLES

    public DbSet<Favorite> Favorites => Set<Favorite>();

    public DbSet<Agency> Agencies => Set<Agency>();

    public DbSet<Listing> Listings => Set<Listing>();

    public DbSet<Lead> Leads => Set<Lead>();

    public DbSet<Appointment> Appointments => Set<Appointment>();

    public DbSet<ListingImage> ListingImages => Set<ListingImage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // AGENCY SLUG

        modelBuilder.Entity<Agency>()
            .HasIndex(x => x.Slug)
            .IsUnique();

        // LISTING SLUG

        modelBuilder.Entity<Listing>()
            .HasIndex(x => x.Slug)
            .IsUnique();

        // RELATIONS

        modelBuilder.Entity<Listing>()
            .HasOne(x => x.Agency)
            .WithMany(x => x.Listings)
            .HasForeignKey(x => x.AgencyId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Lead>()
            .HasOne(x => x.Listing)
            .WithMany()
            .HasForeignKey(x => x.ListingId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Lead>()
            .HasOne(x => x.Agency)
            .WithMany()
            .HasForeignKey(x => x.AgencyId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ListingImage>()
            .HasOne(x => x.Listing)
            .WithMany(x => x.Images)
            .HasForeignKey(x => x.ListingId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}