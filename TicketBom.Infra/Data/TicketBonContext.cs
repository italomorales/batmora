using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyModel;

using TicketBom.Domain;
using TicketBom.Domain.Entities;
using TicketBom.Domain.Entities.AccountAggregate;
using TicketBom.Domain.Entities.TicketAggregate;
using TicketBom.Infra.Data.Configurations;
using TicketBom.Infra.User;

namespace TicketBom.Infra.Data
{
    public class TicketBomContext : DbContext
    {
        private readonly IUserAccessor _userAccessor;

        private static IList<Type> _entityTypeCache;
        private static readonly MethodInfo SetGlobalQueryMethod = typeof(TicketBomContext).GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Single(t => t.IsGenericMethod && t.Name == "SetGlobalQuery");

        public TicketBomContext(DbContextOptions<TicketBomContext> options, IUserAccessor userAccessor)
            : base(options)
        {
            _userAccessor = userAccessor;
        }
        
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Domain.Entities.AccountAggregate.User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PointOfSale> PointOfSales { get; set; }
        public DbSet<FinancialEvent> FinancialEvents { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<ItemSale> ItemsSale { get; set; }

        public override int SaveChanges()
        {
            var addedEntities = ChangeTracker.Entries().Where(c => c.State == EntityState.Added).Select(c => c.Entity).OfType<TenantEntity>();

            var tenantId = _userAccessor.TenantId;

            foreach (var entity in addedEntities)
            {
                entity.TenantId = tenantId;
            }

            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            var addedEntities = ChangeTracker.Entries().Where(c => c.State == EntityState.Added).Select(c => c.Entity).OfType<TenantEntity>();

            var tenantId = _userAccessor.TenantId;

            foreach (var entity in addedEntities)
            {
                entity.TenantId = tenantId;
            }

            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            foreach (var type in GetEntityTypes())
            {
                var method = SetGlobalQueryMethod.MakeGenericMethod(type);
                method.Invoke(this, new object[] { modelBuilder });
            }

            modelBuilder.Ignore<Notifiable>();
            modelBuilder.Ignore<Notification>();

            modelBuilder.ApplyConfiguration(new TenantConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ProfileConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new PointOfSaleConfiguration());
            modelBuilder.ApplyConfiguration(new FinancialEventConfiguration());
            modelBuilder.ApplyConfiguration(new EventTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SaleConfiguration());
            modelBuilder.ApplyConfiguration(new ItemSaleConfiguration());

            Seed(modelBuilder);
        }

        private static void Seed(ModelBuilder modelBuilder)
        {
            var tenants = new List<Tenant>
                {new Tenant("Cliente 01") {Id = new Guid("9693682c-e613-4945-9bc9-df379f702d69").ToString()}};

            modelBuilder.Entity<Tenant>().HasData(tenants);
        }

        private static IEnumerable<Type> GetEntityTypes()
        {
            if (_entityTypeCache != null) return _entityTypeCache.ToList();

            _entityTypeCache = (from a in GetReferencingAssemblies()
                from t in a.DefinedTypes
                where t.BaseType == typeof(TenantEntity)
                select t.AsType()).ToList();

            return _entityTypeCache;
        }

        private static IEnumerable<Assembly> GetReferencingAssemblies()
        {
            var assemblies = new List<Assembly>();
            var dependencies = DependencyContext.Default.RuntimeLibraries;

            foreach (var library in dependencies)
            {
                try
                {
                    var assembly = Assembly.Load(new AssemblyName(library.Name));
                    assemblies.Add(assembly);
                }
                catch (FileNotFoundException)
                { }
            }

            return assemblies;
        }

        public void SetGlobalQuery<T>(ModelBuilder builder) where T : TenantEntity
        {        
            builder.Entity<T>().HasKey(e => e.Id);
            builder.Entity<T>().HasQueryFilter(e => e.TenantId == _userAccessor.TenantId);
        }
        

    }
}
