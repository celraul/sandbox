using Cupcake.Api.Core.Models;
using Cupcake.Application.Models.Adm.ProductModels;
using Cupcake.Application.Models.Store;
using Cupcake.Application.UseCases.Adm.ProductUseCases.Commands.CreateProduct;
using Cupcake.Application.UseCases.ProductUseCases.Queries;
using Cupcake.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cupcake.Api.Controllers.store;

[ApiController]
[Route("api/v1/store/[controller]")]
public class ProductController : BaseRepository
{
    private readonly ILogger<ProductController> _logger;
    private readonly IMediator _mediator;

    public ProductController(ILogger<ProductController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Get list of products.
    /// </summary>
    /// <returns>Product Id.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiPagedResponse<List<ProdutModel>>), StatusCodes.Status200OK)]
    public async Task<ActionResult> Get(string? name, CupcakeType? cupcakeType, [FromQuery] PaginationParameters pagination)
    {
        (List<ProdutModel>, int) result =
            await _mediator.Send(new GetProductsQuery(name ?? string.Empty, cupcakeType, pagination.Skip, pagination.Take));

        ApiPagedResponse<List<ProdutModel>> apiResponse = new(result.Item1, result.Item2, pagination.Take);

        return Ok(apiResponse);
    }

    /// <summary>
    /// Get product details.
    /// </summary>
    /// <returns>Product Id.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<ProdutModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult> Get(string id)
    {
        ProdutModel result = await _mediator.Send(new GetProductDetailQuery(id));

        ApiResponse<ProdutModel> apiResponse = new(result);

        return Ok(apiResponse);
    }

    /// <summary>
    /// Create product.
    /// </summary>
    /// <returns>Product Id.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
    public async Task<ActionResult> Post(CreateProductModel model)
    {
        string productId = await _mediator.Send(new CreateProductCommand(model));

        return Ok(new ApiResponse<string>(productId));
    }
}
