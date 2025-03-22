using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Moq;
using Shopi.Product.Domain.Entities;
using Shopi.Product.Infrastructure.Data;
using Shopi.Product.Infrastructure.Repositories;

namespace Shopi.Product.Tests.Unit.Infrastructure.Repositories;

public class CategoryWriteRepositoryTests
{
    private CategoryWriteRepository _repository;
    private readonly AppProductDbContext _dbContext;

    public CategoryWriteRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AppProductDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _dbContext = new AppProductDbContext(options);

        _repository = new CategoryWriteRepository(_dbContext);
    }

    [Fact]
    public async Task Create_ShouldReturnACategory_WhenCreationSucceed()
    {
        var faker = new Faker<Category>()
            .RuleFor(c => c.Id, f => Guid.NewGuid())
            .RuleFor(c => c.Name, f => f.Commerce.Categories(1).First())
            .RuleFor(c => c.Description, f => f.Lorem.Sentence())
            .RuleFor(c => c.IsActive, f => f.Random.Bool());

        var category = faker.Generate();

        var result = await _repository.Create(category);

        Assert.NotNull(result);
        Assert.Equal(category.Id, result.Id);
        Assert.Equal(category.Name, result.Name);
        Assert.Equal(category.Description, result.Description);

        var savedCategory = await _dbContext.Categories.FindAsync(result.Id);
        Assert.NotNull(savedCategory);
        Assert.Equal(category.Id, savedCategory.Id);
    }
}