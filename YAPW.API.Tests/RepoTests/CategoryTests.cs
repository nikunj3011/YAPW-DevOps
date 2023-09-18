using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using YAPW.Domain.Repositories.Main;
using YAPW.MainDb;
using YAPW.MainDb.DbModels;

namespace YAPW.API.Tests.RepoTests;

public class CategoryTests
{
    private DbContextOptionsBuilder<DataContext> dbOptionsBuilder;
    private DataContext dataContext;
    private CategoryRepository<Category, DataContext> repo;

    [Fact]
    public void GetCategories()
    {

        dbOptionsBuilder = new DbContextOptionsBuilder<DataContext>();
        dbOptionsBuilder.EnableSensitiveDataLogging(true);
        dbOptionsBuilder.EnableDetailedErrors();
        dbOptionsBuilder.UseSqlServer("Data Source=JR-PROG\\SQLEXPRESS;Initial Catalog=YAPWDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False", c => c.MigrationsAssembly("YAPW.MainDb"));

        dataContext = new DataContext(dbOptionsBuilder.Options);
        repo = new CategoryRepository<Category, DataContext>(dataContext);

        // arrange
        var Categorys = new List<Category>();

        // act
        int? count = repo.FindCount();

        // assert
        //var Category = Assert.Single(count);
        Assert.NotNull(count);

    }
}