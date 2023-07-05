using Microsoft.EntityFrameworkCore;
using YAPW.MainDb.DbModels;

namespace YAPW.MainDb
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        /// <summary>
        /// main
        /// </summary>
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Link> Links { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<DbModels.Type> Types { get; set; }
        public virtual DbSet<Video> Videos { get; set; }
        public virtual DbSet<Pornstar> Pornstars { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //base.OnModelCreating(modelBuilder);

        //    ////Handle the active equals true by using a global filter and reflection
        //    ////
        //    //Expression<Func<EntityBase, bool>> filterExpr = bm => bm.Active.Equals(true);
        //    //foreach (var mutableEntityType in modelBuilder.Model.GetEntityTypes())
        //    //{
        //    //    // check if current entity type is child of entity base
        //    //    //
        //    //    if (mutableEntityType.ClrType.IsAssignableTo(typeof(EntityBase)))
        //    //    {
        //    //        // modify expression to handle correct child type
        //    //        var parameter = Expression.Parameter(mutableEntityType.ClrType);
        //    //        var body = ReplacingExpressionVisitor.Replace(filterExpr.Parameters[0], parameter, filterExpr.Body);
        //    //        var lambdaExpression = Expression.Lambda(body, parameter);

        //    //        // set filter
        //    //        //
        //    //        mutableEntityType.SetQueryFilter(lambdaExpression);
        //    //    }
        //    //}
        //}
        //dotnet ef migrations add InitialDb -c DataContext --project../YAPW.MainDb
        //dotnet ef database update
        //ef migrations add InitialDb -c DataContext --project YAPW.MainDb
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseSqlServer(connection, c => c.MigrationsAssembly("YAPW"));
            //server=localhost\\SQLEXPRESS;Database=GameStoreDb;Integrated Security=true;MultipleActiveResultSets=true;
            //optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=YAPWDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False", c => c.MigrationsAssembly("YAPW.MainDb"));
            //optionsBuilder.EnableSensitiveDataLogging(true);
            //optionsBuilder.EnableDetailedErrors(true);
            //if (!optionsBuilder.IsConfigured)
            ////{Data Source=tcp:DEVSERVER,49189;Initial Catalog=ditechDev;Integrated Security=False;User ID=devUser;Password=6RmZcEgZD3b5engcgFGBgq7D;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
            //{
            //    optionsBuilder.UseSqlServer("Data Source=MSIDITECHPROG;Initial Catalog=DitechTestIdentity;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            //}
        }
    }
}