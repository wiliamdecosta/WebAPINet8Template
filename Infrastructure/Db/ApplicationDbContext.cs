using Entities.Auth;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Db
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}

		public DbSet<User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<UserRole> UserRoles { get; set; }
		public DbSet<EndpointPath> Endpoints { get; set; }
		public DbSet<EndpointRole> EndpointRoles { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//base.OnModelCreating(modelBuilder);

			//One To Many
			modelBuilder.Entity<User>()
				.HasMany(u => u.UserRole)
				.WithOne(ur => ur.User)
				.HasForeignKey(ur => ur.UserId);

			modelBuilder.Entity<Role>()
				.HasMany(r => r.UserRole)
				.WithOne(ur => ur.Role)
				.HasForeignKey(ur => ur.RoleId);

			modelBuilder.Entity<Role>()
				.HasMany(r => r.EndpointRoles)
				.WithOne(er => er.Role)
				.HasForeignKey(er => er.RoleId);


			//Many To One
			modelBuilder.Entity<EndpointRole>()
			.HasKey(er => er.Id);

			modelBuilder.Entity<EndpointRole>()
				.HasOne(er => er.EndpointPath)
				.WithMany(e => e.EndpointRoles)
				.HasForeignKey(er => er.EndpointId);

			modelBuilder.Entity<EndpointRole>()
				.HasOne(er => er.Role)
				.WithMany(r => r.EndpointRoles)
				.HasForeignKey(er => er.RoleId);


			modelBuilder.Entity<UserRole>()
				.HasOne(ur => ur.Role)
				.WithMany()
				.HasForeignKey(ur => ur.RoleId);

			modelBuilder.Entity<UserRole>()
				 .HasOne(ur => ur.User)
				 .WithMany()
				 .HasForeignKey(ur => ur.UserId);

		}
	}
}
