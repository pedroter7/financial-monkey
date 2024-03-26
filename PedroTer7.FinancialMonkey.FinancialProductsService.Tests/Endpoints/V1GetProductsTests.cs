using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using PedroTer7.FinancialMonkey.FinancialProductsService.Endpoints;
using PedroTer7.FinancialMonkey.FinancialProductsService.Entities;
using PedroTer7.FinancialMonkey.FinancialProductsService.Repository;
using PedroTer7.FinancialMonkey.FinancialProductsService.ViewModels;

namespace PedroTer7.FinancialMonkey.FinancialProductsService.Tests;

public class V1GetProductsTests
{
    private readonly Fixture _fixture;

    public V1GetProductsTests()
    {
        _fixture = new();
        _fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });
    }

    [Fact(DisplayName = "Get products works correctly for first page")]
    public async void Test_GetProducts_WorksCorrectly_ForFirstPage()
    {
        // Arrange
        var products = _fixture.CreateMany<FinancialProduct>(20).OrderBy(p => p.Id).ToList();
        var pageSize = 5;
        var expectedNextId = products[pageSize].Id;
        var productsRepoReturn = products.Take(pageSize).ToList();
        var repositoryMock = _fixture.Freeze<Mock<IFinancialProductsRepository>>();
        repositoryMock
            .Setup(r => r.GetProductsPage(null, (uint)pageSize))
            .ReturnsAsync(new Tuple<ICollection<FinancialProduct>, string?>(productsRepoReturn, expectedNextId));

        // Act
        var result = await V1Endpoints.GetProductsEndpoint(repositoryMock.Object, pageSize: (uint)pageSize);

        // Assert
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(expectedNextId, result.Value?.NextId);
        Assert.True(SequenceEqualsById(productsRepoReturn, result.Value?.Value?.ToList() ?? []));
    }

    [Fact(DisplayName = "Get products works correctly for any page > 1")]
    public async void Test_GetProducts_WorksCorrectly_ForAnyPage_OtherThanTheFirst()
    {
        // Arrange
        var products = _fixture.CreateMany<FinancialProduct>(20).OrderBy(p => p.Id).ToList();
        var pageSize = 5;
        var currentPageFirstIdx = new Random().Next(1, 17);
        var currentId = products[currentPageFirstIdx].Id;
        var expectedNextId = currentPageFirstIdx + pageSize >= products.Count - 1
                                ? null : products[currentPageFirstIdx + pageSize].Id;
        var productsRepoReturn = products.Skip(currentPageFirstIdx + 1).Take(pageSize).ToList();
        var repositoryMock = _fixture.Freeze<Mock<IFinancialProductsRepository>>();
        repositoryMock
            .Setup(r => r.GetProductsPage(currentId, (uint)pageSize))
            .ReturnsAsync(new Tuple<ICollection<FinancialProduct>, string?>(productsRepoReturn, expectedNextId));

        // Act
        var result = await V1Endpoints.GetProductsEndpoint(repositoryMock.Object, currentId, (uint)pageSize);

        // Assert
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(expectedNextId, result.Value?.NextId);
        Assert.True(SequenceEqualsById(productsRepoReturn, result.Value?.Value?.ToList() ?? []));
    }

    private static bool SequenceEqualsById(List<FinancialProduct> a, List<FinancialProductOutViewModel> b)
    {
        if (a.Count != b.Count) return false;
        foreach (var i in Enumerable.Range(0, a.Count))
        {
            if (a[i].Id != b[i].Id) return false;
        }
        return true;
    }
}
