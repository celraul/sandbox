using AutoFixture.Xunit2;
using AutoMapper;
using Cupcake.Application.Interfaces;
using Cupcake.Application.Models.Adm.ProductModels;
using Cupcake.Application.UseCases.Adm.ProductUseCases.Commands.CreateProduct;
using Cupcake.Domain.Entities;
using FluentAssertions;
using Moq;
using Xunit;

namespace Cupcake.Application.UnitTests.UseCases.ProductUseCases;

public class CreateProductCommandUnitTests
{
    private readonly Mock<IRepository<Product>> _productRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly CreateProductCommandHandler _handler;

    public CreateProductCommandUnitTests()
    {
        _productRepository = new();
        _mapper = new();

        _handler = new CreateProductCommandHandler(_productRepository.Object, _mapper.Object);
    }

    [Theory]
    [AutoData]
    public async Task Handle_ShouldCreateProduct(CreateProductModel model)
    {
        // Arrange
        CreateProductCommand command = new(model);

        var product = new Product { Id = "NewId" };
        _mapper.Setup(x => x.Map<Product>(model)).Returns(new Product { });
        _productRepository.Setup(x => x.InsertAndReturnAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(product);

        // Act
        string result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(product.Id);
    }
}
