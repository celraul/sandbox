using MediatR;

namespace Cupcake.Application.UseCases.Common.Queries;

public record PaggedQuery<T>(int Skip, int Take) : IRequest<(List<T>, int)>;
