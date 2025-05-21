using GestionDocumental.WebApi.Features.Users.Entities;
using GestionDocumental.WebApi.Features.Common.Entities;
using Microsoft.EntityFrameworkCore;
using GestionDocumental.WebApi.Features.Common.Dto;
using BaseApi.WebApi.Models;

namespace GestionDocumental.WebApi.Infraestructure
{
    public class GestionDocumentalDbContext : DbContext
    {
        public GestionDocumentalDbContext(DbContextOptions<GestionDocumentalDbContext> options) : base(options)
        {
        }
        public DbSet<User> User { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Theme> Theme { get; set; }
        public DbSet<TypePermission> TypePermission { get; set; }
        public DbSet<Documents> Documents { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new User.Map(modelBuilder.Entity<User>());
            new Permission.Map(modelBuilder.Entity<Permission>());
            new RolePermission.Map(modelBuilder.Entity<RolePermission>());
            new Role.Map(modelBuilder.Entity<Role>());
            new Theme.Map(modelBuilder.Entity<Theme>());
            new TypePermission.Map(modelBuilder.Entity<TypePermission>());
          //  new Documents.Map(modelBuilder.Entity<Documents>());

            base.OnModelCreating(modelBuilder);
        }
    }
}

