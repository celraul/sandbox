using Cupcake.Application.Models.Adm.ProductModels;
using Cupcake.Application.UseCases.Adm.ProductUseCases.Commands.CreateProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cupcake.Admin.Api.Controllers.admin;

[ApiController]
[Route("api/v1/admin/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IMediator _mediator;

    public ProductController(ILogger<ProductController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Create product.
    /// </summary>
    /// <returns>Product Id.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<ActionResult> Post(CreateProductModel model)
    {
        string result = await _mediator.Send(new CreateProductCommand(model));

        return Ok(result);
    }
}
