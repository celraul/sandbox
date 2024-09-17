using AutoMapper;
using Cupcake.Application.Interfaces;
using Cupcake.Application.Models.Store;
using Cupcake.Application.UseCases.Common.Queries;
using Cupcake.Domain.Entities;
using Cupcake.Domain.Enums;
using MediatR;
using System.Linq.Expressions;

namespace Cupcake.Application.UseCases.ProductUseCases.Queries;

public record GetProductsQuery(string Name, CupcakeType? Type, int Skip, int Take) : PaggedQuery<ProdutModel>(Skip, Take);

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, (List<ProdutModel>, int)>
{
    private readonly IRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(IRepository<Product> productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<(List<ProdutModel>, int)> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Product, bool>> filter = x => string.IsNullOrEmpty(request.Name) || x.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase);

        (List<Product>, long) result = await _productRepository.GetPaginatedWithTotal(filter, request.Skip, request.Take, cancellationToken);

        return (_mapper.Map<List<ProdutModel>>(result.Item1), (int)result.Item2);
    }
}
