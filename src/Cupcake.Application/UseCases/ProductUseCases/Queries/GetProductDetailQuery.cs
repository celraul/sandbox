using AutoMapper;
using Cupcake.Application.Interfaces;
using Cupcake.Application.Models.Store;
using Cupcake.Domain.Entities;
using Cupcake.Exceptions;
using MediatR;

namespace Cupcake.Application.UseCases.ProductUseCases.Queries;

public record GetProductDetailQuery(string id) : IRequest<ProdutModel>;

public class GetProductDetailQueryHandler : IRequestHandler<GetProductDetailQuery, ProdutModel>
{
    private readonly IRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public GetProductDetailQueryHandler(IRepository<Product> productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProdutModel> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
    {
        Product result = await _productRepository.GetById(request.id) ??
             throw new NotFoundException(typeof(Product));

        return _mapper.Map<ProdutModel>(result);
    }
}
