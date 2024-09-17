using AutoMapper;
using Cupcake.Application.Interfaces;
using Cupcake.Application.Models.Adm.ProductModels;
using Cupcake.Domain.Entities;
using MediatR;

namespace Cupcake.Application.UseCases.Adm.ProductUseCases.Commands.CreateProduct;

public record CreateProductCommand(CreateProductModel CreateProductModel) : IRequest<string>;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, string>
{
    private readonly IRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IRepository<Product> productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<string> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Product product = _mapper.Map<Product>(request.CreateProductModel);

        product = await _productRepository.InsertAndReturnAsync(product);

        return product.Id;
    }
}
