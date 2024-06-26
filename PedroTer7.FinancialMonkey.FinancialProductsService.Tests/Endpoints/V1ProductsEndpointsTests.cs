﻿using AutoFixture;
using AutoFixture.AutoMoq;
using Bogus;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using PedroTer7.FinancialMonkey.FinancialProductsService.Endpoints;
using PedroTer7.FinancialMonkey.FinancialProductsService.Entities;
using PedroTer7.FinancialMonkey.FinancialProductsService.Repository;
using PedroTer7.FinancialMonkey.FinancialProductsService.ViewModels;

namespace PedroTer7.FinancialMonkey.FinancialProductsService.Tests.Endpoints;

public class V1ProductsEndpointsTests
{
    private readonly Fixture _fixture;
    private Faker _faker;

    public V1ProductsEndpointsTests()
    {
        _fixture = new();
        _fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });
        _faker = new();
    }

    [Fact(DisplayName = "Get products returns first page when nextId is null")]
    public async void Test_GetProducts_ReturnsFirstPage_WhenNextIdIsNull()
    {
        // Arrange
        var returningProducts = _fixture.CreateMany<FinancialProduct>(20).OrderBy(p => p.Id).ToList();
        var pageSize = 5;
        var expectedNextId = returningProducts[pageSize].Id;
        var productsRepoReturn = returningProducts.Take(pageSize).ToList();
        var repositoryMock = _fixture.Freeze<Mock<IFinancialProductsRepository>>();
        repositoryMock
            .Setup(r => r.GetProductsPage(null, (uint)pageSize))
            .ReturnsAsync(new Tuple<ICollection<FinancialProduct>, string?>(productsRepoReturn, expectedNextId));

        // Act
        var result = await V1Endpoints.GetProductsEndpoint(repositoryMock.Object, pageSize: (uint)pageSize);

        // Assert
        Assert.NotNull(result.Value);
        var resultCollection = Assert.IsAssignableFrom<IEnumerable<FinancialProductOutViewModel>>(result.Value.Value);
        Assert.Equal(productsRepoReturn.Select(p => p.Id), resultCollection.Select(p => p.Id));
        Assert.Equal(expectedNextId, result.Value?.NextId);
        repositoryMock.Verify(r => r.GetProductsPage(null, (uint)pageSize), Times.Once);
        repositoryMock.VerifyNoOtherCalls();
    }

    [Fact(DisplayName = "Get products returns the correct page when nextId is not null")]
    public async void Test_GetProducts_ReturnsTheCorrectPage_WhenNextIdIsNotNull()
    {
        // Arrange
        var returningProducts = _fixture.CreateMany<FinancialProduct>(20).OrderBy(p => p.Id).ToList();
        var pageSize = 5;
        var currentPageFirstIdx = new Random().Next(1, 17);
        var currentId = returningProducts[currentPageFirstIdx].Id;
        var expectedNextId = currentPageFirstIdx + pageSize >= returningProducts.Count - 1
                                ? null : returningProducts[currentPageFirstIdx + pageSize].Id;
        var productsRepoReturn = returningProducts.Skip(currentPageFirstIdx + 1).Take(pageSize).ToList();
        var repositoryMock = _fixture.Freeze<Mock<IFinancialProductsRepository>>();
        repositoryMock
            .Setup(r => r.GetProductsPage(currentId, (uint)pageSize))
            .ReturnsAsync(new Tuple<ICollection<FinancialProduct>, string?>(productsRepoReturn, expectedNextId));

        // Act
        var result = await V1Endpoints.GetProductsEndpoint(repositoryMock.Object, currentId, (uint)pageSize);

        // Assert
        Assert.NotNull(result.Value);
        var resultCollection = Assert.IsAssignableFrom<IEnumerable<FinancialProductOutViewModel>>(result.Value.Value);
        Assert.Equal(productsRepoReturn.Select(p => p.Id), resultCollection.Select(p => p.Id));
        Assert.Equal(expectedNextId, result.Value?.NextId);
        repositoryMock.Verify(r => r.GetProductsPage(currentId, (uint)pageSize), Times.Once);
        repositoryMock.VerifyNoOtherCalls();
    }

    [Fact(DisplayName = "Get one product returns the requested product")]
    public async void Test_GetProduct_ReturnsTheRequestedProduct()
    {
        // Arrange
        var returningProduct = _fixture.Create<FinancialProduct>();
        var requestingId = _faker.Random.String(10);
        var repositoryMock = _fixture.Freeze<Mock<IFinancialProductsRepository>>();
        repositoryMock
            .Setup(r => r.GetProduct(requestingId))
            .ReturnsAsync(returningProduct);

        // Act
        var result = await V1Endpoints.GetProductEndpoint(repositoryMock.Object, requestingId);

        // Assert
        var viewResult = Assert.IsAssignableFrom<Ok<FinancialProductOutViewModel>>(result.Result);
        Assert.NotNull(viewResult.Value);
        Assert.Equal(returningProduct.Id, viewResult.Value.Id);
        repositoryMock.Verify(r => r.GetProduct(requestingId), Times.Once);
        repositoryMock.VerifyNoOtherCalls();
    }

    [Fact(DisplayName = "Get one product returns not found when product doesn't exist")]
    public async void Test_GetProduct_ReturnsNotFound_WhenProductDoesntExist()
    {
        // Arrange
        var requestingId = _faker.Random.String(10);
        var repositoryMock = _fixture.Freeze<Mock<IFinancialProductsRepository>>();
        repositoryMock
            .Setup(r => r.GetProduct(It.IsAny<string>()))
            .ReturnsAsync((FinancialProduct?)null);

        // Act
        var result = await V1Endpoints.GetProductEndpoint(repositoryMock.Object, requestingId);

        // Assert
        var viewResult = Assert.IsAssignableFrom<NotFound>(result.Result);
        repositoryMock.Verify(r => r.GetProduct(requestingId), Times.Once);
        repositoryMock.VerifyNoOtherCalls();
    }

    [Fact(DisplayName = "Create product correctly creates a valid product")]
    public async void Test_CreateProduct_CorrectlyCreates_AValidProduct()
    {
        // Arrange
        var creatingProduct = MockFactory.CreateValidInViewModel();
        var returningCreatedProduct = _fixture.Create<FinancialProduct>();
        var repositoryMock = _fixture.Freeze<Mock<IFinancialProductsRepository>>();
        repositoryMock
            .Setup(r => r.CreateProduct(It.Is<FinancialProduct>(p => p.Name == creatingProduct.Name)))
            .ReturnsAsync(returningCreatedProduct);
        var validator = new FinancialProductInViewModelValidator();

        // Act
        var result = await V1Endpoints.CreateProductEndpoint(creatingProduct, validator, repositoryMock.Object);

        // Assert
        var viewResult = Assert.IsAssignableFrom<Created<FinancialProductOutViewModel>>(result.Result);
        Assert.NotNull(viewResult.Value);
        Assert.Equal(returningCreatedProduct.Id, viewResult.Value.Id);
        Assert.Equal($"/products/{returningCreatedProduct.Id}", viewResult.Location);
        repositoryMock.Verify(r => r.CreateProduct(It.Is<FinancialProduct>(p => p.Name == creatingProduct.Name)), Times.Once);
        repositoryMock.VerifyNoOtherCalls();
    }

    [Fact(DisplayName = "Create product returns validation problems when product is invalid")]
    public async void Test_CreateProduct_ReturnsValidationProblems_WhenProductIsInvalid()
    {
        // Arrange
        var creatingProduct = MockFactory.CreateInvalidInViewModel();
        var repositoryMock = _fixture.Create<Mock<IFinancialProductsRepository>>();
        var validator = new FinancialProductInViewModelValidator();

        // Act
        var result = await V1Endpoints.CreateProductEndpoint(creatingProduct, validator, repositoryMock.Object);

        // Assert
        Assert.IsAssignableFrom<ValidationProblem>(result.Result);
        repositoryMock.Verify(r => r.CreateProduct(It.IsAny<FinancialProduct>()), Times.Never);
        repositoryMock.VerifyNoOtherCalls();
    }

    [Fact(DisplayName = "Update product correctly updates a valid product that exists")]
    public async void Test_UpdateProduct_CorrectlyUpdates_AValidProduct_ThatExists()
    {
        // Arrange
        var creatingProduct = MockFactory.CreateValidInViewModel();
        var returningCreatedProduct = _fixture.Create<FinancialProduct>();
        var repositoryMock = _fixture.Freeze<Mock<IFinancialProductsRepository>>();
        repositoryMock
            .Setup(r => r.UpdateProduct(returningCreatedProduct.Id, It.Is<FinancialProduct>(p => p.Name == creatingProduct.Name)))
            .ReturnsAsync(returningCreatedProduct);
        var validator = new FinancialProductInViewModelValidator();

        // Act
        var result = await V1Endpoints.UpdateProductEndpoint(creatingProduct, returningCreatedProduct.Id, validator, repositoryMock.Object);

        // Assert
        var viewResult = Assert.IsAssignableFrom<Ok<FinancialProductOutViewModel>>(result.Result);
        Assert.NotNull(viewResult.Value);
        Assert.Equal(returningCreatedProduct.Id, viewResult.Value.Id);
        repositoryMock.Verify(r => r.UpdateProduct(returningCreatedProduct.Id, It.Is<FinancialProduct>(p => p.Name == creatingProduct.Name)), Times.Once);
        repositoryMock.VerifyNoOtherCalls();
    }

    [Fact(DisplayName = "Update product returns validation problems when the product is invalid")]
    public async void Test_UpdateProduct_ReturnsBadRequest_WhenProductIsInvalid()
    {
        // Arrange
        var creatingProduct = MockFactory.CreateInvalidInViewModel();
        var repositoryMock = _fixture.Create<Mock<IFinancialProductsRepository>>();
        var validator = new FinancialProductInViewModelValidator();

        // Act
        var result = await V1Endpoints.UpdateProductEndpoint(creatingProduct, _fixture.Create<string>(), validator, repositoryMock.Object);

        // Assert
        var viewResult = Assert.IsAssignableFrom<ValidationProblem>(result.Result);
        repositoryMock.Verify(r => r.UpdateProduct(It.IsAny<string>(), It.IsAny<FinancialProduct>()), Times.Never);
        repositoryMock.VerifyNoOtherCalls();
    }

    [Fact(DisplayName = "Update product returns not found with valid product that does not exists")]
    public async void Test_UpdateProduct_ReturnsNotFound_WithAValidProduct_ThatDoesntExist()
    {
        // Arrange
        var creatingProduct = MockFactory.CreateValidInViewModel();
        var randomId = _fixture.Create<string>();
        var repositoryMock = _fixture.Freeze<Mock<IFinancialProductsRepository>>();
        repositoryMock
            .Setup(r => r.UpdateProduct(It.IsAny<string>(), It.IsAny<FinancialProduct>()))
            .ReturnsAsync((FinancialProduct?)null);
        var validator = new FinancialProductInViewModelValidator();

        // Act
        var result = await V1Endpoints.UpdateProductEndpoint(creatingProduct, randomId, validator, repositoryMock.Object);

        // Assert
        var viewResult = Assert.IsAssignableFrom<NotFound>(result.Result);
        repositoryMock.Verify(r => r.UpdateProduct(randomId, It.Is<FinancialProduct>(p => p.Name == creatingProduct.Name)), Times.Once);
        repositoryMock.VerifyNoOtherCalls();
    }

    [Fact(DisplayName = "Delete product correctly deletes a product that exists")]
    public async void Test_DeleteProduct_CorrectlyDeletes_AProduct_ThatExists()
    {
        // Arrange
        var returningDeletedProduct = _fixture.Create<FinancialProduct>();
        var repositoryMock = _fixture.Freeze<Mock<IFinancialProductsRepository>>();
        repositoryMock
            .Setup(r => r.Delete(returningDeletedProduct.Id))
            .ReturnsAsync(returningDeletedProduct);

        // Act
        var result = await V1Endpoints.DeleteProductEndpoint(returningDeletedProduct.Id, repositoryMock.Object);

        // Assert
        var viewResult = Assert.IsAssignableFrom<Ok<FinancialProductOutViewModel>>(result.Result);
        Assert.NotNull(viewResult.Value);
        Assert.Equal(returningDeletedProduct.Id, viewResult.Value.Id);
        repositoryMock.Verify(r => r.Delete(returningDeletedProduct.Id), Times.Once);
        repositoryMock.VerifyNoOtherCalls();
    }

    [Fact(DisplayName = "Delete product returns not found when product doesnt exist")]
    public async void Test_DeleteProduct_ReturnsNotFound_WhenProduct_DoesntExist()
    {
        // Arrange
        var randomId = _fixture.Create<string>();
        var repositoryMock = _fixture.Freeze<Mock<IFinancialProductsRepository>>();
        repositoryMock
            .Setup(r => r.Delete(It.IsAny<string>()))
            .ReturnsAsync((FinancialProduct?)null);
        var validator = new FinancialProductInViewModelValidator();

        // Act
        var result = await V1Endpoints.DeleteProductEndpoint(randomId, repositoryMock.Object);

        // Assert
        var viewResult = Assert.IsAssignableFrom<NotFound>(result.Result);
        repositoryMock.Verify(r => r.Delete(randomId), Times.Once);
        repositoryMock.VerifyNoOtherCalls();
    }
}
