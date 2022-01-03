namespace ApiTests;

[TestClass]
public class CompanyRepositoryTests
{
    private MongoDbContext _context;

    [TestInitialize]
    public void InitializeMongo()
    {
        Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));

        ConfigurationBuilder builder = new ConfigurationBuilder();

        IConfigurationRoot config = builder
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        IConfigurationSection section = config.GetSection("MongoDbConnectionSettings");

        string connectionString = section.GetValue<string>("ConnectionString");
        string databaseName = section.GetValue<string>("DatabaseName");

        _context = new MongoDbContext(connectionString, databaseName);
    }

    [TestMethod]
    public async Task TestAddingCompany()
    {
        CompanyRepository repository = new CompanyRepository(_context);

        Company company = await repository.AddAsync(new Company {
            Id = string.Empty,
            Label = "Test"
        });

        Assert.AreNotEqual(string.Empty, company.Id);

        DeleteResult result = await repository.DeleteAsync(company.Id);

        Assert.AreEqual(1, result.DeletedCount);
    }
}