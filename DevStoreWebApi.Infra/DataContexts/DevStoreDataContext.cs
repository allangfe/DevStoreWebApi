using DevStoreWebApi.Domain;
using DevStoreWebApi.Infra.Mappings;
using System.Data.Entity;

namespace DevStoreWebApi.Infra.DataContexts
{
    public class DevStoreDataContext : DbContext
    {
        public DevStoreDataContext() : base("DevStoreConnectionString")
        {
            Database.SetInitializer<DevStoreDataContext>(new DevStoreDataContextInitializer());
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public IDbSet<Product> Products { get; set; }
        public IDbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new CategoryMap());

            base.OnModelCreating(modelBuilder);
        }
    }

    public class DevStoreDataContextInitializer : DropCreateDatabaseIfModelChanges<DevStoreDataContext>
    {
        protected override void Seed(DevStoreDataContext context)
        {
            context.Categories.Add(new Category { Id = 1, Title = "Informática" });
            context.Categories.Add(new Category { Id = 2, Title = "Games" });
            context.Categories.Add(new Category { Id = 3, Title = "Papelaria" });
            context.SaveChanges();

            context.Products.Add(new Product { Id = 1, CategoryId = 2, IsActive = true, Title = "Uncharted 3" });
            context.Products.Add(new Product { Id = 2, CategoryId = 2, IsActive = true, Title = "Uncharted 4" });
            context.Products.Add(new Product { Id = 3, CategoryId = 1, IsActive = true, Title = "GForce 1070" });
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
