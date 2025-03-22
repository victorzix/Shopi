using System.Net;
using System.Net.Http.Json;
using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Shopi.Product.Application.DTOs;

namespace Shopi.Product.Tests.Integration.Category;

[TestFixture]
public class CategoryControllerIntegrationTests
{
    private HttpClient _client;
    private WebApplicationFactory<Program> _factory;

    [SetUp]
    public void SetUp()
    {
        _factory = new WebApplicationFactory<Program>();
        _client = _factory.CreateClient();
    }

    [TearDown]
    public void TearDown()
    {
        _client.Dispose();
        _factory.Dispose();
    }

    [Test]
    public async Task FilterCategories_ShouldReturnFilteredResults()
    {
        var query = new FilterCategoriesDto
        {
            Name = "Electronics",
            Visible = true,
            Limit = 3,
        };

        var response = await _client.GetAsync($"/Category/filter?name={query.Name}&visible={query.Visible}");
        var responseData = response.Content.ReadAsStringAsync();
        var mapper = JsonConvert.DeserializeObject(responseData.Result);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var result = await response.Content.ReadFromJsonAsync<object>();
        Assert.IsNotNull(result);
        Assert.That(result, Is.Not.Empty);
    }
}