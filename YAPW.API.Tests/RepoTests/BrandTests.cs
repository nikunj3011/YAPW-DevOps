using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using YAPW.Domain.Repositories.Main;
using YAPW.MainDb;
using YAPW.MainDb.DbModels;

namespace YAPW.API.Tests.RepoTests;

public class BrandTests
{
    private DbContextOptionsBuilder<DataContext> dbOptionsBuilder;
    private DataContext dataContext;
    private BrandRepository<Brand, DataContext> repo;

    [Fact]
    public void GetBrands()
    {

        dbOptionsBuilder = new DbContextOptionsBuilder<DataContext>();
        dbOptionsBuilder.EnableSensitiveDataLogging(true);
        dbOptionsBuilder.EnableDetailedErrors();
        dbOptionsBuilder.UseSqlServer("Data Source=JR-PROG\\SQLEXPRESS;Initial Catalog=YAPWDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False", c => c.MigrationsAssembly("YAPW.MainDb"));

        dataContext = new DataContext(dbOptionsBuilder.Options);
        repo = new BrandRepository<Brand, DataContext>(dataContext);

        // arrange
        var brands = new List<Brand>();

        // act
        int? count = repo.FindCount();

        // assert
        //var brand = Assert.Single(count);
        Assert.NotNull(count);

    }
}